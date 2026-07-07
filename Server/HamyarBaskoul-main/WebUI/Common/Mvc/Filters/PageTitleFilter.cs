using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace WebUI.Common.Mvc.Filters
{
    public class PageTitleFilter : IAsyncActionFilter
    {
        private readonly IReadDbContext _db;

        private static readonly Dictionary<string, string> RouteMenuKeys = new(StringComparer.OrdinalIgnoreCase)
        {
            ["BargeBaskoul.BargeAnbar"] = "BargeAnbar",
            ["BargeBaskoul.ListBargeAnbar"] = "ListBargeAnbar",
            ["BargeBaskoul.UploadExcel"] = "UploadExcel",
            ["APIs.SyncServer"] = "SyncServer",
            ["Reports.ReportSetting"] = "ReportSetting",
            ["WeighbridgeSite.SiteList"] = "SiteList",
            ["WeighbridgeSite.SelectSite"] = "SelectSite",
            ["BaseData.DataList"] = "DataList",
            ["BaseData.ShowList"] = "DataList",
            ["WindowsAPIs.Index"] = "WindowsAPIs",
            ["UserManager.Index"] = "UserManager",
            ["UserManager.Insert"] = "UserManager",
            ["UserManager.Edit"] = "UserManager",
            ["UserManager.ResetUserPassword"] = "UserManager",
            ["WeighbridgeOrganization.CodeURLList"] = "CodeURLList",
            ["WeighbridgeOrganization.InsertURLMarkaz"] = "CodeURLList",
            ["WeighbridgeOrganization.Edit"] = "CodeURLList"
        };

        private static readonly Dictionary<string, string> FallbackTitles = new(StringComparer.OrdinalIgnoreCase)
        {
            ["Home.Index"] = "صفحه اصلی"
        };

        public PageTitleFilter(IReadDbContext db)
        {
            _db = db;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var executedContext = await next();

            if (executedContext.Controller is not Controller controller)
            {
                return;
            }

            var controllerName = executedContext.RouteData.Values["controller"]?.ToString() ?? string.Empty;
            var actionName = executedContext.RouteData.Values["action"]?.ToString() ?? string.Empty;
            var routeKey = $"{controllerName}.{actionName}";
            var menuKey = string.Equals(routeKey, "UserManager.Index", StringComparison.OrdinalIgnoreCase) &&
                string.Equals(executedContext.HttpContext.Request.Query["mode"].ToString(), "password", StringComparison.OrdinalIgnoreCase)
                    ? "ChangePassword"
                    : null;

            if (string.IsNullOrWhiteSpace(menuKey))
            {
                RouteMenuKeys.TryGetValue(routeKey, out menuKey);
            }

            if (!string.IsNullOrWhiteSpace(menuKey))
            {
                var title = await _db.ObjectForms
                    .Where(x => x.Name == menuKey)
                    .Select(x => x.NameFarsi)
                    .FirstOrDefaultAsync();

                controller.ViewData["Title"] = string.IsNullOrWhiteSpace(title) ? menuKey : title;
                return;
            }

            if (string.IsNullOrWhiteSpace(controller.ViewData["Title"]?.ToString()) &&
                FallbackTitles.TryGetValue(routeKey, out var fallbackTitle))
            {
                controller.ViewData["Title"] = fallbackTitle;
            }
        }
    }
}
