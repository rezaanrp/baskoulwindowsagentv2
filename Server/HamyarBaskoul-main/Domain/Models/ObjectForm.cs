using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ObjectForm : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
        [MaxLength(100)]
		public string Name { get; set; }
        [MaxLength(100)]
        public string NameFarsi { get; set; }
        public string GroupName { get; set; }
        public string GroupNameFarsi { get; set; }
        public string Departement { get; set; }
		// Navigation property for the related ObjectFormUser entities
		public ICollection<ObjectFormUser> ObjectFormUsers { get; set; }
	}
}
