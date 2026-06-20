using Application.ViewModels.BuilderProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBuilderProfileEstimatedNeedService
    {
        Task<BuilderProfileEstimatedNeedViewModels> GetByIdAsync(int id);
        Task<IEnumerable<BuilderProfileEstimatedNeedViewModels>> GetAllAsync(int BuilderProfileId);
        Task AddAsync(BuilderProfileEstimatedNeedViewModels entity);
        Task UpdateAsync(BuilderProfileEstimatedNeedViewModels entity);
        Task DeleteAsync(int id);
    }
}
