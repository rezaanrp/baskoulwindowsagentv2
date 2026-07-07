using AutoMapper;
using Domain.Models;
using Application.ViewModels.Users;
using Domain.ViewModels.Users;
using Domain.ViewModels.BaseTable;
using Application.ViewModels.BaseTable;
using Domain.ViewModels.Weighbridge;
using Application.ViewModels.Weighbridge;
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

            #region Weighbridge
            CreateMap<WeighbridgeViewModel, Weighbridge>()
                .ForMember(destination => destination.WeighbridgeSite, options => options.Ignore());
            CreateMap<Weighbridge, WeighbridgeViewModel>();

            CreateMap<WeighbridgeDomainViewModel, Weighbridge>()
                .ForMember(destination => destination.WeighbridgeSite, options => options.Ignore());
            CreateMap<Weighbridge, WeighbridgeDomainViewModel>();

            CreateMap<WeighbridgeDomainViewModel, WeighbridgeViewModel>();
            CreateMap<WeighbridgeViewModel, WeighbridgeDomainViewModel>();
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

            #region WeighbridgeSite

            CreateMap<WeighbridgeSiteDomainViewModel, WeighbridgeSiteViewModel>();
            CreateMap<WeighbridgeSiteViewModel, WeighbridgeSiteDomainViewModel>();

            CreateMap<WeighbridgeSiteDomainViewModel, WeighbridgeSite>()
                .ForMember(destination => destination.Company, options => options.Ignore())
                .ForMember(destination => destination.CompanyId, options => options.Ignore())
                .ForMember(destination => destination.Weighbridges, options => options.Ignore());
            CreateMap<WeighbridgeSite, WeighbridgeSiteDomainViewModel>()
                .ForMember(destination => destination.Company, options => options.MapFrom(source => source.Company == null ? null : source.Company.CodMarkaz))
                .ForMember(destination => destination.CompanyName, options => options.MapFrom(source => source.Company == null ? null : source.Company.CoName));

            CreateMap<WeighbridgeSiteViewModel, WeighbridgeSite>()
                .ForMember(destination => destination.Company, options => options.Ignore())
                .ForMember(destination => destination.CompanyId, options => options.Ignore())
                .ForMember(destination => destination.Weighbridges, options => options.Ignore());
            CreateMap<WeighbridgeSite, WeighbridgeSiteViewModel>()
                .ForMember(destination => destination.Company, options => options.MapFrom(source => source.Company == null ? null : source.Company.CodMarkaz))
                .ForMember(destination => destination.CompanyName, options => options.MapFrom(source => source.Company == null ? null : source.Company.CoName));

            #endregion

            #region Company

            CreateMap<Company, CompanyViewModel>();
            CreateMap<CompanyViewModel, Company>();

            CreateMap<CompanyViewModel, CompanyDomainViewModel>();
            CreateMap<CompanyDomainViewModel, CompanyViewModel>();

            CreateMap<CompanyDomainViewModel, Company>();
            CreateMap<Company, CompanyDomainViewModel>();

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





