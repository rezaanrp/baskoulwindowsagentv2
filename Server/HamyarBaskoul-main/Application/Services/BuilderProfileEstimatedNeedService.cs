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
    public class BuilderProfileEstimatedNeedService
        : IBuilderProfileEstimatedNeedService
    {
        private readonly IBuilderProfileEstimatedNeedRepository _repository;

        private readonly IMapper _mapper;
        public BuilderProfileEstimatedNeedService(IMapper mapper,
            IBuilderProfileEstimatedNeedRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<BuilderProfileEstimatedNeedViewModels> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);

            var model = _mapper.Map<BuilderProfileEstimatedNeedViewModels>(result);
            var cs = new csShamciToMiladi();
            model.EstimatedResolutionTimeFarsi = cs.MiladiToShamci(model.EstimatedResolutionTime);
            return model;
        }

        public async Task<IEnumerable<BuilderProfileEstimatedNeedViewModels>> GetAllAsync(int BuilderProfileId)
        {
            var result = await _repository.GetAllAsync(BuilderProfileId);
            var result2 = _mapper.Map<List<BuilderProfileEstimatedNeedViewModels>>(result);
            var cs = new csShamciToMiladi();

            foreach ( var item in result2)
            {
                item.EstimatedResolutionTimeFarsi = cs.MiladiToShamci(item.EstimatedResolutionTime);
            }
            return result2;
        }

        public async Task AddAsync(BuilderProfileEstimatedNeedViewModels entity)
        {
            var cs = new csShamciToMiladi();
            entity.EstimatedResolutionTime = entity.EstimatedResolutionTimeFarsi == null ? entity.EstimatedResolutionTime : cs.ShamciToMiladi(entity.EstimatedResolutionTimeFarsi);
            var result2 = _mapper.Map<BuilderProfileEstimatedNeed>(entity);

            await _repository.AddAsync(result2);
        }

        public async Task UpdateAsync(BuilderProfileEstimatedNeedViewModels entity)
        {
            var cs = new csShamciToMiladi();
            entity.EstimatedResolutionTime = entity.EstimatedResolutionTimeFarsi == null ? entity.EstimatedResolutionTime : cs.ShamciToMiladi(entity.EstimatedResolutionTimeFarsi);
            var result2 = _mapper.Map<BuilderProfileEstimatedNeed>(entity);
            await _repository.UpdateAsync(result2);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }

}
