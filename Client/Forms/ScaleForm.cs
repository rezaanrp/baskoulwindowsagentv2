using HidSharp;
using MyWinformsApp.Models;
using MyWinformsApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyWinformsApp.Forms
{
    public class ScaleForm : Form
    {
        private readonly XElement _portElement;
        private readonly PortConfig _config;

        private PictureBox pictureDisplay;
        private SignalRService _signalR;
        private SerialPortService _serialService;

        private System.Threading.Timer _weightTimer;
        private string _lastWeight;
        private readonly int _updateDelay = 600; // ms

        public ScaleForm(XElement portElement)
        {
            _portElement = portElement;
            _config = Models.PortConfig.FromXElement(portElement);

            InitializeComponent();
            Text = _config.DisplayName ?? _config.Name ?? "Digital Scale";

            // start services but do not block UI
            _ = StartSystemAsync();
        }

        private void InitializeComponent()
        {
            pictureDisplay = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureDisplay).BeginInit();
            SuspendLayout();
            // 
            // pictureDisplay
            // 
            pictureDisplay.BackColor = Color.Black;
            pictureDisplay.Dock = DockStyle.Fill;
            pictureDisplay.Location = new Point(0, 0);
            pictureDisplay.Name = "pictureDisplay";
            pictureDisplay.Size = new Size(576, 215);
            pictureDisplay.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureDisplay.TabIndex = 0;
            pictureDisplay.TabStop = false;
            // 
            // ScaleForm
            // 
            ClientSize = new Size(576, 215);
            Controls.Add(pictureDisplay);
            Name = "ScaleForm";
            StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)pictureDisplay).EndInit();
            ResumeLayout(false);
        }

        private async Task StartSystemAsync()
        {
            try
            {
                // SignalR
                var hubUrl = BuildHubUrl(_config.BaseUrl, _config.AppName);
                try
                {
                    _signalR = new SignalRService(hubUrl);
                    //MessageBox.Show("SignalR created OK");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"ساخت اتصال به سرور ناموفق بود.\n\nجزئیات خطا:\n{ex.Message}",
                        "خطای اتصال به سرور",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

                try
                {
                    await _signalR.StartAsync();
                    Console.WriteLine($"اتصال به سرور برای {_config.Name} برقرار شد.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"اتصال به سرور ناموفق بود: {ex.Message}");
                }

                // Serial or HID
                if (string.Equals(_config.Type, "COM", StringComparison.OrdinalIgnoreCase))
                {
                    //MessageBox.Show($"start program");
                    _serialService = new SerialPortService(_config.Name, _config.BaudRate, _config.StopBits);
                    _serialService.WeightReceivedAsync += OnWeightReceivedAsync;
                    try
                    {
                        _serialService.Start();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            $"شروع پورت سریال {_config.Name} ناموفق بود.\n\nجزئیات خطا:\n{ex.Message}",
                            "خطای پورت باسکول",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else if (string.Equals(_config.Type, "USB", StringComparison.OrdinalIgnoreCase))
                {
                    StartHidPort(_config.Name, _config.SiteCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطا هنگام شروع سیستم باسکول: {ex.Message}");
            }
        }

        private string BuildHubUrl(string baseUrl, string appName)
        {
            string b = (baseUrl ?? string.Empty).TrimEnd('/');
            string a = (appName ?? string.Empty).Trim('/');

            if (string.IsNullOrEmpty(b))
                return string.Empty;

            if (string.IsNullOrEmpty(a))
                return $"{b}/ReceiveWeightFromScale";

            return $"{b}/{a}/ReceiveWeightFromScale";
        }

        private void StartHidPort(string port, string siteCode)
        {
            //int vid = int.Parse(port.Attribute("vid")?.Value ?? "0", System.Globalization.NumberStyles.HexNumber);
            //int pid = int.Parse(port.Attribute("pid")?.Value ?? "0", System.Globalization.NumberStyles.HexNumber);

            //int.TryParse(port.Attribute("code")?.Value, out _);
            //string scaleCode = port.Attribute("code")?.Value ?? "USB";
            //string codmarkaz = port.Attribute("codmarkaz")?.Value ?? "";

            //Task.Run(() =>
            //{
            //    var device = DeviceList.Local.GetHidDevices(vid, pid).FirstOrDefault();
            //    if (device == null)
            //    {
            //        MessageBox.Show($"No HID device found for VID={vid:X4} PID={pid:X4}");
            //        return;
            //    }

            //    device.TryOpen(out HidStream stream);
            //    var buffer = new byte[device.MaxInputReportLength];

            //    StringBuilder weightBuffer = new StringBuilder();
            //    string currentScale = "";

            //    while (true)
            //    {
            //        int count = stream.Read(buffer, 0, buffer.Length);
            //        if (count <= 0) continue;

            //        string ascii = Encoding.ASCII.GetString(buffer, 0, count).Trim('\0');
            //        if (ascii == "") continue;

            //        if (ascii.StartsWith("i+") || ascii.StartsWith("o+"))
            //        {
            //            if (weightBuffer.Length > 0)
            //            {
            //                string numeric = Regex.Match(weightBuffer.ToString(), @"\d+").Value;
            //                numeric = numeric.TrimStart('0');
            //                if (numeric == "") numeric = "0";

            //                ScheduleWeightUpdate(numeric);
            //                await SendWeightToServer(numeric, scaleCode, codmarkaz, siteCode).Wait();
            //            }

            //            currentScale = ascii.Substring(0, 2);
            //            weightBuffer.Clear();
            //        }
            //        else
            //        {
            //            string digits = Regex.Match(ascii, @"\d+").Value;
            //            if (digits != "") weightBuffer.Append(digits);
            //        }
            //    }
            //});
        }

        private async Task OnWeightReceivedAsync(string digitsOnly)
        {
            MessageBox.Show(digitsOnly);
            // schedule UI update
            ScheduleWeightUpdate(digitsOnly);

            // send to server (fire-and-forget)
            if (_signalR != null)
            {
                try
                {
                    await _signalR.SendWeightAsync(_config.SiteCode, _config.Code, digitsOnly, _config.CodMarkaz);
                }
                catch { }
            }
        }

        private void ScheduleWeightUpdate(string weight)
        {
            _lastWeight = weight;

            _weightTimer?.Dispose();

            _weightTimer = new System.Threading.Timer(_ =>
            {
                try
                {
                    this.Invoke((Action)(() => UpdateWeightUI(_lastWeight)));
                }
                catch { }

            }, null, _updateDelay, Timeout.Infinite);
        }

        private void UpdateWeightUI(string weight)
        {
            // Simple display: draw weight text into a bitmap and show in picture box
            try
            {
                var bmp = new Bitmap(pictureDisplay.Width > 1 ? pictureDisplay.Width : 480, pictureDisplay.Height > 1 ? pictureDisplay.Height : 320);
                using (var g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.Black);
                    var font = new Font("Tahoma", 48, FontStyle.Bold, GraphicsUnit.Pixel);
                    var text = weight;
                    var size = g.MeasureString(text, font);
                    var x = (bmp.Width - size.Width) / 2f;
                    var y = (bmp.Height - size.Height) / 2f;
                    g.DrawString(text, font, Brushes.White, x, y);
                }
                var old = pictureDisplay.Image;
                pictureDisplay.Image = bmp;
                old?.Dispose();
            }
            catch { }
        }
    }
}
