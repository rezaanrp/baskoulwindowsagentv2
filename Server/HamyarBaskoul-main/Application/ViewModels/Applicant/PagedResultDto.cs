namespace Application.ViewModels.Applicant
{
	public class PagedResultDto<T> : ListResultDto<T>
	{
		public int TotalCount { get; set; }
		public PagedResultDto()
		{
		}
		public PagedResultDto(int totalCount, IReadOnlyList<T> items) : base(items)
		{
			this.TotalCount = totalCount;
		}
	}
}
