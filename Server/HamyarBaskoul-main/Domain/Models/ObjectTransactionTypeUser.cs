using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ObjectTransactionTypeUser : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
		public string UserId { get; set; }
        public int ObjectId { get; set; }
        public DateTime CreatedDate { get; set; }
        [MaxLength(50)]
        public string SupplierId { get; set; }

    }
}
