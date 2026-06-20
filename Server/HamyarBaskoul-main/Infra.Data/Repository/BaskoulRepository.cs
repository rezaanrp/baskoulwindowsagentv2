using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.ViewModels.Baskoul;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository
{
    public class BaskoulRepository : IBaskoulRepository
    {
        private readonly WriteDbContext _context;
        private readonly IMapper _mapper;

        public BaskoulRepository(WriteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddAsync(BaskoulDomainViewModel entity)
        {
            var baskoul = _mapper.Map<Baskoul>(entity);
            await _context.baskouls.AddAsync(baskoul);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BaskoulDomainViewModel>> GetBySiteAsync(int siteId, string codeMarkaz)
        {
            _context.Database.SetCommandTimeout(180); // 180 seconds
            var list = await _context.baskouls.Where(b => b.CodMarkaz == codeMarkaz && b.Site == siteId).ToListAsync();
            var baskouls = _mapper.Map<IEnumerable<BaskoulDomainViewModel>>(list);
            return baskouls;
        }

        public BaskoulDomainViewModel GetById(int id)
        {
            var entity = _context.baskouls.Find(id);
            var baskoul = _mapper.Map<BaskoulDomainViewModel>(entity);
            return baskoul;
        }

        public async Task<BaskoulDomainViewModel> GetByIdAsync(int id)
        {
            var entity = await _context.baskouls.FindAsync(id);
            var baskoul = _mapper.Map<BaskoulDomainViewModel>(entity);
            return baskoul;
        }

        public async Task RemoveAsync(BaskoulDomainViewModel entity)
        {
            var baskoul = _mapper.Map<Baskoul>(entity);
            // Check if the entity is already being tracked
            var trackedEntity = _context.baskouls.Local.FirstOrDefault(b => b.Id == baskoul.Id);

            if (trackedEntity != null)
            {
                // If the entity is being tracked, detach it
                _context.Entry(trackedEntity).State = EntityState.Detached;
            }

            _context.baskouls.Remove(baskoul);
            await _context.SaveChangesAsync();
        }

        public void Update(BaskoulDomainViewModel entity)
        {
            var baskoul = _mapper.Map<Baskoul>(entity);
            _context.baskouls.Update(baskoul);
            _context.SaveChanges();
        }

        public async Task UpdateAsync(BaskoulDomainViewModel entity)
        {
            var baskoul = _mapper.Map<Baskoul>(entity);
            _context.baskouls.Update(baskoul);
            await _context.SaveChangesAsync();
        }
    }
}
