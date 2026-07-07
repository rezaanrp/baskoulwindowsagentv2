using Application.ViewModels;
using Application.ViewModels.Users;
using Domain.Models;
using Domain.ViewModels;
using Domain.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUsersService
    {
		void DeleteUser(string v);
		UsersListViewModel Get(string Id);
		AppUser GetById(string Id);
		string GetCodMarkazById(string Id);
		string GetCodMarkazByURL(string Id);
		public IEnumerable<AppUser> GetAll1(string roleName1, string roleName2);
		public IEnumerable<WeighbridgeSiteViewModel> GetAllActiveSites(string id);
		public Task<IEnumerable<AppUser>> GetAllByDepartementasync(string Departement, string codemarkaz);
		public IEnumerable<AppUser> GetAllByDepartement(string Departement);

		void UpdateUser(UsersListViewModel category);
		Task UpdateUserWindowsToken(string token, string id);
        AppUser GetUserByEmail(string Email);
        UsersListViewModel GetUserInformation(string email);
		void InsertUser(InsertUsersListViewModel category);
		string GetUserRole(string id);
		ObjectFormListViewModel GetUserFormAccessList(string id);
		bool user_have_access_to_object(int objectId, string UserId);
		bool edit_object_access_for_user(ObjectFormListViewModel objectForm,string supplier_);
		public bool user_access_form(string from, string UserId);
		string get_name_and_family_by_id(string id);
		public void UpdateUserRole(string user_id, string role_name);
		public  Task<string> get_name_and_family_by_id_async(string id);
		public Task<bool> SaveSelectedSiteAsync(int siteId, string userId);
		public Task<List<UsersListViewModel>> GetUsersBySite(int id, string codemarkaz);
		Task<ObjectFormListViewModel?> GetUserFormAccessListAsync(string id);
	}
}


