using Application.ViewModels.BaseTable;
using Application.ViewModels.FilingUser.Apartment;
using Application.ViewModels.FilingUser.Commercial;
using Application.ViewModels.FilingUser.Owner;
using Application.ViewModels.FilingUser.Villa;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.FilingUser
{
	public class FilingUserDetailByBuildingViewModel
	{


		public List<BaseTableViewModel>? TransactionType { get; set; }
		public List<BaseTableViewModel>? BuildingType { get; set; }
		public List<AppUser>?  appUsers { get; set; }
		public List<BaseTableViewModel>? BaseTableTypeOfAdmission { get; set; }
		public List<BaseTableViewModel>? FileStatusResidence { get; set; }
		public List<BaseTableViewModel>? FileStatusCoordinateTheVisit { get; set; }
		public List<BaseTableViewModel>? DocumentStatus { get; set; }

        public List<BaseTableViewModel>? AttributesIPhoneType { get; set; }
        public List<BaseTableViewModel>? KitchenCabinets { get; set; }
        public List<BaseTableViewModel>? KitchenCabinetPlate { get; set; }

        public List<BaseTableViewModel>? CurtainType { get; set; }
		public List<BaseTableViewModel>? DoubleGlazedWindowType { get; set; }

		public List<BaseTableViewModel>? ArchiveReason { get; set; }
		public List<BaseTableViewModel>? ReturnArchiveReason { get; set; }

        public List<BaseTableViewModel>? WarehouseLocation { get; set; }
        public List<BaseTableViewModel>? KitchenDesktopOvenNumberFlamesType { get; set; }
        public List<BaseTableViewModel>? KitchenHoodType { get; set; }
        public List<BaseTableViewModel>? TypeOfAdmissionInternetResource { get; set; }
        public List<BaseTableViewModel>? FloorNumber { get; set; }

		public List<BaseTableViewModel>? BranchesWaterNumber { get; set; }
		public List<BaseTableViewModel>? BranchesElectricityNumber { get; set; }
		public List<BaseTableViewModel>? BranchesGasNumber { get; set; }
		public List<BaseTableViewModel>? BranchesPhoneNumber { get; set; }
		public List<BaseTableViewModel>? Parking { get; set; }



        public FilingUserDetailViewModel? FilingUserDetails { get; set; }
		public FilingUserDetailApartmentViewModel? FilingUserDetailsApartment { get; set; }
		public FilingUserDetailCommercialViewModel? FilingUserDetailsCommercial { get; set; }
		public FilingUserDetailVillaViewModel? FilingUserDetailsVilla { get; set; }
		public List<FilingUserDetailOwnerViewModel>?  filingUserDetailOwners { get; set; }

	}
}
