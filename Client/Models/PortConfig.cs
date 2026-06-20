using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyWinformsApp.Models
{
    public class PortConfig
    {
        public string DisplayName { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public int BaudRate { get; set; }
        public int StopBits { get; set; }
        public string Code { get; set; }
        public string CodMarkaz { get; set; }
        public string SiteCode { get; set; }
        public string AppName { get; set; }
        public string BaseUrl { get; set; }

        public static PortConfig FromXElement(XElement el)
        {
            if (el == null) return null;

            return new PortConfig
            {
                DisplayName = (string)el.Attribute("DisplayName") ?? (string)el.Attribute("name"),
                Type = (string)el.Attribute("type"),
                Name = (string)el.Attribute("name"),
                BaudRate = int.TryParse((string)el.Attribute("baudrate"), out var b) ? b : 9600,
                StopBits = int.TryParse((string)el.Attribute("stopbits"), out var s) ? s : 1,
                Code = (string)el.Attribute("code"),
                CodMarkaz = (string)el.Attribute("codmarkaz"),
                SiteCode = (string)el.Attribute("siteCode"),
                AppName = (string)el.Attribute("appname"),
                BaseUrl = (string)el.Attribute("baseUrl")
            };
        }

        public string GetHubUrl()
        {
            var baseUrl = (BaseUrl ?? string.Empty).TrimEnd('/');
            var app = (AppName ?? string.Empty).Trim('/');
            var path = string.IsNullOrEmpty(app) ? "ReceiveWeightFromScale" : app + "/ReceiveWeightFromScale";
            return $"{baseUrl}/{path}";
        }
    }
}
