using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class ReportSettingDomainViewModel
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        public string? ShomareSanad { get; set; }

        [MaxLength(50)]
        public string? ShomareBaznegari { get; set; }

        [MaxLength(10)]
        public string? TarikhBaznegari { get; set; }

        [MaxLength(255)]
        public string? OfficeAddress { get; set; }

        [MaxLength(255)]
        public string? FactoryAddress { get; set; }

        [MaxLength(255)]
        public string? LogoPath { get; set; }

        [MaxLength(50)]
        public string? FactoryPhone { get; set; }

        [MaxLength(50)]
        public string? OfficePhone { get; set; }

        [MaxLength(50)]
        public string? Fax { get; set; }

        [MaxLength(50)]
        public string? Telfax { get; set; }

        [MaxLength(50)]
        public string? Postcode { get; set; }

        [MaxLength(50)]
        public string? KarbarIns { get; set; }

        [MaxLength(50)]
        public string? KarbarUp { get; set; }

        public DateTime? DateIns { get; set; }

        public DateTime? DateUp { get; set; }

        public string? CodeMarkaz { get; set; }
    }
}

