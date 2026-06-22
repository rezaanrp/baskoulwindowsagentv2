using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.BankAccount
{
    public class BankAccountInfoViewModel
    {
        [Key] // مشخص کردن این فیلد به عنوان کلید اصلی
        public int Id { get; set; }

        [Required(ErrorMessage = "نام صاحب حساب الزامی است.")]
        [StringLength(100, ErrorMessage = "نام صاحب حساب نباید بیشتر از 100 کاراکتر باشد.")]
        public string AccountHolderName { get; set; }

        [Required(ErrorMessage = "نام بانک الزامی است.")]
        [StringLength(50, ErrorMessage = "نام بانک نباید بیشتر از 50 کاراکتر باشد.")]
        public string BankName { get; set; }

        [StringLength(50, ErrorMessage = "نام شعبه نباید بیشتر از 50 کاراکتر باشد.")]
        public string BranchName { get; set; }

        [Required(ErrorMessage = "شماره حساب الزامی است.")]
        [StringLength(20, ErrorMessage = "شماره حساب نباید بیشتر از 20 کاراکتر باشد.")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "شماره شبا الزامی است.")]
        [RegularExpression(@"^[A-Z]{2}\d{24}$", ErrorMessage = "فرمت شماره شبا معتبر نیست.")]
        public string IBAN { get; set; }

        [StringLength(16, MinimumLength = 16, ErrorMessage = "شماره کارت باید 16 رقمی باشد.")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "شماره کارت باید فقط شامل اعداد باشد.")]
        public string CardNumber { get; set; }
        [StringLength(10, ErrorMessage = "کد ملی باید 10 کاراکتر باشد.")]

        [RegularExpression(@"^\d{10}$", ErrorMessage = "کد ملی باید 10 رقمی باشد.")]
        public string? NationalCode { get; set; }
        [StringLength(11, ErrorMessage = "شماره تلفن باید 11 کاراکتر باشد.")]
        public string? PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "ایمیل معتبر نیست.")]
        public string? Email { get; set; }

		public bool IsActive { get; set; } = true;

		public DateTime CreatedDate { get; set; } // تاریخ ایجاد
        public DateTime ModifiedDate { get; set; } // تاریخ تغییر

        [MaxLength(50)]
        public string? CreatedBy { get; set; } // نام کاربر ایجادکننده، حداکثر طول: 50 کاراکتر
        public bool IsDeleted { get; set; } = false;
    }
}

