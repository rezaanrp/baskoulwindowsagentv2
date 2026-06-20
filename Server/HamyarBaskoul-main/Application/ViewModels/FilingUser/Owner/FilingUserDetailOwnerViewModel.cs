using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.FilingUser.Owner
{
    public class FilingUserDetailOwnerViewModel
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage ="وارد کردن نام برای ماکین دیگر اجباری می باشد")]
        public string Name { get; set; }
        [MaxLength(100)]
		[Required(ErrorMessage = "وارد کردن نام خانوادگی برای ماکین دیگر اجباری می باشد")]

		public string Family { get; set; }
        [MaxLength(11)]
		[Required(ErrorMessage = "وارد کردن شماره همراه برای ماکین دیگر اجباری می باشد")]

		public string Mobile { get; set; }
        [MaxLength(11)]
        public string? Tel { get; set; }
        public int PercentageOwnership { get; set; }
        public int? FilingUserDetails_ { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? CreatedDate { get; set; }
        public string? Supplier_ { get; set; }
    }
}
