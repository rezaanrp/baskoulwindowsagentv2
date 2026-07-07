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
using System.Text.Json;


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
            var activeSites = _userservise.GetAllActiveSites(user.Id).ToList();

            if (user.SelectedSiteId == null || activeSites.All(site => site.ID != user.SelectedSiteId.Value))
            {
                if (activeSites.Any())
                {
                    return RedirectToAction("SelectSite", "WeighbridgeSite");
                }

                TempData["NullSelectedSite"] = "سایت فعالی در دسترسی‌های شما تعریف نشده است.";
                return View();
            }

            var selectedSite = activeSites.FirstOrDefault(site => site.ID == user.SelectedSiteId.Value);
            ViewBag.DefaultSiteName = GetSiteDisplayName(selectedSite?.name, selectedSite?.CompanyName, selectedSite?.Company);
            ViewBag.DefaultSiteId = user.SelectedSiteId.Value;
            ViewBag.AvailableSitesJson = JsonSerializer.Serialize(activeSites.Select(site => new
            {
                id = site.ID,
                name = GetSiteDisplayName(site.name, site.CompanyName, site.Company)
            }));
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

        private static string GetSiteDisplayName(string? siteName, string? companyName, string? codeMarkaz)
        {
            var name = siteName ?? string.Empty;
            var company = !string.IsNullOrWhiteSpace(companyName) ? companyName : codeMarkaz;
            return string.IsNullOrWhiteSpace(company) ? name : $"{name} - {company}";
        }
    }
}
