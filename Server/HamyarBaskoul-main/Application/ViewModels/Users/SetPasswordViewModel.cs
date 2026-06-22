using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Users
{
	public class SetPasswordViewModel
    {
        public string Id { get; set; }
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
    }
}

