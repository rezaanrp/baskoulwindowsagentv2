using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class CompanyTreeViewModel
    {
        public List<CompanyNodeViewModel> Companies { get; set; } = new();
        public List<CompanyUserOptionViewModel> Users { get; set; } = new();
    }

    public class CompanyNodeViewModel
    {
        public int Id { get; set; }
        public string? CompanyCode { get; set; }
        public string? CompanyName { get; set; }
        public string? ConnectionUrl { get; set; }
        public string? ApiUrl { get; set; }
        public bool AutoSync { get; set; }
        public List<CompanySiteNodeViewModel> WeighbridgeSites { get; set; } = new();
    }

    public class CompanySiteNodeViewModel
    {
        public int Id { get; set; }
        public string? SiteName { get; set; }
        public string? CompanyCode { get; set; }
        public bool IsActive { get; set; }
        public List<CompanyScaleNodeViewModel> Scales { get; set; } = new();
        public List<CompanyUserOptionViewModel> Users { get; set; } = new();
    }

    public class CompanyScaleNodeViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ScaleCode { get; set; }
        public int? Type { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
    }

    public class CompanyUserOptionViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public string? DisplayName { get; set; }
    }

    public class CompanyInputViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "نام شرکت را وارد کنید")]
        public string CompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "کد شرکت را وارد کنید")]
        public string CompanyCode { get; set; } = string.Empty;

        public string? ConnectionUrl { get; set; }
        public string? ApiUrl { get; set; }
        public bool AutoSync { get; set; }
    }

    public class CompanySiteInputViewModel
    {
        public int Id { get; set; }

        [Required]
        public string CompanyCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "نام سایت را وارد کنید")]
        public string SiteName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }

    public class CompanyScaleInputViewModel
    {
        public int Id { get; set; }

        [Required]
        public string CompanyCode { get; set; } = string.Empty;

        [Required]
        public int SiteId { get; set; }

        [Required(ErrorMessage = "نام باسکول را وارد کنید")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "کد باسکول را وارد کنید")]
        public string ScaleCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "کاربر باسکول را انتخاب کنید")]
        public string UserId { get; set; } = string.Empty;

        public int? Type { get; set; }
    }

    public class CompanyScaleUserInputViewModel
    {
        [Required]
        public string CompanyCode { get; set; } = string.Empty;

        [Required]
        public int SiteId { get; set; }

        [Required(ErrorMessage = "کاربر باسکول را انتخاب کنید")]
        public string UserId { get; set; } = string.Empty;
    }
    public class CompanyScaleUserDeleteViewModel
    {
        [Required]
        public string CompanyCode { get; set; } = string.Empty;

        [Required]
        public int SiteId { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;
    }
}
