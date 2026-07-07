using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Interfaces;
using Domain.ViewModels;

namespace Application.Services
{
    public class WeighbridgeSiteService : IWeighbridgeSiteService
    {
        private readonly IWeighbridgeSiteRepository _siterepository;
        private readonly IMapper _mapper;

        public WeighbridgeSiteService(IWeighbridgeSiteRepository siterepository, IMapper mapper)
        {
            _siterepository = siterepository;
            _mapper = mapper;
        }

        public async Task AddSite(WeighbridgeSiteViewModel siteViewModel)
        {
            var model = _mapper.Map<WeighbridgeSiteDomainViewModel>(siteViewModel);
            await _siterepository.AddAsync(model);
        }

        public List<WeighbridgeSiteViewModel> GetAllAsync(string codemarkaz)
        {
            var list = _siterepository.GetAllAsync(codemarkaz);
            var sites = _mapper.Map<List<WeighbridgeSiteViewModel>>(list);
            return sites;
        }

        public List<WeighbridgeSiteViewModel> GetAllActiveAsync(string codemarkaz)
        {
            var list = _siterepository.GetAllActiveAsync(codemarkaz);
            var sites = _mapper.Map<List<WeighbridgeSiteViewModel>>(list);
            return sites;
        }

        public async Task<WeighbridgeSiteViewModel> GetByIdAsync(int id)
        {
            var site = await _siterepository.GetByIdAsync(id);
            var model = _mapper.Map<WeighbridgeSiteViewModel>(site);
            return model;
        }

        public async Task UpdateSite(WeighbridgeSiteViewModel siteViewModel)
        {
            var model = _mapper.Map<WeighbridgeSiteDomainViewModel>(siteViewModel);
            await _siterepository.UpdateAsync(model);
        }

        public async Task<string> GetNameById(int id)
        {
            return await _siterepository.GetNAmeByIdAsync(id);
        }
        public async Task<int> GetIdByName(string name, string codemarkaz)
        {
            return await _siterepository.GetIdByNameAsync(name, codemarkaz);
        }
        public async Task<bool> IsActive(int id)
        {
            return await _siterepository.IsActive(id);
        }
    }
}

