using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.FilingUser.Villa
{
	public class FilingUserDetailVillaViewModel
    {
        public int   Id { get; set; }
        public bool LocationSouth { get; set; }
        public bool LocationNorthern { get; set; }
        public bool LocationWestern { get; set; }
        public bool LocationEastern { get; set; }


        public float?  TotalLandArea { get; set; }
        public int?   NumberFloors { get; set; }

        public int?   LandDimensionsNorth { get; set; }
        public int?   LandDimensionsSouth { get; set; }
        public int?   LandDimensionsEast { get; set; }
        public int?   LandDimensionsWest { get; set; }
        public int?   NumberUnits { get; set; }
        public float?  TotalAreaBuilding { get; set; }
        public int?   PermissibleCeilingHeight { get; set; }
        public int?   PermissibleCeilingM { get; set; }
        public bool Onstructionlicense { get; set; }
        public int?   AltitudeCodeArea { get; set; }
        public int?   DensityLicenseIssuance { get; set; }
        public int?   YearLicenseIssuance { get; set; }


        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        public Guid Supplier_ { get; set; }
		public int? FilingUserDetailId { get; set; }

	}
}
