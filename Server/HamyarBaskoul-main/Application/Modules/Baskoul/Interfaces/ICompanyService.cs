using Application.Classes;
using Application.ViewModels;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICompanyService
    {
        Task Insert(CompanyViewModel model);
        Task Update(CompanyViewModel model);
        Task<PagedResultCompanyList> GetAllAsync(int page, int pageSize);
        Task<CompanyViewModel> GetByIdAsync(int id);
        Task Delete(int id);
        Task<string> GetCoNameAsync(string appName);
        Task<bool> ValidateCodemarkaz(string codmarkaz);
        Task<string> GetCodMarkazByUrl(string appname);
        public Task<string> GetAppNameByCode(string codemarkaz);
    }
}


