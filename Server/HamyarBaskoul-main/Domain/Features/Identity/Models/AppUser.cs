using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AppUser : IdentityUser
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Family { get; set; }
        [MaxLength(11)]
        [AllowNull]
        public string Mobile { get; set; }
        [MaxLength(10)]
        [AllowNull]
        public string PersonnelCode { get; set; }
        [MaxLength(100)]
        public string? Departments { get; set; }

        [DefaultValue("false")]
		public bool IsDelete { get; set; }

        public string? CodMarkaz { get; set; }

        public string? Token { get; set; } // this is for Hamyar APIs
        public string? WindowsToken { get; set; } // this is for receiving weight from windows

        public ICollection<WeighbridgeSiteUser> WeighbridgeSiteUsers { get; set; } = new List<WeighbridgeSiteUser>();

        public int? SelectedSiteId { get; set; }
    }
}

