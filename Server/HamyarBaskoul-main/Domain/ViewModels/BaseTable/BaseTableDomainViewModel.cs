using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.BaseTable
{
    public class BaseTableDomainViewModel
	{
		public int Id { get; set; }
		[MaxLength(100)]
		public string? GroupName { get; set; }
		[MaxLength(100)]
		public string? Value { get; set; }
		public string? ValueFarsi { get; set; }
		[MaxLength(100)]
		public string? Description { get; set; }
		public bool IsDeleted { get; set; }
	}
}
