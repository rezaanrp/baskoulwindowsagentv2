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
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public bool isActive { get; set; } = true;
        public ICollection<Weighbridge> Weighbridges { get; set; } = new List<Weighbridge>();
        public ICollection<UserSiteAccess> UserSiteAccesses { get; set; } = new List<UserSiteAccess>();
    }
}

