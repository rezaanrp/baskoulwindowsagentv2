using Application.Interfaces;
using Application.ViewModels;
using Application.ViewModels.Users;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.ViewModels;
using Domain.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
	public class UsersService : IUsersService
	{
		private readonly IUsersRepository _usersRepository;
		private readonly IMapper mapper;
		public UsersService(IUsersRepository UsersRepository, IMapper mapper)
		{
			this._usersRepository = UsersRepository;
			this.mapper = mapper;
		}

		public UsersListViewModel Get(string Id)
		{
			var result = _usersRepository.GetById(Id);
			var result2 = mapper.Map<UsersListViewModel>(result);
			if (result?.SiteAccesses != null)
			{
				result2.SelectedSiteIds = result.SiteAccesses
					.Select(access => access.SiteId)
					.Distinct()
					.ToList();
			}
			//result2.role = _usersRepository.GetRole_by_user_id(Id);
			return result2;
		}
		public void DeleteUser(string id)
		{
			_usersRepository.Delete(id);


		}
		public IEnumerable<AppUser> GetAll1(string roleName1, string roleName2)
		{
			var result = _usersRepository.GetAll(roleName1,roleName2);
			return result;
		}
		public async Task<IEnumerable<AppUser>> GetAllByDepartementasync(string Departement, string codemarkaz)
		{
			var result = await _usersRepository.GetAllByDepartementasync(Departement, codemarkaz);
			return result;
		}
		public IEnumerable<AppUser> GetAllByDepartement(string Departement)
		{
			var result =  _usersRepository.GetAllByDepartement(Departement);
			return result;
		}
		public void UpdateUser(UsersListViewModel category)
		{
			var model = mapper.Map<UsersListDomainViewModel>(category);
			_usersRepository.Update(model);

		}
		public void UpdateUserRole(string user_id,string role_name)
		{
			
			string role_id = _usersRepository.GetRole_id_by_name(role_name);
		 	string user_role = _usersRepository.GetRole_by_user_id(user_id) ;
			if (user_role.ToUpper() == "ADMINISTRATOR")
				return;
			if (role_id != string.Empty)
			{
				_usersRepository.UpdateUserRole(user_id, role_id);
			}

		}
		public AppUser GetById(string Id)
		{
			return _usersRepository.GetById(Id);
		}
		public AppUser GetUserByEmail(string Email)
		{
			return _usersRepository.GetUserByEmail(Email);
		}
		public UsersListViewModel GetUserInformation(string email)
		{
			var user = GetUserByEmail(email);
			UsersListViewModel usersListViewModel = new UsersListViewModel();
			usersListViewModel.Name = user.Name;
			usersListViewModel.Family = user.Family;
			usersListViewModel.Mobile = user.Mobile;
			usersListViewModel.UserName = user.UserName;
            usersListViewModel.PersonnelCode = user.PersonnelCode;
            return usersListViewModel;
		}
		public void InsertUser(InsertUsersListViewModel category)
		{
			var m = new AppUser();
			m.UserName = category.UserName;
			m.Name = category.Name;
			m.Family = category.Family;
			m.Mobile = category.Mobile;
            m.PersonnelCode = category.PersonnelCode;
            m.PasswordHash = category.Password;

			_usersRepository.Add(m);

		}
		public string GetUserRole(string id)
		{
			return _usersRepository.GetRole_by_user_id(id);
		}
		public ObjectFormListViewModel GetUserFormAccessList(string id)
		{
			ObjectFormListViewModel m = new ObjectFormListViewModel();

			var model = GetById(id);
			if (model == null)
			{
				return m;
			}
			var oj = _usersRepository.GetObjectFormByDepartement(model.Departments??"");
			List<ObjectFormViewModel> mmm = new List<ObjectFormViewModel>();
			foreach (var item in oj)
			{
				ObjectFormViewModel objectFormViewModel = new ObjectFormViewModel();
				objectFormViewModel.Id = item.Id;
				objectFormViewModel.UserName = item.UserName;
				objectFormViewModel.Name = item.Name;
				objectFormViewModel.NameFarsi = item.NameFarsi;
				objectFormViewModel.GroupName = item.GroupName;
				objectFormViewModel.GroupNameFarsi = item.GroupNameFarsi;
				objectFormViewModel.IsAccess = user_have_access_to_object(item.Id, id);
				mmm.Add(objectFormViewModel);
			}
			m.objectFormViews = mmm;
			m.UserId = id;
			return m;
		}
		public string get_name_and_family_by_id(string id)
		{
			var us = _usersRepository.GetById(id);
			return us != null ? us.Name + " " + us.Family : "";
		}
        public async Task<string> get_name_and_family_by_id_async(string id)
        {
            var us = await _usersRepository.GetByIdAsync(id);
			if (us != null)
				return us.Name ?? "" + " " + us.Family ?? "";
			else
				return "#";
        }
        public bool user_have_access_to_object(int objectId, string UserId)
		{
			var objectFormUsers = _usersRepository.get_all_object_user_access(UserId);
			var mm = objectFormUsers.Where(x => x.ObjectFormId == objectId);
			return mm.Any();
		}
		public bool user_access_form(string from, string UserId)
		{
			return _usersRepository.user_access_form(from, UserId);
		}
		public bool edit_object_access_for_user(ObjectFormListViewModel objectForm, string supplier_)
		{
			_usersRepository.delete_all_object_for_user(objectForm.UserId);
			List<ObjectFormUser> ob = new List<ObjectFormUser>();
			foreach (var item in objectForm.objectFormViews)
			{
				if (item.IsAccess)
				{
					ObjectFormUser objectFormUser = new ObjectFormUser();
					objectFormUser.User_ = objectForm.UserId;
					objectFormUser.ObjectFormId = item.Id;
					objectFormUser.Supplier_ = supplier_;
					ob.Add(objectFormUser);
				}
			}
			_usersRepository.add_objectform_for_user(ob);

			return true;
		}
        public string GetCodMarkazByURL(string url)
        {
            return _usersRepository.FindCodeMarkazByURL(url);
        }
        public string GetCodMarkazById(string id)
        {
            return _usersRepository.FindCodeMarkazById(id);
        }
        public IEnumerable<WeighbridgeSiteViewModel> GetAllActiveSites(string id)
        {
			var sites = _usersRepository.GetAllActiveSites(id);
			var model = mapper.Map<List<WeighbridgeSiteViewModel>>(sites);
			return model;
        }
        public async Task<bool> SaveSelectedSiteAsync(int siteId, string userId)
		{
			return await _usersRepository.SaveSelectedSiteAsync(siteId, userId);
		}
        public async Task<List<UsersListViewModel>> GetUsersBySite(int id, string codemarkaz)
        {
			var modellist = await _usersRepository.GetUsersBySite(id, codemarkaz);
			var users = mapper.Map<List<UsersListViewModel>>(modellist);
			return users;
        }
		public async Task<AppUser?> GetByIdAsync(string Id)
		{
			return await _usersRepository.GetByIdAsync(Id);
		}
		public async Task<ObjectFormListViewModel?> GetUserFormAccessListAsync(string id)
		{
			ObjectFormListViewModel m = new ObjectFormListViewModel();

			var model = await GetByIdAsync(id);
			if (model is null)
				return null;
			var oj = await _usersRepository.GetObjectFormByDepartementAsync(model.Departments ?? "");
			List<ObjectFormViewModel> mmm = new List<ObjectFormViewModel>();
			foreach (var item in oj)
			{
				ObjectFormViewModel objectFormViewModel = new ObjectFormViewModel();
				objectFormViewModel.Id = item.Id;
				objectFormViewModel.Name = item.Name;
				objectFormViewModel.NameFarsi = item.NameFarsi;
				objectFormViewModel.GroupName = item.GroupName;
				objectFormViewModel.GroupNameFarsi = item.GroupNameFarsi;
				objectFormViewModel.IsAccess = user_have_access_to_object(item.Id, id);
				mmm.Add(objectFormViewModel);
			}
			m.objectFormViews = mmm;
			m.UserId = id;
			return m;
		}

        public async Task UpdateUserWindowsToken(string token, string id)
        {
            await _usersRepository.UpdateUserWindowsToken(token, id); 
        }
    }
}


