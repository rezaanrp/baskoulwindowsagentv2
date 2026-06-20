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
	public class FilingUserDetailByBuildingSearchViewModel
	{

		public List<BaseTableViewModel>? TransactionType { get; set; }
		public List<BaseTableViewModel>? BuildingType { get; set; }
		public List<AppUser>?  appUsers { get; set; }
        public FilingUserDetailViewModel? FilingUserDetails { get; set; }
	}
}
