using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.FilingUser
{
	public class FilingUserDetailsListSummeryViewModel
	{
		public int Id { get; set; }
        public string Mobile { get; set; }

        public string? DatedmissionFarsi { get; set; }
		public string? AdmissionsSpecialistUser_ { get; set; }
		public string? FileIndicatorId { get; set; }
		public string? SerialFilingForm { get; set; }
		public ulong? PriceInformationTotalPrice { get; set; }
		public string? PriceInformationTotalPrice_s { get; set; }
        public string? BuildingType { get; set; }
        public string? TransactionType { get; set; }
        public string? name_family { get; set; }
		public string? MainStreet { get; set; }
		public float? TotalLandArea { get; set; }

        public int? NumberSleeps { get; set; }



		public bool IsArchive { get; set; }
		public string? ArchiveDate { get; set; }
		public string? ArchiveUser_ { get; set; }
		public string? ArchiveReason_ { get; set; }
		public string? ReturnArchiveDate { get; set; }
		public string? ReturnArchiveUser_ { get; set; }
		public string? ReturnArchiveReason_ { get; set; }
        public int? RebuildabilityBaseTableId { get; set; }//قابلیت بازسازی
        public string? RebuildabilityBaseTable { get; set; }//قابلیت بازسازی
        public string? Rebuildability { get; set; }//قابلیت بازسازی
        public int? TotalRecord { get; set; }

    }
}
