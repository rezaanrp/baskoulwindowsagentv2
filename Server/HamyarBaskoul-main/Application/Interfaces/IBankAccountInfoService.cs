using Application.ViewModels.BankAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBankAccountInfoService
    {
        public Task<BankAccountInfoViewModel> GetByIdAsync(int id);
        public Task<IEnumerable<BankAccountInfoViewModel>> GetAllAsync();
        public List<BankAccountInfoViewModel> GetAllActive();


		public Task AddAsync(BankAccountInfoViewModel model);
        public Task UpdateAsync(BankAccountInfoViewModel model);
        public Task DeleteAsync(int id);
    }
}

