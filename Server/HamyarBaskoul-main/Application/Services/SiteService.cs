using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Interfaces;
using Domain.ViewModels;

namespace Application.Services
{
    public class SiteService : ISite
    {
        private readonly ISiteRepository _siterepository;
        private readonly IMapper _mapper;

        public SiteService(ISiteRepository siterepository, IMapper mapper)
        {
            _siterepository = siterepository;
            _mapper = mapper;
        }

        public async Task AddSite(SiteViewModel siteViewModel)
        {
            var model = _mapper.Map<SiteDomainViewModel>(siteViewModel);
            await _siterepository.AddAsync(model);
        }

        public List<SiteViewModel> GetAllAsync(string codemarkaz)
        {
            var list = _siterepository.GetAllAsync(codemarkaz);
            var sites = _mapper.Map<List<SiteViewModel>>(list);
            return sites;
        }

        public List<SiteViewModel> GetAllActiveAsync(string codemarkaz)
        {
            var list = _siterepository.GetAllActiveAsync(codemarkaz);
            var sites = _mapper.Map<List<SiteViewModel>>(list);
            return sites;
        }

        public async Task<SiteViewModel> GetByIdAsync(int id)
        {
            var site = await _siterepository.GetByIdAsync(id);
            var model = _mapper.Map<SiteViewModel>(site);
            return model;
        }

        public async Task UpdateSite(SiteViewModel siteViewModel)
        {
            var model = _mapper.Map<SiteDomainViewModel>(siteViewModel);
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

