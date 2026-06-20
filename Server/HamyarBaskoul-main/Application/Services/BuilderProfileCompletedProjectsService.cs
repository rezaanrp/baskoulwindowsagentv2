using Application.Interfaces;
using Application.Tools;
using Application.ViewModels.BuilderProfile;
using AutoMapper;
using Domain.Interfaces.RealEstate;
using Domain.Models.BuilderProfile;

namespace Application.Services
{


    public class BuilderProfileCompletedProjectsService : IBuilderProfileCompletedProjectsService
    {
        private readonly IBuilderProfileCompletedProjectsRepository _repository;
        private readonly IMapper _mapper;


        public BuilderProfileCompletedProjectsService(IMapper mapper, IBuilderProfileCompletedProjectsRepository repository)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<BuilderProfileCompletedProjectViewModels> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);

            var model =  _mapper.Map<BuilderProfileCompletedProjectViewModels>(result);
            var cs = new csShamciToMiladi();
            model.EndDateFarsi = cs.MiladiToShamci(model.EndDate);
            model.StartDateFarsi = cs.MiladiToShamci(model.StartDate);
            return model;  
        }

        public async Task<IEnumerable<BuilderProfileCompletedProjectViewModels>> GetAllAsync(int BuilderProfileId)
        {
            var result = await _repository.GetAllAsync( BuilderProfileId);
            var result2 = _mapper.Map<List<BuilderProfileCompletedProjectViewModels>>(result);
            return result2;
        }

        public async Task AddAsync(BuilderProfileCompletedProjectViewModels entity)
        {
            var cs = new csShamciToMiladi();
            entity.EndDate = entity.EndDateFarsi == null ? entity.EndDate : cs.ShamciToMiladi(entity.EndDateFarsi);
            entity.StartDate = entity.StartDateFarsi == null ? entity.StartDate : cs.ShamciToMiladi(entity.StartDateFarsi);
            var result2 = _mapper.Map<BuilderProfileCompletedProject>(entity);
           
            await _repository.AddAsync(result2);
        }

        public async Task UpdateAsync(BuilderProfileCompletedProjectViewModels entity)
        {
            var cs = new csShamciToMiladi();
            entity.EndDate = entity.EndDateFarsi == null ? entity.EndDate : cs.ShamciToMiladi(entity.EndDateFarsi);
            entity.StartDate = entity.StartDateFarsi == null ? entity.StartDate : cs.ShamciToMiladi(entity.StartDateFarsi);
            var result2 = _mapper.Map<BuilderProfileCompletedProject>(entity);
            await _repository.UpdateAsync(result2);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<int> CalculateExecutionTimeAsync(int id)
        {
            var project = await _repository.GetByIdAsync(id);
            if (project == null) throw new KeyNotFoundException("Project not found.");
            return project.ExecutionTimeInDays;
        }
    }

}
