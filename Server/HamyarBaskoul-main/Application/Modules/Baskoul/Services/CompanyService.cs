using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _codemarkazRepository;
        public CompanyService(IMapper mapper, ICompanyRepository codeMarkazRepository)
        {
            _mapper = mapper;
            _codemarkazRepository = codeMarkazRepository;
        }

        public async Task Delete(int id)
        {
            await _codemarkazRepository.Delete(id);
        }

        public async Task<PagedResultCompanyList> GetAllAsync(int page, int pageSize)
        {
            var entity = await _codemarkazRepository.GetAllAsync(page, pageSize);
            var models = _mapper.Map<List<CompanyViewModel>>(entity.Items);
            var model = new PagedResultCompanyList
            {
                Markazes = models,
                TotalCount = entity.TotalCount
            };
            return model;
        }

        public async Task<CompanyViewModel> GetByIdAsync(int id)
        {
            var model = await _codemarkazRepository.GetByIdAsync(id);
            var entity = _mapper.Map<CompanyViewModel>(model);
            return entity;
        }

        public async Task<string> GetCodMarkazByUrl(string appname)
        {
            if (string.IsNullOrEmpty(appname))
                return null;
            return await _codemarkazRepository.GetCodMarkazByUrl(appname);
            
        }

        public async Task<string> GetCoNameAsync(string appName)
        {
            var coName = await _codemarkazRepository.GetCoNameByLinkAsync(appName);
            return string.IsNullOrWhiteSpace(coName)
                ? "سیستم باسکول همیار"  // Default
                : coName;
        }

        public async Task Insert(CompanyViewModel model)
        {
            var entity = _mapper.Map<CompanyDomainViewModel>(model);
            await _codemarkazRepository.Insert(entity);
        }

        public async Task Update(CompanyViewModel model)
        {
            var entity = _mapper.Map<CompanyDomainViewModel>(model);
            await _codemarkazRepository.Update(entity);
        }

        public async Task<bool> ValidateCodemarkaz(string codmarkaz)
        {
            if (string.IsNullOrEmpty(codmarkaz)) return false;
            return await _codemarkazRepository.ValidateCodemarkaz(codmarkaz);
        }

        public async Task<string> GetAppNameByCode(string codemarkaz)
        {
            return await _codemarkazRepository.GetAppNameByCode(codemarkaz);
        }
    }
}


