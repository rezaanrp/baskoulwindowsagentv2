using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Application.Common.Interfaces
{
    public interface IWriteDbContext
    {
        DbSet<Weighbridge> Weighbridges { get; set; }
        DbSet<BargeBaskoul> BargeBaskouls { get; set; }
        DbSet<Mabani> Mabanis { get; set; }
        DbSet<Company> Companies { get; set; }
        DbSet<BaseTable> BaseTables { get; set; }
        DbSet<AppUser> Users { get; set; }
        DbSet<ObjectForm> ObjectForms { get; set; }
        DbSet<ObjectFormUser> ObjectFormUsers { get; set; }
        DbSet<UserSiteAccess> UserSiteAccesses { get; set; }
        DbSet<WeighbridgeSite> WeighbridgeSites { get; set; }
        DbSet<GhabzSerialTracker> GhabzSerialTrackers { get; set; }
        DbSet<ReportSetting> ReportSettings { get; set; }
        DbSet<Settings> Settings { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<T> ExecuteInTransactionAsync<T>(
            Func<CancellationToken, Task<T>> operation,
            IsolationLevel isolationLevel = IsolationLevel.Serializable,
            CancellationToken cancellationToken = default);
    }
}

