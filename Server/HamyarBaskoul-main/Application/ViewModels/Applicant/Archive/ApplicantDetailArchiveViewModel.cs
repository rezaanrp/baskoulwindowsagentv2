using Application.ViewModels.BaseTable;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Applicant.Archive
{
	public class ApplicantDetailArchiveViewModel
	{
        [Key]
        public int Id { get; set; }
		[MaxLength(100)]
		public string? Name { get; set; }
		[MaxLength(100)]
		public string Family { get; set; }

		public string Mobile { get; set; }

        public DateTime? ArchiveDate { get; set; }
        public string? ArchiveDateFarsi { get; set; }
        [MaxLength(50)]
        public string? ArchiveUser_ { get; set; }
        public bool IsArchive { get; set; }
        public DateTime? ReturnArchiveDate { get; set; }
        public string? ReturnArchiveDateFarsi { get; set; }
        [MaxLength(50)]
        public string? ReturnArchiveUser_ { get; set; }
        public int? ArchiveReason_ { get; set; }
        public int? ReturnArchiveReason_ { get; set; }

        public bool IsDeleted { get; set; }
        public List<BaseTableViewModel>? ArchiveReason { get; set; }
        public List<BaseTableViewModel>? ReturnArchiveReason { get; set; }
        public List<BaseTableViewModel>? TransactionType { get; set; }
        public List<BaseTableViewModel>? BuildingType { get; set; }
        public List<AppUser>? appUsers { get; set; }

    }
}
