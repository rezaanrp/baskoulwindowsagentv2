using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Applicant
{
	public class ApplicantDetailSearchViewModel
    {
		public int Id { get; set; }
		public string? DatedmissionFarsi { get; set; }
		public string? NameFamily { get; set; }
        public string? AdmissionsSpecialistUser_ { get; set; }
		public string? ApplicantRow { get; set; }
		public string? ApplicantFormSerial { get; set; }

	}
}
