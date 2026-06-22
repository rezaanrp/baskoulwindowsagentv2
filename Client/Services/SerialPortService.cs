using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWinformsApp.Services
{
    public class SerialPortService : IDisposable
    {
        private readonly SerialPort _serialPort;
        public event Func<string, Task> WeightReceivedAsync; // payload is digits-only weight

        public SerialPortService(string portName, int baudRate, int stopBits)
        {
            _serialPort = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One)
            {
                Handshake = Handshake.None,
                Encoding = Encoding.ASCII,
                ReadTimeout = 1000
            };

            _serialPort.NewLine = "\r\n"; // if your scale sends lines
        }

        public void Start()
        {
            if (_serialPort.IsOpen) _serialPort.Close();

            _serialPort.Open();
            _serialPort.DiscardInBuffer(); // flush old data

            _serialPort.DataReceived += async (s, e) =>
            {
                try
                {
                    string raw = _serialPort.ReadExisting();

                    // Flush buffer after reading
                    _serialPort.DiscardInBuffer();

                    string digitsOnly = new string(raw.Where(char.IsDigit).ToArray());
                    if (string.IsNullOrEmpty(digitsOnly)) return;

                    if (WeightReceivedAsync != null)
                        await WeightReceivedAsync.Invoke(digitsOnly);
                }
                catch (Exception ex) 
                {
                    throw new InvalidOperationException($"خواندن داده از پورت {_serialPort.PortName} ناموفق بود: {ex.Message}", ex);
                }
            };
        }

        private async void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                var raw = _serialPort.ReadExisting();
                var digitsOnly = new string(raw.Where(char.IsDigit).ToArray());
                if (string.IsNullOrEmpty(digitsOnly)) return;

                if (WeightReceivedAsync != null)
                    await WeightReceivedAsync.Invoke(digitsOnly);
            }
            catch { }
        }

        public void Dispose()
        {
            try
            {
                if (_serialPort != null)
                {
                    _serialPort.DataReceived -= SerialPort_DataReceived;
                    if (_serialPort.IsOpen) _serialPort.Close();
                    _serialPort.Dispose();
                }
            }
            catch { }
        }
    }
}
