using Domain.Interfaces.RealEstate;
using Domain.Models.BuilderProfile;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repository.RealEstate
{
    public class BuilderProfileFollowUpResultRepository : IBuilderProfileFollowUpResultRepository
    {
        private readonly WriteDbContext _context;
        private readonly DbSet<BuilderProfileFollowUpResult> _dbSet;

        public BuilderProfileFollowUpResultRepository(WriteDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<BuilderProfileFollowUpResult>();
        }
        public async Task<BuilderProfileFollowUpResult> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<IEnumerable<BuilderProfileFollowUpResult>> GetAllAsync(int BuilderProfileId)
        {
            return await _dbSet.Where(x => x.BuilderProfileId == BuilderProfileId && !x.IsDeleted).ToListAsync();
        }

        public async Task AddAsync(BuilderProfileFollowUpResult entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BuilderProfileFollowUpResult entity)
        {
            entity.ModifiedDate = DateTime.UtcNow;
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                entity.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
    }
}
