using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class GhabzSerialTracker : AuditableEntity
    {
        public int Year { get; set; }
        public int Serial { get; set; }
        public string CodMarkaz { get; set; }
    }
}

