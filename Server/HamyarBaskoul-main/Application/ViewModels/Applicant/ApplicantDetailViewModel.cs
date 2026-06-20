using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Applicant
{

	[AttributeUsage(AttributeTargets.Class)]
	public class AtLeastOneCheckboxAttribute : ValidationAttribute
	{
		private string[] _checkboxNames;

		public AtLeastOneCheckboxAttribute(params string[] checkboxNames)
		{
			_checkboxNames = checkboxNames;
			
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var propertyInfos = value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
				.Where(x => _checkboxNames.Contains(x.Name));

			var values = propertyInfos.Select(x => x.GetGetMethod().Invoke(value, null));
			if (values.Any(x => Convert.ToBoolean(x)))
				return ValidationResult.Success;
			else
			{
				//ErrorMessage = "حداقل یک گزینه را انتخاب کنید";
				return new ValidationResult(ErrorMessage);
			}
		}
	}

	[AttributeUsage(AttributeTargets.Class)]
	public class AtLeastOneCheckboxDemandBuildingAttribute : ValidationAttribute
	{
		private string[] _checkboxNames;

		public AtLeastOneCheckboxDemandBuildingAttribute(params string[] checkboxNames)
		{
			_checkboxNames = checkboxNames;

		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var propertyInfos = value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
				.Where(x => _checkboxNames.Contains(x.Name));

			var values = propertyInfos.Select(x => x.GetGetMethod().Invoke(value, null));
			if (values.Any(x => Convert.ToBoolean(x)))
				return ValidationResult.Success;
			else
			{
				//ErrorMessage = "حداقل یک گزینه را انتخاب کنید";
				return new ValidationResult(ErrorMessage);
			}
		}
	}

	[AtLeastOneCheckbox(new string[] { "ApplicantTypeBuy", "ApplicantTypeMortgageAndRent", "ApplicantTypeMortgage", 
										"ApplicantTypePrebuy", "ApplicantTypeParticipation" },ErrorMessage ="حداقل یک نوع تقاضا انتخاب شود")]
	[AtLeastOneCheckboxDemandBuilding(new string[] {  "DemandBuildingOffice", "DemandBuildingGround", "DemandBuildingKalingi",
														"DemandBuildinVila", "DemandBuildingApartment", "DemandBuildingAgriculture", 
														"DemandBuildingStore", "DemandBuildingIndustrial", "DemandBuildingCommercial",
														"DemandBuildingGarden" },ErrorMessage ="حداقل یک نوع ملک انتخاب شود")]
	public class ApplicantDetailViewModel
	{
        [Key]
        public int Id { get; set; }
		[MaxLength(100)]
		

		public string? Name { get; set; }
		[MaxLength(100)]
		[Required(ErrorMessage = "لطفا نام خانوادگی را وارد کنید")]


		public string Family { get; set; }
		[MaxLength(11, ErrorMessage = "فرمت شماره همراه اشتباه است ")]
		[MinLength(11, ErrorMessage = "فرمت شماره همراه اشتباه است ")]
		[RegularExpression(@"^[0-9]{11}", ErrorMessage = "فرمت شماره همراه اشتباه است ")]
		[Required(ErrorMessage = "لطفا شماره همراه را وارد کنید")]
		public string Mobile { get; set; }
		public string? Tel { get; set; }
		public DateTime Datedmission { get; set; }
		public string? DatedmissionFarsi { get; set; }
		[MaxLength(50)]
		[Required(ErrorMessage ="لطفا یک کارشناس انتخاب کنید")]
		public string AdmissionsSpecialistUser_ { get; set; }
		public int? TypeOfAdmissionBaseTableId { get; set; }

		[AllowNull]
		public bool TypeOfAdmissionCredit { get; set; }
		[AllowNull]
		public bool TypeOfAdmissionIntroduction { get; set; }
		[AllowNull]
		public bool TypeOfAdmissionAdvertising { get; set; }
		[AllowNull]
		public bool TypeOfAdmissionInternet { get; set; }
		[MaxLength(50)]

		public string ApplicantRow { get; set; } = string.Empty;
		[MaxLength(50)]
		public string? ApplicantFormSerial { get; set; }
		public bool ApplicantTypeBuy { get; set; }
		public bool ApplicantTypeMortgageAndRent { get; set; }
		public bool ApplicantTypeMortgage { get; set; }
		public bool ApplicantTypePrebuy { get; set; }
		public bool ApplicantTypeParticipation { get; set; }

		public bool DemandBuildingApartment { get; set; }
		public bool DemandBuildinVila { get; set; }
		public bool DemandBuildingKalingi { get; set; }
		public bool DemandBuildingGround { get; set; }
		public bool DemandBuildingOffice { get; set; }
		public bool DemandBuildingGarden { get; set; }
		public bool DemandBuildingCommercial { get; set; }
		public bool DemandBuildingIndustrial { get; set; }
		public bool DemandBuildingStore { get; set; }
		public bool DemandBuildingAgriculture { get; set; }

		public int? RequiredSizeFrom { get; set; }
		public int? RequiredSizeTo { get; set; }
		public int? RequiredBedroomSizeFrom { get; set; }
		public int? RequiredBedroomSizeTo { get; set; }
		[MaxLength(300)]
		public string? DemandRange1 { get; set; }
		[MaxLength(300)]
		public string? DemandRange2 { get; set; }
		[MaxLength(300)]
		public string? DemandRange3 { get; set; }

		public bool BudgetSourceCash { get; set; } = false;
		public bool BudgetPreviousLessor { get; set; } = false;
		public bool BudgetFuturePayment { get; set; } = false;

		public bool BudgetSourceExchange { get; set; }
		public bool BudgetSourceLoan { get; set; }
		public bool BudgetSourceCar { get; set; }
		public bool BudgetSourceBuilding { get; set; }
		[MaxLength(300)]
		public string? BudgetSourceComment { get; set; }

		public ulong? BudgetBuyFrom { get; set; }
		public string? BudgetBuyFrom_s { get; set; }
		public ulong? BudgetBuyTo { get; set; }
		public string? BudgetBuyTo_s { get; set; }

		public ulong? BudgetRentFrom { get; set; }
		public string? BudgetRentFrom_s { get; set; }
		public ulong? BudgetRentTo { get; set; }
		public string? BudgetRentTo_s { get; set; }

		public ulong? BudgetMortgageFrom { get; set; }
		public string? BudgetMortgageFrom_s { get; set; }
		public ulong? BudgetMortgageTo { get; set; }
		public string? BudgetMortgageTo_s { get; set; }

		public DateTime? ArchiveDate { get; set; }
		public string? ArchiveDateFarsi { get; set; }
		[MaxLength(50)]
		public string?	 ArchiveUser_ { get; set; }
		public bool	 IsArchive { get; set; }
		public DateTime? ReturnArchiveDate { get; set; }
		public string? ReturnArchiveDateFarsi { get; set; }
		[MaxLength(50)]
		public string? ReturnArchiveUser_ { get; set; }
        public int? ArchiveReason_ { get; set; }
        public int? ReturnArchiveReason_ { get; set; }
		[Required(ErrorMessage = "لطفا پر شود")]
		public int? RealEstateConsultantFormId { get; set; }


		public bool IsDeleted { get; set; }
		public int? TypeOfResidenceBaseTableId { get; set; }
		public int? TypeOfResidenceNumber { get; set; }
		public DateTime? RelocationDeadline { get; set; }//مهلت جابجایی
		public string? RelocationDeadlineFarsi { get; set; }//مهلت جابجایی

	}

	//public class ValidUrlAttribute : Attribute, IModelValidator
	//{
	//	public string ErrorMessage { get; set; }

	//	public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
	//	{
	//		var url = context.Model as string;
	//		if (url != null && Uri.IsWellFormedUriString(url, UriKind.Absolute))
	//		{
	//			return Enumerable.Empty<ModelValidationResult>();
	//		}

	//		return new List<ModelValidationResult>
	//	{
	//		new ModelValidationResult(context.ModelMetadata.PropertyName, ErrorMessage)
	//	};
	//	}
	//}
}
