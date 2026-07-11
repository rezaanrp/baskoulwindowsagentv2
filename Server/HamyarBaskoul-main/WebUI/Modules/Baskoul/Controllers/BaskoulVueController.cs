using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

[Authorize]
public sealed class BaskoulVueController : Controller
{
    [HttpGet("BaskoulVue")]
    public IActionResult Index() => View();
}
