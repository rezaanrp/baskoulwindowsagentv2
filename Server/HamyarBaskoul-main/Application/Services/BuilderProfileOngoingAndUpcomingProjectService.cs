using Application.Interfaces;
using Application.Tools;
using Application.ViewModels.BuilderProfile;
using AutoMapper;
using Domain.Interfaces.RealEstate;
using Domain.Models.BuilderProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BuilderProfileOngoingAndUpcomingProjectService
        : IBuilderProfileOngoingAndUpcomingProjectService
    {
        private readonly IBuilderProfileOngoingAndUpcomingProjectRepository _repository;

        private readonly IMapper _mapper;
        public BuilderProfileOngoingAndUpcomingProjectService(IMapper mapper,
            IBuilderProfileOngoingAndUpcomingProjectRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<BuilderProfileOngoingAndUpcomingProjectViewModels> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);

            var model = _mapper.Map<BuilderProfileOngoingAndUpcomingProjectViewModels>(result);
            var cs = new csShamciToMiladi();
            model.StartDateFarsi = cs.MiladiToShamci(model.StartDate);
            return model;
        }

        public async Task<IEnumerable<BuilderProfileOngoingAndUpcomingProjectViewModels>> GetAllAsync(int BuilderProfileId)
        {
            var result = await _repository.GetAllAsync(BuilderProfileId);
            var result2 = _mapper.Map<List<BuilderProfileOngoingAndUpcomingProjectViewModels>>(result);
            return result2;
        }

        public async Task AddAsync(BuilderProfileOngoingAndUpcomingProjectViewModels entity)
        {
            var cs = new csShamciToMiladi();
            entity.StartDate = entity.StartDateFarsi == null ? entity.StartDate : cs.ShamciToMiladi(entity.StartDateFarsi);
            var result2 = _mapper.Map<BuilderProfileOngoingAndUpcomingProject>(entity);

            await _repository.AddAsync(result2);
        }

        public async Task UpdateAsync(BuilderProfileOngoingAndUpcomingProjectViewModels entity)
        {
            var cs = new csShamciToMiladi();
            entity.StartDate = entity.StartDateFarsi == null ? entity.StartDate : cs.ShamciToMiladi(entity.StartDateFarsi);
            var result2 = _mapper.Map<BuilderProfileOngoingAndUpcomingProject>(entity);
            await _repository.UpdateAsync(result2);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }

}
