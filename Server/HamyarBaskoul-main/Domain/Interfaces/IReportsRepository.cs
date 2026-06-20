using Domain.ViewModels;
using Domain.ViewModels.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IReportsRepository
    {
        Task<bool> SaveReportSettings(ReportSettingDomainViewModel model);
        Task<ReportSettingDomainViewModel> GetReportSettingByCodMarkaz(string codmarkaz);
        Task<SimpleReportDomainViewModel> GetSimpleReportViewModel(int id);
        Task<TripleReportDomainViewModel> GetTripleReportViewModel(int id, string username);
    }
}
