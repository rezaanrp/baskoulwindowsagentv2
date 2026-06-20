using System.Xml.Linq;
using MyWinformsApp.Forms;

namespace MyWinformsApp;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        // هندل کردن خطاهای عمومی
        Application.ThreadException += (sender, args) =>
        {
            AppLogger.Error("UI Thread exception", args.Exception);
        };

        AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
        {
            var ex = args.ExceptionObject as Exception;
            AppLogger.Error("Unhandled exception (non-UI thread)", ex);
        };

        AppLogger.Info("Application starting...");

        string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.xml");

        if (!File.Exists(configPath))
        {
            // Create a sample config if not exists
            var doc = new XDocument(
                new XElement("ports",
                    new XElement("port",
                        new XAttribute("DisplayName", "ورودی"),
                        new XAttribute("type", "COM"),
                        new XAttribute("name", "COM4"),
                        new XAttribute("baudrate", "9600"),
                        new XAttribute("stopbits", "1"),
                        new XAttribute("code", "S1"),
                        new XAttribute("codmarkaz", "12273"),
                        new XAttribute("siteCode", "Site_1"),
                        new XAttribute("appname", "sanayeboresh"),
                        new XAttribute("baseUrl", "https://public.hamyarmsc.ir")
                    ),
                    new XElement("port",
                        new XAttribute("DisplayName", "خروجی"),
                        new XAttribute("type", "COM"),
                        new XAttribute("name", "COM3"),
                        new XAttribute("baudrate", "9600"),
                        new XAttribute("stopbits", "1"),
                        new XAttribute("code", "S2"),
                        new XAttribute("codmarkaz", "12273"),
                        new XAttribute("siteCode", "Site_1"),
                        new XAttribute("appname", "sanayeboresh"),
                        new XAttribute("baseUrl", "https://public.hamyarmsc.ir")
                    )
                )
            );
            doc.Save(configPath);
        }

        var xml = XDocument.Load(configPath);
        var portElements = xml.Descendants("port").ToList();

        if (portElements.Count == 0)
        {
            MessageBox.Show("No port configuration found.");
            return;
        }

        // Create a ScaleForm for every port. The first one becomes the main application form.
        var forms = portElements.Select(p => new Form1(p)).ToList();

        // Show additional forms non-modally
        for (int i = 1; i < forms.Count; i++)
        {
            AppLogger.Info($"Form shown: title={forms[i].Text}");
            forms[i].Show();

        }

        Application.Run(forms[0]);
        AppLogger.Info("Application exiting...");
    }
}
