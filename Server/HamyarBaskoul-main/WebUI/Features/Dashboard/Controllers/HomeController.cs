using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;
using Application.ViewModels.Weighbridge;
using Application.ViewModels;
using Domain.Interfaces;


namespace WebUI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
		private readonly IUsersService _userservise;
        private readonly IWeighbridgeService _baskoulService;
        private readonly IWeighbridgeSiteService _siteService;

		public HomeController(ILogger<HomeController> logger, IWeighbridgeService baskoulService,
			  UserManager<AppUser> userManager, IUsersService userservise, IWeighbridgeSiteService site
			) : base(userManager)
		{
			_userservise = userservise;
            _userManager = userManager;
            _logger = logger;
            _baskoulService = baskoulService;
            _siteService = site;
        }
		[Authorize]
		public async Task<IActionResult> Index()
        {
            var user = _userservise.GetById(OnGetUserId());
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("Admin"))
            {
                return View();
            }
            if (user.SelectedSiteId == null)
            {
                var activeSites = _userservise.GetAllActiveSites(OnGetUserId());
                if (activeSites.Any() && (activeSites.Count() > 1))
                {
                    return RedirectToAction("SelectSite", "WeighbridgeSite", 1);
                }
                else if (activeSites.Count() == 1)
                {
                    var site = activeSites.First();
                    await _userservise.SaveSelectedSiteAsync(site.ID, user.Id);
                    return View();
                }
                else if (!activeSites.Any())
                {
                    var markazSites = _siteService.GetAllActiveAsync(user.CodMarkaz);
                    if(markazSites.Any())
                    {
                        if (roles.Contains("Admin"))
                        {
                            TempData["ErrorMessage"] = "ابتدا یک سایت فعال انتخاب کنید";
                            return RedirectToAction("Edit", "UserManager", new { id = user.Id });
                        }
                    }
                    TempData["ErrorMessage"] = "لطفاً ابتدا یک سایت فعال ایجاد کنید.";
                    return RedirectToAction("SiteList", "WeighbridgeSite");
                }
            }
            return View();
        }
        //admin@localhost.com
        //admin@1010
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
