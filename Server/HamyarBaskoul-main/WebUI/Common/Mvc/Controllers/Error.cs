using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class Error : Controller
    {
        public IActionResult NotFound()
        {
            return View();
        }

        public IActionResult ServerError()
        {
            return View();
        }
    }
}

