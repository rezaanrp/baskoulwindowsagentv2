using System.ComponentModel.DataAnnotations;


namespace Application.ViewModels.FilingUser.FilingOperationFourStageExpert
{
	public class FilingUserDetailFourStageExpertViewModel
	{

		[Key]
		public int Id { get; set; }
		[MaxLength(100)]
		public string? FilingOperationFourStageExpertUserId { get; set; }
		public string? FilingOperationFourStageExpertUserFullName { get; set; }
        public DateTime? FilingOperationFourStageExpertDate { get; set; }
		public string? FilingOperationFourStageExpertDateFarsi { get; set; }
        public bool Rebuildability { get; set; } = false;//قابلیت بازسازی
        public int? RebuildabilityBaseTableId { get; set; }//قابلیت بازسازی
        public string? RebuildabilityBaseTable { get; set; }//قابلیت بازسازی
        public int? FilingUserDetailId { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
		[MaxLength(50)]
		public string? RegisterUserId { get; set; }
		public bool? IsDeleted { get; set; } = false;
	}
}
