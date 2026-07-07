using Application.ViewModels.Weighbridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IWeighbridgeService
    {
        Task<WeighbridgeViewModel> GetByIdAsync(int id);
        Task<IEnumerable<WeighbridgeViewModel>> GetBySiteAsync(int siteId, string codeMarkaz);
        Task AddAsync(WeighbridgeViewModel entity);
        Task UpdateAsync(WeighbridgeViewModel entity);
        Task DeleteAsync(WeighbridgeViewModel entity);
    }
}


