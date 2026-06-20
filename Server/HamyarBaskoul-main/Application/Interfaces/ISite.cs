using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISite
    {
        Task AddSite(SiteViewModel siteViewModel);
        Task UpdateSite(SiteViewModel siteViewModel);
        List<SiteViewModel> GetAllAsync(string codemarkaz);
        List<SiteViewModel> GetAllActiveAsync(string codemarkaz);
        Task<SiteViewModel> GetByIdAsync(int id);
        Task<string> GetNameById(int id);
        Task<int> GetIdByName(string name, string codemarkaz);
        Task<bool> IsActive(int id);
    }
}
