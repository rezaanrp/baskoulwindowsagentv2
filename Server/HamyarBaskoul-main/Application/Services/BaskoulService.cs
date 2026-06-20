using Application.Interfaces;
using Application.ViewModels.BuilderProfile;
using AutoMapper;
using Domain.Models;
using Application.Tools;
using Application.ViewModels.Baskoul;
using Domain.Interfaces;
using Domain.ViewModels.Baskoul;

namespace Application.Services
{
    public class BaskoulService : IBaskoulService
    {
        private readonly IBaskoulRepository _repository;
        private readonly IMapper _mapper;
        public BaskoulService(IBaskoulRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task AddAsync(BaskoulViewModel entity)
        {
            var result = _mapper.Map<BaskoulDomainViewModel>(entity);
            await _repository.AddAsync(result);
        }

        public async Task DeleteAsync(BaskoulViewModel entity)
        {
            var baskoul = await _repository.GetByIdAsync(entity.Id);
            if (baskoul != null)
                await _repository.RemoveAsync(baskoul);
        }

        public async Task<IEnumerable<BaskoulViewModel>> GetBySiteAsync(int siteId, string codeMarkaz)
        {
            var result = await _repository.GetBySiteAsync(siteId, codeMarkaz);
            if (result == null) return null;
            var result2 = _mapper.Map<IEnumerable<BaskoulViewModel>>(result);
            return result2;
        }

        public async Task<BaskoulViewModel> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            var result2 = _mapper.Map<BaskoulViewModel>(result);
            return result2;
        }

        public async Task UpdateAsync(BaskoulViewModel entity)
        {
            var baskoul = _mapper.Map<BaskoulDomainViewModel>(entity);
            await _repository.UpdateAsync(baskoul);
        }
    }
}
