using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Domain.ViewModels.FilingUserDetail
{
    public class CreateFilingUserDetailViewModels
	{
		public int Id { get; set; }
        [Required]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
        [MaxLength(100)]
		public string? Name { get; set; }
		[MaxLength(100)]
		public string Family { get; set; }
		[MaxLength(11)]
		public string Mobile { get; set; }
		[MaxLength(11)]
		public string? Tel { get; set; }
        public int? PercentageOwnership { get; set; }

        public DateTime Datedmission { get; set; }
        [MaxLength(10)]

        public string? FileIndicatorId { get; set; }
        [MaxLength(40)]

        public string AdmissionsSpecialistUser_ { get; set; }
        [MaxLength(10)]

        public string? SerialFilingForm { get; set; }//حذف

		public int? BaseTableTypeOfAdmission_ { get; set; }

		[MaxLength(100)]
		public int? TypeOfAdmissionInternetResource_ { get; set; }


		public bool TransactionTypeSale { get; set; }
		public bool TransactionTypeRent { get; set; }
		public bool TransactionTypePresell { get; set; }
		public bool TransactionTypeParticipation { get; set; }
		public bool TransactionTypeExchange { get; set; }


		public bool BuildingTypeApartment { get; set; }
		public bool BuildingTypeVilla { get; set; }
		public bool BuildingTypeKalingi { get; set; }
		public bool BuildingTypeGround { get; set; }
		public bool BuildingTypeShop { get; set; }
		public bool BuildingTypeOffice { get; set; }
		public bool BuildingTypeGarden { get; set; }
		public bool BuildingTypeIndustrial { get; set; }
		public bool BuildingTypeSuite { get; set; }
		public bool TypeOfUseResidential { get; set; }
		public bool TypeOfUseCommercial { get; set; }
		public bool TypeOfUseOffice { get; set; }
		public bool TypeOfUseAgriculture { get; set; }
		public bool TypeOfUseIndustrial { get; set; }
		public bool TypeOfUseSport { get; set; }
		public bool TypeOfUseCultural { get; set; }

		public int? Country_ { get; set; }
		public int? State_ { get; set; }
		[MaxLength(50)]

        public string MainStreet { get; set; }
        [MaxLength(50)]

        public string? SubStreet { get; set; }
        [MaxLength(50)]

        public string? Alley { get; set; }
        [MaxLength(100)]

        public string? Address { get; set; }
        [MaxLength(100)]

        public string? AddressNumber { get; set; }

		public int? DocumentStatus_ { get; set; }
		public DateTime DocumentStatusDate { get; set; }

		public int? ConstructionPeriodYear { get; set; }

		public bool BuildingLocationSouth { get; set; }

		public bool BuildingLocationNorth { get; set; }
		public bool BuildingLocationEast { get; set; }
		public bool BuildingLocationWest { get; set; }


		public bool BuildingFacadesRock { get; set; }
		public bool BuildingFacadesReward { get; set; }
		public bool BuildingFacadesWood { get; set; }
		public bool BuildingFacadesComposite { get; set; }
		public bool BuildingFacadesCeramic { get; set; }
		public bool BuildingFacadesThreeCmBrick { get; set; }

		public bool BuildingFacadesGlass { get; set; }
		public bool BuildingFacadesAluminium { get; set; }
		public bool BuildingFacadesWithoutFacade { get; set; }
		public bool BuildingFacadesLighting { get; set; }

		public int? ElevatorCapacity { get; set; }
		public bool ElevatorPeopleCarrier { get; set; }
		public bool ElevatorCoolie { get; set; }


		public bool AttributesLobby { get; set; }
		public bool AttributesSauna { get; set; }
		public bool AttributesSwimmingpool { get; set; }
		public bool AttributesConferenceHall { get; set; }
		public bool AttributesSportsHall { get; set; }
		public bool AttributesJanitor { get; set; }
		public bool AttributesRoofGarden { get; set; }
		public bool AttributesBarbecue { get; set; }
		public bool AttributesFountain { get; set; }
		public bool Attributes3PhaseElectricity { get; set; }
		public bool AttributesWaterPump { get; set; }
		public bool AttributesFireAlarm { get; set; }
		public bool AttributesFireExtinguishing { get; set; }
		public bool AttributesShootingWaste { get; set; }
		public bool AttributesCentralAntenna { get; set; }
		public bool AttributesIphone { get; set; }
		public int? AttributesIPhoneType_ { get; set; }
		public bool AttributesSmartSystem { get; set; }
		public string? AttributesSmartSystemType { get; set; }
		public int? TransactionType_ { get; set; }


		public int? BuildingType_ { get; set; }

		public bool IsDeleted { get; set; }
		public int? FilingUser_ { get; set; }
		public string? ImageUrl1 { get; set; }
		public string? ImageUrl2 { get; set; }
		public string? ImageUrl3 { get; set; }
		public string? ImageUrl4 { get; set; }
        public string? ImageUrl5 { get; set; }
        public string? ImageUrl6 { get; set; }
        public string? ImageUrl7 { get; set; }
        public string? FilingUserDetailSupplier_ { get; set; }



		public int? FileStatusResidence_ { get; set; }
		public int? FileStatusMortgageAmount { get; set; }
		public int? FileStatusRentAmount { get; set; }
		public DateTime FileStatusDischargeDate { get; set; }
		public DateTime FileStatusUnderConstructionDeliveryDate { get; set; }
		public int? FileStatusCoordinateTheVisit_ { get; set; }
		[MaxLength(11)]
		public string? FileStatusCoordinateTheVisitPhone { get; set; }
		public bool FileStatusKeyAvailable { get; set; }
		public bool FileStatusOwnerInThePlace { get; set; }

        public ulong? PriceInformationTotalPrice { get; set; }
        public ulong? PriceInformationPricePerMeter { get; set; }
        public bool PriceInformationLoan { get; set; }
        public ulong? PriceInformationLoanAmount { get; set; }
        public ulong? PriceInformationNumberInstallments { get; set; }
        public ulong? PriceInformationamountInstallments { get; set; }
        public ulong? PriceInformationMortgage { get; set; }
        public ulong? PriceInformationRent { get; set; }
        public ulong? PriceInformationMortgageConversionFrom { get; set; }
        public ulong? PriceInformationMortgageConversionTo { get; set; }
        public ulong? PriceInformationRentFrom { get; set; }
        public ulong? PriceInformationRentTo { get; set; }
        public int? RealEstateConsultantFormId { get; set; }





    }
}
