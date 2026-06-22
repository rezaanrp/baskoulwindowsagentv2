using Application.ViewModels;
using Application.ViewModels.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IReports
    {
        Task<bool> SaveReportSettings(ReportSettingViewModel model);
        Task<ReportSettingViewModel> GetReportSettingByCodMarkaz(string codMarkaz);
        Task<SimpleReportViewModel> GetSimpleReportViewModel(int id);
        Task<TripleReportViewModel> GetTripleleReportViewModel(int id, string username);
    }
}

