using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.FileUpload
{
	public class FileUploadImageViewModel
	{
		public string Description { get; set; }

		public IFormFile Image { get; set; }
	}
}

