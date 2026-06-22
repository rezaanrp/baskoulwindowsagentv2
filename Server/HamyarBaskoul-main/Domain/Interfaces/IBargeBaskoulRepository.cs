using Domain.Classes;
using Domain.ViewModels.Baskoul;

namespace Domain.Interfaces
{
    public interface IBargeBaskoulRepository
    {
        Task<BargeBaskoulDomainViewModel?> GetByGhabzBaskoulAsync(long ghabzId);
        Task<BargeBaskoulDomainViewModel> GetByIDAsync(int id);
        public BargeBaskoulDomainViewModel? GetByGhabzBaskoul(long ghabzId);
        public IEnumerable<BargeBaskoulDomainViewModel> GetAll(string codeMarkaz);
        Task<PagedResultDomain<BargeBaskoulDomainViewModel>> GetAllAsync(string codeMarkaz, int siteId, int pageNumber, int pageSize);
        Task<PagedResultDomain<BargeBaskoulDomainViewModel>> GetAllAsyncbyType(int type, string codeMarkaz, int pageNumber, int pageSize);
        Task<IEnumerable<MabaniDomainViewModel>> GetAllMabanisAsync(string codeMarkaz);
        Task AddAsync(BargeBaskoulDomainViewModel entity);
        Task UpdateAsync(BargeBaskoulDomainViewModel entity);
        void RemoveAsync(BargeBaskoulDomainViewModel entity);
        Task<Dictionary<int, long?>> MabaniDict(List<long> idsToFetch);
        Task<PagedResultDomain<BargeBaskoulDomainViewModel>> GetFilteredAsyncbyType(
    int type, string codeMarkaz, string searchTerm, int page, int pageSize, string sortColumn, string sortDirection);
        Task<bool> EbtalBargeAnbar(int bargId, string userId);
        Task<bool> SabtBargeAnbar(int bargId, string userId);
    }
}

