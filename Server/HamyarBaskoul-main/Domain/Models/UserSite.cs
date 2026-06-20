using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UserSite : AuditableEntity
    {
        public int ID { get; set; }

        [Required]
        public string UserId { get; set; }
        public AppUser User { get; set; }

        [Required]
        public int SiteId { get; set; }
        public Site Site { get; set; }

        public DateTime AssignedAt { get; set; } = DateTime.Now;
    }
}
