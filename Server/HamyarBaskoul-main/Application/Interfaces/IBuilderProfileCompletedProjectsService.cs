using Application.ViewModels.BuilderProfile;
using Domain.Models.BuilderProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBuilderProfileCompletedProjectsService
    {
        Task<BuilderProfileCompletedProjectViewModels> GetByIdAsync(int id);
        Task<IEnumerable<BuilderProfileCompletedProjectViewModels>> GetAllAsync(int BuilderProfileId);
        Task AddAsync(BuilderProfileCompletedProjectViewModels entity);
        Task UpdateAsync(BuilderProfileCompletedProjectViewModels entity);
        Task DeleteAsync(int id);
        Task<int> CalculateExecutionTimeAsync(int id); // محاسبه مدت زمان اجرا
    }
}
