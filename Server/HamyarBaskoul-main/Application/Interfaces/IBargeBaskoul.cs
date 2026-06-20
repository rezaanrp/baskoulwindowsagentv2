using Application.ViewModels.Baskoul;
using Application.Classes;
using System.Threading.Tasks;
using Application.Classes;
using Domain.ViewModels.Baskoul;
using System.IO;

namespace Application.Interfaces
{
    public interface IBargeBaskoul
    {
        Task<BargeBaskoulViewModel> CreateBargeBaskoul(int type);
        Task AddBargeBaskoulAsync(BargeBaskoulViewModel entity);
        Task UpdateBargeBaskoulAsync(BargeBaskoulViewModel entity);
        Task<PagedResultBarge> GetAllAsync(string codeMarkaz, int siteID, int pageNumber, int pageSize);
        Task<PagedResultBarge> GetAllAsyncbyType(int type,string codeMarkaz, int pageNumber, int pageSize);
        Task<IEnumerable<MabaniViewModel>> GetAllMabanisAsync(string codeMarkaz);
        Task<BargeBaskoulViewModel> GetBargeBaskoul(int id);
        public Task<PagedResultBarge> GetFilteredAsyncbyType(int type, string codeMarkaz, string searchTerm,
    int page, int pageSize, string sortColumn, string sortDirection);
        Task<bool> EbtalBargeAnbar(int bargId, string userId);
        Task<bool> SabtBargeAnbar(int bargId, string userId);
        Task<bool> SaveToDBFromExcelFile(Stream fileStream, string fileName, string codemarkaz);
    }
}
