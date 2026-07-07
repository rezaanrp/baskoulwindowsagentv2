using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class WeighbridgeSite : AuditableEntity
    { 
        public int ID { get; set; }
        public string? name { get; set; }
        public string? Company { get; set; }
        public bool isActive { get; set; } = true;
        public ICollection<WeighbridgeSiteUser> WeighbridgeSiteUsers { get; set; } = new List<WeighbridgeSiteUser>();
    }
}

