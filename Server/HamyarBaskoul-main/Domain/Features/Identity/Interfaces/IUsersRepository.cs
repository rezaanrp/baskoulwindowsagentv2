using Domain.Models;
using Domain.ViewModels;
using Domain.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUsersRepository
    {
        Task<List<AppUser>> GetAllAsync(string roleName, bool filter_role);
        public List<AppUser> GetAll(string roleName1, string roleName2);
        public List<SiteDomainViewModel> GetAllActiveSites(string id);

        public Task<bool> SaveSelectedSiteAsync(int siteId, string userId);
        public string FindCodeMarkazById(string id);
        public string FindCodeMarkazByURL(string url);

        AppUser? GetById(string Id);
		Task<AppUser?> GetByIdAsync(string Id);
        void Add(AppUser category);
        void Update(UsersListDomainViewModel category);
        bool DeleteAll();
        void Delete(string id);
        AppUser GetUserByEmail(string Email);
        public string GetRole_by_user_id(string user_Id);
        public Task<List<AppUser>> GetAllByDepartementasync(string Departemant, string codemarkaz);
        public List<AppUser> GetAllByDepartement(string Departemant);
		IEnumerable<ObjectForm>  GetObjectForm();
        public IEnumerable<ObjectForm> GetObjectFormByDepartement(string Departement);
		IEnumerable<ObjectFormUser> get_all_object_user_access(string UserId);
        bool delete_all_object_for_user(string UserId);
        bool add_objectform_for_user(List<ObjectFormUser > objectForm);
        public bool user_access_form(string from, string UserId);
        public void UpdateUserRole(string user_id, string new_role_id);
        public string GetRole_id_by_name(string role_name);

        public Task<List<UsersListDomainViewModel>> GetUsersBySite(int siteid, string codemarkaz);
		Task<IEnumerable<ObjectForm?>> GetObjectFormByDepartementAsync(string Departement);

        public Task UpdateUserWindowsToken(string token, string id);

    }
}


