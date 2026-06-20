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
    public class BuilderProfileFollowUpResultService
        : IBuilderProfileFollowUpResultService
    {
        private readonly IBuilderProfileFollowUpResultRepository _repository;

        private readonly IMapper _mapper;
        public BuilderProfileFollowUpResultService(IMapper mapper,
            IBuilderProfileFollowUpResultRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<BuilderProfileFollowUpResultViewModels> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);

            var model = _mapper.Map<BuilderProfileFollowUpResultViewModels>(result);
            var cs = new csShamciToMiladi();
            return model;
        }

        public async Task<IEnumerable<BuilderProfileFollowUpResultViewModels>> GetAllAsync(int BuilderProfileId)
        {
            var result = await _repository.GetAllAsync(BuilderProfileId);
            var result2 = _mapper.Map<List<BuilderProfileFollowUpResultViewModels>>(result);
            var cs = new csShamciToMiladi();

            foreach ( var item in result2)
            {
            }
            return result2;
        }

        public async Task AddAsync(BuilderProfileFollowUpResultViewModels entity)
        {
            var cs = new csShamciToMiladi();
            var result2 = _mapper.Map<BuilderProfileFollowUpResult>(entity);

            await _repository.AddAsync(result2);
        }

        public async Task UpdateAsync(BuilderProfileFollowUpResultViewModels entity)
        {
            var cs = new csShamciToMiladi();
            var result2 = _mapper.Map<BuilderProfileFollowUpResult>(entity);
            await _repository.UpdateAsync(result2);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }

}
