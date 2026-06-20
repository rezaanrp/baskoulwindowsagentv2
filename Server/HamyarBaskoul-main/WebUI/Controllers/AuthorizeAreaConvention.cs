using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{

    public class AuthorizeAreaConvention : IControllerModelConvention
    {
        private readonly string _area;
        private readonly string[] _roles;

        public AuthorizeAreaConvention(string area, params string[] roles)
        {
            _area = area;
            _roles = roles;
        }

        public void Apply(ControllerModel controller)
        {
            var areaName = controller.Attributes
                .OfType<AreaAttribute>()
                .FirstOrDefault()?.RouteValue;

            if (string.Equals(areaName, _area, StringComparison.OrdinalIgnoreCase))
            {
                controller.Filters.Add(new AuthorizeFilter(
                    new AuthorizationPolicyBuilder()
                        .RequireRole(_roles)
                        .Build()
                ));
            }
        }
    }

}
