using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.ViewModels.BaseTable;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repository
{
    public class BaseTableRepository : IBaseTableRepository
	{
        private readonly WriteDbContext _cleanArchDataBaseContext;
        private readonly IMapper _mapper;
        public BaseTableRepository(WriteDbContext cleanArchDataBaseContext, IMapper mapper)
        {
            _cleanArchDataBaseContext = cleanArchDataBaseContext;
            _mapper = mapper;
        }



        public List<BaseTableDomainViewModel> GetAll()
        {
			var models = _cleanArchDataBaseContext.BaseTables.ToList();
			var result = _mapper.Map<List<BaseTableDomainViewModel>>(models);
			return result;
		}
        public async Task<List<BaseTableDomainViewModel>> GetAllasync(string groupname)
        {
            var models = await _cleanArchDataBaseContext.BaseTables.Where(x=>x.GroupName == groupname).ToListAsync();
            var result = _mapper.Map<List<BaseTableDomainViewModel>>(models);
            return result;
        }
        public async Task< List<BaseTableDomainViewModel> > GetAllasync()
        {
            var models = await _cleanArchDataBaseContext.BaseTables.ToListAsync();
            var result = _mapper.Map<List<BaseTableDomainViewModel>>(models);
            return result;
        }
        public BaseTable? GetById(int? Id)
        {
            var baseTables = _cleanArchDataBaseContext.BaseTables.FirstOrDefault(x => x.Id == Id);
            return baseTables;
        }

	

		public bool Insert(BaseTable table)
		{
			try
			{
				var baseTables = _cleanArchDataBaseContext.BaseTables.Add(table);
				_cleanArchDataBaseContext.SaveChanges();
				return true;
			}
			catch (Exception)
			{

			return false;
			}


		}
	}
}


