using Application.ViewModels.BaseTable;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBaseTableService
    {
		List<BaseTableViewModel> GetAllBaseTable(string GroupName);
        public Task<List<BaseTableViewModel>> GetAllBaseTableAsync(string _GroupName);

        BaseTableViewModel GetBaseTableById(int Id);

        BaseTable GetBaseTable(int Id);
        bool AddBaseTable(BaseTableViewModel model);
	}
}

