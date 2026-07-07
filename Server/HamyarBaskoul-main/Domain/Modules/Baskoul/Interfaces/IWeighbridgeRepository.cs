using Domain.Dtos;
using Domain.ViewModels.Weighbridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IWeighbridgeRepository
    {
        Task<WeighbridgeDomainViewModel> GetByIdAsync(int id);
        public WeighbridgeDomainViewModel GetById(int id);
        Task<IEnumerable<WeighbridgeDomainViewModel>> GetBySiteAsync(int siteId, string codeMarkaz);
        Task AddAsync(WeighbridgeDomainViewModel baskoul);
        void Update(WeighbridgeDomainViewModel entity);
        Task UpdateAsync(WeighbridgeDomainViewModel entity);
        Task RemoveAsync(WeighbridgeDomainViewModel entity);
    }
}

