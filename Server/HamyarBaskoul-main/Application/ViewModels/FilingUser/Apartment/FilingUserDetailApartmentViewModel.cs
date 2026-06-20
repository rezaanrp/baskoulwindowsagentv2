using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.FilingUser.Apartment
{
	public class FilingUserDetailApartmentViewModel
	{
        public int  Id { get; set; }

        public float?  UnitSize { get; set; }
        public bool UnitPositionSouth { get; set; }
        public bool UnitPositionNorthern { get; set; }
        public bool UnitPositionEastern { get; set; }
        public bool UnitPositionWestern { get; set; }
        public int?  FloorNumber_ { get; set; }
        public int?  NumberUnitsFloor { get; set; }
        public int?  TotalNumberUnits { get; set; }
        public int?  TotalNumberFloors { get; set; }
        public int?  NumberSleeps { get; set; }
        public int?  NumberClosets { get; set; }
        public int?  NumberMasterSleeps { get; set; }
        public int?  NumberCloset { get; set; }
        public bool WallPlugsOilColored { get; set; }
        public bool WallPlugsWallpaper { get; set; }
        public bool WallPlugsPlasticalColor { get; set; }
        public bool WallPlugsKnockingWall { get; set; }
        public bool WallPlugsWoodworking { get; set; }
        public bool WallPlugsBelka { get; set; }
        public bool WallPlugsWithoutCovers { get; set; }


        public int?  NumberTerraces { get; set; }
        public bool Barbecue { get; set; }
        public bool FlooringCeramic { get; set; }
        public bool FlooringStone { get; set; }
        public bool FlooringParquet { get; set; }
        public bool FlooringCovering { get; set; }
        public bool Flooringcarpet { get; set; }
        public bool FlooringMosaic { get; set; }
        public bool Warehouse { get; set; }
        public float?  WarehouseMeterage { get; set; }
        public int?  WarehouseLocation_ { get; set; }
        public int?  Parking_ { get; set; }
        public int?  ParkingMeterage { get; set; }
        public bool ParkingRemoteControl { get; set; }

        public int?  KitchenCabinets_ { get; set; }
        public int?  KitchenCabinetPlate_ { get; set; }
        public bool KitchenIsland { get; set; }
        public bool KitchenSecond { get; set; }
        public bool KitchenElectricHeater { get; set; }
        public bool KitchenAvan { get; set; }
        public bool KitchenMicrowave { get; set; }

        public bool KitchenDesktopOven { get; set; }
        public int?  KitchenDesktopOvenNumberFlames { get; set; }
        public int?  KitchenDesktopOvenNumberFlamesType_ { get; set; }

        public bool KitchenHood { get; set; }
        public int?  KitchenHoodNumber { get; set; }
        public int?  KitchenHoodType_ { get; set; }

        public bool HeatingSystemPackage { get; set; }
        public bool HeatingSystemRadiator { get; set; }
        public bool HeatingSystemHeating { get; set; }

        public bool HeatingSystemWaterHeater { get; set; }
        public bool HeatingSystemUnderFloor { get; set; }
        public bool HeatingSystemChiller { get; set; }
        public bool HeatingSystemAirConditioner { get; set; }
        public bool HeatingSystemHeater { get; set; }
        public bool HeatingSystemSpilt { get; set; }
        public bool HeatingSystemFireplace { get; set; }
        public bool HeatingSystemSplitDuct { get; set; }

        public bool CoolingSystemWaterCooler { get; set; }
        public bool CoolingSystemSpilt { get; set; }
        public bool CoolingSystemSplitDuct { get; set; }
        public bool CoolingSystemChiller { get; set; }
        public bool CoolingSystemAirConditioner { get; set; }

        public bool FaucetLever { get; set; }

        public bool FaucetBuiltIn { get; set; }
        public bool FaucetSimple { get; set; }
        public bool FaucetElectric { get; set; }
        public bool IranianToilet { get; set; }
        public bool Toilet { get; set; }
        public int?  ToiletNumber { get; set; }

        public bool ToiletMaster { get; set; } //حذف
        public int?  ToiletMasterNumber { get; set; }//حذف
        public bool Jacuzzi { get; set; }

        public bool BranchesWater { get; set; }
        public int?  BranchesWaterNumber_ { get; set; }
        public bool BranchesElectricity { get; set; }
        public int?  BranchesElectricityNumber_ { get; set; }
        public bool BranchesGas { get; set; }
        public int?  BranchesGasNumber_ { get; set; }
        public bool BranchesPhone { get; set; }
        public int?  BranchesPhoneNumber { get; set; }
        public bool Kenaf { get; set; }
        public bool Lighting { get; set; }
        public bool LightLine { get; set; }
        public bool Laundry { get; set; }
        public bool Patio { get; set; }
        public bool SecurityDoor { get; set; }
        public bool DigitalEntryLock { get; set; }
        public bool Trass { get; set; } = false;//spring_sleep
		public bool Curtain { get; set; }
        public int?  CurtainType_ { get; set; }
        public bool DoubleGlazedWindow { get; set; }
        public int?  DoubleGlazedWindowType_ { get; set; }

		public bool private_yard { get; set; } = false;
		public int? private_yard_size { get; set; }
	//	public bool spring_sleep { get; set; } = false;
		public int? spring_sleep_size { get; set; }
		public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        public Guid Supplier_ { get; set; }


		public int?  FilingUserDetailId { get; set; }


	}
}
