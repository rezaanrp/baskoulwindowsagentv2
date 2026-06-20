using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IBufferedFileUploadService
    {
        Task<bool> UploadFile(IFormFile file);
    }
}
