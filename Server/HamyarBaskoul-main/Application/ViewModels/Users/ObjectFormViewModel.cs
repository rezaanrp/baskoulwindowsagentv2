using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Users
{
	public class ObjectFormViewModel
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

		public string GroupName { get; set; } = string.Empty;

		public string GroupNameFarsi { get; set; } = string.Empty;

		public bool IsAccess { get; set; } = false;
	}
}

