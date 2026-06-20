using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Classes
{
    public static class SoapActions
    {
        public const string TokenString = "http://tempuri.org/GetTokenString";
        public const string BargiriString = "http://tempuri.org/GetBargiri";
        public const string TakhlieString = "http://tempuri.org/GetBargiri";
        public const string Insert = "http://tempuri.org/Insert";
        public const string Update = "http://tempuri.org/Update";
        public const string GetFrom = "http://tempuri.org/BindingLoockUps";

        public const string GetCycleString = "http://tempuri.org/GetCycleList";
        public const string Soap = "http://www.w3.org/2003/05/soap-envelope";
        public const string Envelope = "http://schemas.xmlsoap.org/soap/envelope/";
        public const string Tempuri = "http://tempuri.org/";
        

        public static string InsertSoapBody(BargeBaskoul barg, string Dore, string token)
        {
            return $@"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Body>
    <Insert xmlns=""http://tempuri.org/"">
      <APICode>{token}</APICode>
      <barge>
        <ID>{barg.GhabzBaskolID ?? barg.ID}</ID>
        <DateBarge>{barg.DateBarge ?? ""}</DateBarge>
        <TimeBarge>{barg.TimeBarge ?? ""}</TimeBarge>
        <TimeBaskol>{barg.TimeBaskol ?? ""}</TimeBaskol>
        <DateBaskol>{barg.DateBaskol ?? ""}</DateBaskol>
        <IDNegahbani>{barg.IDNegahbani ?? 0}</IDNegahbani>
        <DateNegahbani>{barg.DateNegahbani ?? ""}</DateNegahbani>
        <TimeNegahbani>{barg.TimeNegahbani ?? ""}</TimeNegahbani>
        <IDTakhlie>{barg.IDTakhlie ?? 0}</IDTakhlie>
        <DateTakhlie>{barg.DateTakhlie ?? ""}</DateTakhlie>
        <TimeTakhlie>{barg.TimeTakhlie ?? ""}</TimeTakhlie>
        <IDBargiri>{barg.IDBargiri ?? 0}</IDBargiri>
        <DateBargiri>{barg.DateBargiri ?? ""}</DateBargiri>
        <TimeBargiri>{barg.TimeBargiri ?? ""}</TimeBargiri>
        <IDNemoneGiri>{barg.IDNemoneGiri ?? 0}</IDNemoneGiri>
        <IDRanande>{barg.IDRanande ?? 0}</IDRanande>
        <OnvanRanande>{barg.OnvanRanandeh ?? ""}</OnvanRanande>
        <IDVasile>{barg.IDVasile ?? 0}</IDVasile>
        <IDTypeMarkazHaml>{barg.IDTypeMarkazHamz ?? 0}</IDTypeMarkazHaml>
        <IDMarkaz>{barg.IDMarkaz ?? 0}</IDMarkaz>
        <IDShakhs>{barg.IDShakhs ?? 0}</IDShakhs>
        <IDTafsili>{barg.IDTafsili ?? 0}</IDTafsili>
        <IDTafsili2>{barg.IDTafsili2 ?? 0}</IDTafsili2>
        <IDAnbar>{barg.IDAnbar ?? 0}</IDAnbar>
        <IDKala>{barg.IDKala ?? 0}</IDKala>
        <ShomareMashin>{barg.ShomareMashin ?? ""}</ShomareMashin>
        <KerayeHaml>{barg.KerayeHaml ?? 0}</KerayeHaml>
        <IDTypeJabejaiee>{barg.IDTypeJabejaiee ?? 0}</IDTypeJabejaiee>
        <IDDarkhastAmaliat>{barg.IDDarkhastAmaliat ?? 0}</IDDarkhastAmaliat>
        <TypeGheymatGozari>{barg.TypeGheymatGozari ?? 0}</TypeGheymatGozari>
        <IDGheymatGozari>{barg.IDGheymatGozari ?? 0}</IDGheymatGozari>
        <NerkhKala>{barg.NerkhKala ?? 0}</NerkhKala>
        <VaznPor>{barg.VaznPor ?? 0}</VaznPor>
        <VaznKhali>{barg.VanKhali ?? 0}</VaznKhali>
        <VaznBasteBandi>{barg.VaznBasteBandi ?? 0}</VaznBasteBandi>
        <TimeVaznPor>{barg.TimeVaznPor}</TimeVaznPor>
        <TimeVaznKhali>{barg.TimeVaznKhali}</TimeVaznKhali>
        <TypeBargeAnbar>{barg.TypeBargeAnbar ?? 0}</TypeBargeAnbar>
        <Tozihat>{barg.Tozihat}</Tozihat>
        <CountChap>{barg.CountChap ?? 0}</CountChap>
        <DateChap>{barg.DateChap}</DateChap>
        <TimeChap>{barg.TimeChap}</TimeChap>
        <MeghdarDarkhastAmaliat>{barg.MeghdarDarkhastAmaliat ?? 0}</MeghdarDarkhastAmaliat>
        <KarbarTaeedKonandeDA>{barg.KarbarTaeedKonandeDA}</KarbarTaeedKonandeDA>
        <FlgSabt>{(barg.FlgSabt != null ? barg.FlgSabt.ToString().ToLower() : "false")}</FlgSabt>
        <FlgEbtal>{(barg.FlgEbtal != null ? barg.FlgEbtal.ToString().ToLower() : "false")}</FlgEbtal>
        <MablaghBaskol>{barg.MablaghBaskol ?? 0}</MablaghBaskol>
        <IDBaskul>{barg.IDBaskul ?? 0}</IDBaskul>
        <IDDore>{Dore}</IDDore>
        <IDKey>{barg.IDWebBarge ?? 0}</IDKey>
        <Karbar_Sabt>0</Karbar_Sabt>
        <Date_Sabt>0</Date_Sabt>
        <Karbar_Ebtal>0</Karbar_Ebtal>
        <Date_Ebtal>0</Date_Ebtal>
        <TypeBarge>{barg.TypeBarge}</TypeBarge>
      </barge>
    </Insert>
  </soap:Body>
</soap:Envelope>";
        }

        public static string UpdateSoapBody(BargeBaskoul barg, string Dore, string token)
        {
            return $@"<?xml version=""1.0"" encoding=""utf-8""?>
                    <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                      <soap:Body>
                        <Update xmlns=""http://tempuri.org/"">
                               <APICode>{token}</APICode>
                          <barge>
                            <ID>{barg.GhabzBaskolID ?? barg.ID}</ID>
                            <DateBarge>{barg.DateBarge ?? ""}</DateBarge>
                            <TimeBarge>{barg.TimeBarge ?? ""}</TimeBarge>
                            <TimeBaskol>{barg.TimeBaskol ?? ""}</TimeBaskol>
                            <DateBaskol>{barg.DateBaskol ?? ""}</DateBaskol>
                            <IDNegahbani>{barg.IDNegahbani ?? 0}</IDNegahbani>
                            <DateNegahbani>{barg.DateNegahbani ?? ""}</DateNegahbani>
                            <TimeNegahbani>{barg.TimeNegahbani ?? ""}</TimeNegahbani>
                            <IDTakhlie>{barg.IDTakhlie ?? 0}</IDTakhlie>
                            <DateTakhlie>{barg.DateTakhlie ?? ""}</DateTakhlie>
                            <TimeTakhlie>{barg.TimeTakhlie ?? ""}</TimeTakhlie>
                            <IDBargiri>{barg.IDBargiri ?? 0}</IDBargiri>
                            <DateBargiri>{barg.DateBargiri ?? ""}</DateBargiri>
                            <TimeBargiri>{barg.TimeBargiri ?? ""}</TimeBargiri>
                            <IDNemoneGiri>{barg.IDNemoneGiri ?? 0}</IDNemoneGiri>
                            <IDRanande>{barg.IDRanande ?? 0}</IDRanande>
                            <OnvanRanande>{barg.OnvanRanandeh ?? ""}</OnvanRanande>
                            <IDVasile>{barg.IDVasile ?? 0}</IDVasile>
                            <IDTypeMarkazHaml>{barg.IDTypeMarkazHamz ?? 0}</IDTypeMarkazHaml>
                            <IDMarkaz>{barg.IDMarkaz ?? 0}</IDMarkaz>
                            <IDShakhs>{barg.IDShakhs ?? 0}</IDShakhs>
                            <IDTafsili>{barg.IDTafsili ?? 0}</IDTafsili>
                            <IDTafsili2>{barg.IDTafsili2 ?? 0}</IDTafsili2>
                            <IDAnbar>{barg.IDAnbar ?? 0}</IDAnbar>
                            <IDKala>{barg.IDKala ?? 0}</IDKala>
                            <ShomareMashin>{barg.ShomareMashin ?? ""}</ShomareMashin>
                            <KerayeHaml>{barg.KerayeHaml ?? 0}</KerayeHaml>
                            <IDTypeJabejaiee>{barg.IDTypeJabejaiee ?? 0}</IDTypeJabejaiee>
                            <IDDarkhastAmaliat>{barg.IDDarkhastAmaliat ?? 0}</IDDarkhastAmaliat>
                            <TypeGheymatGozari>{barg.TypeGheymatGozari ?? 0}</TypeGheymatGozari>
                            <IDGheymatGozari>{barg.IDGheymatGozari ?? 0}</IDGheymatGozari>
                            <NerkhKala>{barg.NerkhKala ?? 0}</NerkhKala>
                            <VaznPor>{barg.VaznPor ?? 0}</VaznPor>
                            <VaznKhali>{barg.VanKhali ?? 0}</VaznKhali>
                            <VaznBasteBandi>{barg.VaznBasteBandi ?? 0}</VaznBasteBandi>
                            <TimeVaznPor>{barg.TimeVaznPor}</TimeVaznPor>
                            <TimeVaznKhali>{barg.TimeVaznKhali}</TimeVaznKhali>
                            <TypeBargeAnbar>{barg.TypeBargeAnbar ?? 0}</TypeBargeAnbar>
                            <Tozihat>{barg.Tozihat}</Tozihat>
                            <CountChap>{barg.CountChap ?? 0}</CountChap>
                            <DateChap>{barg.DateChap}</DateChap>
                            <TimeChap>{barg.TimeChap}</TimeChap>
                            <MeghdarDarkhastAmaliat>{barg.MeghdarDarkhastAmaliat ?? 0}</MeghdarDarkhastAmaliat>
                            <KarbarTaeedKonandeDA>{barg.KarbarTaeedKonandeDA}</KarbarTaeedKonandeDA>
                            <FlgSabt>{(barg.FlgSabt != null ? barg.FlgSabt.ToString().ToLower() : "false")}</FlgSabt>
                            <FlgEbtal>{(barg.FlgEbtal != null ? barg.FlgEbtal.ToString().ToLower() : "false")}</FlgEbtal>
                            <MablaghBaskol>{barg.MablaghBaskol ?? 0}</MablaghBaskol>
                            <IDBaskul>{barg.IDBaskul ?? 0}</IDBaskul>
                            <IDDore>{Dore}</IDDore>
                            <IDKey>{barg.IDWebBarge ?? 0}</IDKey>
                            <Karbar_Sabt>0</Karbar_Sabt>
                            <Date_Sabt>0</Date_Sabt>
                            <Karbar_Ebtal>0</Karbar_Ebtal>
                            <Date_Ebtal>0</Date_Ebtal>
                            <TypeBarge>{barg.TypeBarge}</TypeBarge>
                          </barge>
                        </Update>
                      </soap:Body>
                    </soap:Envelope>";
        }
    }
}
