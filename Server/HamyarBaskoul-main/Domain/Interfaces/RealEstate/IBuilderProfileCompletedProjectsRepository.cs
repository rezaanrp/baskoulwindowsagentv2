using Domain.Models.BuilderProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.RealEstate
{
    public interface IBuilderProfileCompletedProjectsRepository
    {
     
            Task<BuilderProfileCompletedProject> GetByIdAsync(int id);
            Task<IEnumerable<BuilderProfileCompletedProject>> GetAllAsync(int BuilderProfileId);
            Task AddAsync(BuilderProfileCompletedProject entity);
            Task UpdateAsync(BuilderProfileCompletedProject entity);
            Task DeleteAsync(int id);
        

    }
}
