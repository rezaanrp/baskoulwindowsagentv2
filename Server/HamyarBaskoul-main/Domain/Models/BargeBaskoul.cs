using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class BargeBaskoul : AuditableEntity
    {
        [Key]
        public long ID { get; set; }

        public string? DateBarge { get; set; }
        public string? TimeBarge { get; set; }
        public string? TimeBaskol { get; set; }
        public string? DateBaskol { get; set; }
        public long? IDNegahbani { get; set; }
        public string? DateNegahbani { get; set; }
        public string? TimeNegahbani { get; set; }
        public long? IDTakhlie { get; set; }
        public string? DateTakhlie { get; set; }
        public string? TimeTakhlie { get; set; }
        public long? IDBargiri { get; set; }
        public string? DateBargiri { get; set; }
        public string? TimeBargiri { get; set; }
        public long? IDNemoneGiri { get; set; }
        public long? IDRanande { get; set; }
        public long? IDVasile { get; set; }
        public long? IDTypeMarkazHamz { get; set; }
        public long? IDMarkaz { get; set; }
        public long? IDShakhs { get; set; }
        public long? IDTafsili { get; set; }
        public long? IDTafsili2 { get; set; }
        public long? IDAnbar { get; set; }
        public long? IDKala { get; set; }
        public string? ShomareMashin { get; set; }
        public float? KerayeHaml { get; set; }
        public long? IDTypeJabejaiee { get; set; }
        public long? IDDarkhastAmaliat { get; set; }
        public int? TypeGheymatGozari { get; set; }
        public long? IDGheymatGozari { get; set; }
        public float? NerkhKala { get; set; }
        public float? VaznPor { get; set; }
        public float? VanKhali { get; set; }
        public float? VaznBasteBandi { get; set; }
        public string? TimeVaznPor { get; set; }
        public string? TimeVaznKhali { get; set; }
        public int? TypeBargeAnbar { get; set; }
        public string? Tozihat { get; set; }
        public int? CountChap { get; set; }
        public string? DateChap { get; set; }
        public string? TimeChap { get; set; }
        public float? MeghdarDarkhastAmaliat { get; set; }
        public string? KarbarTaeedKonandeDA { get; set; }
        public string? OnvanRanandeh { get; set; }
        public bool? FlgSabt { get; set; }
        public bool? FlgEbtal { get; set; }
        public float? MablaghBaskol { get; set; }
        public string? Karbar_Ins { get; set; }
        public string? Karbar_Up { get; set; }

        public long? IDBaskul { get; set; }
        public int? IDWebBarge { get; set; }
        /// <summary>
        /// 1 : voroud kala - havale anbar,
        /// 2 : khorouj kala - resid anbar
        /// </summary>
        public int? TypeBarge { get; set; }
        public string? Karbar_Sabt { get; set; }

        public DateTime? Date_Sabt { get; set; }
        public DateTime? Date_Up { get; set; }
        public string? Karbar_Ebtal { get; set; }
        public DateTime? Date_Ebtal { get; set; }
        public DateTime? DateTimeBarge { get; set; }
        public DateTime? DateInsToWeb { get; set; }
        public DateTime? DateUpToWeb { get; set; }

        public long? IDTypeBaskul { get; set; }
        public long? GhabzBaskolID { get; set; }
        public string? CodMarkaz { get; set; }
        public int? siteId { get; set; }
    }
}

