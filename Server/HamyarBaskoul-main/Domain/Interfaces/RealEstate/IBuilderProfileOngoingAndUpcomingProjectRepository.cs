using Domain.Models.BuilderProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.RealEstate
{
    public interface IBuilderProfileOngoingAndUpcomingProjectRepository
    {
        Task<BuilderProfileOngoingAndUpcomingProject> GetByIdAsync(int id);
        Task<IEnumerable<BuilderProfileOngoingAndUpcomingProject>> GetAllAsync(int BuilderProfileId);
        Task AddAsync(BuilderProfileOngoingAndUpcomingProject entity);
        Task UpdateAsync(BuilderProfileOngoingAndUpcomingProject entity);
        Task DeleteAsync(int id);
    }
}
