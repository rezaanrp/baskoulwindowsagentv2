using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.ViewModels;
using Domain.ViewModels.Weighbridge;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Infra.Data.Repository
{
    public class WeighbridgeSiteRepository : IWeighbridgeSiteRepository
    {
        private readonly WriteDbContext _context;
        private readonly IMapper _mapper;

        public WeighbridgeSiteRepository(WriteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(WeighbridgeSiteDomainViewModel siteDomainView)
        {
            var model = _mapper.Map<WeighbridgeSite>(siteDomainView);
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public List<WeighbridgeSiteDomainViewModel> GetAllAsync(string codemarkaz)
        {
            var models = _context.WeighbridgeSites.Where(b => b.Company == codemarkaz).ToList();
            var result = _mapper.Map<List<WeighbridgeSiteDomainViewModel>>(models);
            return result;
        }

        public List<WeighbridgeSiteDomainViewModel> GetAllActiveAsync(string codemarkaz)
        {
            var models = _context.WeighbridgeSites.Where(b => (b.Company == codemarkaz) && (b.isActive)).ToList();
            var result = _mapper.Map<List<WeighbridgeSiteDomainViewModel>>(models);
            return result;
        }

        public async Task<WeighbridgeSiteDomainViewModel> GetByIdAsync(int id)
        {
            var model = await _context.WeighbridgeSites.FirstOrDefaultAsync(b => b.ID == id);
            var result = _mapper.Map<WeighbridgeSiteDomainViewModel>(model);
            return result;
        }

        public async Task UpdateAsync(WeighbridgeSiteDomainViewModel siteDomainView)
        {
            var site = await _context.WeighbridgeSites.FirstOrDefaultAsync(b => b.ID == siteDomainView.ID);
            if (site == null) return;

            _mapper.Map(siteDomainView, site); // map onto the existing tracked entity

            await _context.SaveChangesAsync();
        }

        public async Task<string> GetNAmeByIdAsync(int id)
        {
            if (id == null) return null;
            var site = await _context.WeighbridgeSites.FirstOrDefaultAsync(s => s.ID == id);
            return site.name ?? "";
        }

        public async Task<int> GetIdByNameAsync(string name, string codemarkaz)
        {
            var site = await _context.WeighbridgeSites.FirstOrDefaultAsync(s => s.Company == codemarkaz && s.name == name);
            if (site == null)
                return 0;
            else return site.ID;
        }

        public async Task<bool> IsActive(int id)
        {
            var site = await _context.WeighbridgeSites.FirstOrDefaultAsync(s => s.ID == id);
            if (site == null)
                return false;
            else return site.isActive;
        }
    }
}

