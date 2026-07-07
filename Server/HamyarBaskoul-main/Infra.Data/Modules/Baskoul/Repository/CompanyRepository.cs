using AutoMapper;
using Domain.Classes;
using Domain.Interfaces;
using Domain.Models;
using Domain.ViewModels;
using Infra.Data.Context;
using Infra.Data.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using static Dapper.SqlMapper;

namespace Infra.Data.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly WriteDbContext _context;
        private readonly IMapper _mapper;
        public CompanyRepository(WriteDbContext cleanArchDataBaseContext, IMapper mapper)
        {
            _context = cleanArchDataBaseContext;
            _mapper = mapper;
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);
            _context.Companies.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<PagedResultDomain<CompanyDomainViewModel>> GetAllAsync(int page, int pageSize)
        {
            var query = _context.Companies.AsQueryable();

            var totalCount = await query.CountAsync();

            var models = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var list = _mapper.Map<List<CompanyDomainViewModel>>(models);
            var model = new PagedResultDomain<CompanyDomainViewModel>
            {
                Items = list,
                TotalCount = totalCount
            };
            return model;
        }

        public async Task<CompanyDomainViewModel> GetByIdAsync(int id)
        {
            var entity = await _context.Companies.FirstOrDefaultAsync(m => m.Id == id);
            var model = _mapper.Map<CompanyDomainViewModel>(entity);
            return model;
        }

        public async Task<string> GetCodMarkazByUrl(string appname)
        {
            var markaz = await _context.Companies.FirstOrDefaultAsync(cm => cm.MarkazURL == appname);
            if (markaz == null) return null;
            return markaz.CodMarkaz;
        }

        public async Task<string?> GetCoNameByUserIdAsync(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null || string.IsNullOrEmpty(user.CodMarkaz)) return null;
            var markaz = await _context.Companies.FirstOrDefaultAsync(m => m.CodMarkaz == user.CodMarkaz);
            return markaz==null ? null : markaz.CoName;
        }

        public async Task<string?> GetCoNameByLinkAsync(string appName)
        {
            var codemarkaz = _context.Companies
                .FirstOrDefault(m => appName == m.MarkazURL);
            if (codemarkaz == null) return null;
            return codemarkaz.CoName;
        }

        public async Task Insert(CompanyDomainViewModel model)
        {
            var entity = _mapper.Map<Company>(model);
            await _context.Companies.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(CompanyDomainViewModel model)
        {
            var entity = _mapper.Map<Company>(model);
            _context.Companies.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateCodemarkaz(string codmarkaz)
        {
            var markaz = await _context.Companies.FirstOrDefaultAsync(m => m.CodMarkaz == codmarkaz);
            return markaz == null ? false : true;
        }

        public async Task<string> GetAppNameByCode(string codemarkaz)
        {
            var markaz = await _context.Companies.FirstOrDefaultAsync(cm => cm.CodMarkaz == codemarkaz);
            if (markaz.MarkazURL == null) return null;
            return markaz.MarkazURL;
        }
    }
}



