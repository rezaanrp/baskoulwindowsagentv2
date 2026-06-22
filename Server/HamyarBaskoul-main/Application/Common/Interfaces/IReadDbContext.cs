using Domain.Models;
using Domain.ViewModels.Baskoul;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IReadDbContext
    {
        DbSet<Baskoul> baskouls { get; }
        DbSet<BargeBaskoul> BargeBaskouls { get; }
        DbSet<Mabani> Mabanis { get; }
        DbSet<CodeMarkaz> CodeMarkazs { get; }
        DbSet<BaseTable> BaseTables { get; }
        DbSet<ObjectForm> ObjectForms { get; }
        DbSet<ObjectFormUser> ObjectFormUsers { get; }
        DbSet<Site> Sites { get; }
        DbSet<UserSite> UserSites { get; }
        DbSet<GhabzSerialTracker> GhabzSerialTrackers { get; }
        DbSet<ReportSetting> ReportSettings { get; }
        DbSet<Settings> Settings { get; }
    }
}

