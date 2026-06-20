using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Applicant
{
	public class ApplicantDetailServiceListDto
	{
		public int? Id { get; set; }
		public string? FileOwner { get; set; }
		public int? FilingUserDetailId { get; set; }
		public int? ApplicantDetailsId { get; set; }
		public DateTime? VisitServiceDate { get; set; }
		public string? VisitServiceTime { get; set; }
		public string? VisitServiceUser { get; set; }
		
	}
}
