using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.BuilderProfile
{
    public class BuilderProfileFollowUpResult
    {
		[Key]
        public int Id { get; set; } // شناسه منحصر به فرد برای هر نتیجه پیگیری
		[MaxLength(300)]
        public string? NeedDescription { get; set; } // توضیحات نیاز
        [MaxLength(10)]
        public string? ProjectNumber { get; set; } // شماره پروژه مرتبط
        [MaxLength(50)]
        public string? Result { get; set; } // نتایج حاصله
        [MaxLength(100)]
        public string? Reason { get; set; } // دلایل حصول یا عدم حصول نتایج
        public DateTime CreatedDate { get; set; } // تاریخ ایجاد
        public DateTime ModifiedDate { get; set; } // تاریخ تغییر
        [MaxLength(50)]
        public string? CreatedBy { get; set; } // نام کاربر ایجادکننده، حداکثر طول: 50 کاراکتر
        public bool IsDeleted { get; set; } = false;

        public int? BuilderProfileId { get; set; }
        [ForeignKey("BuilderProfileId")]
        public BuilderProfile? BuilderProfile { get; set; }
    }
}
