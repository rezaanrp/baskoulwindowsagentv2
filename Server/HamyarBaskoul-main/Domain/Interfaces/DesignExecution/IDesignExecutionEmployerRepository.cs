using Domain.Dtos;
using Domain.Models.DesignExecution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.DesignExecution
{
	public interface IDesignExecutionEmployerRepository
	{
		public  Task<IEnumerable<DesignExecutionEmployer>> GetAllAsync(ParametersModels parametersModels);

		public  Task<IEnumerable<DesignExecutionEmployerFollowUp>> GetAllFollowUpAsync(ParametersModels parametersModels);
	
		public Task<DesignExecutionEmployerFollowUp> GetFollowUpByIdAsync(int id);



    }
}
