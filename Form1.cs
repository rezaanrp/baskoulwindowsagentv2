using HidSharp;
using Microsoft.AspNetCore.SignalR.Client;
using MyWinformsApp.Models;
using System.IO.Ports;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace MyWinformsApp
{
    public partial class Form1 : Form
    {
        private HubConnection? connection;
        private string _lastWeight = "0";
        //private System.Threading.Timer? _weightTimer;
        //private readonly int _updateDelay = 200;
        private readonly XElement _portElement;
        private SerialPort _serial;
        private DateTime _lastReceivedTime = DateTime.Now;
        private System.Windows.Forms.Timer _watchDog;

        public Form1(XElement portElement)
        {
            InitializeComponent();
            _portElement = portElement;
            AppLogger.Info($"[Form1] Constructor called for port={_portElement.Attribute("name")?.Value}");

            pictureDisplay.Dock = DockStyle.Fill;
            pictureDisplay.BackColor = Color.Black;
            pictureDisplay.SizeMode = PictureBoxSizeMode.CenterImage;

            Controls.Add(pictureDisplay);
            //	Text = "Digital Scale";
            //WindowState = FormWindowState.Maximized;

            SetFormTitleFromConfig();

            ResumeLayout(false);
 
        }
        private void SetFormTitleFromConfig()
        {
            // مقادیر مهم از کانفیگ
            string displayName = _portElement.Attribute("DisplayName")?.Value ?? "";
            string name = _portElement.Attribute("name")?.Value ?? "";
            string code = _portElement.Attribute("code")?.Value ?? "";
            string appName = _portElement.Attribute("appname")?.Value ?? "";
            string site = _portElement.Attribute("siteCode")?.Value ?? "";

            // ساخت متن خلاصه
            this.Text = $"{displayName} | {name} | {code} | {appName} | {site}";
        }
        private async void StartSystem()
        {
            var port = _portElement;

            if (port == null)
            {
                AppLogger.Error("StartSystem called but _portElement is null.");
                MessageBox.Show("No port configuration found.");
                return;
            }

            string siteCode = port.Attribute("siteCode")?.Value?.Trim('/') ?? "";
            string appname = port.Attribute("appname")?.Value?.Trim('/') ?? "";
            string baseUrl = port.Attribute("baseUrl")?.Value?.Trim('/') ?? "";

            string hubUrl = $"{baseUrl}/{appname}/hubs/weight";
            AppLogger.Info($"[{Text}] SignalR hub URL: {hubUrl}");

            connection = new HubConnectionBuilder()
                .WithUrl(hubUrl, options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult<string?>(GetSavedToken());
                })
                .WithAutomaticReconnect()
                .Build();

            connection.HandshakeTimeout = TimeSpan.FromSeconds(10);
            connection.ServerTimeout = TimeSpan.FromSeconds(30);
            connection.Reconnecting += ex =>
            {
                AppLogger.Error($"[{Text}] SignalR reconnecting.", ex);
                return Task.CompletedTask;
            };
            connection.Reconnected += connectionId =>
            {
                AppLogger.Info($"[{Text}] SignalR reconnected. connectionId={connectionId}");
                return Task.CompletedTask;
            };
            connection.Closed += ex =>
            {
                AppLogger.Error($"[{Text}] SignalR closed.", ex);
                return Task.CompletedTask;
            };

            try
            {
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(15));
                await connection.StartAsync(cts.Token);
                AppLogger.Info($"[{Text}] SignalR connected.");
            }
            catch (Exception ex)
            {
                AppLogger.Error($"[{Text}] SignalR connection failed. hubUrl={hubUrl}", ex);
                MessageBox.Show("SignalR connection failed: " + ex.Message);
            }

            string type = port.Attribute("type")?.Value;

            if (type == "COM")
            {
                StartSerialPort(
                    port.Attribute("name")?.Value,
                    int.Parse(port.Attribute("baudrate")?.Value ?? "9600"),
                    int.Parse(port.Attribute("stopbits")?.Value ?? "1"),
                    port.Attribute("code")?.Value,
                    port.Attribute("codmarkaz")?.Value,
                    siteCode
                );
            }
            else if (type == "USB")
            {
                StartHidPort(port, siteCode);
            }
        }
        private string _scaleCode;
        private string _codmarkaz;
        private string _siteCode;

        private string GetSavedToken()
        {
            try
            {
                string[] candidatePaths =
                {
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "token", "token.txt"),
                    Path.Combine(Directory.GetCurrentDirectory(), "token", "token.txt")
                };

                string? tokenFilePath = candidatePaths.FirstOrDefault(File.Exists);
                if (tokenFilePath == null)
                {
                    AppLogger.Error($"[{Text}] Token file not found. Checked: {string.Join(", ", candidatePaths)}");
                    return string.Empty;
                }

                var token = File.ReadAllText(tokenFilePath).Trim();
                LogTokenExpiry(token, tokenFilePath);
                return token;
            }
            catch (Exception ex)
            {
                AppLogger.Error($"[{Text}] Error reading token.", ex);
            }

            return string.Empty;
        }

        private void LogTokenExpiry(string token, string tokenFilePath)
        {
            try
            {
                var parts = token.Split('.');
                if (parts.Length < 2)
                    return;

                var payload = parts[1].Replace('-', '+').Replace('_', '/');
                payload = payload.PadRight(payload.Length + ((4 - payload.Length % 4) % 4), '=');
                var json = Encoding.UTF8.GetString(Convert.FromBase64String(payload));

                using var doc = JsonDocument.Parse(json);
                if (!doc.RootElement.TryGetProperty("exp", out var expElement))
                    return;

                var expiresAt = DateTimeOffset.FromUnixTimeSeconds(expElement.GetInt64()).LocalDateTime;
                if (expiresAt <= DateTime.Now)
                    AppLogger.Error($"[{Text}] Token is expired. path={tokenFilePath}, expiredAt={expiresAt:yyyy-MM-dd HH:mm:ss}");
                else
                    AppLogger.Info($"[{Text}] Token loaded. path={tokenFilePath}, expiresAt={expiresAt:yyyy-MM-dd HH:mm:ss}");
            }
            catch (Exception ex)
            {
                AppLogger.Error($"[{Text}] Could not inspect token expiry.", ex);
            }
        }

        private void StartSerialPort(string portName, int baudRate, int stopBits,
                                     string scaleCode, string codmarkaz, string siteCode)
        {
            try
            {
                // ذخیره پارامترها برای استفاده در DataReceived
                _scaleCode = scaleCode;
                _codmarkaz = codmarkaz;
                _siteCode = siteCode;

                if (_serial != null)
                {
                    try
                    {
                        _serial.DataReceived -= SerialPort_DataReceived;
                        _serial.Close();
                        _serial.Dispose();
                    }
                    catch (Exception ex)
                    {
                        AppLogger.Error("Error while closing existing serial port.", ex);
                    }
                }

                _serial = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One)
                {
                    Handshake = Handshake.None,
                    ReadTimeout = 1000,
                    WriteTimeout = 1000
                };

                _serial.DataReceived += SerialPort_DataReceived;

                _serial.Open();
                _serial.DiscardInBuffer();

                StartWatchDog();
            }
            catch (Exception ex)
            {
                AppLogger.Error($"Failed to open {portName}.", ex);
                MessageBox.Show($"Failed to open {portName}: {ex.Message}");
            }
        }
        private async void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (_serial == null || !_serial.IsOpen) return;

                string raw = _serial.ReadExisting();
                _lastReceivedTime = DateTime.Now;

                // لاگ کامل از دیتای خام
                string safeRaw = raw.Replace("\r", "\\r").Replace("\n", "\\n");
                AppLogger.Info($"[{Text}] DataReceived on {_serial.PortName}: raw='{safeRaw}'");

                string digitsOnly = new string(raw.Where(char.IsDigit).ToArray());

                AppLogger.Info($"[{Text}] Parsed digits: '{digitsOnly}'");

                if (string.IsNullOrWhiteSpace(digitsOnly))
                {
                    AppLogger.Info($"[{Text}] No digits found, skipping frame.");
                    return;
                }

                // اگر طولش خیلی زیاد شد، فقط ۶ رقم آخر
                if (digitsOnly.Length > 6)
                {
                    digitsOnly = digitsOnly.Substring(digitsOnly.Length - 6);
                    AppLogger.Info($"[{Text}] Trimmed digits to last 6: '{digitsOnly}'");
                }

                if (!int.TryParse(digitsOnly, out int weight))
                {
                    AppLogger.Error($"[{Text}] Failed to parse digits '{digitsOnly}' to int.");
                    return;
                }

                AppLogger.Info($"[{Text}] Final weight parsed: {weight}");

                ScheduleWeightUpdate(weight.ToString());

                await SendWeightToServer(weight.ToString(), _scaleCode, _codmarkaz, _siteCode);
            }
            catch (Exception ex)
            {
                AppLogger.Error($"[{Text}] Exception in SerialPort_DataReceived.", ex);
            }
        }


        private void StartWatchDog()
        {
            if (_watchDog == null)
            {
                _watchDog = new System.Windows.Forms.Timer();
                _watchDog.Interval = 3000; // هر ۳ ثانیه
                _watchDog.Tick += WatchDog_Tick;
            }
            _watchDog.Start();
        }
        private void WatchDog_Tick(object sender, EventArgs e)
        {
            if ((DateTime.Now - _lastReceivedTime).TotalSeconds > 5)
            {
                Console.WriteLine("⚠️ No data for 5s → Restarting Serial Port...");
                RestartSerialPort();
            }
        }
        private void RestartSerialPort()
        {
            AppLogger.Info("RestartSerialPort called.");
            try
            {
                _serial.DataReceived -= SerialPort_DataReceived;
                _serial.Close();
                _serial.Dispose();
            }
            catch (Exception ex)
            {
                AppLogger.Error("Error while closing serial port in RestartSerialPort.", ex);
            }

            string portName = _portElement.Attribute("name")?.Value;
            int baud = int.Parse(_portElement.Attribute("baudrate")?.Value ?? "9600");
            int stop = int.Parse(_portElement.Attribute("stopbits")?.Value ?? "1");

            string code = _portElement.Attribute("code")?.Value;
            string codmarkaz = _portElement.Attribute("codmarkaz")?.Value;
            string site = _portElement.Attribute("siteCode")?.Value;

            Thread.Sleep(300);

            StartSerialPort(portName, baud, stop, code, codmarkaz, site);
        }



        private void StartHidPort(XElement port, string siteCode)
        {
            int vid = int.Parse(port.Attribute("vid")?.Value ?? "0", System.Globalization.NumberStyles.HexNumber);
            int pid = int.Parse(port.Attribute("pid")?.Value ?? "0", System.Globalization.NumberStyles.HexNumber);

            int.TryParse(port.Attribute("code")?.Value, out _);
            string scaleCode = port.Attribute("code")?.Value ?? "USB";
            string codmarkaz = port.Attribute("codmarkaz")?.Value ?? "";

            Task.Run(() =>
            {
                var device = DeviceList.Local.GetHidDevices(vid, pid).FirstOrDefault();
                if (device == null)
                {
                    MessageBox.Show($"No HID device found for VID={vid:X4} PID={pid:X4}");
                    return;
                }

                device.TryOpen(out HidStream stream);
                var buffer = new byte[device.MaxInputReportLength];

                StringBuilder weightBuffer = new StringBuilder();
                string currentScale = "";

                while (true)
                {
                    int count = stream.Read(buffer, 0, buffer.Length);
                    if (count <= 0) continue;

                    string ascii = Encoding.ASCII.GetString(buffer, 0, count).Trim('\0');
                    if (ascii == "") continue;

                    if (ascii.StartsWith("i+") || ascii.StartsWith("o+"))
                    {
                        if (weightBuffer.Length > 0)
                        {
                            string numeric = Regex.Match(weightBuffer.ToString(), @"\d+").Value;
                            numeric = numeric.TrimStart('0');
                            if (numeric == "") numeric = "0";

                            ScheduleWeightUpdate(numeric);
                            SendWeightToServer(numeric, scaleCode, codmarkaz, siteCode).Wait();
                        }

                        currentScale = ascii.Substring(0, 2);
                        weightBuffer.Clear();
                    }
                    else
                    {
                        string digits = Regex.Match(ascii, @"\d+").Value;
                        if (digits != "") weightBuffer.Append(digits);
                    }
                }
            });
        }


        private SevenSegmentRenderer renderer = new SevenSegmentRenderer();

        private void UpdateWeightUI(string weight)
        {
            if (InvokeRequired)
            {
                Invoke(() => UpdateWeightUI(weight));
                return;
            }

            if (pictureDisplay.Width <= 0 || pictureDisplay.Height <= 0)
                return;

       //     AppLogger.Info($"[{Text}] UpdateWeightUI → {weight}");

            Bitmap bmp = renderer.Render(weight, pictureDisplay.Width, pictureDisplay.Height);
            pictureDisplay.Image = bmp;
        }


        private void ScheduleWeightUpdate(string weight)
        {
            _lastWeight = weight;

            try
            {
                if (IsDisposed || !IsHandleCreated)
                    return;

                // مستقیماً بفرست روی UI
                BeginInvoke(new Action(() => UpdateWeightUI(_lastWeight)));
            }
            catch (ObjectDisposedException)
            {
            }
            catch (Exception ex)
            {
                AppLogger.Error("Exception in ScheduleWeightUpdate/BeginInvoke.", ex);
            }
        }



        private async Task SendWeightToServer(string weight, string scaleCode, string codmarkaz, string siteCode)
        {
            if (connection == null) return;

            try
            {
                await connection.InvokeAsync("SendWeightUpdate", siteCode, scaleCode, weight, codmarkaz);
            }
            catch (Exception ex)
            {
                AppLogger.Error($"Error sending weight to server. weight={weight}, scaleCode={scaleCode}, siteCode={siteCode}", ex);
                Console.WriteLine($"Error sending weight: {ex.Message}");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // base.OnLoad(e);
            AppLogger.Info($"[Form1] OnLoad → StartSystem. Title={this.Text}");
            StartSystem();
        }
    }
}
public class SevenSegmentRenderer
{
	private readonly Dictionary<char, bool[]> digitMap;

	public Color SegmentOnColor { get; set; } = Color.Red;
	public Color SegmentOffColor { get; set; } = Color.FromArgb(40, 0, 0);

	public int DigitSpacing { get; set; } = 10; 

	public SevenSegmentRenderer()
	{
		digitMap = new Dictionary<char, bool[]>
		{
			{ '0', new[]{ true,true,true,true,true,true,false } },
			{ '1', new[]{ false,true,true,false,false,false,false } },
			{ '2', new[]{ true,true,false,true,true,false,true } },
			{ '3', new[]{ true,true,true,true,false,false,true } },
			{ '4', new[]{ false,true,true,false,false,true,true } },
			{ '5', new[]{ true,false,true,true,false,true,true } },
			{ '6', new[]{ true,false,true,true,true,true,true } },
			{ '7', new[]{ true,true,true,false,false,false,false } },
			{ '8', new[]{ true,true,true,true,true,true,true } },
			{ '9', new[]{ true,true,true,true,false,true,true } }
		};
	}

	public Bitmap Render(string number, int width, int height)
	{
        if (width <= 0 || height <= 0)
        {
            // یا می‌تونی throw نکنی و یه مقدار پیش‌فرض بدی
            width = Math.Max(width, 1);
            height = Math.Max(height, 1);
        }

        // همیشه 6 رقم
        number = number.PadLeft(6, '0');

		Bitmap bmp = new Bitmap(width, height);
		using Graphics g = Graphics.FromImage(bmp);

		g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
		g.Clear(Color.Black);

		int totalSpacing = (number.Length - 1) * DigitSpacing;
		int digitWidth = (width - totalSpacing) / number.Length;
		int digitHeight = height;

		int firstNonZero = number.TakeWhile(c => c == '0').Count();

		if (firstNonZero == number.Length)
			firstNonZero = number.Length - 1;

		int x = 0;
		for (int i = 0; i < number.Length; i++)
		{
			char c = number[i];

			if (i < firstNonZero)
			{
				// صفرهای پیشرو → خاموش
				DrawDigit(g, new bool[7], x, 0, digitWidth, digitHeight);
			}
			else
			{
				if (digitMap.ContainsKey(c))
					DrawDigit(g, digitMap[c], x, 0, digitWidth, digitHeight);
			}

			x += digitWidth + DigitSpacing;
		}

		return bmp;
	}

	private void DrawDigit(Graphics g, bool[] seg, int x, int y, int w, int h)
	{
		int thickness = w / 7;     
		int length = w - 2 * thickness;

		Brush on = new SolidBrush(SegmentOnColor);
		Brush off = new SolidBrush(SegmentOffColor);

		g.FillEllipse(seg[0] ? on : off, x + thickness, y, length, thickness);

		g.FillEllipse(seg[1] ? on : off, x + thickness + length, y + thickness, thickness, (h / 2) - thickness);

		g.FillEllipse(seg[2] ? on : off, x + thickness + length, y + (h / 2), thickness, (h / 2) - thickness);

		g.FillEllipse(seg[3] ? on : off, x + thickness, y + h - thickness, length, thickness);

		g.FillEllipse(seg[4] ? on : off, x, y + (h / 2), thickness, (h / 2) - thickness);

		g.FillEllipse(seg[5] ? on : off, x, y + thickness, thickness, (h / 2) - thickness);

		g.FillEllipse(seg[6] ? on : off, x + thickness, (h / 2) - (thickness / 2), length, thickness);
	}
}

