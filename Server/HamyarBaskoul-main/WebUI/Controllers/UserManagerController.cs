using Application.Dtos;
using Application.Interfaces;
using Application.Services;
using Application.ViewModels.Users;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace WebUI.Controllers
{
    [Authorize(Roles = "Administrator,SuperAdmin, NonHamyarAdmin")]
    public class UserManagerController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly ISite siteService;
        public UserManagerController(IUsersService _usersService
            , UserManager<AppUser> userManager, ISite site
            ) : base(userManager)
        {
            this.usersService = _usersService;
            _userManager = userManager;
            siteService = site;
        }
        [Authorize(Roles = "Administrator,SuperAdmin, NonHamyarAdmin")]
        [HttpGet]
        public IActionResult Index(string id)
        {
			ObjectFormListViewModel objectForm = usersService.GetUserFormAccessList(id);

			return View(objectForm);
        }
		[HttpPost]
		public IActionResult Index(ObjectFormListViewModel objectForm)
		{
			//usersService.edit_object_access_for_user(objectForm,OnGetUserId());
			return View();
		}

		[HttpGet]
        public async Task<JsonResult> load_data_users()
        {
            try
            {
                var user = usersService.GetById(OnGetUserId());
                var m = await usersService.GetAllByDepartementasync(user.Departments ?? "", user.CodMarkaz);

                var result = m.Select(u => new UserDto
                {
                    Name = u.Name,
                    Id = u.Id,
                    userName = u.UserName,
                    Family = u.Family,
                    Mobile = u.Mobile,
                    IsDelete = u.IsDelete,
                    CodMarkaz = u.CodMarkaz,
                    SelectedSiteId = u.SelectedSiteId
                }).ToList();

                return Json(new { data = result });
            }
            catch (Exception ex)
            {
                // Log or return debug info for Release mode
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert(InsertUsersListViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Extract URL from request
                string fullUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}{Request.Path}{Request.QueryString}";
                string pathBase = Request.PathBase.Value; // "/irisa"
                string appname = pathBase.Trim('/');
                //var appname = "irisa";

                var m = new AppUser();
                var codemarkaz = usersService.GetCodMarkazByURL(appname);
                if (string.IsNullOrEmpty(codemarkaz))
                {
                    TempData["InvalidCodMrakaz"] = "ابتدا کد مرکز را تعریف کنید";
                    return View(model);
                }
                if (await _userManager.FindByNameAsync(model.UserName) == null)
                {
                    m.UserName = model.UserName;
                    m.Name = model.Name;
                    m.Family = model.Family;
                    m.Mobile = model.Mobile;
                    m.Email = model.UserName;
                    m.PersonnelCode = "0";
                    m.EmailConfirmed = true;
                    m.LockoutEnabled = false;
                    m.Token = model.Token;
                    m.CodMarkaz = codemarkaz;
                    //m.WindowsToken = model.WindowsToken;
                    m.UserSites = model.SelectedSiteIds.Select(siteId => new UserSite
                    {
                        SiteId = siteId,
                        AssignedAt = DateTime.Now
                    }).ToList();

                    var result = await _userManager.CreateAsync(m, model.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(m, model.Role);
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ViewBag.ErrorMessag = error.Description;
                        }
                    }
                    return RedirectToAction("Index", "UserManager");
                }
                return RedirectToAction("Index", "UserManager");
			}
			else
            {
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Insert()
        {
            var model = new InsertUsersListViewModel();
			//if (User.IsInRole("SuperAdmin"))
			//{
			//	model.AllMarkazes = usersService.get.GetAllMarkazes()
			//		.ToDictionary(m => m.CodMarkaz, m => m.MarkazName);
			//}
			var codeMarkaz = usersService.GetCodMarkazById(OnGetUserId());
            model.AvailableSites = siteService.GetAllActiveAsync(codeMarkaz);
            var user = usersService.GetById(OnGetUserId());
            model.UserRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UsersListViewModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var entry in ModelState)
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        ViewBag.ErrorMessage = error.ErrorMessage;
                    }
                }
                var usm = usersService.Get(model.Id);
                var codeMarkaz = usersService.GetCodMarkazById(OnGetUserId());
                usm.ActiveSites = siteService.GetAllActiveAsync(codeMarkaz);
                return View(usm);
            }

            if (!string.IsNullOrEmpty(model.Role))
            {
                var user = usersService.GetById(model.Id);
                if (user == null)
                {
                    ModelState.AddModelError("", "کاربر پیدا نشد.");
                    return View(model);
                }
                var currentRoles = await _userManager.GetRolesAsync(user);

                if (!currentRoles.Contains("SuperAdmin"))
                {
                    await _userManager.RemoveFromRolesAsync(user, currentRoles);

                    var addRoleResult = await _userManager.AddToRoleAsync(user, model.Role);
                    if (!addRoleResult.Succeeded)
                    {
                        foreach (var error in addRoleResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View(model);
                    }
                }
            }
            usersService.UpdateUser(model);
            return RedirectToAction("Index", "UserManager");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
			var usm = usersService.Get(id);
            var codeMarkaz = usersService.GetCodMarkazById(OnGetUserId());
            usm.ActiveSites = siteService.GetAllActiveAsync(codeMarkaz);
            usm.CodeMarkaz = codeMarkaz;
            var user = await _userManager.FindByIdAsync(id);
            usm.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            return View(usm);
        }
		[Authorize(Roles = "Administrator,SuperAdmin, NONHAMYAADMIN")]
		[HttpGet]
        public IActionResult ResetUserPassword(string id)
        {
			SetPasswordViewModel set = new SetPasswordViewModel();
            set.Id = id;
			return View(set);
        }
		[HttpPost]
		public async Task<IActionResult> ResetUserPassword(SetPasswordViewModel setPasswordViewModel)
		{
            if (!ModelState.IsValid)
            {
                foreach (var entry in ModelState)
                {
                    var key = entry.Key;
                    var errors = entry.Value.Errors;
                    foreach (var error in errors)
                    {
                        Infra.Data.Classes.Logger.LogToFile($"reset password falied: {error}");
                    }
                }

                return View(setPasswordViewModel);
            }

            var user = await _userManager.FindByIdAsync(setPasswordViewModel.Id);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var addPasswordResult = await _userManager.ResetPasswordAsync(user, token, setPasswordViewModel.Password);
            //	var addPasswordResult = await _userManager.Reset(user, setPasswordViewModel.Password);
            if (!addPasswordResult.Succeeded)
            {
                foreach (var error in addPasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View();
            }
            return RedirectToAction("Index");
		}
		[HttpPost]
        [ActionName("DeleteUser")]
        public IActionResult DeleteUser(AppUser input)
        {
			usersService.DeleteUser(input.Id.ToString());
            if (input != null)
            {

                return Json(new { result = true, msg = "با موفقیت حذف گردید" });

            }
            else
            {
                return Json(new { result = false, msg = "موردی برای حذف انتخاب نشده است" });
            }
        }

        public async Task<IActionResult> GetUsersBySite(int siteid)
        {
            var user = usersService.GetById(OnGetUserId());
            if (user == null) return null;
            // Fetch baskoul data from DB
            var users = await usersService.GetUsersBySite(siteid, user.CodMarkaz);
            foreach (var item in users)
            {
                var us = usersService.GetById(item.Id);
                var role = await _userManager.GetRolesAsync(us);

                if (role.Contains("Administrator"))
                    item.Role = "مدیر سیستم";
                else if (role.Contains("User"))
                    item.Role = "کاربر عادی";
            }
            ViewBag.SiteName = await siteService.GetNameById(siteid);

            return PartialView("_UsersTablePartial", users);
        }
    }
}



