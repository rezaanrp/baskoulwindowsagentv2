using Application.Classes;
using Application.Interfaces;
using Application.Services;
using Application.ViewModels.Weighbridge;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Wordprocessing;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace WebUI.Controllers
{
    [Authorize]
    public class BargeBaskoulController : BaseController
    {
        private readonly IBargeBaskoul _bargebaskoulservice;
        private readonly IWeighbridgeService _baskoulService;
        private readonly IUsersService _userservice;

        public BargeBaskoulController(IBargeBaskoul bargebaskoulservice, IWeighbridgeService baskoulService,
            UserManager<AppUser> userManager, IUsersService userservice) : base(userManager)
        {
            _bargebaskoulservice = bargebaskoulservice;
            _baskoulService = baskoulService;
            _userManager = userManager;
            _userservice = userservice;
        }

        [HttpGet]
        public IActionResult BargeAnbar()
        {
            return RedirectToAction("Index", "BaskoulVue");
        }

        [HttpGet]
        public async Task<IActionResult> BargeAnbarLegacy(int type = 3, int page = 1, int pageSize = 10, 
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
        type, codeMarkaz, user.SelectedSiteId.Value, searchTerm, page, pageSize, sortColumn, sortDirection);

            var Weighbridges = await _baskoulService.GetBySiteAsync(user.SelectedSiteId ?? 0, user.CodMarkaz);

            var model = new BargeAnbarViewModel
            {
                Baskouls = Weighbridges,
                Codemarkaz = codeMarkaz,
                BargeBaskouls = pagedResult.bargeBaskoulViews,
                BargeAnbar = new BargeBaskoulViewModel
                {
                    isManual = true,
                    TypeBarge = null,
                    DateBarge = ToPersianDate(DateTime.Now),
                    DateBaskol = ToPersianDate(DateTime.Now),
                    TimeBarge = DateTime.Now.ToString("HH:mm:ss"),
                    TimeBaskol = DateTime.Now.ToString("HH:mm:ss"),
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

        [HttpGet]
        public async Task<IActionResult> LookupPlateWeight(string plate)
        {
            var user = _userservice.GetById(OnGetUserId());
            if (user.SelectedSiteId == null)
            {
                return BadRequest(new { success = false, message = "سایت فعالی برای کاربر انتخاب نشده است" });
            }

            var codeMarkaz = user.CodMarkaz;
            var normalizedPlate = (plate ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(normalizedPlate))
            {
                return Json(new { success = false, hasPrevious = false, previousWeight = 0 });
            }

            var record = await _bargebaskoulservice.GetLatestByPlateAsync(codeMarkaz, user.SelectedSiteId.Value, normalizedPlate);
            if (record == null)
            {
                return Json(new { success = true, hasPrevious = false, previousWeight = 0 });
            }

            var previousWeight = record.VanKhali.HasValue && record.VanKhali.Value > 0
                ? record.VanKhali.Value
                : record.VaznPor ?? 0;
            var vaznBarge = record.VaznPor.HasValue && record.VanKhali.HasValue
                ? Math.Abs(record.VaznPor.Value - record.VanKhali.Value)
                : (float?)null;
            var mabanis = await _bargebaskoulservice.GetAllMabanisAsync(codeMarkaz);
            var driver = record.IDRanande.HasValue
                ? mabanis.FirstOrDefault(m => m.TableName == "Ranande" && m.IDLinq == record.IDRanande.Value)
                : null;

            return Json(new
            {
                success = true,
                hasPrevious = true,
                hasIncomplete = true,
                id = record.ID,
                plate = record.ShomareMashin,
                previousWeight,
                vaznPor = record.VaznPor,
                vanKhali = record.VanKhali,
                vaznBarge,
                ghabzBaskolId = record.GhabzBaskolID,
                driverId = driver?.ID,
                driverName = record.OnvanRanandeh ?? driver?.Onvan,
                isManualDriver = !record.IDRanande.HasValue,
                dateBarge = record.DateBarge,
                timeBarge = record.TimeBarge
            });
        }

        [HttpPost]
        public async Task<IActionResult> BargeAnbar(BargeBaskoulViewModel entity, int page = 1, int pageSize = 10)
        {
            var user = _userservice.GetById(OnGetUserId());
            var codeMarkaz = _userservice.GetCodMarkazById(OnGetUserId());
            int siteId = (int)user.SelectedSiteId;
            if (entity == null) return RedirectToAction("Error", "Home");
            entity.CodMarkaz = codeMarkaz;
            entity.siteId = siteId;
            ValidateManualWeight(entity);
            entity.TypeBarge ??= InferBargeType(entity.VaznPor, entity.VanKhali);
            // Reload all necessary data
            var bargebaskouls = await _bargebaskoulservice.GetAllAsync(codeMarkaz, siteId,page, pageSize);
            if (bargebaskouls.bargeBaskoulViews == null) bargebaskouls.bargeBaskoulViews =  new List<BargeBaskoulViewModel>();
            var Weighbridges = await _baskoulService.GetBySiteAsync(user.SelectedSiteId??0, user.CodMarkaz);
            entity.Mabanis = await _bargebaskoulservice.GetAllMabanisAsync(codeMarkaz);

            var model = new BargeAnbarViewModel
            {
                BargeBaskouls = bargebaskouls.bargeBaskoulViews,
                Baskouls = Weighbridges,
                Codemarkaz = codeMarkaz
            };

            if (!ModelState.IsValid)
            {
                model.BargeAnbar = entity;
                ViewBag.ErrorMessage = "ذخیره با خطا مواجه شد لطفا دوباره تلاش کنید!";
                model.BargeAnbar.TypeBarge = entity.TypeBarge;
                model.BargeAnbar.DateBarge = NormalizePersianDate(entity.DateBarge);
                model.BargeAnbar.DateBaskol = NormalizePersianDate(entity.DateBaskol);
                model.BargeAnbar.TimeBarge = entity.TimeBarge ?? DateTime.Now.ToString("HH:mm:ss");
                model.BargeAnbar.TimeBaskol = entity.TimeBaskol ?? DateTime.Now.ToString("HH:mm:ss");
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

            try
            {
                var completedPreviousWeight = await _bargebaskoulservice.AddOrCompleteByPlateAsync(entity);
                TempData["BargeSuccessMessage"] = completedPreviousWeight
                    ? "وزن دوم ثبت و برگه تکمیل شد؛ آماده ثبت خودروی بعدی"
                    : "وزن اول ثبت شد؛ آماده ثبت خودروی بعدی";
            }
            catch (Exception)
            {
                model.BargeAnbar = entity;
                ViewBag.ErrorMessage = "ذخیره برگه انجام نشد؛ اطلاعات فرم حفظ شده است.";
                ViewBag.totalPages = (int)Math.Ceiling((double)bargebaskouls.TotalCount / pageSize);
                ViewBag.totalEntries = bargebaskouls.TotalCount;
                ViewBag.startEntry = ((page - 1) * pageSize) + 1;
                ViewBag.endEntry = Math.Min(page * pageSize, bargebaskouls.TotalCount);
                ViewBag.type = entity.TypeBarge;
                ViewBag.currentPage = page;
                ViewBag.codeMarkaz = codeMarkaz;
                ViewBag.siteId = user.SelectedSiteId;
                ViewBag.EditOrSubmit = 1;
                return View(model);
            }

            return RedirectToAction(nameof(BargeAnbar));
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

            var result = await _bargebaskoulservice.GetFilteredAsyncbyType(type, codeMarkaz, user.SelectedSiteId.Value, searchTerm, page,
                pageSize, sortColumn, sortDirection);
            int totalPages = (int)Math.Ceiling((double)result.TotalCount / pageSize);

            result.TotalPages = totalPages;
            result.StartEntry = ((page - 1) * pageSize) + 1;
            result.EndEntry = Math.Min(page * pageSize, result.TotalCount);
            result.Type = type;
            result.CurrentPage = page;
            result.SearchTerm = searchTerm;
            result.Company = codeMarkaz;
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
            var Weighbridges = await _baskoulService.GetBySiteAsync(user.SelectedSiteId ?? 0, user.CodMarkaz);
            barg.Mabanis = await _bargebaskoulservice.GetAllMabanisAsync(barg.CodMarkaz);
            barg.DateBarge = NormalizePersianDate(barg.DateBarge);
            barg.DateBaskol = NormalizePersianDate(barg.DateBaskol);
            int totalPages = (int)Math.Ceiling((double)bargebaskouls.TotalCount / pageSize);
            var model = new BargeAnbarViewModel
            {
                Baskouls = Weighbridges,
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
            ValidateManualWeight(entity);
            entity.TypeBarge ??= InferBargeType(entity.VaznPor, entity.VanKhali);
            // Reload all necessary data
            var codeMarkaz = entity.CodMarkaz;
            var bargebaskouls = await _bargebaskoulservice.GetAllAsync(codeMarkaz, (int)user.SelectedSiteId, page, pageSize);
            if (bargebaskouls.bargeBaskoulViews == null) bargebaskouls.bargeBaskoulViews = new List<BargeBaskoulViewModel>();
            var Weighbridges = await _baskoulService.GetBySiteAsync(user.SelectedSiteId ?? 0, user.CodMarkaz);

            var model = new BargeAnbarViewModel
            {
                BargeBaskouls = bargebaskouls.bargeBaskoulViews,
                Baskouls = Weighbridges,
            };

            if (!ModelState.IsValid)
            {
                model.BargeAnbar = entity;
                model.BargeAnbar.DateBarge = NormalizePersianDate(entity.DateBarge);
                model.BargeAnbar.DateBaskol = NormalizePersianDate(entity.DateBaskol);
                model.BargeAnbar.TimeBarge = entity.TimeBarge ?? DateTime.Now.ToString("HH:mm:ss");
                model.BargeAnbar.TimeBaskol = entity.TimeBaskol ?? DateTime.Now.ToString("HH:mm:ss");
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

                if (entity.FlgSabt == true && entity.VaznPor == null)
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
            var user = _userservice.GetById(OnGetUserId());
            codeMarkaz = user.CodMarkaz;
            siteId = user.SelectedSiteId ?? 0;
            var data = new PagedResultBarge();
            if (string.IsNullOrEmpty(searchTerm))
                data = await _bargebaskoulservice.GetAllAsync(codeMarkaz, siteId, page, pageSize);
            else
                data = await _bargebaskoulservice.GetFilteredAsyncbyType(type, codeMarkaz, user.SelectedSiteId.Value, searchTerm, page, pageSize, sortColumn, sortDirection);

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

        private static string ToPersianDate(DateTime date)
        {
            var calendar = new PersianCalendar();
            return $"{calendar.GetYear(date):0000}/{calendar.GetMonth(date):00}/{calendar.GetDayOfMonth(date):00}";
        }

        private static string NormalizePersianDate(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return ToPersianDate(DateTime.Now);
            }

            var normalized = value.Trim().Replace("-", "/");
            var parts = normalized.Split('/', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 3
                && int.TryParse(parts[0], out var year)
                && int.TryParse(parts[1], out var month)
                && int.TryParse(parts[2], out var day))
            {
                if (year >= 1700)
                {
                    return ToPersianDate(new DateTime(year, month, day));
                }

                return $"{year:0000}/{month:00}/{day:00}";
            }

            return normalized;
        }

        private static int? InferBargeType(float? currentWeight, float? previousWeight)
        {
            if (!currentWeight.HasValue || !previousWeight.HasValue)
            {
                return null;
            }

            if (currentWeight.Value <= 0 || previousWeight.Value <= 0 || currentWeight.Value == previousWeight.Value)
            {
                return null;
            }

            return previousWeight.Value > currentWeight.Value ? 1 : 2;
        }

        private void ValidateManualWeight(BargeBaskoulViewModel entity)
        {
            if (!entity.UseManualWeight)
            {
                return;
            }

            if (!entity.ManualWeight.HasValue || entity.ManualWeight.Value <= 0)
            {
                ModelState.AddModelError(nameof(entity.ManualWeight), "وزن دستی باید یک عدد مثبت باشد.");
            }
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

