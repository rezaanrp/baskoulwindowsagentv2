using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IWeighbridgeSiteRepository
    {
        Task AddAsync(WeighbridgeSiteDomainViewModel siteDomainView);
        Task UpdateAsync(WeighbridgeSiteDomainViewModel siteDomainView);
        List<WeighbridgeSiteDomainViewModel> GetAllAsync(string codemarkaz);
        List<WeighbridgeSiteDomainViewModel> GetAllActiveAsync(string codemarkaz);
        Task<WeighbridgeSiteDomainViewModel> GetByIdAsync(int id);
        Task<string> GetNAmeByIdAsync(int id);
        Task<int> GetIdByNameAsync(string name, string codemarkaz);
        Task<bool> IsActive(int id);
    }
}

