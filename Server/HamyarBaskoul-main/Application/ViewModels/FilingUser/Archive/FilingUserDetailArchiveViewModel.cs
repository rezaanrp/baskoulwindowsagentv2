using Application.ViewModels.BaseTable;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.FilingUser.Archive
{
	public class FilingUserDetailArchiveViewModel
	{
		public int Id { get; set; }
		[MaxLength(100)]
		public string Name { get; set; }
		[MaxLength(100)]
		public string Family { get; set; }
		[MaxLength(11, ErrorMessage = "فرمت شماره همراه اشتباه است ")]
		[MinLength(11, ErrorMessage = "فرمت شماره همراه اشتباه است ")]
		[RegularExpression(@"^[0-9]{11}", ErrorMessage = "فرمت شماره همراه اشتباه است ")]
		public string Mobile { get; set; }
		[MaxLength(11, ErrorMessage = "فرمت شماره تلفن اشتباه است ")]
		[MinLength(11, ErrorMessage = "فرمت شماره تلفن اشتباه است ")]
		[RegularExpression(@"^[0-9]{11}", ErrorMessage = "فرمت شماره تلفن اشتباه است ")]
		public string? Tel { get; set; }

		[Column(TypeName = "Date")]
		public DateTime Datedmission { get; set; }

		public string? DatedmissionFarsi { get; set; }
		[MaxLength(10)]

		public string? FileIndicatorId { get; set; }
		public string AdmissionsSpecialistUser_ { get; set; }
		[MaxLength(10)]

		public string SerialFilingForm { get; set; }

		public int BaseTableTypeOfAdmission_ { get; set; }


		public int TransactionType_ { get; set; }
		public int BuildingType_ { get; set; }
		public bool IsArchive { get; set; }
		public DateTime? ArchiveDate { get; set; }
		public string? ArchiveDateFarsi { get; set; }
		[MaxLength(50)]
		public string? ArchiveUser_ { get; set; }
		public int? ArchiveReason_ { get; set; }
		public DateTime? ReturnArchiveDate { get; set; }
		public string? ReturnArchiveDateFarsi { get; set; }
		[MaxLength(50)]
		public string? ReturnArchiveUser_ { get; set; }
		public int? ReturnArchiveReason_ { get; set; }
        public List<BaseTableViewModel>? ArchiveReason { get; set; }
        public List<BaseTableViewModel>? ReturnArchiveReason { get; set; }
        public List<BaseTableViewModel>? TransactionType { get; set; }
        public List<BaseTableViewModel>? BuildingType { get; set; }
        public List<AppUser>? appUsers { get; set; }

    }
}
