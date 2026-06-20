using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Reports
{
    public class TripleReportViewModel
    {
        public string? TypeBarge { get; set; }
        public string? CoName { get; set; }
        public long? GhabzBaskoul { get; set; }
        public DateTime? DateBarge { get; set; }
        public long? IdBargiri { get; set; }
        public long? IdTakhlie { get; set; }
        public string? NameRanande { get; set; }
        public string? Vasile { get; set; }
        public string? Kala { get; set; }
        public string? ShomareMashin { get; set; }
        public float? VaznPor { get; set; }
        public float? VaznKhali { get; set; }
        public float? VazneBasteBandi { get; set; }
        public string? Vaznekhlaes { get; set; }
        public string? TimeVaznePor { get; set; }
        public string? TimeVazneKhali { get; set; }
        public string? Tozihat { get; set; }
        public string? OfficeAdress { get; set; }
        public string? FactoryAddress { get; set; }
        public string? AdminName { get; set; }
        public string? ShomareSanad { get; set; }
        public string? BaznegariDate { get; set; }
        public string? BaznegariNumber { get; set; }
        public string? LogoPath { get; set; }
    }
}
