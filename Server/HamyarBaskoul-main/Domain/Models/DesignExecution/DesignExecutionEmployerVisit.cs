using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DesignExecution
{
	public class DesignExecutionEmployerVisit
    {
		[Key]
		public int Id { get; set; }
        public DateTime VisitDate { get; set; }
		[MaxLength(500)]
        public string ExistingSituation { get; set; }
        [MaxLength(50)]
        public string? AdmissionsSpecialistUserId { get; set; }//کارشناس یازدید
		public bool WasItMeasured { get; set; } = false;
		public bool WasItPhotoTaken { get; set; } = false;
		public bool WasItVideoTaken { get; set; } = false;
		[MaxLength(500)]
		public string EmployerComments { get; set; }
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
