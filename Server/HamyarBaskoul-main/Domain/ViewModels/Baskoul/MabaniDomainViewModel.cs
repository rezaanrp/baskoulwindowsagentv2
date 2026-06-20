using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.Baskoul
{
    public class MabaniDomainViewModel
    {
        public long? IDLinq { get; set; }
        public int ID { get; set; }
        public string? Onvan { get; set; }
        public string? Tozihat { get; set; }
        public string? KarbarIns { get; set; }
        public string? KarbarUp { get; set; }
        public DateTime? DateIns { get; set; }
        public DateTime? DateUp { get; set; }
        public string? CIP { get; set; }
        public string? TableName { get; set; }
        public string? CodMarkaz { get; set; }
    }
}
