using AutoMapper;
using Domain.Dtos;
using Domain.Interfaces.DesignExecution;
using Domain.Models;
using Domain.Models.DesignExecution;
using Infra.Data.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repository.DesignExecution
{
	public class DesignExecutionEmployerRepository: IDesignExecutionEmployerRepository
	{
		private readonly WriteDbContext _context;
		private readonly IMapper _mapper;
		public DesignExecutionEmployerRepository(WriteDbContext DataBaseContext, IMapper mapper)
        {
			_context = DataBaseContext;
			_mapper = mapper;
		}

		public async Task<IEnumerable<DesignExecutionEmployer>> GetAllAsync(ParametersModels parametersModels)
		{
			var queryable = _context.DesignExecutionEmployers.AsQueryable();

			if (parametersModels.hashtable != null)
			foreach (DictionaryEntry item in parametersModels.hashtable)
			{
				if (item.Key != null && item.Key.ToString() == "TypeOfProject")
				{
					if (item.Value != null && item.Value.ToString() == "FacadeDesign")
					{
						queryable = queryable.Where(x => x.TypeOfProjectFacadeDesign == true);
					}
					if (item.Value != null && item.Value.ToString() == "AreaDesign")
					{
						queryable = queryable.Where(x => x.TypeOfProjectAreaDesign == true);
					}
					if (item.Value != null && item.Value.ToString() == "InteriorDesign")
					{
						queryable = queryable.Where(x => x.TypeOfProjectInteriorDesign == true);
					}
					if (item.Value != null && item.Value.ToString() == "PlanDesign")
					{
						queryable = queryable.Where(x => x.TypeOfProjectPlanDesign == true);
					}
				}
					if (item.Key != null && item.Key.ToString() == "TypeOfemployer")
					{
						if (item.Value != null)

							queryable = queryable.Where(x => x.TypeOfProjectEmployerBaseTableId == (int)item.Value);

					}

				}

			return await queryable.ToListAsync();

		}
		public async Task<IEnumerable<DesignExecutionEmployerFollowUp>> GetAllFollowUpAsync(ParametersModels parametersModels)
		{
			var queryable = _context.DesignExecutionEmployerFollowUps.AsQueryable();
			queryable = queryable.Where(x => x.DesignExecutionEmployerId == parametersModels.ParentId);
			return await queryable.ToListAsync();
		}
		public async Task<DesignExecutionEmployerFollowUp> GetFollowUpByIdAsync(int id)
		{

			var followUp = await _context.DesignExecutionEmployerFollowUps
				.FirstOrDefaultAsync(x => x.Id == id);

			if (followUp == null)
			{
				// هندل کردن مقدار null
				throw new Exception("No record found with the given parent ID.");
			}

			return followUp;

		}
	}
}
