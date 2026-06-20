using Application.ViewModels.FilingUser.FilingOperationFourStageExpert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	public interface IFilingUserDetailFourStageExpertService
	{
		Task<IEnumerable<FilingUserDetailFourStageExpertViewModel>> GetAllAsync(int id);
		Task<FilingUserDetailFourStageExpertViewModel> GetByIdAsync(int id);
		Task AddAsync(FilingUserDetailFourStageExpertViewModel model);
		Task UpdateAsync(FilingUserDetailFourStageExpertViewModel model);
		Task DeleteAsync(int id);
	}
}
