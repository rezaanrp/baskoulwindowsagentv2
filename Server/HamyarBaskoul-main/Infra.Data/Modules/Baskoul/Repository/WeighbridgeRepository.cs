using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.ViewModels.Weighbridge;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository
{
    public class WeighbridgeRepository : IWeighbridgeRepository
    {
        private readonly WriteDbContext _context;
        private readonly IMapper _mapper;

        public WeighbridgeRepository(WriteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddAsync(WeighbridgeDomainViewModel entity)
        {
            var baskoul = _mapper.Map<Weighbridge>(entity);
            await _context.Weighbridges.AddAsync(baskoul);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WeighbridgeDomainViewModel>> GetBySiteAsync(int siteId, string codeMarkaz)
        {
            _context.Database.SetCommandTimeout(180); // 180 seconds
            var list = await _context.Weighbridges.Where(b => b.CodMarkaz == codeMarkaz && b.WeighbridgeSite == siteId).ToListAsync();
            var Weighbridges = _mapper.Map<IEnumerable<WeighbridgeDomainViewModel>>(list);
            return Weighbridges;
        }

        public WeighbridgeDomainViewModel GetById(int id)
        {
            var entity = _context.Weighbridges.Find(id);
            var baskoul = _mapper.Map<WeighbridgeDomainViewModel>(entity);
            return baskoul;
        }

        public async Task<WeighbridgeDomainViewModel> GetByIdAsync(int id)
        {
            var entity = await _context.Weighbridges.FindAsync(id);
            var baskoul = _mapper.Map<WeighbridgeDomainViewModel>(entity);
            return baskoul;
        }

        public async Task RemoveAsync(WeighbridgeDomainViewModel entity)
        {
            var baskoul = _mapper.Map<Weighbridge>(entity);
            // Check if the entity is already being tracked
            var trackedEntity = _context.Weighbridges.Local.FirstOrDefault(b => b.Id == baskoul.Id);

            if (trackedEntity != null)
            {
                // If the entity is being tracked, detach it
                _context.Entry(trackedEntity).State = EntityState.Detached;
            }

            _context.Weighbridges.Remove(baskoul);
            await _context.SaveChangesAsync();
        }

        public void Update(WeighbridgeDomainViewModel entity)
        {
            var baskoul = _mapper.Map<Weighbridge>(entity);
            _context.Weighbridges.Update(baskoul);
            _context.SaveChanges();
        }

        public async Task UpdateAsync(WeighbridgeDomainViewModel entity)
        {
            var baskoul = _mapper.Map<Weighbridge>(entity);
            _context.Weighbridges.Update(baskoul);
            await _context.SaveChangesAsync();
        }
    }
}

