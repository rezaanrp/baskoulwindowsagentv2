using Application.ViewModels.Baskoul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBaskoulService
    {
        Task<BaskoulViewModel> GetByIdAsync(int id);
        Task<IEnumerable<BaskoulViewModel>> GetBySiteAsync(int siteId, string codeMarkaz);
        Task AddAsync(BaskoulViewModel entity);
        Task UpdateAsync(BaskoulViewModel entity);
        Task DeleteAsync(BaskoulViewModel entity);
    }
}


