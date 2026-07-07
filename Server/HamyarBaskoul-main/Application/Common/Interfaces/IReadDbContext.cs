using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IReadDbContext
    {
        DbSet<Weighbridge> Weighbridges { get; }
        DbSet<BargeBaskoul> BargeBaskouls { get; }
        DbSet<Mabani> Mabanis { get; }
        DbSet<Company> Companies { get; }
        DbSet<BaseTable> BaseTables { get; }
        DbSet<AppUser> Users { get; }
        DbSet<ObjectForm> ObjectForms { get; }
        DbSet<ObjectFormUser> ObjectFormUsers { get; }
        DbSet<WeighbridgeSite> WeighbridgeSites { get; }
        DbSet<WeighbridgeSiteUser> WeighbridgeSiteUsers { get; }
        DbSet<GhabzSerialTracker> GhabzSerialTrackers { get; }
        DbSet<ReportSetting> ReportSettings { get; }
        DbSet<Settings> Settings { get; }
    }
}

