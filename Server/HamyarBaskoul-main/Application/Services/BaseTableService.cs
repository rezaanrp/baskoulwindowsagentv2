using AutoMapper;
using Application.Interfaces;
using Application.ViewModels.BaseTable;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class BaseTableService : IBaseTableService
    {
        private readonly IBaseTableRepository BaseTableRepository;
        private readonly IMapper mapper;
        public BaseTableService(IBaseTableRepository BaseTableRepository, IMapper mapper)
        {
            this.BaseTableRepository = BaseTableRepository;
            this.mapper = mapper;
        }

        public List<BaseTableViewModel> GetAllBaseTable(string _GroupName)
        {
            var result = BaseTableRepository.GetAll().Where(x=>x.GroupName == _GroupName).ToList();
			var result2 = mapper.Map<List<BaseTableViewModel>>(result);

			return result2;
        }
        public async Task<List<BaseTableViewModel>> GetAllBaseTableAsync(string _GroupName)
        {
            var result = await BaseTableRepository.GetAllasync(_GroupName);
            var result2 = mapper.Map<List<BaseTableViewModel>>(result);
            return result2;
        }
        public BaseTableViewModel GetBaseTableById(int Id)
        {
            var result = BaseTableRepository.GetById(Id);
            var result2 = mapper.Map<BaseTableViewModel>(result);
            return result2;
        }

        public BaseTable GetBaseTable(int Id)
        {
            var result = BaseTableRepository.GetById(Id);
            return result;
        }

		public bool AddBaseTable(BaseTableViewModel model)
		{
			var result2 = mapper.Map<BaseTable>(model);
			var result = BaseTableRepository.Insert(result2);
            return result;

		}
	}
}

