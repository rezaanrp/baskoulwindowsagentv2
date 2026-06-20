using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.FilingUserDetail
{
	public class FilingUserDetailSearchDto
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string Family { get; set; }
		public string Mobile { get; set; }
		public string? Tel { get; set; }
		public string TransactionType { get; set; }
		public string BuildingType { get; set; }
		public int? TransactionTypeId { get; set; }
		public int? BuildingTypeId { get; set; }
		public ulong PriceInformationTotalPrice { get; set; }
		public ulong PriceInformationMortgage { get; set; }
		public ulong PriceInformationRent { get; set; }
		public float? TotalLandArea { get; set; }
		public int? NumberSleeps { get; set; }

		public DateTime? Datedmission { get; set; }
		public string? AdmissionsSpecialistUser_ { get; set; }
		public string? MainStreet { get; set; }

		public bool IsArchive { get; set; }
		public DateTime? ArchiveDate { get; set; }
		public string? ArchiveUser_ { get; set; }
		public string? ArchiveReason_ { get; set; }
		public DateTime? ReturnArchiveDate { get; set; }
		public string? ReturnArchiveUser_ { get; set; }
		public string? ReturnArchiveReason_ { get; set; }
       /// <summary>
	   /// public string? RebulildAbilityGrade { get; set; }
	   /// </summary>
        
        public int? RebuildabilityBaseTableId { get; set; }
        public int? TotalRecord { get; set; }



    }
}
