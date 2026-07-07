using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.ViewModels.BaseData;
using Domain.ViewModels.Weighbridge;
using Domain.Classes;
using Infra.Data.Classes;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository
{
    public class BaseRepository : IBaseRepository
    {
        private readonly WriteDbContext _context;
        private readonly IMapper _mapper;

        public BaseRepository(WriteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddAsync(BaseDataDmainViewModel model, string codmarkaz, string userId)
        {
            if (model != null)
            {
                var data = _mapper.Map<Mabani>(model);
                data.CodMarkaz = codmarkaz;
                data.KarbarUp = userId;
                data.KarbarIns = userId;
                await _context.Mabanis.AddAsync(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<BaseDataDmainViewModel> GetByIdAsync(int Id)
        {
            var model = await _context.Mabanis.FirstOrDefaultAsync(x => x.ID == Id);
            var data = _mapper.Map<BaseDataDmainViewModel>(model);
            return data;
        }

        public async Task<List<BaseDataDmainViewModel>> GetDataAsync(string tableName, string codmarkaz)
        {
            var list = await _context.Mabanis.Where(x => x.TableName == tableName
            && x.CodMarkaz == codmarkaz).ToListAsync();
            var model = _mapper.Map<List<BaseDataDmainViewModel>>(list);
            return model;
        }

        public async Task RemoveAsync(BaseDataDmainViewModel entity)
        {
            var model = _mapper.Map<Mabani>(entity);
            // Check if the entity is already being tracked
            var trackedEntity = _context.Mabanis.Local.FirstOrDefault(b => b.ID == model.ID);

            if (trackedEntity != null)
            {
                // If the entity is being tracked, detach it
                _context.Entry(trackedEntity).State = EntityState.Detached;
            }

            _context.Mabanis.Remove(model);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(BaseDataDmainViewModel entity)
        {
            try
            {
                // Map to entity model
                var model = _mapper.Map<Mabani>(entity);

                // Check if the entity is already tracked
                var trackedEntity = _context.Set<Mabani>().Local.FirstOrDefault(e => e.ID == model.ID);
                if (trackedEntity != null)
                {
                    _context.Entry(trackedEntity).State = EntityState.Detached;
                }

                // Mark model as modified
                _context.Entry(model).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.LogToFile(ex.ToString());
            }
        }

    }
}



