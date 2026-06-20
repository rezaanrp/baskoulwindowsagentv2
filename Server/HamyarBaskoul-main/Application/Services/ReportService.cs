using Application.Interfaces;
using Application.ViewModels;
using Application.ViewModels.Reports;
using AutoMapper;
using Domain.Interfaces;
using Domain.ViewModels;

namespace Application.Services
{
    public class ReportService : IReports
    {
        private readonly IReportsRepository _reportrepo;
        private readonly IMapper _mapper;
        private readonly ICodeMarkaz _codeMarkaz;

        public ReportService(IReportsRepository reportrepo, IMapper mapper, ICodeMarkaz codeMarkaz)
        {
            _reportrepo = reportrepo;
            _mapper = mapper;
            _codeMarkaz = codeMarkaz;
        }

        public async Task<bool> SaveReportSettings(ReportSettingViewModel model)
        {
            if (model.Logo != null && model.Logo.Length > 0)
            {
                var extension = Path.GetExtension(model.Logo.FileName);
                var fileName = $"{model.CodeMarkaz}{extension}";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Logo.CopyToAsync(stream);
                }
                var appname = await _codeMarkaz.GetAppNameByCode(model.CodeMarkaz);
                model.LogoPath = $"/{appname}/uploads/{fileName}"; 
            }
            model.DateIns = DateTime.Now;
            model.DateUp = DateTime.Now;
            var entity = _mapper.Map<ReportSettingDomainViewModel>(model);
            return await _reportrepo.SaveReportSettings(entity);
        }

        public async Task<ReportSettingViewModel> GetReportSettingByCodMarkaz(string codMarkaz)
        {
            var entity = await _reportrepo.GetReportSettingByCodMarkaz(codMarkaz);
            var model = _mapper.Map<ReportSettingViewModel>(entity);
            return model;
        }

        public async Task<SimpleReportViewModel> GetSimpleReportViewModel(int id)
        {
            var entity = await _reportrepo.GetSimpleReportViewModel(id);
            var model = _mapper.Map<SimpleReportViewModel>(entity);
            var vazn = model.VaznPor - model.VaznKhali - (model.VazneBasteBandi == null ? 0 : model.VazneBasteBandi);
            model.Vaznekhlaes = vazn.ToString();
            return model;
        }

        public async Task<TripleReportViewModel> GetTripleleReportViewModel(int id, string username)
        {
            var entity = await _reportrepo.GetTripleReportViewModel(id, username);
            if (entity == null) return null;
            var model = _mapper.Map<TripleReportViewModel>(entity);
            var vazn = model.VaznPor - model.VaznKhali - (model.VazneBasteBandi == null ? 0 : model.VazneBasteBandi);
            model.Vaznekhlaes = vazn.ToString();
            return model;
        }
    }
}
