using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISiteRepository
    {
        Task AddAsync(SiteDomainViewModel siteDomainView);
        Task UpdateAsync(SiteDomainViewModel siteDomainView);
        List<SiteDomainViewModel> GetAllAsync(string codemarkaz);
        List<SiteDomainViewModel> GetAllActiveAsync(string codemarkaz);
        Task<SiteDomainViewModel> GetByIdAsync(int id);
        Task<string> GetNAmeByIdAsync(int id);
        Task<int> GetIdByNameAsync(string name, string codemarkaz);
        Task<bool> IsActive(int id);
    }
}
