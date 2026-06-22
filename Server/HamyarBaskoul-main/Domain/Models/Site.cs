using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Site : AuditableEntity
    { 
        public int ID { get; set; }
        public string? name { get; set; }
        public string? CodeMarkaz { get; set; }
        public bool isActive { get; set; } = true;
        public ICollection<UserSite> UserSites { get; set; } = new List<UserSite>();
    }
}

