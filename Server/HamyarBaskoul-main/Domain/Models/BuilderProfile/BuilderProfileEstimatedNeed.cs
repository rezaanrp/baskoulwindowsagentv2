using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.BuilderProfile
{
    public class BuilderProfileEstimatedNeed
    {
        [Key]
        public int Id { get; set; } // شناسه منحصر به فرد برای هر نیاز
        [MaxLength(200)]
        public string? NeedDescription { get; set; } // توضیحات نیاز احتمالی
        [MaxLength(10)]
        public string? ProjectNumber { get; set; } // شماره پروژه مرتبط
        public DateTime? EstimatedResolutionTime { get; set; } // زمان تقریبی برطرف شدن نیاز

        [MaxLength(50)]
        public string? ResponsiblePerson { get; set; } // مسئول پیگیری
        [MaxLength(50)]
        public string? FollowUpMethod { get; set; } // روش پیگیری (مثلاً تلفن، ایمیل، جلسه)
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
