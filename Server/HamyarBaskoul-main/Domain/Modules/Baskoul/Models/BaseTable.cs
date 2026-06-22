using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class BaseTable : AuditableEntity
	{
        public int Id { get; set; }
        [MaxLength(100)]
		public string? GroupName { get; set; }
		[MaxLength(100)]
		public string? Value { get; set; }
		public string? ValueFarsi{ get; set; }
        [MaxLength(100)]
		public string? Description { get; set; }
        public bool IsDeleted { get; set; }

	}
}


