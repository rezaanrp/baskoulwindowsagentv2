using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.BuilderProfile
{

    public class BuilderProfileViewModel
        {
            [Key]
            public int Id { get; set; }

            [MaxLength(100)]
            public string? FullName { get; set; } // حداکثر طول: 100 کاراکتر

            public int? EducationBaseTableId { get; set; } // حداکثر طول: 50 کاراکتر

            public int? MaritalStatusBaseTableId { get; set; } // حداکثر طول: 20 کاراکتر

            public int? NumberOfDaughters { get; set; }
            public int? NumberOfSons { get; set; }

            [MaxLength(100)]
            public string? MainOccupation { get; set; } // حداکثر طول: 100 کاراکتر

            [MaxLength(15)]
            public string? MobileNumber { get; set; } // حداکثر طول: 15 کاراکتر و بررسی فرمت تلفن

            [MaxLength(200)]
            public string? WorkAddress { get; set; } // حداکثر طول: 200 کاراکتر

        // نوع و سابقه فعالیت
        public bool Renovation { get; set; } = false;
            public bool Reconstruction { get; set; } = false;
        public int YearsOfExperience { get; set; }

        public bool Personal { get; set; } = false;
        public bool Partnership { get; set; } = false;
        public bool Corporate { get; set; } = false;
        public bool ManagementContract { get; set; } = false;


        public int? AnnualActivityVolumeBaseTableId { get; set; }//حجم فعالیت سالیانه:
            public int? BusinessPriorityBaseTableId { get; set; }//اولویت تجاری :



            // توانمندی‌ها و امکانات
            public int? BuyingAndSellingBaseTableId { get; set; }//BuilderProfileSkillLevel
            public int? DesignBaseTableId { get; set; }//BuilderProfileSkillLevel
            public int? ExecutionBaseTableId { get; set; }//BuilderProfileSkillLevel
            public int? MaterialSupplyBaseTableId { get; set; }//BuilderProfileSkillLevel
            public int? PermitObtainingBaseTableId { get; set; }//BuilderProfileSkillLevel

            // همکاری‌های گذشته
            public bool? PreviousCooperation { get; set; }
            public int? CooperationDuration { get; set; } // مدت همکاری به سال یا ماه

            [MaxLength(100)]
            public string? CooperationField { get; set; } // حداکثر طول: 100 کاراکتر

            [MaxLength(1000)]
            public string? CooperationResults { get; set; } // حداکثر طول: 1000 کاراکتر
                                                            // ستون‌های تاریخ و نام کاربر


        }
}
