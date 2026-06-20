using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.FilingUser.Commercial
{
	public class FilingUserDetailCommercialViewModel
    {
        public int  Id { get; set; }

        public float?   Meterage { get; set; }
        public int?  Floor { get; set; }

        public bool LocationSouth { get; set; }
        public bool LocationNorthern { get; set; }
        public bool LocationWestern { get; set; }
        public bool LocationEastern { get; set; }
        public int?  CeilingHeight { get; set; }

        public bool RoofTopFree { get; set; }
        public bool RoofTopResidential { get; set; }
        public bool RoofTopCommercial { get; set; }
        public bool RoofTopOfficial { get; set; }
        public bool Underground { get; set; }
        public float?   UndergroundMeterage { get; set; }
        public bool Parking { get; set; }
        public int?  ParkingMeterage { get; set; }
        public bool Warehouse { get; set; }
        public float?   WarehouseMeterage { get; set; }
        public bool Balcony { get; set; }
        public float?   BalconyMeterage { get; set; }
        public bool Step { get; set; }
        public int?  StepNumber { get; set; }



        public bool ElectricDoor { get; set; }
        public bool BurglarAlarm { get; set; }
        public bool CCTV { get; set; }
        public bool InternalLiftElevator { get; set; }
        public bool Toilet { get; set; }



        public bool WallPlugsCeramic { get; set; }
        public bool WallPlugsStone { get; set; }
		public bool WallPlugsOilColored { get; set; }
		public bool WallPlugsWallpaper { get; set; }
        public bool WallPlugsPlasticalColor { get; set; }
        public bool WallPlugsKnockingWall { get; set; }
        public bool WallPlugsWoodworking { get; set; }
        public bool WallPlugsWithoutCovers { get; set; }


        public bool FlooringCeramic { get; set; }
        public bool FlooringStone { get; set; }
        public bool FlooringParquet { get; set; }
        public bool FlooringCovering { get; set; }
        public bool Flooringcarpet { get; set; }
        public bool FlooringMosaic { get; set; }

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

        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        public Guid Supplier_ { get; set; }
		public int?  FilingUserDetailId { get; set; }


	}
}
