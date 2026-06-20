using Application.ViewModels.BaseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBaseService
    {
        public Task AddAsync(ListPageViewModel model, string codmarkaz, string userId);
        public Task<List<BaseDataViewModel>> GetDataAsync(int type, string codmarkaz, string userId);
        public Task<BaseDataViewModel> GetByIdAsync(int Id);
        public Task DeleteAsync(BaseDataViewModel entity);
        public Task UpdateAsync(BaseDataViewModel entity, string codmarkaz);
    }
}
