using Application.Common.Interfaces;
using Domain.Models;
using Domain.ViewModels.Baskoul;
using Infra.Data.Seed;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infra.Data.Context
{
    public class WriteDbContext : IdentityDbContext<AppUser>, IDataProtectionKeyContext, IReadDbContext, IWriteDbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public WriteDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) 
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public DbSet<Baskoul> baskouls { get; set; }
        public DbSet<BargeBaskoul> BargeBaskouls { get; set; }
        public DbSet<Mabani> Mabanis { get; set; }
        public DbSet<CodeMarkaz> CodeMarkazs { get; set; }
        public DbSet<BaseTable> BaseTables { get; set; }
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
        public DbSet<ObjectForm> ObjectForms { get; set; }
        public DbSet<ObjectFormUser> ObjectFormUsers { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<UserSite> UserSites { get; set; }
        public DbSet<GhabzSerialTracker> GhabzSerialTrackers { get; set; }
        public DbSet<ReportSetting> ReportSettings { get; set; }
        public DbSet<Settings> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplyQueryFilters(modelBuilder);

            ConfigureDefaultValues(modelBuilder);

            modelBuilder.Entity<GhabzSerialTracker>()
    .HasKey(x => new { x.CodMarkaz, x.Year });

            // Configurations for AppUser
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.Family).HasMaxLength(100);
                entity.Property(e => e.Mobile).HasMaxLength(11).IsRequired(false);
                entity.Property(e => e.PersonnelCode).HasMaxLength(10).IsRequired(false);
                entity.Property(e => e.IsDelete).HasDefaultValue(false);
            });

            modelBuilder.Entity<Baskoul>()
        .HasOne(b => b.User)
        .WithMany() 
        .HasForeignKey(b => b.UserID);

            modelBuilder.Entity<DataProtectionKey>().ToTable("DataProtectionKeys");
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;
                if (clrType.GetProperty("CodMarkaz") == null && clrType.GetProperty("CodeMarkaz") == null)
                {
                    modelBuilder.Entity(clrType)
                        .Property<string>("CodMarkaz")
                        .IsRequired(false);
                }
            }
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(AuditableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType).Property<string>("CreateIp").HasMaxLength(50);
                    modelBuilder.Entity(entityType.ClrType).Property<string>("ModifyIp").HasMaxLength(50);
                }
            }
            base.OnModelCreating(modelBuilder);

            new Seeder(modelBuilder).Seed();
            new SeedObject(modelBuilder).Seed();

            modelBuilder.Entity<UserSite>()
        .HasKey(us => us.ID); // You can also use composite key if preferred

            modelBuilder.Entity<UserSite>()
                .HasOne(us => us.User)
                .WithMany(u => u.UserSites)
                .HasForeignKey(us => us.UserId);

            modelBuilder.Entity<UserSite>()
                .HasOne(us => us.Site)
                .WithMany(s => s.UserSites)
                .HasForeignKey(us => us.SiteId);

            modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = "55e62da2-2c86-4438-afdd-4025c16802d4"
            },
            new IdentityRole
            {
                Id = "cbc43a8e-f7bb-4445-baaf-1add431ffbbf",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
                ConcurrencyStamp = "dde4cd0a-c55c-4c1b-874d-8d2e33c0c7eb"
            },
            new IdentityRole
            {
                Id = "abc12def-1234-4567-89ab-1234567890ab",
                Name = "SuperAdmin",
                NormalizedName = "SUPERADMIN",
                ConcurrencyStamp = "39a9015e-1958-405b-a4ea-25bf2249e783"
			},
             new IdentityRole
             {
                 Id = "abc12def-1234-8954-89ab-1234567890ab",
                 Name = "NonHamyarUser",
                 NormalizedName = "NONHAMYARUSER",
                 ConcurrencyStamp = "ae356140-5de4-4a67-b818-e1021aeacfcf"
			 },
              new IdentityRole
              {
                  Id = "abc12def-1234-2548-89ab-1234567890ab",
                  Name = "NonHamyarAdmin",
                  NormalizedName = "NONHAMYARADMIN",
                  ConcurrencyStamp = "55a33924-8173-4f79-b41e-d438d1eada2a"
			  }
        );

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(
                    new AppUser
                    {
                        Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                        Email = "admin@localhost.com",
                        NormalizedEmail = "ADMIN@LOCALHOST.COM",
                        UserName = "admin@localhost.com",
                        Name = "Manager",
                        Family = "Manager",
                        Mobile = "0",
                        NormalizedUserName = "ADMIN@LOCALHOST.COM",
                        PasswordHash = "AQAAAAEAACcQAAAAEBVmwtKAZCwB5vQrSQOzAGjo6EQItNmhaLdwnEPd4Q4N7aoTBiYmDdUqTSLS3wHMtg==",
                        EmailConfirmed = true,
                        ConcurrencyStamp = "7de05ab0-564d-49de-986a-bbf977e70579",
                        CodMarkaz = "1",
                        SecurityStamp = "af18f0b4-8f6d-4e16-b956-750144f3e4d0"

                        //"ConcurrencyStamp", "PasswordHash", "SecurityStamp"
                    },
                    new AppUser
                    {
                        Id = "9e224968-33e4-4652-b7b7-8574d048cdb9",
                        Email = "user@localhost.com",
                        NormalizedEmail = "USER@LOCALHOST.COM",
                        UserName = "user@localhost.com",
                        Name = "User",
                        Family = "User",
                        Mobile = "0",
                        NormalizedUserName = "USER@LOCALHOST.COM",
                        PasswordHash = "AQAAAAEAACcQAAAAENu/R9ZIq/ANzxX5EQy0MN86ie8onZipiGbGNTkHOi6mwycIuAEW7fK1AEf2atQJwA==",
                        EmailConfirmed = true,
                        ConcurrencyStamp = "457de0cb-4367-4939-b5eb-542191a662b1",
                        SecurityStamp = "8b5c7ddc-4304-4f49-a415-41430ac47561"
                    }
                );

            modelBuilder.Entity<CodeMarkaz>().HasData(
                   new CodeMarkaz
                   {
                       CodMarkaz = "1",
                       Id = 1,
                       CoName = "شرکت بین‌المللی سیستم‌ها و اتوماسیون (ایریسا)"
                   }
               );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData
               (
                new IdentityUserRole<string> { RoleId = "abc12def-1234-4567-89ab-1234567890ab", UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                new IdentityUserRole<string> { RoleId = "cac43a6e-f7bb-4448-baaf-1add431ccbbf", UserId = "9e224968-33e4-4652-b7b7-8574d048cdb9" }
                );

            //	modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "117d1b41-6753-4622-89e6-8126a3b7d3f0", ConcurrencyStamp = "da7a4f42-ff3c-42a8-935a-62af68f978b0", Name = "Admin", NormalizedName = "Admin".ToUpper() });
        }

        private void ConfigureRelationships(ModelBuilder modelBuilder)
        {
            throw new NotImplementedException();
        }

        private void ConfigureDefaultValues(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var createdDateProperty = entityType.FindProperty("CreatedDate");
                if (createdDateProperty != null && createdDateProperty.ClrType == typeof(DateTime))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property<DateTime>("CreatedDate")
                        .HasDefaultValueSql("getdate()");
                }

                var modifiedDateProperty = entityType.FindProperty("ModifiedDate");
                if (modifiedDateProperty != null && modifiedDateProperty.ClrType == typeof(DateTime))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property<DateTime>("ModifiedDate")
                        .HasDefaultValueSql("getdate()");
                }
            }
        }

        private void ApplyQueryFilters(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var isDeletedProperty = entityType.FindProperty("IsDeleted");
                if (isDeletedProperty != null && isDeletedProperty.ClrType == typeof(bool))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var filter = Expression.Lambda(
                        Expression.Equal(
                            Expression.Property(parameter, isDeletedProperty.PropertyInfo),
                            Expression.Constant(false)),
                        parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
                }
            }
        }

        public override int SaveChanges()
        {
            SetIpInfo();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetIpInfo();
            return await base.SaveChangesAsync(cancellationToken);
        }


        private void SetIpInfo()
        {
            var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();

            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreateIp = ip;
                    entry.Entity.ModifyIp = ip;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifyIp = ip;
                }
            }
        }
    }
}



