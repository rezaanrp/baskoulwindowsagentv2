using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IWeighbridgeSiteService
    {
        Task AddSite(WeighbridgeSiteViewModel siteViewModel);
        Task UpdateSite(WeighbridgeSiteViewModel siteViewModel);
        List<WeighbridgeSiteViewModel> GetAllAsync(string codemarkaz);
        List<WeighbridgeSiteViewModel> GetAllActiveAsync(string codemarkaz);
        Task<WeighbridgeSiteViewModel> GetByIdAsync(int id);
        Task<string> GetNameById(int id);
        Task<int> GetIdByName(string name, string codemarkaz);
        Task<bool> IsActive(int id);
    }
}

