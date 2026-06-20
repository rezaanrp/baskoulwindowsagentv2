using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.BuilderProfile
{
    public class BuilderProfileOngoingAndUpcomingProjectViewModels
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string? Address { get; set; }            // آدرس پروژه
        public int? NumberOfFloors { get; set; }        // تعداد طبقات
        public int? TotalArea { get; set; }          // متراژ کل
        public DateTime? StartDate { get; set; }        // تاریخ شروع
        [MaxLength(10)]
        public string? StartDateFarsi { get; set; }        // تاریخ شروع
        [MaxLength(100)]
        public string? CurrentStage { get; set; }       // مرحله فعلی
        public DateTime ModifiedDate { get; set; } // تاریخ تغییر

        [MaxLength(50)]
        public string? CreatedBy { get; set; } // نام کاربر ایجادکننده، حداکثر طول: 50 کاراکتر
        public bool IsDeleted { get; set; } = false;
        //public override string ToString()
        //{
        //    return $"Address: {Address}, Floors: {NumberOfFloors}, Total Area: {TotalArea} sqm, Start Date: {StartDate.ToShortDateString()}, Current Stage: {CurrentStage}";
        //}
        public int? BuilderProfileId { get; set; }
    }
}
