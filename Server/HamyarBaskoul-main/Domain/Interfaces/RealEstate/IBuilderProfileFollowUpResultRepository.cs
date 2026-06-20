using Domain.Models.BuilderProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.RealEstate
{
    public interface IBuilderProfileFollowUpResultRepository
    {
        Task<BuilderProfileFollowUpResult> GetByIdAsync(int id);
        Task<IEnumerable<BuilderProfileFollowUpResult>> GetAllAsync(int BuilderProfileId);
        Task AddAsync(BuilderProfileFollowUpResult entity);
        Task UpdateAsync(BuilderProfileFollowUpResult entity);
        Task DeleteAsync(int id);
    }
}
