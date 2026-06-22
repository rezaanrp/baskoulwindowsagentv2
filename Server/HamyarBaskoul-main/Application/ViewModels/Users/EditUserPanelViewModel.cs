using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Application.ViewModels.Users
{
	public class EditUserPanelViewModel
	{
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Family { get; set; }
        [MaxLength(11)]
        [AllowNull]
        public string Mobile { get; set; }

    }
}

