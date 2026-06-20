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
	public class ApplicantDetailEditViewModel
	{
		public List<AppUser>? appUsers { get; set; }

		public ApplicantDetailViewModel?  ApplicantDetail { get; set; }

	}
}
