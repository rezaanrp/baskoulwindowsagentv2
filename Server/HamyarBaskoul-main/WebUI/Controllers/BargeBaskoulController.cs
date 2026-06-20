using Application.Classes;
using Application.Interfaces;
using Application.Services;
using Application.ViewModels.Baskoul;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Wordprocessing;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Authorize]
    public class BargeBaskoulController : BaseController
    {
        private readonly IBargeBaskoul _bargebaskoulservice;
        private readonly IBaskoulService _baskoulService;
        private readonly IUsersService _userservice;

        public BargeBaskoulController(IBargeBaskoul bargebaskoulservice, IBaskoulService baskoulService,
            UserManager<AppUser> userManager, IUsersService userservice) : base(userManager)
        {
            _bargebaskoulservice = bargebaskoulservice;
            _baskoulService = baskoulService;
            _userManager = userManager;
            _userservice = userservice;
        }
        [HttpGet]
        public async Task<IActionResult> BargeAnbar(int type = 3, int page = 1, int pageSize = 10, 
            string searchTerm = "", string sortColumn = "", string sortDirection = "")
        {
            var user = _userservice.GetById(OnGetUserId());
            if (user.SelectedSiteId == null)
            {
                TempData["NullSelectedSite"] = "سایت فعالی برای کاربر انتخاب نشده است";
                return RedirectToAction("Index", "Home");
            }

            var codeMarkaz = user.CodMarkaz;
            var pagedResult = await _bargebaskoulservice.GetFilteredAsyncbyType(
        type, codeMarkaz, searchTerm, page, pageSize, sortColumn, sortDirection);

            var baskouls = await _baskoulService.GetBySiteAsync(user.SelectedSiteId ?? 0, user.CodMarkaz);

            var model = new BargeAnbarViewModel
            {
                Baskouls = baskouls,
                Codemarkaz = codeMarkaz,
                BargeBaskouls = pagedResult.bargeBaskoulViews,
                BargeAnbar = new BargeBaskoulViewModel
                {
                    isManual = true,
                    TypeBarge = type,
                    Mabanis = await _bargebaskoulservice.GetAllMabanisAsync(codeMarkaz),
                    CodMarkaz = codeMarkaz,
                    siteId = user.SelectedSiteId
                },
                TotalPages = (int)Math.Ceiling(pagedResult.TotalCount / (double)pageSize),
                CurrentPage = page
            };

            ViewBag.EditOrSubmit = 1;
            int totalPages = (int)Math.Ceiling((double)pagedResult.TotalCount / pageSize);
            ViewBag.totalPages = totalPages;
            ViewBag.totalEntries = pagedResult.TotalCount;
            ViewBag.startEntry = ((page - 1) * pageSize) + 1;
            ViewBag.endEntry = Math.Min(page * pageSize, pagedResult.TotalCount);
            ViewBag.type = type;
            ViewBag.currentPage = page;
            ViewBag.codeMarkaz = model.Codemarkaz;
            ViewBag.siteId = user.SelectedSiteId;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> BargeAnbar(BargeBaskoulViewModel entity, int page = 1, int pageSize = 10)
        {
            var user = _userservice.GetById(OnGetUserId());
            var codeMarkaz = _userservice.GetCodMarkazById(OnGetUserId());
            int siteId = (int)user.SelectedSiteId;
            if (entity == null) return RedirectToAction("Error", "Home");
            // Reload all necessary data
            var bargebaskouls = await _bargebaskoulservice.GetAllAsync(codeMarkaz, siteId,page, pageSize);
            if (bargebaskouls.bargeBaskoulViews == null) bargebaskouls.bargeBaskoulViews =  new List<BargeBaskoulViewModel>();
            var baskouls = await _baskoulService.GetBySiteAsync(user.SelectedSiteId??0, user.CodMarkaz);

            var model = new BargeAnbarViewModel
            {
                BargeBaskouls = bargebaskouls.bargeBaskoulViews,
                Baskouls = baskouls,
                Codemarkaz = codeMarkaz
            };

            if (!ModelState.IsValid)
            {
                model.BargeAnbar = new BargeBaskoulViewModel();
                if (entity.TypeBarge == null)
                    ViewBag.ErrorMessage = "لطفا نوع برگه را مشخص کنید!";
                else
                { 
                    ViewBag.ErrorMessage = "ذخیره با خطا مواجه شد لطفا دوباره تلاش کنید!";
                    model.BargeAnbar.TypeBarge = entity.TypeBarge;
                }
                int totalPages = (int)Math.Ceiling((double)bargebaskouls.TotalCount / pageSize);
                ViewBag.totalPages = totalPages;
                ViewBag.totalEntries = bargebaskouls.TotalCount;
                ViewBag.startEntry = ((page - 1) * pageSize) + 1;
                ViewBag.endEntry = Math.Min(page * pageSize, bargebaskouls.TotalCount);
                ViewBag.type = model.BargeAnbar.TypeBarge;
                ViewBag.currentPage = page;
                ViewBag.codeMarkaz = model.BargeAnbar.CodMarkaz;
                ViewBag.siteId = user.SelectedSiteId;
                return View(model); // Return the full model to reload the view
            }

            // Save to database
            await _bargebaskoulservice.AddBargeBaskoulAsync(entity);

            // Redirect to GET method to reload fresh data
            return RedirectToAction("ListBargeAnbar");
        }
        [HttpGet]   
        public async Task<IActionResult> ListBargeAnbar(int type = 3, int page = 1, int pageSize = 10,
            string searchTerm = "", string sortColumn = "", string sortDirection = "")
        {
            var user = _userservice.GetById(OnGetUserId());
            if (user.SelectedSiteId == null)
            {
                TempData["NullSelectedSite"] = "سایت فعالی برای کاربر انتخاب نشده است";
                return RedirectToAction("Index", "Home");
            }

            var codeMarkaz = _userservice.GetCodMarkazById(OnGetUserId());

            var result = await _bargebaskoulservice.GetFilteredAsyncbyType(type, codeMarkaz, searchTerm, page,
                pageSize, sortColumn, sortDirection);
            int totalPages = (int)Math.Ceiling((double)result.TotalCount / pageSize);

            result.TotalPages = totalPages;
            result.StartEntry = ((page - 1) * pageSize) + 1;
            result.EndEntry = Math.Min(page * pageSize, result.TotalCount);
            result.Type = type;
            result.CurrentPage = page;
            result.SearchTerm = searchTerm;
            result.CodeMarkaz = codeMarkaz;
            result.SiteId = user.SelectedSiteId;
            result.SortColumn = sortColumn;
            result.SortDirection = sortDirection;
            if (type == 4)
            {
                TempData["ReportError"] = "لطفا ابتدا تنظیمات چاپ را مشخص کنید";
                ViewBag.type = 3;
            }
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                // AJAX request: return partial view
                return PartialView("_BargeAnbarTable", result);
            }
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> EditBargeBaskoul(int id, int page = 1, int pageSize = 10)
        {
            var user = _userservice.GetById(OnGetUserId());
            var barg = await _bargebaskoulservice.GetBargeBaskoul(id);
            if (barg == null)
            {
                return NotFound();
            }
            var bargebaskouls = await _bargebaskoulservice.GetAllAsync(barg.CodMarkaz, (int)user.SelectedSiteId, page, pageSize);
            if(bargebaskouls.bargeBaskoulViews == null) bargebaskouls.bargeBaskoulViews = new List<BargeBaskoulViewModel>();
            var baskouls = await _baskoulService.GetBySiteAsync(user.SelectedSiteId ?? 0, user.CodMarkaz);
            barg.Mabanis = await _bargebaskoulservice.GetAllMabanisAsync(barg.CodMarkaz);
            int totalPages = (int)Math.Ceiling((double)bargebaskouls.TotalCount / pageSize);
            var model = new BargeAnbarViewModel
            {
                Baskouls = baskouls,
                BargeBaskouls = bargebaskouls.bargeBaskoulViews,
                BargeAnbar = barg,
                TotalPages = totalPages,
                Codemarkaz = barg.CodMarkaz,
                CurrentPage = page
            };
            ViewBag.totalPages = totalPages;
            ViewBag.totalEntries = bargebaskouls.TotalCount;
            ViewBag.startEntry = ((page - 1) * pageSize) + 1;
            ViewBag.endEntry = Math.Min(page * pageSize, bargebaskouls.TotalCount);
            ViewBag.type = barg.TypeBarge;
            ViewBag.currentPage = page;
            ViewBag.codeMarkaz = barg.CodMarkaz;
            ViewBag.siteId = user.SelectedSiteId;
            // submit = 1, edit = 2
            ViewBag.EditOrSubmit = 2;
            return View("BargeAnbar", model);
        }
        [HttpPost]
        public async Task<IActionResult> EditBargeBaskoul(BargeBaskoulViewModel entity, bool SubmitFinal = false, int page = 1, int pageSize = 10)
        {
            var user = _userservice.GetById(OnGetUserId());
            if (entity == null) return RedirectToAction("Error", "Home");
            // Reload all necessary data
            var codeMarkaz = entity.CodMarkaz;
            var bargebaskouls = await _bargebaskoulservice.GetAllAsync(codeMarkaz, (int)user.SelectedSiteId, page, pageSize);
            if (bargebaskouls.bargeBaskoulViews == null) bargebaskouls.bargeBaskoulViews = new List<BargeBaskoulViewModel>();
            var baskouls = await _baskoulService.GetBySiteAsync(user.SelectedSiteId ?? 0, user.CodMarkaz);

            var model = new BargeAnbarViewModel
            {
                BargeBaskouls = bargebaskouls.bargeBaskoulViews,
                Baskouls = baskouls,
            };

            if (!ModelState.IsValid)
            {
                model.BargeAnbar = new BargeBaskoulViewModel
                {
                    TypeBarge = entity.TypeBarge
                };
                int totalPages = (int)Math.Ceiling((double)bargebaskouls.TotalCount / pageSize);
                ViewBag.ErrorMessage = "ذخیره با خطا مواجه شد. دوباره سعی کنید!";
                ViewBag.totalPages = totalPages;
                ViewBag.totalEntries = bargebaskouls.TotalCount;
                ViewBag.startEntry = ((page - 1) * pageSize) + 1;
                ViewBag.endEntry = Math.Min(page * pageSize, bargebaskouls.TotalCount);
                ViewBag.type = model.BargeAnbar.TypeBarge;
                ViewBag.currentPage = page;
                ViewBag.codeMarkaz = model.BargeAnbar.CodMarkaz;
                ViewBag.siteId = user.SelectedSiteId;
                ViewBag.EditOrSubmit = 2;
                return View("BargeAnbar", model); // Return the full model to reload the view
            }

            // Save to database
            await _bargebaskoulservice.UpdateBargeBaskoulAsync(entity);

            // final submit logic
            if (SubmitFinal)
            {
                bool result = await _bargebaskoulservice.SabtBargeAnbar((int)entity.ID, OnGetUserId());

                if (entity.FlgSabt == true && (entity.VanKhali == null || entity.VaznPor == null))
                {
                    TempData["ErrorMessage"] = "عملیات توزین برگه کامل نشده است!";
                    return RedirectToAction("EditBargeBaskoul", new { id = entity.ID });
                }

                TempData["SuccessMessage"] = "برگه انبار با موفقیت تایید شد.";
            }
            // Redirect to GET method to reload fresh data
            return RedirectToAction("ListBargeAnbar", new { type = 3 });
        }
        [HttpPost]
        public async Task<IActionResult> EbtalBarge(int bargId)
        {
            bool result = await _bargebaskoulservice.EbtalBargeAnbar(bargId, OnGetUserId());
            if (result) return Ok();
            else return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> SabtBarge(int bargId)
        {
            bool result = await _bargebaskoulservice.SabtBargeAnbar(bargId, OnGetUserId());
            if (result) return Ok();
            else return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> PartialBargeBaskoul(string searchTerm, string codeMarkaz, int siteId, int type = 3, int page = 1, int pageSize = 10,
            string sortColumn = "", string sortDirection = "")
        {
            var data = new PagedResultBarge();
            if (string.IsNullOrEmpty(searchTerm))
                data = await _bargebaskoulservice.GetAllAsync(codeMarkaz, siteId, page, pageSize);
            else
                data = await _bargebaskoulservice.GetFilteredAsyncbyType(type, codeMarkaz, searchTerm, page, pageSize, sortColumn, sortDirection);

            var user = _userservice.GetById(OnGetUserId());

            var model = new BargeAnbarViewModel
            {
                BargeBaskouls = data.bargeBaskoulViews ?? new List<BargeBaskoulViewModel>(),
                BargeAnbar = new BargeBaskoulViewModel { TypeBarge = type, CodMarkaz = codeMarkaz },
                Codemarkaz = codeMarkaz
            };

            int totalPages = (int)Math.Ceiling((double)data.TotalCount / pageSize);
            ViewBag.ErrorMessage = "ذخیره با خطا مواجه شد. دوباره سعی کنید!";
            ViewBag.totalPages = totalPages;
            ViewBag.totalEntries = data.TotalCount;
            ViewBag.startEntry = ((page - 1) * pageSize) + 1;
            ViewBag.endEntry = Math.Min(page * pageSize, data.TotalCount);
            ViewBag.type = type;
            ViewBag.currentPage = page;
            ViewBag.codeMarkaz = codeMarkaz;
            ViewBag.siteId = user.SelectedSiteId;

            return PartialView("_BargiriTakhlieCard", model);
        }

        [HttpGet]
        public IActionResult UploadExcel()
        {
            return View();
        }

        [HttpPost("/upload")]
        public async Task<IActionResult> UploadFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null && uploadedFile.Length > 0)
            {
                using (var stream = uploadedFile.OpenReadStream())
                {
                    var user = _userservice.GetById(OnGetUserId());
                    // Pass the stream to your service here
                    var result = await _bargebaskoulservice.SaveToDBFromExcelFile(stream, uploadedFile.FileName, user.CodMarkaz);

                    if (result)
                        return Ok();
                    else
                        return BadRequest("Error uploading file");
                }
            }
            return BadRequest("No file uploaded.");
        }
    }
}
