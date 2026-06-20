using Domain.Dtos;
using Domain.ViewModels.Baskoul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBaskoulRepository
    {
        Task<BaskoulDomainViewModel> GetByIdAsync(int id);
        public BaskoulDomainViewModel GetById(int id);
        Task<IEnumerable<BaskoulDomainViewModel>> GetBySiteAsync(int siteId, string codeMarkaz);
        Task AddAsync(BaskoulDomainViewModel baskoul);
        void Update(BaskoulDomainViewModel entity);
        Task UpdateAsync(BaskoulDomainViewModel entity);
        Task RemoveAsync(BaskoulDomainViewModel entity);
    }
}
