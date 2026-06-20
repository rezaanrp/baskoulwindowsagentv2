using Application.ViewModels.BaseTable;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.FilingUser.FilingOperation
{
	public class FilingUserDetailFilingOperationViewModel
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

		public bool FilingOperationFourStageExpert { get; set; }

		public string? FilingOperationFourStageExpertName_ { get; set; }
		public DateTime? FilingOperationFourStageExpertDate { get; set; }
		public string? FilingOperationFourStageExpertDateFarsi { get; set; }
		public bool FilingOperationInsertAd { get; set; }

		public string? FilingOperationInsertAdName_ { get; set; }
		public DateTime? FilingOperationInsertAdDate { get; set; }
		public string? FilingOperationInsertAdDateFarsi { get; set; }
		public string? FilingOperationInsertAdPlace { get; set; }

		public bool FilingOperationOccasionOperation { get; set; }
		public bool FilingOperationOccasion { get; set; }
		public DateTime? FilingOperationOccasionDate { get; set; }
		public string? FilingOperationOccasionDateFarsi { get; set; }
		public bool FilingOperationOccasionReturn { get; set; }
		public DateTime? FilingOperationOccasionReturnDate { get; set; }
		public string? FilingOperationOccasionReturnDateFarsi { get; set; }


		public List<BaseTableViewModel>? TransactionType { get; set; }
        public List<BaseTableViewModel>? BuildingType { get; set; }
        public List<AppUser>? appUsers { get; set; }

    }
}
