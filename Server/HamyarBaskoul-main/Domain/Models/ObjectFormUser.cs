using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ObjectFormUser : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
		public string User_ { get; set; }
        public int ObjectFormId { get; set; }
		// Navigation property to link to the parent ObjectForm
		public ObjectForm ObjectForm { get; set; }
		public DateTime CreatedDate { get; set; }
        [MaxLength(50)]
        public string Supplier_ { get; set; }

    }
}

