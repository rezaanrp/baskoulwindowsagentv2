using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Infra.Data.Classes
{
    public static class SoapServiceHelper
    {
        public static async Task<XmlDocument> SendSoapRequestAsync(string url, string soapAction, string soapBody)
        {
            using var httpClient = new HttpClient();
            var requestContent = new StringContent(soapBody, Encoding.UTF8, "text/xml");

            requestContent.Headers.Clear();
            requestContent.Headers.Add("Content-Type", "text/xml; charset=utf-8");
            requestContent.Headers.Add("SOAPAction", soapAction);

            var response = await httpClient.PostAsync(url, requestContent);

            if (!response.IsSuccessStatusCode)
            {
                Logger.LogToFile($"SOAP call failed: {response.StatusCode}");
                return null;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(responseContent);

            return xmlDoc;
        }

        public static XmlNamespaceManager CreateNamespaceManager(XmlDocument xmlDoc)
        {
            var nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("soap", SoapActions.Soap);
            nsmgr.AddNamespace("tempuri", SoapActions.Tempuri);
            return nsmgr;
        }

        public static async Task<(string Url, string Token)> GetApiInfoAsync(WriteDbContext context, string userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return (null, null);

            var markaz = await context.Companies.FirstOrDefaultAsync(m => m.CodMarkaz == user.CodMarkaz);
            if (string.IsNullOrEmpty(markaz?.APIURL)) return (null, null);

            return (markaz.APIURL, user.Token);
        }

        public static async Task<string> GetApiUrlAsync(WriteDbContext context, string codMarkaz)
        {
            var markaz = await context.Companies.FirstOrDefaultAsync(m => m.CodMarkaz == codMarkaz);
            return markaz?.APIURL;
        }
    }

}

