using AutoMapper;
using Domain.Models;
using Application.ViewModels.Users;
using Domain.ViewModels.Users;
using Domain.ViewModels.BaseTable;
using Application.ViewModels.BaseTable;
using Domain.ViewModels.Baskoul;
using Application.ViewModels.Baskoul;
using Domain.ViewModels;
using Application.ViewModels;
using Application.ViewModels.Reports;
using Domain.ViewModels.Reports;
using Domain.ViewModels.APIs;
using Application.ViewModels.APIs;
using Domain.ViewModels.BaseData;
using Application.ViewModels.BaseData;
namespace Application.Profiles
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
			#region BaseTable
			CreateMap<BaseTableViewModel, BaseTable>();
				CreateMap<BaseTable, BaseTableViewModel>();

				CreateMap<BaseTableDomainViewModel, BaseTable>();
				CreateMap<BaseTable, BaseTableDomainViewModel>();

				CreateMap<BaseTableDomainViewModel, BaseTableViewModel>();
				CreateMap<BaseTableViewModel, BaseTableDomainViewModel>();
            #endregion

            #region Baskoul
            CreateMap<BaskoulViewModel, Baskoul>();
            CreateMap<Baskoul, BaskoulViewModel>();

            CreateMap<BaskoulDomainViewModel, Baskoul>();
            CreateMap<Baskoul, BaskoulDomainViewModel>();

            CreateMap<BaskoulDomainViewModel, BaskoulViewModel>();
            CreateMap<BaskoulViewModel, BaskoulDomainViewModel>();
            #endregion
            #region BargeBaskoul

            CreateMap<BargeBaskoulViewModel, BargeBaskoulDomainViewModel>();
            CreateMap<BargeBaskoulDomainViewModel, BargeBaskoulViewModel>();

            CreateMap<BargeBaskoulDomainViewModel, BargeBaskoul>();
            CreateMap<BargeBaskoul, BargeBaskoulDomainViewModel>();
            
            CreateMap<BargeBaskoulViewModel, BargeBaskoul>();
            CreateMap<BargeBaskoul, BargeBaskoulViewModel>();

            #endregion

            #region Mabani

            CreateMap<MabaniViewModel, MabaniDomainViewModel>();
            CreateMap<MabaniDomainViewModel, MabaniViewModel>();

            CreateMap<MabaniDomainViewModel, Mabani>();
            CreateMap<Mabani, MabaniDomainViewModel>();

            CreateMap<MabaniViewModel, Mabani>();
            CreateMap<Mabani, MabaniViewModel>();

            #endregion
			#region Users
			CreateMap<AppUser, UsersListViewModel>();
			CreateMap<UsersListDomainViewModel, UsersListViewModel>();
			CreateMap<UsersListViewModel, UsersListDomainViewModel>();

            #endregion

            #region Site

            CreateMap<SiteDomainViewModel, SiteViewModel>();
            CreateMap<SiteViewModel, SiteDomainViewModel>();

            CreateMap<SiteDomainViewModel, Site>();
            CreateMap<Site, SiteDomainViewModel>();

            CreateMap<SiteViewModel, Site>();
            CreateMap<Site, SiteViewModel>();

            #endregion

            #region CodeMarkaz

            CreateMap<CodeMarkaz, CodeMarkazViewModel>();
            CreateMap<CodeMarkazViewModel, CodeMarkaz>();

            CreateMap<CodeMarkazViewModel, CodeMarkazDomainViewModel>();
            CreateMap<CodeMarkazDomainViewModel, CodeMarkazViewModel>();

            CreateMap<CodeMarkazDomainViewModel, CodeMarkaz>();
            CreateMap<CodeMarkaz, CodeMarkazDomainViewModel>();

            #endregion

            #region ReportSetting

            CreateMap<ReportSetting, ReportSettingDomainViewModel>();
            CreateMap<ReportSettingDomainViewModel, ReportSetting>();

            CreateMap<ReportSetting, ReportSettingViewModel>();
            CreateMap<ReportSettingViewModel, ReportSetting>();

            CreateMap<ReportSettingViewModel, ReportSettingDomainViewModel>();
            CreateMap<ReportSettingDomainViewModel, ReportSettingViewModel>();

            #endregion

            #region Reports

            CreateMap<SimpleReportViewModel, SimpleReportDomainViewModel>();
            CreateMap<SimpleReportDomainViewModel, SimpleReportViewModel>();

            CreateMap<TripleReportDomainViewModel, TripleReportViewModel>();
            CreateMap<TripleReportViewModel, TripleReportDomainViewModel>();

            #endregion

            #region APIs

            CreateMap<BargiriDomainViewModel, BargiriViweModel>();
            CreateMap<BargiriViweModel, BargiriDomainViewModel>();

            CreateMap<TakhlieDomainViewModel, TakhlieViewModel>();
            CreateMap<TakhlieViewModel, TakhlieDomainViewModel>();

            CreateMap<SyncServerDomainViewModel, SyncServerViewModel>();
            CreateMap<SyncServerViewModel, SyncServerDomainViewModel>();

            #endregion

            #region BaseData

            CreateMap<BaseDataDmainViewModel, BaseDataViewModel>();
            CreateMap<BaseDataViewModel, BaseDataDmainViewModel>();


            CreateMap<BaseDataDmainViewModel, Mabani>();
            CreateMap<Mabani, BaseDataDmainViewModel>();

            CreateMap<Mabani, BaseDataViewModel>();
            CreateMap<BaseDataViewModel, Mabani>();

            #endregion
        }
    }
}





