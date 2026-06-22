using Domain.Models;
using Domain.ViewModels.Baskoul;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IWriteDbContext
    {
        DbSet<Baskoul> baskouls { get; set; }
        DbSet<BargeBaskoul> BargeBaskouls { get; set; }
        DbSet<Mabani> Mabanis { get; set; }
        DbSet<CodeMarkaz> CodeMarkazs { get; set; }
        DbSet<BaseTable> BaseTables { get; set; }
        DbSet<ObjectForm> ObjectForms { get; set; }
        DbSet<ObjectFormUser> ObjectFormUsers { get; set; }
        DbSet<Site> Sites { get; set; }
        DbSet<UserSite> UserSites { get; set; }
        DbSet<GhabzSerialTracker> GhabzSerialTrackers { get; set; }
        DbSet<ReportSetting> ReportSettings { get; set; }
        DbSet<Settings> Settings { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

