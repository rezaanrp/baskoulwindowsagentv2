using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.BuilderProfile
{

    public class BuilderProfileCompletedProject
    {
		[Key]
		public int Id { get; set; }
        [MaxLength(200)]
        public string? Address { get; set; } // آدرس ساختمان
        public int NumberOfFloors { get; set; } // تعداد طبقات
        public double TotalArea { get; set; } // متراژ کل
        public int ConstructionQualityBaseTableId { get; set; } // کیفیت ساخت
        public DateTime StartDate { get; set; } // تاریخ شروع اجرا
        public DateTime EndDate { get; set; } // تاریخ پایان اجرا
        // محاسبه مدت زمان اجرا به روز
        public int ExecutionTimeInDays => (EndDate - StartDate).Days;
        public DateTime CreatedDate { get; set; } // تاریخ ایجاد
		public DateTime ModifiedDate { get; set; } // تاریخ تغییر
		[MaxLength(50)]
		public string? CreatedBy { get; set; } // نام کاربر ایجادکننده، حداکثر طول: 50 کاراکتر
		public bool IsDeleted { get; set; } =false;

        public int? BuilderProfileId { get; set; }
        [ForeignKey("BuilderProfileId")]
        public BuilderProfile? BuilderProfile { get; set; }
	}

}
