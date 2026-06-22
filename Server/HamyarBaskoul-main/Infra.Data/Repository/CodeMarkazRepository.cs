using AutoMapper;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Wordprocessing;
using Domain.Classes;
using Domain.Interfaces;
using Domain.Models;
using Domain.ViewModels;
using Infra.Data.Context;
using Infra.Data.Migrations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using static Dapper.SqlMapper;

namespace Infra.Data.Repository
{
    public class CodeMarkazRepository : ICodeMarkazRepository
    {
        private readonly WriteDbContext _context;
        private readonly IMapper _mapper;
        public CodeMarkazRepository(WriteDbContext cleanArchDataBaseContext, IMapper mapper)
        {
            _context = cleanArchDataBaseContext;
            _mapper = mapper;
        }

        public async Task Delete(int id)
        {
            var entity = await _context.CodeMarkazs.FirstOrDefaultAsync(c => c.Id == id);
            _context.CodeMarkazs.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<PagedResultDomain<CodeMarkazDomainViewModel>> GetAllAsync(int page, int pageSize)
        {
            var query = _context.CodeMarkazs.AsQueryable();

            var totalCount = await query.CountAsync();

            var models = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var list = _mapper.Map<List<CodeMarkazDomainViewModel>>(models);
            var model = new PagedResultDomain<CodeMarkazDomainViewModel>
            {
                Items = list,
                TotalCount = totalCount
            };
            return model;
        }

        public async Task<CodeMarkazDomainViewModel> GetByIdAsync(int id)
        {
            var entity = await _context.CodeMarkazs.FirstOrDefaultAsync(m => m.Id == id);
            var model = _mapper.Map<CodeMarkazDomainViewModel>(entity);
            return model;
        }

        public async Task<string> GetCodMarkazByUrl(string appname)
        {
            var markaz = await _context.CodeMarkazs.FirstOrDefaultAsync(cm => cm.MarkazURL == appname);
            if (markaz == null) return null;
            return markaz.CodMarkaz;
        }

        public async Task<string?> GetCoNameByUserIdAsync(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null || string.IsNullOrEmpty(user.CodMarkaz)) return null;
            var markaz = await _context.CodeMarkazs.FirstOrDefaultAsync(m => m.CodMarkaz == user.CodMarkaz);
            return markaz==null ? null : markaz.CoName;
        }

        public async Task<string?> GetCoNameByLinkAsync(string appName)
        {
            var codemarkaz = _context.CodeMarkazs
                .FirstOrDefault(m => appName == m.MarkazURL);
            if (codemarkaz == null) return null;
            return codemarkaz.CoName;
        }

        public async Task Insert(CodeMarkazDomainViewModel model)
        {
            var entity = _mapper.Map<CodeMarkaz>(model);
            await _context.CodeMarkazs.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(CodeMarkazDomainViewModel model)
        {
            var entity = _mapper.Map<CodeMarkaz>(model);
            _context.CodeMarkazs.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateCodemarkaz(string codmarkaz)
        {
            var markaz = await _context.CodeMarkazs.FirstOrDefaultAsync(m => m.CodMarkaz == codmarkaz);
            return markaz == null ? false : true;
        }

        public async Task<string> GetAppNameByCode(string codemarkaz)
        {
            var markaz = await _context.CodeMarkazs.FirstOrDefaultAsync(cm => cm.CodMarkaz == codemarkaz);
            if (markaz.MarkazURL == null) return null;
            return markaz.MarkazURL;
        }
    }
}

