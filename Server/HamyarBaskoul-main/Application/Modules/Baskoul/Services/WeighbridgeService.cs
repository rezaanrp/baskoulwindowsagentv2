using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Application.Tools;
using Application.ViewModels.Weighbridge;
using Domain.Interfaces;
using Domain.ViewModels.Weighbridge;

namespace Application.Services
{
    public class WeighbridgeService : IWeighbridgeService
    {
        private readonly IWeighbridgeRepository _repository;
        private readonly IMapper _mapper;
        public WeighbridgeService(IWeighbridgeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task AddAsync(WeighbridgeViewModel entity)
        {
            var result = _mapper.Map<WeighbridgeDomainViewModel>(entity);
            await _repository.AddAsync(result);
        }

        public async Task DeleteAsync(WeighbridgeViewModel entity)
        {
            var baskoul = await _repository.GetByIdAsync(entity.Id);
            if (baskoul != null)
                await _repository.RemoveAsync(baskoul);
        }

        public async Task<IEnumerable<WeighbridgeViewModel>> GetBySiteAsync(int siteId, string codeMarkaz)
        {
            var result = await _repository.GetBySiteAsync(siteId, codeMarkaz);
            if (result == null) return null;
            var result2 = _mapper.Map<IEnumerable<WeighbridgeViewModel>>(result);
            return result2;
        }

        public async Task<WeighbridgeViewModel> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            var result2 = _mapper.Map<WeighbridgeViewModel>(result);
            return result2;
        }

        public async Task UpdateAsync(WeighbridgeViewModel entity)
        {
            var baskoul = _mapper.Map<WeighbridgeDomainViewModel>(entity);
            await _repository.UpdateAsync(baskoul);
        }
    }
}


