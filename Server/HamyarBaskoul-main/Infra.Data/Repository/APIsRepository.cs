using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.ViewModels.APIs;
using Domain.ViewModels.Baskoul;
using Infra.Data.Classes;
using Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain.Classes;
using System.Xml;

namespace Infra.Data.Repository
{
    public class APIsRepository : IAPIsRepository
    {
        private readonly WriteDbContext _context;
        private readonly JwtTokenHelper _jwt;
        private readonly IMapper _mapper;

        public APIsRepository(WriteDbContext context, IMapper mapper, JwtTokenHelper jwt)
        {
            _context = context;
            _mapper = mapper;
            _jwt = jwt;
        }

        public async Task<BargiriDomainViewModel> Bargiri(string userId, long idBargiri)
        {
            var (url, token) = await SoapServiceHelper.GetApiInfoAsync(_context, userId);
            if (string.IsNullOrEmpty(url)) return null;

            const string soapAction = SoapActions.BargiriString;
            string soapBody = $@"<?xml version=""1.0"" encoding=""utf-8""?>
        <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" 
                       xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
                       xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
          <soap:Body>
            <GetBargiri xmlns=""http://tempuri.org/"">
              <APICode>{token}</APICode>
              <ID>{idBargiri}</ID>
            </GetBargiri>
          </soap:Body>
        </soap:Envelope>";

            try
            {
                var xmlDoc = await SoapServiceHelper.SendSoapRequestAsync(url, soapAction, soapBody);
                if (xmlDoc == null) return null;

                var nsmgr = SoapServiceHelper.CreateNamespaceManager(xmlDoc);
                var resultNode = xmlDoc.SelectSingleNode("//soap:Body/tempuri:GetBargiriResponse/tempuri:GetBargiriResult", nsmgr);
                if (resultNode == null) return null;

                var stateNode = resultNode.SelectSingleNode("tempuri:State", nsmgr);
                bool parsedState = false;
                bool state = stateNode != null && bool.TryParse(stateNode.InnerText, out parsedState) && parsedState;

                if (state)
                {
                    return new BargiriDomainViewModel
                    {
                        DateBargiri = resultNode.SelectSingleNode("tempuri:DateBarge", nsmgr)?.InnerText ?? string.Empty,
                        TimeBargiri = resultNode.SelectSingleNode("tempuri:TimeBarge", nsmgr)?.InnerText ?? string.Empty,
                        NameRanande = resultNode.SelectSingleNode("tempuri:NameRanande", nsmgr)?.InnerText ?? string.Empty,
                        ShomareMashin = resultNode.SelectSingleNode("tempuri:ShoPelak", nsmgr)?.InnerText ?? string.Empty,
                        Tozihat = resultNode.SelectSingleNode("tempuri:Tozihat", nsmgr)?.InnerText ?? string.Empty
                    };
                }

                Logger.LogToFile("اطلاعات یافت نشد.اخطار");
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogToFile($"Exception during SOAP request: {ex.Message}");
                return null;
            }
        }

        public async Task<SyncServerDomainViewModel> GetDore(string codmarkaz, string token, string userid)
        {
            var url = await SoapServiceHelper.GetApiUrlAsync(_context, codmarkaz);
            const string soapAction = SoapActions.GetCycleString;

            string soapBody = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" 
                xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                <soap:Body>
                <GetCycleList xmlns=""http://tempuri.org/"">
                <APICode>{token}</APICode>
                </GetCycleList>
                </soap:Body>
                </soap:Envelope>";
            try
            {
                var xmlDoc = await SoapServiceHelper.SendSoapRequestAsync(url, soapAction, soapBody);
                if (xmlDoc == null) return null;

                var model = new SyncServerDomainViewModel();

                var nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
                nsmgr.AddNamespace("soap", SoapActions.Envelope);
                nsmgr.AddNamespace("ns", SoapActions.Tempuri);


                var stateNode = xmlDoc.SelectSingleNode("//soap:Body/ns:GetCycleListResponse/ns:GetCycleListResult/ns:State", nsmgr);
                var messageNode = xmlDoc.SelectSingleNode("//soap:Body/ns:GetCycleListResponse/ns:GetCycleListResult/ns:Messege", nsmgr);
                var tableNameNode = xmlDoc.SelectSingleNode("//soap:Body/ns:GetCycleListResponse/ns:GetCycleListResult/ns:Data/ns:TableName", nsmgr);
                var comboItemNodes = xmlDoc.SelectNodes("//soap:Body/ns:GetCycleListResponse/ns:GetCycleListResult/ns:Data/ns:Items/ns:ComboItem", nsmgr);

                model.stateNode = (stateNode != null && stateNode.InnerText == "true");
                model.messageNode = messageNode?.ToString();
                model.tableNameNode = tableNameNode?.ToString();
                model.ListDore = new List<MabaniDomainViewModel>();

                if (comboItemNodes != null)
                {
                    _context.Database.SetCommandTimeout(180); // 180 seconds

                    await _context.Database.ExecuteSqlRawAsync(
                        "DELETE FROM Mabanis WHERE TableName = {0} AND CodMarkaz = {1}",
                        "Dore", codmarkaz
                    );

                    model.ListDore = new List<MabaniDomainViewModel>();
                    if (comboItemNodes != null) {
                        foreach (XmlNode item in comboItemNodes)
                        {
                            string idText = (item.SelectSingleNode("ns:ID", nsmgr) != null) ? item.SelectSingleNode("ns:ID", nsmgr).InnerText : string.Empty;
                            string description = (item.SelectSingleNode("ns:Des", nsmgr) != null) ? item.SelectSingleNode("ns:Des", nsmgr).InnerText : string.Empty;
                            string tozihat = (item.SelectSingleNode("ns:Tozihat", nsmgr) != null) ? item.SelectSingleNode("ns:Tozihat", nsmgr).InnerText : string.Empty;

                            if (long.TryParse(idText, out long id))
                            {
                                var entity = new MabaniDomainViewModel
                                {
                                    IDLinq = id,
                                    Onvan = description,
                                    Tozihat = tozihat,
                                    TableName = "Dore",
                                    KarbarIns = userid,
                                    KarbarUp = userid,
                                    DateIns = DateTime.Now,
                                    DateUp = DateTime.Now
                                };
                                model.ListDore.Add(entity);
                                var dore = _mapper.Map<Mabani>(entity);
                                await _context.Mabanis.AddAsync(dore);
                            }
                        }
                    }
                }
                await _context.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                Logger.LogToFile($"Exception during SOAP request: {ex.Message}");
                return null;
            }
        }

        public async Task<ReturnType<string>> GetToken(string username, string password, string codmarkaz)
        {
            //Logger.LogToFile($"Token API codmarkaz: {codmarkaz}");
            var url = await SoapServiceHelper.GetApiUrlAsync(_context, codmarkaz);
            //Logger.LogToFile($"Token API url: {url}");
            if (string.IsNullOrEmpty(url)) return new ReturnType<string>
            {
                type = Classes.Type.Error,
                message = "لینک API یافت نشد."
            };

            const string soapAction = SoapActions.TokenString;

            string soapBody = $@"<?xml version=""1.0"" encoding=""utf-8""?>
        <soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" 
                         xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
                         xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">
          <soap12:Body>
            <GetTokenString xmlns=""http://tempuri.org/"">
              <UserId>{username}</UserId>
              <Pass>{password}</Pass>
            </GetTokenString>
          </soap12:Body>
        </soap12:Envelope>";

            try
            {
                var xmlDoc = await SoapServiceHelper.SendSoapRequestAsync(url, soapAction, soapBody);
                if (xmlDoc == null) return new ReturnType<string>
                {
                    type = Classes.Type.Error,
                    message = "اتصال به سرور با خطا مواجه شد"
                };

                var nsmgr = SoapServiceHelper.CreateNamespaceManager(xmlDoc);
                var dataNode = xmlDoc.SelectSingleNode("//soap:Body/tempuri:GetTokenStringResponse/tempuri:GetTokenStringResult/tempuri:Data", nsmgr);
                Logger.LogToFile($"Token API innertext: {dataNode?.InnerText}");
                return new ReturnType<string>
                {
                    type = Classes.Type.Success,
                    message = dataNode?.InnerText
                };
            }
            catch (Exception ex)
            {
                Logger.LogToFile($"Exception during SOAP request: {ex.Message}");
                return new ReturnType<string>
                {
                    type = Classes.Type.Error,
                    message = ex.Message
                }; 
            }
        }

        public async Task<TakhlieDomainViewModel> Takhlie(string UserId, long idTakhlie)
        {
            var (url, token) = await SoapServiceHelper.GetApiInfoAsync(_context, UserId);
            if (string.IsNullOrEmpty(url)) return null;

            const string soapAction = SoapActions.BargiriString;
            string soapBody = $@"<?xml version=""1.0"" encoding=""utf-8""?>
        <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" 
                       xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
                       xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
          <soap:Body>
            <GetBargiri xmlns=""http://tempuri.org/"">
              <APICode>{token}</APICode>
              <ID>{idTakhlie}</ID>
            </GetBargiri>
          </soap:Body>
        </soap:Envelope>";

            try
            {
                var xmlDoc = await SoapServiceHelper.SendSoapRequestAsync(url, soapAction, soapBody);
                if (xmlDoc == null) return null;

                var nsmgr = SoapServiceHelper.CreateNamespaceManager(xmlDoc);
                var resultNode = xmlDoc.SelectSingleNode("//soap:Body/tempuri:GetBargiriResponse/tempuri:GetBargiriResult", nsmgr);
                if (resultNode == null) return null;

                var stateNode = resultNode.SelectSingleNode("tempuri:State", nsmgr);
                bool parsedState = false;
                bool state = stateNode != null && bool.TryParse(stateNode.InnerText, out parsedState) && parsedState;

                if (state)
                {
                    return new TakhlieDomainViewModel
                    {
                        DateTakhlie = resultNode.SelectSingleNode("tempuri:DateBarge", nsmgr)?.InnerText ?? string.Empty,
                        TimeTakhlie = resultNode.SelectSingleNode("tempuri:TimeBarge", nsmgr)?.InnerText ?? string.Empty,
                        NameRanande = resultNode.SelectSingleNode("tempuri:NameRanande", nsmgr)?.InnerText ?? string.Empty,
                        ShomareMashin = resultNode.SelectSingleNode("tempuri:ShoPelak", nsmgr)?.InnerText ?? string.Empty,
                        Tozihat = resultNode.SelectSingleNode("tempuri:Tozihat", nsmgr)?.InnerText ?? string.Empty
                    };
                }

                Logger.LogToFile("اطلاعات یافت نشد.اخطار");
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogToFile($"Exception during SOAP request: {ex.Message}");
                return null;
            }
        }

        public async Task<SendToServerDomainViewModel> SendToServer(string codmarkaz, string userid, string selectedDore)
        {
            var bargs = _context.BargeBaskouls
      .AsNoTracking()
      .Where(o =>
          o.CodMarkaz == codmarkaz &&
          (
              o.IDWebBarge == null ||
              o.DateInsToWeb == null ||
              (o.DateUpToWeb.HasValue && o.Date_Up.HasValue && o.DateUpToWeb.Value < o.Date_Up.Value && (o.FlgEbtal == true || o.FlgSabt == true) )
          )
      )
      .ToList();
            if (bargs.Count() == 0)
            {
                return new SendToServerDomainViewModel
                {
                    State = false,
                    Message = "هیچ برگه ای برای ارسال به سرور وجود ندارد"
                };
            }

            var (url, token) = await SoapServiceHelper.GetApiInfoAsync(_context, userid);
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(token)) return new SendToServerDomainViewModel
            {
                State = false,
                Message = "خطا در ارسال برگه‌ها"
            };

            foreach(var barg in bargs)
            {
                if(barg.DateInsToWeb == null)
                {
                    const string soapAction = SoapActions.Insert;
                    string soapBody = SoapActions.InsertSoapBody(barg, selectedDore, token);

                    try
                    {
                        var xmlDoc = await SoapServiceHelper.SendSoapRequestAsync(url, soapAction, soapBody);
                        if (xmlDoc == null) return new SendToServerDomainViewModel{
                            State = false,
                            Message = "خطا در ارسال برگه‌ها"
                        };

                        // Setup namespace manager
                        var nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
                        nsmgr.AddNamespace("soap", SoapActions.Envelope);
                        nsmgr.AddNamespace("t", SoapActions.Tempuri);

                        // Extract <InsertResult>
                        var resultNode = xmlDoc.SelectSingleNode("//t:InsertResult", nsmgr);
                        if (resultNode == null)
                        {
                            return new SendToServerDomainViewModel
                            {
                                State = false,
                                Message = "پاسخ نامعتبر از سرور"
                            };
                        }

                        // Extract values
                        var state = resultNode.SelectSingleNode("t:State", nsmgr)?.InnerText;
                        var message = resultNode.SelectSingleNode("t:Messege", nsmgr)?.InnerText;
                        var data = resultNode.SelectSingleNode("t:Data", nsmgr)?.InnerText;

                        bool success = state?.ToLower() == "true";

                        barg.IDWebBarge = int.Parse(data);
                        barg.DateInsToWeb = DateTime.Now;
                        barg.DateUpToWeb = DateTime.Now;
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        Logger.LogToFile($"API Insert: {ex.Message}");
                        return new SendToServerDomainViewModel
                        {
                            State = false,
                            Message = "عملیات با خطا مواجه شد"
                        };
                    }
                } 
                else {
                    const string soapAction = SoapActions.Update;
                    string soapBody = SoapActions.UpdateSoapBody(barg, selectedDore, token);
                    try
                    {
                        var xmlDoc = await SoapServiceHelper.SendSoapRequestAsync(url, soapAction, soapBody);
                        if (xmlDoc == null) return new SendToServerDomainViewModel
                        {
                            State = false,
                            Message = "خطا در ارسال برگه‌ها"
                        };

                        // Setup namespace manager
                        var nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
                        nsmgr.AddNamespace("soap", SoapActions.Envelope);
                        nsmgr.AddNamespace("t", SoapActions.Tempuri);

                        // Extract <UpdateResult>
                        var resultNode = xmlDoc.SelectSingleNode("//t:UpdateResult", nsmgr);
                        if (resultNode == null)
                        {
                            return new SendToServerDomainViewModel
                            {
                                State = false,
                                Message = "پاسخ نامعتبر از سرور"
                            };
                        }

                        // Extract values
                        var state = resultNode.SelectSingleNode("t:State", nsmgr)?.InnerText;
                        var message = resultNode.SelectSingleNode("t:Messege", nsmgr)?.InnerText;
                        var data = resultNode.SelectSingleNode("t:Data", nsmgr)?.InnerText;

                        bool success = state?.ToLower() == "true";

                        barg.DateUpToWeb = DateTime.Now;
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        Logger.LogToFile($"API Insert: {ex.Message}");
                        return new SendToServerDomainViewModel
                        {
                            State = false,
                            Message = "عملیات با خطا مواجه شد"
                        };
                    }
                }
            }
            return new SendToServerDomainViewModel
            {
                State = true,
                Message = "عملیات با موفقیت انجام شد"
            };
        }

        public async Task<bool> GetFromServer(long Dore, string ListId, string codmarkaz, string userId)
        {
            var (url, token) = await SoapServiceHelper.GetApiInfoAsync(_context, userId);
            if (string.IsNullOrEmpty(url)) return false;

            const string soapAction = SoapActions.GetFrom;

            string soapBody = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                            <soap:Body>
                            <BindingLoockUps xmlns=""http://tempuri.org/"">
                            <APICode>{token}</APICode>
                            <Cycle_ID>{Dore}</Cycle_ID>
                            <LookupId>{ListId}</LookupId>
                            </BindingLoockUps>
                          </soap:Body>
                        </soap:Envelope>";

            try
            {
                var xmlDoc = await SoapServiceHelper.SendSoapRequestAsync(url, soapAction, soapBody);
                if (xmlDoc == null) return false;

                XmlNamespaceManager ns = new XmlNamespaceManager(xmlDoc.NameTable);
                ns.AddNamespace("soap", SoapActions.Envelope);
                ns.AddNamespace("tempuri", SoapActions.Tempuri);

                XmlNode stateNode = xmlDoc.SelectSingleNode("//soap:Body/tempuri:BindingLoockUpsResponse/tempuri:BindingLoockUpsResult/tempuri:State", ns);
                string stateText = stateNode?.InnerText?.Trim();

                bool state = stateText == "true" || stateText == "falseداده ای یافت نشد";
                if (!state) return true;

                if (state)
                {
                    _context.Database.SetCommandTimeout(180); // 180 seconds
                    await _context.Database.ExecuteSqlRawAsync(
                        "DELETE FROM Mabanis WHERE TableName = {0} AND CodMarkaz = {1}",
                        ListId, codmarkaz
                    );

                    // Parse and insert new data
                    XmlNodeList items = xmlDoc.SelectNodes("//tempuri:Items/tempuri:ComboItem", ns);
                    foreach (XmlNode item in items)
                    {
                        string idText = item.SelectSingleNode("tempuri:ID", ns)?.InnerText;
                        string desText = item.SelectSingleNode("tempuri:Des", ns)?.InnerText;
                        string tozihatText = item.SelectSingleNode("tempuri:Tozihat", ns)?.InnerText;

                        if (!string.IsNullOrEmpty(idText))
                        {
                            await _context.Mabanis.AddAsync(new Mabani
                            {
                                IDLinq = long.Parse(idText),
                                Onvan = desText,
                                Tozihat = tozihatText,
                                TableName = ListId,
                                KarbarIns = userId,
                                DateIns = DateTime.Now,
                                KarbarUp = userId,
                                DateUp = DateTime.Now,
                                CodMarkaz = codmarkaz
                            });
                        }
                    }

                    // Save changes to the database
                    await _context.SaveChangesAsync();
                }

                return state;
            }
            catch (Exception ex)
            {
                Logger.LogToFile($"Exception during SOAP request: {ex.Message}");
                return false;
            }
        }

        public async Task InsertDefaultTypeJabejaiee(string userid, string codmarkaz)
        {
            _context.Database.SetCommandTimeout(180); // 180 seconds
            await _context.Database.ExecuteSqlRawAsync(
                "DELETE FROM Mabanis WHERE TableName = {0} AND CodMarkaz = {1}",
                "TypeJabejaiee", codmarkaz
            );

            string[] descriptions = { "ورود به کارخانه", "خروج از کارخانه", "جا به جایی داخلی", "متفرقه" };
            for (int i = 1; i <= descriptions.Length; i++)
            {
                await _context.Mabanis.AddAsync(new Mabani
                {
                    IDLinq = i,
                    Onvan = descriptions[i - 1],
                    TableName = "TypeJabejaiee",
                    KarbarIns = userid,
                    DateIns = DateTime.Now,
                    CodMarkaz = codmarkaz
                });
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSelectedCycle(string codmarkaz, string Dore)
        {
            try
            {
                var setting = await _context.Settings.FirstOrDefaultAsync(s => s.CodMarkaz == codmarkaz);
                if (setting != null)
                {
                    setting.Sal = Dore;
                }
                else
                {
                    await _context.AddAsync(new Settings
                    {
                        Sal = Dore,
                        SettingName = 1.ToString(),
                        CodMarkaz = codmarkaz
                    });
                }
                await _context.SaveChangesAsync();
            } catch(Exception ex)
            {
                Logger.LogToFile($"Error while saving setting.sal: {ex.Message}");
            }
        }

        public async Task<bool> isAutoAsyncOn(string codmarkaz)
        {
            var markaz = await _context.CodeMarkazs.FirstOrDefaultAsync(m => m.CodMarkaz == codmarkaz);
            if (markaz == null) return false;
            if (markaz.AutoAsync != true) return false;
            return true;
        }

        public async Task<string?> GetSelectedCycle(string codmarkaz)
        {
            var setting = await _context.Settings.FirstOrDefaultAsync(s => s.CodMarkaz == codmarkaz);
            if (setting == null || setting.Sal == null) return null;
            return setting.Sal;
        }

        public string CreateWindowsToken(IdentityUser user)
        {
            var token = _jwt.CreateToken(user);
            return token;
        }
    }
}
