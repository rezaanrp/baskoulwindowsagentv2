using System.ComponentModel.DataAnnotations;

namespace WebUI.Features.Identity.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "نام کاربری را وارد نمایید")]
        [Display(Name = "نام کاربری")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "رمز عبور را وارد نمایید")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }
}
