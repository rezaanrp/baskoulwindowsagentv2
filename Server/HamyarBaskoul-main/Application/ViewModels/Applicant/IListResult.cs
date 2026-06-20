namespace Application.ViewModels.Applicant
{
	public interface IListResult<T>
	{
		IReadOnlyList<T> Items { get; set; }
	}
}
