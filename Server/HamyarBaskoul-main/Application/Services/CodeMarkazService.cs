using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CodeMarkazService : ICodeMarkaz
    {
        private readonly IMapper _mapper;
        private readonly ICodeMarkazRepository _codemarkazRepository;
        public CodeMarkazService(IMapper mapper, ICodeMarkazRepository codeMarkazRepository)
        {
            _mapper = mapper;
            _codemarkazRepository = codeMarkazRepository;
        }

        public async Task Delete(int id)
        {
            await _codemarkazRepository.Delete(id);
        }

        public async Task<PagedResultCodeMarkazList> GetAllAsync(int page, int pageSize)
        {
            var entity = await _codemarkazRepository.GetAllAsync(page, pageSize);
            var models = _mapper.Map<List<CodeMarkazViewModel>>(entity.Items);
            var model = new PagedResultCodeMarkazList
            {
                Markazes = models,
                TotalCount = entity.TotalCount
            };
            return model;
        }

        public async Task<CodeMarkazViewModel> GetByIdAsync(int id)
        {
            var model = await _codemarkazRepository.GetByIdAsync(id);
            var entity = _mapper.Map<CodeMarkazViewModel>(model);
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

        public async Task Insert(CodeMarkazViewModel model)
        {
            var entity = _mapper.Map<CodeMarkazDomainViewModel>(model);
            await _codemarkazRepository.Insert(entity);
        }

        public async Task Update(CodeMarkazViewModel model)
        {
            var entity = _mapper.Map<CodeMarkazDomainViewModel>(model);
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

