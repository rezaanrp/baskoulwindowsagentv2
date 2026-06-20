using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.FilingUser
{
	public class FilingUserDetailVisitViewModel
	{
		[Key]
		public int Id { get; set; }
		public DateTime? VisitServiceDate { get; set; }
		[StringLength(50)]
		public string? VisitServiceDateFarsi { get; set; }
		[MaxLength(8)]
		public string? VisitServiceTime { get; set; }
		[MaxLength(50)]
		public string? VisitServiceUser_ { get; set; }
		[MaxLength(10)]
		public string? VisitServiceRowNumber { get; set; }
		public int FilingUserDetail_ { get; set; }
		public List<AppUser>? appUsers { get; set; }
		public string? FilingUserDetailVisitSupplier_ { get; set; }

		public bool IsDeleted { get; set; } = false;
		public int? ApplicantDetailId { get; set; }

	}
}
