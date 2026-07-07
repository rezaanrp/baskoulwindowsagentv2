using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.Users
{
    public class UsersListDomainViewModel
    {
        [MaxLength(100)]
        public string Id { get; set; }
        [Required]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Family { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? PersonnelCode { get; set; }
        [MaxLength(50)]
        public string? Departments { get; set; }
        public string FullName => $"{Name} {Family}";
        // Active sites to display
        public List<WeighbridgeSiteDomainViewModel> ActiveSites { get; set; } = new();

        // Selected site IDs
        public List<int> SelectedSiteIds { get; set; } = new();
        public string? Company { get; set; }
        public string? Token { get; set; }
        public string? WindowsToken { get; set; }
        public string? Role { get; set; }
    }
}


