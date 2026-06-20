using Application.ViewModels.BuilderProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBuilderProfileFollowUpResultService
    {
        Task<BuilderProfileFollowUpResultViewModels> GetByIdAsync(int id);
        Task<IEnumerable<BuilderProfileFollowUpResultViewModels>> GetAllAsync(int BuilderProfileId);
        Task AddAsync(BuilderProfileFollowUpResultViewModels entity);
        Task UpdateAsync(BuilderProfileFollowUpResultViewModels entity);
        Task DeleteAsync(int id);
    }
}
