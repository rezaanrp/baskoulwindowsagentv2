using Application.ViewModels.BaseTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.FilingUser
{
	public class FilingUserDetailsListSearchViewModel
	{
		public int Id { get; set; }
		public string? DatedmissionFarsi { get; set; }
		public string? AdmissionsSpecialistUser_ { get; set; }
		public string? FileIndicatorId { get; set; }
		public ulong? PriceInformationTotalPrice { get; set; }
		public string? PriceInformationTotalPrice_s { get; set; }
        public string? BuildingType { get; set; }
		public string? TransactionType { get; set; }
		public string? Name { get; set; }
		public string? Family { get; set; }
		public string? Mobile { get; set; }

		public string? name_family { get; set; }
		public string? MainStreet { get; set; }

		public ulong PriceInformationMortgage { get; set; }
		public ulong PriceInformationRent { get; set; }
		public string? PriceInformationMortgage_s { get; set; }
		public string? PriceInformationRent_s { get; set; }
		public float? TotalLandArea { get; set; }
		public int? NumberSleeps { get; set; }

		

	}
}
