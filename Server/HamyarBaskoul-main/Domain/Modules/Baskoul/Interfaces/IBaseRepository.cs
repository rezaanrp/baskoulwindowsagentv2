using Domain.ViewModels.BaseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBaseRepository
    {
        public Task AddAsync(BaseDataDmainViewModel model, string codmarkaz, string userId);
        public Task<List<BaseDataDmainViewModel>> GetDataAsync(string tableName, string codmarkaz);
        public Task<BaseDataDmainViewModel> GetByIdAsync(int Id);
        public Task RemoveAsync(BaseDataDmainViewModel entity);
        public Task UpdateAsync(BaseDataDmainViewModel entity);
    }
}

