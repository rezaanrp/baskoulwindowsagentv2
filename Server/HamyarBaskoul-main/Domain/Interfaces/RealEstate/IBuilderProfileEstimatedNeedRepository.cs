using Domain.Models.BuilderProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.RealEstate
{
    public interface IBuilderProfileEstimatedNeedRepository
    {
        Task<BuilderProfileEstimatedNeed> GetByIdAsync(int id);
        Task<IEnumerable<BuilderProfileEstimatedNeed>> GetAllAsync(int BuilderProfileId);
        Task AddAsync(BuilderProfileEstimatedNeed entity);
        Task UpdateAsync(BuilderProfileEstimatedNeed entity);
        Task DeleteAsync(int id);
    }
}
