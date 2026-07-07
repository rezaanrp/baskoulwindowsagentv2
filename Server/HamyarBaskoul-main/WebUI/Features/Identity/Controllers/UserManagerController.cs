using Application.Dtos;
using Application.Interfaces;
using Application.Services;
using Application.ViewModels.Users;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserManagerController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly IWeighbridgeSiteService siteService;
        public UserManagerController(IUsersService _usersService
            , UserManager<AppUser> userManager, IWeighbridgeSiteService site
            ) : base(userManager)
        {
            this.usersService = _usersService;
            _userManager = userManager;
            siteService = site;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Index(string id)
        {
			ObjectFormListViewModel objectForm = string.IsNullOrWhiteSpace(id)
                ? new ObjectFormListViewModel()
                : usersService.GetUserFormAccessList(id);
            var currentUser = usersService.GetById(OnGetUserId());
            var codeMarkaz = usersService.GetCodMarkazById(OnGetUserId());
            var currentUserRole = _userManager.GetRolesAsync(currentUser).GetAwaiter().GetResult().FirstOrDefault();
            var activeSites = siteService.GetAllActiveAsync(codeMarkaz);

            ViewBag.ActiveSitesJson = JsonSerializer.Serialize(activeSites.Select(site => new
            {
                id = site.ID,
                name = site.name
            }));
            ViewBag.RoleOptionsJson = JsonSerializer.Serialize(GetRoleOptions(currentUserRole));
            ViewBag.CurrentUserRole = currentUserRole;
			return View(objectForm);
        }
		[HttpPost]
		public IActionResult Index(ObjectFormListViewModel objectForm)
		{
			usersService.edit_object_access_for_user(objectForm, OnGetUserId());
			return RedirectToAction("Index", "UserManager");
		}

        [HttpGet]
        public IActionResult GetAccess(string id)
        {
            var model = usersService.GetUserFormAccessList(id);
            return Json(new
            {
                success = true,
                userId = model.UserId,
                items = model.objectFormViews.Select(item => new
                {
                    item.Id,
                    item.Name,
                    item.NameFarsi,
                    item.GroupName,
                    item.GroupNameFarsi,
                    item.IsAccess
                })
            });
        }

        [HttpPost]
        public IActionResult SaveAccess([FromBody] ObjectFormListViewModel objectForm)
        {
            if (objectForm == null || string.IsNullOrWhiteSpace(objectForm.UserId))
            {
                return Json(new { success = false, message = "کاربر انتخاب نشده است." });
            }

            usersService.edit_object_access_for_user(objectForm, OnGetUserId());
            return Json(new { success = true, message = "دسترسی‌های کاربر ذخیره شد." });
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
                var currentUser = usersService.GetById(OnGetUserId());
                var codemarkaz = usersService.GetCodMarkazByURL(appname);
                if (string.IsNullOrEmpty(codemarkaz))
                {
                    codemarkaz = currentUser?.CodMarkaz;
                }
                if (string.IsNullOrEmpty(codemarkaz))
                {
                    codemarkaz = usersService.GetCodMarkazById(OnGetUserId());
                }
                if (string.IsNullOrEmpty(codemarkaz))
                {
                    codemarkaz = "1";
                }
                if (string.IsNullOrEmpty(codemarkaz))
                {
                    TempData["InvalidCodMrakaz"] = "ابتدا کد مرکز را تعریف کنید";
                    return RedirectToAction("Index", "UserManager");
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
                    m.Departments = currentUser?.Departments;
                    m.SelectedSiteId = model.SelectedSiteIds.Any()
                        ? model.SelectedSiteIds.First()
                        : null;
                    //m.WindowsToken = model.WindowsToken;
                    m.WeighbridgeSiteUsers = model.SelectedSiteIds.Select(siteId => new WeighbridgeSiteUser
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
                TempData["InvalidCodMrakaz"] = string.Join(" - ", ModelState.Values
                    .SelectMany(value => value.Errors)
                    .Select(error => error.ErrorMessage));
                return RedirectToAction("Index", "UserManager");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Insert()
        {
            var model = new InsertUsersListViewModel();
			//if (User.IsInRole("Admin"))
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
            var existingUser = usersService.GetById(model.Id);
            var currentUser = usersService.GetById(OnGetUserId());
            model.Company = existingUser?.CodMarkaz
                ?? currentUser?.CodMarkaz
                ?? usersService.GetCodMarkazById(OnGetUserId())
                ?? "1";
            usersService.UpdateUser(model);
            return RedirectToAction("Index", "UserManager");
        }

		[HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
			var usm = usersService.Get(id);
            var codeMarkaz = usersService.GetCodMarkazById(OnGetUserId());
            usm.ActiveSites = siteService.GetAllActiveAsync(codeMarkaz);
            usm.Company = codeMarkaz;
            var user = await _userManager.FindByIdAsync(id);
            usm.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            return View(usm);
        }

        [HttpGet]
        public async Task<JsonResult> GetUserForEdit(string id)
        {
            var usm = usersService.Get(id);
            if (usm == null)
            {
                return Json(new { success = false, message = "کاربر پیدا نشد." });
            }

            var codeMarkaz = usersService.GetCodMarkazById(OnGetUserId());
            var activeSites = siteService.GetAllActiveAsync(codeMarkaz);
            var user = await _userManager.FindByIdAsync(id);
            var role = user == null ? string.Empty : (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            return Json(new
            {
                success = true,
                user = new
                {
                    usm.Id,
                    usm.UserName,
                    usm.Name,
                    usm.Family,
                    usm.Mobile,
                    Role = role,
                    usm.Token,
                    usm.WindowsToken,
                    usm.SelectedSiteIds
                },
                sites = activeSites.Select(site => new
                {
                    id = site.ID,
                    name = site.name
                })
            });
        }
		[Authorize(Roles = "Admin")]
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

                if (role.Contains("Admin"))
                    item.Role = "مدیر سیستم";
                else if (role.Contains("User"))
                    item.Role = "کاربر عادی";
            }
            ViewBag.SiteName = await siteService.GetNameById(siteid);

            return PartialView("_UsersTablePartial", users);
        }

        private static IEnumerable<object> GetRoleOptions(string? role)
        {
            return role switch
            {
                "Admin" => new[]
                {
                    new { value = "User", label = "کاربر عادی" },
                    new { value = "Admin", label = "مدیر سیستم" }
                },
                _ => Array.Empty<object>()
            };
        }
    }
}



