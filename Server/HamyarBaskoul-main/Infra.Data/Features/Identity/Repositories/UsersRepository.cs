using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.ViewModels;
using Domain.ViewModels.Weighbridge;
using Domain.ViewModels.Users;
using Domain.Classes;
using Infra.Data.Classes;
using Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository
{
	public class UsersRepository : IUsersRepository
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly WriteDbContext _Context;
		private readonly IMapper _mapper;
		public UsersRepository(WriteDbContext cleanArchDataBaseContext, IMapper mapper, UserManager<AppUser> userManager)
		{
			_Context = cleanArchDataBaseContext;
			_userManager = userManager;
			_mapper = mapper;
		}

		public void Add(AppUser category)
		{
			_Context.Users.Add(category);
			_Context.SaveChanges();
		}

		public string FindCodeMarkazByURL(string url)
		{
			Logger.LogToFile(url);
            var codemarkaz = _Context.Companies
				.FirstOrDefault(m => url == m.MarkazURL);
			if (codemarkaz == null) return null;
			return codemarkaz.CodMarkaz;
        }
        public string FindCodeMarkazById(string id)
        {
            var user = _Context.Users
                .FirstOrDefault(m => m.Id == id);
			Logger.LogToFile($"{user.CodMarkaz}");
            return user.CodMarkaz;
        }
        public async void Delete(string id)
		{
			//var user = await _userManager.FindByIdAsync(id);
			var user = _Context.Users.FirstOrDefault(x => x.Id == id);
			if (user != null)
			{
				user.IsDelete = true;
				_Context.SaveChanges();

			}

		}

		public bool DeleteAll()
		{
			return false;
		}

		public async Task<List<AppUser>> GetAllAsync(string roleName, bool filter_role)
		{
			//var categories = _Context.Users.Where(x => x.IsDelete == false).ToList();
			//return categories;



			if (string.IsNullOrWhiteSpace(roleName))
			{

			}

			// Ensure the role exists
			var role = await _Context.Roles.SingleOrDefaultAsync(r => r.Name == roleName);
			if (role == null)
			{

			}

			// Get users in the specified role
			var usersInRole = await (from user in _Context.Users
									 join userRole in _Context.UserRoles on user.Id equals userRole.UserId
									 where userRole.RoleId == role.Id
									 select user).ToListAsync();

			// Return the list of users (you can customize this as needed)
			return usersInRole; // Or return a view, if necessary: return View(usersInRole);

		}
		public async Task<List<AppUser>> GetAllByDepartementasync(string Departemant, string codemarkaz)
		{
			var usersInRoles = await _Context.Users.Where(u => u.CodMarkaz == codemarkaz && u.IsDelete != true)
				.OrderBy(x => x.Family)
				.ToListAsync();
			return usersInRoles;
		}
		public  List<AppUser> GetAllByDepartement(string Departemant)
		{
			var usersInRoles =  _Context.Users.Where(u => (u.Departments == null && !u.IsDelete)
																	|| (u.Departments != null && u.Departments.ToLower() == Departemant.ToLower())
																			&& !u.IsDelete)
														.OrderBy(x => x.Family)
														.ToList();
			return usersInRoles;
		}
		public List<AppUser> GetAll(string roleName1, string roleName2)
		{
			// Handle case where no roles are specified
			if (string.IsNullOrWhiteSpace(roleName1) && string.IsNullOrWhiteSpace(roleName2))
			{
				return _Context.Users
												.Where(x => x.IsDelete == false)
												.ToList();
			}

			// Get roles by names
			var roles = _Context.Roles
												.Where(r => r.Name == roleName1 || r.Name == roleName2 || r.Name == "Admin")
												.ToList();

			// Check if roles exist
			if (!roles.Any())
			{
				return new List<AppUser>();
			}

			// Get role IDs
			var roleIds = roles.Select(r => r.Id).ToList();

			// Get user IDs for the specified roles
			var userIds = _Context.UserRoles
													.Where(ur => roleIds.Contains(ur.RoleId))
													.Select(ur => ur.UserId)
													.Distinct()
													.ToList();

			// Get users based on user IDs
			var usersInRoles = _Context.Users
														.Where(u => userIds.Contains(u.Id) && !u.IsDelete)
														.OrderBy(x => x.Family)
														.ToList();

			return usersInRoles;
		}
		public AppUser? GetById(string Id)
		{
			var category = _Context.Users.Include(u => u.WeighbridgeSiteUsers).FirstOrDefault(x => x.Id == Id);
			return category;
		}
	 	public async Task<AppUser?> GetByIdAsync(string Id)
		{
            return await _Context.Users.FirstOrDefaultAsync(x => x.Id == Id);
		}
        public void Update(UsersListDomainViewModel category)
        {
            var model = _Context.Users
    .Include(u => u.WeighbridgeSiteUsers) // Needed to access and modify WeighbridgeSiteUsers
    .FirstOrDefault(u => u.Id == category.Id);


            if (model != null)
            {
                model.Name = category.Name;
                model.Family = category.Family;
                model.Mobile = category.Mobile;
                model.UserName = category.UserName;
				model.CodMarkaz = category.Company;
				model.Token = category.Token;
				model.WindowsToken = category.WindowsToken;
                var toRemove = model.WeighbridgeSiteUsers.ToList();
                _Context.WeighbridgeSiteUsers.RemoveRange(toRemove);

                foreach (var site in category.SelectedSiteIds)
                {
                    model.WeighbridgeSiteUsers.Add(new WeighbridgeSiteUser
                    {
                        SiteId = site,
                        UserId = category.Id,
                        AssignedAt = DateTime.Now
                    });
                }
                _Context.SaveChanges();
            }
        }

        public void UpdateUserRole(string user_id,string new_role_id)
		{
			var model = _Context.UserRoles.Where(x => x.UserId == user_id ).FirstOrDefault();
			if(model != null)
			{
				_Context.UserRoles.Remove(model);
				_Context.UserRoles.Add(new IdentityUserRole<string> { RoleId = new_role_id, UserId = user_id });

				_Context.SaveChanges();

			}
			else
			{
				_Context.UserRoles.Add(new IdentityUserRole<string> { RoleId = new_role_id, UserId = user_id });
				_Context.SaveChanges();

			}
		}

		public AppUser GetUserByEmail(string Email)
        {
			return _Context.Users.SingleOrDefault(u => u.Email == Email && u.IsDelete == false);
        }

        public string GetRole_by_user_id(string user_Id)
        {
            var dRid = _Context.UserRoles.FirstOrDefault(x => x.UserId == user_Id);
			if (dRid == null)
				return "";
			var s = _Context.Roles.FirstOrDefault(x => x.Id == dRid.RoleId);
			if (s == null)
				return "";
            return s.Name;
        }
		public string GetRole_id_by_name(string role_name)
		{
			var dRid = _Context.Roles.FirstOrDefault(x => x.NormalizedName == role_name.ToUpper());
			if(dRid != null)
				return dRid.Id;
			else
				return "";
		}
		public IEnumerable<ObjectForm> GetObjectForm()
		{
			return _Context.ObjectForms
				.OrderBy(x => x.GroupName)
				.ThenBy(x => x.Id)
				.ToList();
		}

		public IEnumerable<ObjectFormUser> get_all_object_user_access(string UserId)
		{
			return _Context.ObjectFormUsers.AsQueryable().Where(x => x.User_ == UserId);

		}

		public bool delete_all_object_for_user(string UserId)
		{

			 _Context.ObjectFormUsers.RemoveRange(_Context.ObjectFormUsers.Where(x => x.User_ == UserId));
			_Context.SaveChanges();
			return true;
		}

		public bool add_objectform_for_user(List<ObjectFormUser> objectForm)
		{
			 _Context.ObjectFormUsers.AddRange(objectForm);
			_Context.SaveChanges();
			return true;
		}

		public bool user_access_form(string from,string UserId) 
		{
		 var objectForm =	_Context.ObjectForms.FirstOrDefault(x =>x.Name == from );
			if (objectForm != null)
			{
				  var ofu =_Context.ObjectFormUsers.FirstOrDefault(x => x.ObjectFormId == objectForm.Id && x.User_ == UserId);
				if(ofu != null)
				{
					return true;
				}

			}
			return false;
		}

		public IEnumerable<ObjectForm> GetObjectFormByDepartement(string Departement)
		{
			var query = _Context.ObjectForms.AsQueryable();
			if (!string.IsNullOrWhiteSpace(Departement))
			{
				query = query.Where(x => x.Departement == Departement || x.Departement == null || x.Departement == "");
			}

			return query
				.OrderBy(x => x.GroupName)
				.ThenBy(x => x.Id)
				.ToList();

		}

        public List<WeighbridgeSiteDomainViewModel> GetAllActiveSites(string userId)
        {
            var user = _Context.Users
                .Include(u => u.WeighbridgeSiteUsers)
                    .ThenInclude(us => us.WeighbridgeSite)
                .FirstOrDefault(u => u.Id == userId);

            if (user == null)
                return new List<WeighbridgeSiteDomainViewModel>();

            var sites = user.WeighbridgeSiteUsers
                .Where(us => us.WeighbridgeSite.isActive)
                .Select(us => new WeighbridgeSite
                {
                    ID = us.WeighbridgeSite.ID,
                    name = us.WeighbridgeSite.name
                })
                .ToList();
			var model = _mapper.Map<List<WeighbridgeSiteDomainViewModel>>(sites);
			return model;
        }

        public async Task<bool> SaveSelectedSiteAsync(int siteId, string userId)
        {
            var user = await _Context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return false;

            // Check that the site exists and is active
            var siteExists = await _Context.WeighbridgeSites.AnyAsync(s => s.ID == siteId && s.isActive);
            if (!siteExists)
                return false;

            // Set selected site
            user.SelectedSiteId = siteId;

            // Check if user already has access to this site
            var alreadyLinked = await _Context.WeighbridgeSiteUsers
                .AnyAsync(us => us.UserId == userId && us.SiteId == siteId);

            if (!alreadyLinked)
            {
                _Context.WeighbridgeSiteUsers.Add(new WeighbridgeSiteUser
                {
                    UserId = userId,
                    SiteId = siteId
                });
            }

            await _Context.SaveChangesAsync();
            return true;
        }

        public async Task<List<UsersListDomainViewModel>> GetUsersBySite(int siteid, string codemarkaz)
        {
            _Context.Database.SetCommandTimeout(180); // 180 seconds
            var list = await _Context.Users
    .Include(u => u.WeighbridgeSiteUsers)
    .Where(u => u.CodMarkaz == codemarkaz &&
                u.WeighbridgeSiteUsers.Any(us => us.SiteId == siteid))
    .ToListAsync();
			var users = new List<UsersListDomainViewModel>();
            foreach (var item in list)
            {
				users.Add(new UsersListDomainViewModel
				{
					Id = item.Id,
					Name = item.Name,
					UserName = item.UserName,
					Family = item.Family
				});
            }
            return users;
        }

		public async Task<IEnumerable<ObjectForm?>> GetObjectFormByDepartementAsync(string Departement)
		{
			var query = _Context.ObjectForms.AsQueryable();
			if (!string.IsNullOrWhiteSpace(Departement))
			{
				query = query.Where(x => x.Departement == Departement || x.Departement == null || x.Departement == "");
			}

			return await query
				.OrderBy(x => x.GroupName)
				.ThenBy(x => x.Id)
				.ToListAsync();

		}

        public async Task UpdateUserWindowsToken(string token, string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return;

            user.WindowsToken = token;

            await _userManager.UpdateAsync(user);
        }
    }
}







