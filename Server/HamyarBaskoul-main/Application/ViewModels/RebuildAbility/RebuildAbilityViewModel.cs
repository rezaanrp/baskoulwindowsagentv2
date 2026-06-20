using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.RebuildAbility
{
    public class RebuildAbilityViewModel
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string? Name { get; set; }
        [MaxLength(100)]
        public string? Family { get; set; }
        public string FullName => $"{Name} {Family}".Trim();
        [MaxLength(11)]
        public string? Mobile { get; set; }
        [MaxLength(200)]
        public string? Address { get; set; }
        public int? RebuildabilityBaseTableId { get; set; }//قابلیت بازسازی
        public string? RebuildabilityBaseTable { get; set; }//قابلیت بازسازی
        public string? Representative { get; set; }

        public DateTime CreatedDate { get; set; } // تاریخ ایجاد
        public DateTime ModifiedDate { get; set; } // تاریخ تغییر
        [MaxLength(50)]
        public string? CreatedBy { get; set; } // نام کاربر ایجادکننده، حداکثر طول: 50 کاراکتر
        [MaxLength(50)]
        public string? Modifiedby { get; set; } // نام کاربر ایجادکننده، حداکثر طول: 50 کاراکتر
        public bool IsDeleted { get; set; } = false;
    }
}
