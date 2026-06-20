using Domain.Models.BuilderProfile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DesignExecution
{
    public class DesignExecutionEmployer
	{
		[Key]
		public int Id { get; set; }
		[MaxLength(100)]
		public string Name { get; set; }
		[MaxLength(100)]
		public string Family { get; set; }
		[MaxLength(11)]
		public string Mobile { get; set; }
        [MaxLength(200)]
        public string? Address { get; set; }
        [MaxLength(100)]
		public string ProjectAddress { get; set; }
		[MaxLength(11)]
		public string? Tel { get; set; }
		public DateTime Datedmission { get; set; }
		[MaxLength(100)]
		public string AdmissionsSpecialistUserId { get; set; }

		public int? TypeOfAdmissionBaseTableId { get; set; }
        [MaxLength(100)]
        public string? IntroducingPerson { get; set; }
        public BaseTable? TypeOfAdmissionBaseTable { get; set; }

		public bool TypeOfProjectFacadeDesign { get; set; } = false;
		public bool TypeOfProjectInteriorDesign { get; set; } = false;
		public bool TypeOfProjectPlanDesign { get; set; } = false;
		public bool TypeOfProjectAreaDesign { get; set; } = false;
		/// <summary>
		/// 199 نوسازی
		/// 200 بازسازی
		/// </summary>
		public int? TypeOfProjectEmployerBaseTableId { get; set; }

		public DateTime? VisitDate { get; set; }

		[MaxLength(500)]
		public string? VisitExistingSituation { get; set; }
		[MaxLength(50)]
		public string? VisitAdmissionsSpecialistUserId { get; set; }//کارشناس یازدید
		public bool VisitWasItMeasured { get; set; } = false;
		public bool VisitWasItPhotoTaken { get; set; } = false;
		public bool VisitWasItVideoTaken { get; set; } = false;
		[MaxLength(500)]
		public string? VisitEmployerComments { get; set; }

        [MaxLength(100)]
        public string? VisitImageUrl1 { get; set; }

        [MaxLength(100)]
        public string? VisitImageUrl2 { get; set; }

        [MaxLength(100)]
        public string? VisitImageUrl3 { get; set; }

        [MaxLength(100)]
        public string? VisitImageUrl4 { get; set; }

        [MaxLength(100)]
        public string? VisitImageUrl5 { get; set; }

        //--------------------------------------------Request

        public DateTime? BirthDate { get; set; }

        public DateTime? RequestDate { get; set; }


        [MaxLength(100)]
        public string? RequestFatherName { get; set; }

        public int? RequestChildNumber { get; set; }
        [MaxLength(50)]

        public string? RequestMunicipalArea { get; set; } //منطقه شهرداری
        

        // اطلاعات شخصی و ملک
        [MaxLength(100)]
        public string? RequestFavoriteColor { get; set; }

        [MaxLength(20)]
        public string? RequestLandlinePhone { get; set; }

        [MaxLength(20)]
        public string? RequestMobilePhone { get; set; }

        [MaxLength(250)]
        public string? RequestAddress { get; set; }

        public int? RequestRenovationReasonsBaseTableId { get; set; }
        [MaxLength(250)]
        public string? RequestRenovationReasonsOthers { get; set; }

        [MaxLength(250)]
        public string? RequestPropertyAddress { get; set; }

        public int? RequestPropertyUsageBaseTableId { get; set; }  // مسکونی، تجاری، صنعتی، باغ، ویلا، اداری، آموزشی، سایر
        [MaxLength(250)]
        public string? RequestPropertyUsageOthers { get; set; }  // مسکونی، تجاری، صنعتی، باغ، ویلا، اداری، آموزشی، سایر

        public int? RequestPropertyPositionBaseTableId { get; set; }  // یک نما، دونما، سه نما

        // ابعاد و مساحت
        public int? RequestPropertyLength { get; set; } // طول ملک (متر)
        public int? RequestPropertyWidth { get; set; }  // عرض ملک (متر)
        public int? RequestPropertyArea { get; set; }   // مساحت ملک (متر مربع)
        public int? RequestYearOfManufacture { get; set; }   //  سال ساخت

        [MaxLength(250)]
        public string? RequestBuildingSpecificationsDescription { get; set; }

        public int? RequestRequiredBedrooms { get; set; }
        public int? RequestRequiredBathrooms { get; set; }
        public int? RequestRequiredToilets { get; set; }

        public int? RequestRequiredStorageSpace { get; set; } // متر
        public int? RequestRequiredTerraceSpace { get; set; }
        public int? RequestRequiredLivingRoomSize { get; set; }
        public int? RequestRequiredKitchenSize { get; set; }
        /// <summary>
        // Laundry
        // Cloakroom
        //  Second kitchen
        /// </summary>
        public bool RequestSecondarySpacesRequiredLaundry { get; set; } = false; // لندری، اتاق لباس، آشپزخانه دوم
        public bool RequestSecondarySpacesRequiredCloakroom { get; set; } = false;  // لندری، اتاق لباس، آشپزخانه دوم
        public bool RequestSecondarySpacesRequiredSecondKitchen { get; set; } = false;  // لندری، اتاق لباس، آشپزخانه دوم
        [MaxLength(100)]

        public string? RequestSecondarySpacesRequiredOthers { get; set; }   // سایر

        // تاسیسات
        public int? RequestElectricalSystemBaseTableId { get; set; }  // معمولی، نیمه هوشمند، تمام هوشمند
        public int? RequestHeatingSystemBaseTableId { get; set; }  // گرمایش از کف، رادیاتور، داکت اسپلیت، چیلر، VRF
        public int? RequestCoolingSystemBaseTableId { get; set; }  // کولر آبی، اسپیلت، داکت اسپیلت، چیلر، VRF
        public int? RequestFireAlarmSystemBaseTableId { get; set; }  // اعلام حریق، اطفا حریق
        public bool? RequestHasCamera { get; set; }  // دارد یا ندارد

        [MaxLength(250)]
        public string? RequestFacilitiesDescription { get; set; } // توضیحات
        // تزئینات و دکوراسیون


        [MaxLength(100)]
        public string? RequestEntranceCeilingMaterial { get; set; }

        [MaxLength(100)]
        public string? RequestEntranceFloorMaterial { get; set; }

        [MaxLength(100)]
        public string? RequestEntranceWallMaterial { get; set; }

        [MaxLength(100)]
        public string? RequestEntranceLighting { get; set; }

        public bool? RequestEntranceHasPlantSpace { get; set; }
        [MaxLength(250)]
        public string? RequestEntranceDescription { get; set; }  // توضیحات
        // سالن
        [MaxLength(100)]
        public string? RequestLivingRoomCeilingMaterial { get; set; }

        [MaxLength(100)]
        public string? RequestLivingRoomFloorMaterial { get; set; }

        [MaxLength(100)]
        public string? RequestLivingRoomWallMaterial { get; set; }

        [MaxLength(100)]
        public string? RequestLivingRoomLighting { get; set; }

        public bool? RequestLivingRoomHasTVWall { get; set; }
        public bool? RequestLivingRoomHasShowcase { get; set; }
        [MaxLength(250)]
        public string? RequestLivingRoomDescription { get; set; }  // توضیحات
        // اتاق ها
        [MaxLength(100)]
        public string? RequestBedroomCeilingMaterial { get; set; }

        [MaxLength(100)]
        public string? RequestBedroomFloorMaterial { get; set; }

        [MaxLength(100)]
        public string? RequestBedroomWallMaterial { get; set; }

        [MaxLength(100)]
        public string? RequestBedroomLighting { get; set; }

        public bool? RequestBedroomHasWardrobe { get; set; }
        public bool? RequestBedroomHasIroningBoard { get; set; }
        public bool? RequestBedroomHasLibrary { get; set; }
        public bool? RequestHasShowcaseDesk { get; set; }
        public bool? RequestHasShowcaseDressingTable { get; set; }
        [MaxLength(250)]
        public string? RequestBedroomDescription { get; set; }  // توضیحات
        // حمام و سرویس
        [MaxLength(100)]
        public string? RequestBathroomCeilingMaterial { get; set; }

        [MaxLength(100)]
        public string? RequestBathroomFloorMaterial { get; set; }

        [MaxLength(100)]
        public string? RequestBathroomWallMaterial { get; set; }

        [MaxLength(100)]
        public string? RequestBathroomLighting { get; set; }

        public int? RequestMasterDoorMaterialBaseTableId { get; set; }  // شیشه، فلز، چوب
        [MaxLength(250)]
        public string? RequestBathroomDescription { get; set; }  // توضیحات
        // آشپزخانه
        [MaxLength(100)]
        public string? RequestKitchenCeilingMaterial { get; set; }

        [MaxLength(100)]
        public string? RequestKitchenFloorMaterial { get; set; }

        [MaxLength(100)]
        public string? RequestKitchenWallMaterial { get; set; }

        [MaxLength(100)]
        public string? RequestKitchenLighting { get; set; }

        [MaxLength(100)]
        public string? RequestBetweenCabinetsMaterial { get; set; }

        [MaxLength(100)]
        public string? RequestCabinetType { get; set; }

        [MaxLength(50)]
        public string? RequestHandleType { get; set; }

        public double? RequestCabinetHeight { get; set; }

        [MaxLength(50)]
        public string? RequestCountertopMaterial { get; set; }

        public bool RequestHasOvenSpace { get; set; } = false;
        public bool RequestHasMicrowaveSpace { get; set; } = false;
        public bool RequestHasGasCooktopSpace { get; set; } = false;
        public bool RequestHasBuiltInGasCooktop { get; set; } = false;
        [MaxLength(100)]
        public string? RequestSuperCabinet { get; set; }
        [MaxLength(100)]
        public string? RequestStorageCabinet { get; set; }
        [MaxLength(100)]
        public string? RequestHasTrashBinSpace { get; set; }
        [MaxLength(250)]
        public string? RequestKitchenDescription { get; set; }  // توضیحات
        // تراس
        [MaxLength(100)]
        public string? RequestTerraceCeilingMaterial { get; set; }

        [MaxLength(100)]
        public string? RequestTerraceFloorMaterial { get; set; }

        [MaxLength(100)]
        public string? RequestTerraceWallMaterial { get; set; }

        [MaxLength(100)]
        public string? RequestTerraceLighting { get; set; }

        public int? RequestBarbecueBaseTableId { get; set; }  // ذغالی، گازی
        public bool? RequestHasPlantSpaceOnTerrace { get; set; }
        [MaxLength(250)]
        public string? RequestTerraceDescription { get; set; }  // توضیحات
        public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
		[MaxLength(100)]
		public string? UserId { get; set; }
		public bool IsDeleted { get; set; } = false;
		public ICollection<DesignExecutionEmployerVisit>? DesignExecutionEmployerVisits { get; set; }
		public ICollection<DesignExecutionEmployerFollowUp>? DesignExecutionEmployerFollowUps { get; set; }


	}
}
