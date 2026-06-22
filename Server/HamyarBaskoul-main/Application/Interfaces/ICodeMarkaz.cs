using Application.Classes;
using Application.ViewModels;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICodeMarkaz
    {
        Task Insert(CodeMarkazViewModel model);
        Task Update(CodeMarkazViewModel model);
        Task<PagedResultCodeMarkazList> GetAllAsync(int page, int pageSize);
        Task<CodeMarkazViewModel> GetByIdAsync(int id);
        Task Delete(int id);
        Task<string> GetCoNameAsync(string appName);
        Task<bool> ValidateCodemarkaz(string codmarkaz);
        Task<string> GetCodMarkazByUrl(string appname);
        public Task<string> GetAppNameByCode(string codemarkaz);
    }
}

