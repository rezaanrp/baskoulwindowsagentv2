namespace Application.ViewModels.Applicant
{
	public class PagingDTO
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public PagingDTO()
		{ }
		public PagingDTO(int pageNumber, int pageSize)
		{
			PageNumber = pageNumber;
			PageSize = pageSize;
		}
	}
}
