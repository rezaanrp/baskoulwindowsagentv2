using DocumentFormat.OpenXml.Spreadsheet;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Application.ViewModels.Users
{
	public class InsertUsersListViewModel
	{
        [EmailAddress(ErrorMessage = "ایمیل وارد شده صحیح نمی باشد")]
        public string? Email { get; set; }
        [Required]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "لطفا رمز  خود را وارد کنید")]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$", 
			ErrorMessage = "رمز جدید باید حداقل 8 رقم، و دارای حداقل یک حرف، یک عدد و یک کاراکتر ویژه باشد")]
		[DataType(DataType.Password)]
		[Display(Name = "رمز عبور ")]
		public string Password { get; set; }

		[Required(ErrorMessage = "لطفا تکرار رمز را وارد کنید")]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "تکرار رمز جدید باید حداقل 8 رقم، و دارای حداقل یک حرف، یک عدد و یک کاراکتر ویژه باشد")]
		[DataType(DataType.Password)]
		[Display(Name = "تکرار رمز عبور ")]
		[Compare("Password", ErrorMessage = "رمز  و تکرار آن یکسان نیستند")]
		public string RepeatePassword { get; set; }
		[MaxLength(100)]
		public string Name { get; set; }
		[MaxLength(100)]
		public string Family { get; set; }
		[StringLength(11,ErrorMessage ="شماره موبایل صحیح نمی باشد")]
		[AllowNull]
		public string Mobile { get; set; }
        [AllowNull]
        [MaxLength(10, ErrorMessage = "طول کد پرسنلی زیاد است")]
        public string? PersonnelCode { get; set; }
        public string? Role { get; set; }
        public string? Token { get; set; }
        public string? WindowsToken { get; set; }

        public string? UserRole { get; set; }
        public List<int> SelectedSiteIds { get; set; } = new List<int>();
		//public List<CodeMarkazViewModel> AllMarkazes { get; set; } = new List<CodeMarkazViewModel>();
		public List<SiteViewModel> AvailableSites { get; set; } = new List<SiteViewModel>();
    }
}
