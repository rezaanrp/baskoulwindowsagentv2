namespace Application.ViewModels.Applicant
{
	[Serializable]
	public class ListResultDto<T> : IListResult<T>
	{
		private IReadOnlyList<T> _items;

		public IReadOnlyList<T> Items
		{
			get => this._items ?? (this._items = (IReadOnlyList<T>)new List<T>());
			set => this._items = value;
		}

		public ListResultDto()
		{
		}

		public ListResultDto(IReadOnlyList<T> items) => this.Items = items;
	}
}
