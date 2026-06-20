using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Application.ViewModels.Applicant.Report
{
	public class ApplicantByUserActivityViewModel
	{
		public string Id { get; set; }
		public string? Name { get; set; }
		public string? Family { get; set; }
		public int? FileCount { get; set; }
		public int? FileTypeAdvertising { get; set; }
		public int? FileTypeCredit { get; set; }
		public int? FileTypeInternet { get; set; }
        public int? FileTypeIntroduction { get; set; }
        public int? TransactionType { get; set; }

		public int? DemandBuildingApartment { get; set; }
		public int? DemandBuildinVila { get; set; }
		public int? DemandBuildingKalingi { get; set; }
		public int? DemandBuildingGround { get; set; }
		public int? DemandBuildingOffice { get; set; }
		public int? DemandBuildingGarden { get; set; }
		public int? DemandBuildingCommercial { get; set; }
		public int? DemandBuildingIndustrial { get; set; }
		public int? DemandBuildingStore { get; set; }
		public int? DemandBuildingAgriculture { get; set; }

		public int? ApplicantService { get; set; }
		public int? CashBudget { get; set; }
		public int? ExchangeBudget { get; set; }
		public int? BuildingBudget { get; set; }


	}
}


