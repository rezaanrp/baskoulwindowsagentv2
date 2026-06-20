using Application.ViewModels.BuilderProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBuilderProfileOngoingAndUpcomingProjectService
    {
        Task<BuilderProfileOngoingAndUpcomingProjectViewModels> GetByIdAsync(int id);
        Task<IEnumerable<BuilderProfileOngoingAndUpcomingProjectViewModels>> GetAllAsync(int BuilderProfileId);
        Task AddAsync(BuilderProfileOngoingAndUpcomingProjectViewModels entity);
        Task UpdateAsync(BuilderProfileOngoingAndUpcomingProjectViewModels entity);
        Task DeleteAsync(int id);
    }
}
