using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.DesignExecution
{
    public class DesignExecutionEmployerFollowUp
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } // تاریخ
        public TimeSpan Time { get; set; } // ساعت
        public int FollowUpMethodBaseTableId { get; set; } // نوع پیگیری
        [MaxLength(500)]
        public string Actions { get; set; } // اقدامات
        [MaxLength(100)]

        public string NextFollowUpMethod { get; set; } // نحوه پیگیری بعدی
        public int FollowUpOutcomeBaseTableId { get; set; } // نتیجه نهایی
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        [MaxLength(100)]
        public string? UserId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int DesignExecutionEmployerId { get; set; }
        [ForeignKey("DesignExecutionEmployerId")]
        public DesignExecutionEmployer DesignExecutionEmployers { get; set; }
    }
}
