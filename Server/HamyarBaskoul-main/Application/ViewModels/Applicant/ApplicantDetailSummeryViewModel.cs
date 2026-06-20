using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Applicant
{
	public class ApplicantDetailSummeryViewModel
	{
		public int Id { get; set; }
		public string? DatedmissionFarsi { get; set; }
		public string? AdmissionsSpecialistUser_ { get; set; }
		public string? ApplicantRow { get; set; }
		public string? ApplicantFormSerial { get; set; }
		public string? ApplicantDemand { get; set; }
		public string? ApplicantType { get; set; }
		public string? BudgetBuyFrom { get; set; }
		public string? BudgetBuyTo { get; set; }
		public string? BudgetRentFrom { get; set; }
		public string? BudgetRentTo { get; set; }
		public string? BudgetMortgageFrom { get; set; }
		public string? BudgetMortgageTo { get; set; }
		public string? name_family { get; set; }

		public string? RequiredBedroomSizeFrom { get; set; }
		public string? RequiredBedroomSizeTo { get; set; }

        public string source_of_budge { get; set; }
        public string? RequiredBedroomSizeFromTo { get; set; }
		public string? RequiredSizeFrom { get; set; }
		public string? RequiredSizeTo { get; set; }


		public string? RequiredSizeFromTo { get; set; }
		public string? ArchiveDate { get; set; }
		public string? ArchiveReason { get; set; }
		public string? ArchiveUser { get; set; }
		public string? ReturnArchiveDate { get; set; }
		public string? ReturnArchiveReason { get; set; }
		public string? ReturnArchiveUser { get; set; }
		public string? BudgetSource { get; set; }
        public int? TypeOfResidenceNumber { get; set; }
        



    }
}
