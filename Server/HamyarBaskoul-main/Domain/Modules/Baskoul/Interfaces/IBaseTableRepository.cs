using Domain.Models;
using Domain.ViewModels.BaseTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBaseTableRepository
	{
        List<BaseTableDomainViewModel> GetAll();
        public Task<List<BaseTableDomainViewModel>> GetAllasync();

        public Task<List<BaseTableDomainViewModel>> GetAllasync(string groupname);

        BaseTable? GetById(int? Id);
		bool Insert(BaseTable table);


	}
}

