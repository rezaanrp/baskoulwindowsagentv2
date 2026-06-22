using Domain.Classes;
using Domain.Models;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICodeMarkazRepository
    {
        Task Insert(CodeMarkazDomainViewModel model);
        Task Update(CodeMarkazDomainViewModel model);
        Task<PagedResultDomain<CodeMarkazDomainViewModel>> GetAllAsync(int page, int pageSize);
        Task<CodeMarkazDomainViewModel> GetByIdAsync(int id);
        Task Delete(int id);
        Task<string?> GetCoNameByUserIdAsync(string userId);
        Task<string?> GetCoNameByLinkAsync(string userId);
        Task<string> GetCodMarkazByUrl(string appname);
        Task<bool> ValidateCodemarkaz(string codmarkaz);
        public Task<string> GetAppNameByCode(string codemarkaz);
    }
}

