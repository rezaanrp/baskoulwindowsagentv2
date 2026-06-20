using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.FilingUser.RebuildingAbility
{
    public class FilingUserDetailRebuildAbilityViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string Family { get; set; }
        public string FullName { get { return $"{Name} {Family}"; } }

        public string Mobile { get; set; }

        public string AdmissionsSpecialistUser { get; set; }


        public string? Tel { get; set; }
        public int PercentageOwnership { get; set; } = 100;

        public DateTime Datedmission { get; set; }

        public string? DatedmissionFarsi { get; set; }

        public string? FileIndicatorId { get; set; }
        public string? SerialFilingForm { get; set; }//حذف

        public int? BaseTableTypeOfAdmission_ { get; set; }
        public string? BaseTableTypeOfAdmission { get; set; }

        public int? TypeOfAdmissionInternetResource_ { get; set; }


        public int TransactionType_ { get; set; }


        public int BuildingType_ { get; set; }
        public string? TypeOfUse {  get; set; }
        public bool TypeOfUseResidential { get; set; }
        public bool TypeOfUseCommercial { get; set; }
        public bool TypeOfUseOffice { get; set; }
        public bool TypeOfUseAgriculture { get; set; }
        public bool TypeOfUseIndustrial { get; set; }
        public bool TypeOfUseSport { get; set; }
        public bool TypeOfUseCultural { get; set; }

        public int? Country_ { get; set; }
        public int? State_ { get; set; }

        public string MainStreet { get; set; }
        public string? SubStreet { get; set; }
        public string? Alley { get; set; }
        public string? Address { get; set; }
        public string? AddressNumber { get; set; }

        public int? DocumentStatus_ { get; set; }
        public DateTime DocumentStatusDate { get; set; }
        public string? DocumentStatusDateFarsi { get; set; }

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
        [MaxLength(100)]
        public string? AttributesSmartSystemType { get; set; }




        public int? FileStatusResidence_ { get; set; }
        public int? FileStatusMortgageAmount { get; set; }
        public int? FileStatusRentAmount { get; set; }
        public DateTime FileStatusDischargeDate { get; set; }
        public string? FileStatusDischargeDateFarsi { get; set; }
        public DateTime FileStatusUnderConstructionDeliveryDate { get; set; }
        public string? FileStatusUnderConstructionDeliveryDateFarsi { get; set; }
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

        [MaxLength(16, ErrorMessage = "خطا")]
        public string? PriceInformationTotalPrice_s { get; set; }
        [MaxLength(16, ErrorMessage = "خطا")]
        public string? PriceInformationPricePerMeter_s { get; set; }
        [MaxLength(16, ErrorMessage = "خطا")]
        public string? PriceInformationLoanAmount_s { get; set; }
        [MaxLength(16, ErrorMessage = "خطا")]
        public string? PriceInformationNumberInstallments_s { get; set; }
        [MaxLength(16, ErrorMessage = "خطا")]
        public string? PriceInformationamountInstallments_s { get; set; }
        [MaxLength(16, ErrorMessage = "خطا")]
        public string? PriceInformationMortgage_s { get; set; }
        [MaxLength(16, ErrorMessage = "خطا")]
        public string? PriceInformationRent_s { get; set; }
        [MaxLength(16, ErrorMessage = "خطا")]
        public string? PriceInformationMortgageConversionFrom_s { get; set; }
        [MaxLength(16, ErrorMessage = "خطا")]
        public string? PriceInformationMortgageConversionTo_s { get; set; }
        [MaxLength(16, ErrorMessage = "خطا")]
        public string? PriceInformationRentFrom_s { get; set; }
        [MaxLength(16, ErrorMessage = "خطا")]
        public string? PriceInformationRentTo_s { get; set; }

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


        public bool IsDeleted { get; set; }



        public string? ImageUrl1 { get; set; }
        public string? ImageUrl2 { get; set; }
        public string? ImageUrl3 { get; set; }
        public string? ImageUrl4 { get; set; }

        public string? FilingUserDetailSupplier_ { get; set; }
        [Required(ErrorMessage = "لطفا سریال فرم فایلینگ را انتخاب کنید")]
        public int? RealEstateConsultantFormId { get; set; }
        public int? RebuildabilityBaseTableId { get; set; }//قابلیت بازسازی

    }
}
