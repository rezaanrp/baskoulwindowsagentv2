using Domain.Models;
using Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Data.Seed
{
    public static class ApplicationStartupSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            await NormalizeApplicationRolesAsync(services);
            await SeedMenuAccessAsync(services);
        }

        private static async Task NormalizeApplicationRolesAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();

            await EnsureRoleAsync(roleManager, "Admin");
            await EnsureRoleAsync(roleManager, "User");

            await MoveUsersToRoleAsync(userManager, "SuperAdmin", "Admin");
            await MoveUsersToRoleAsync(userManager, "Administrator", "Admin");
            await MoveUsersToRoleAsync(userManager, "NonHamyarAdmin", "Admin");
            await MoveUsersToRoleAsync(userManager, "NONHAMYAADMIN", "Admin");
            await MoveUsersToRoleAsync(userManager, "NonHamyarUser", "User");
            await MoveUsersToRoleAsync(userManager, "NONHAMYARUSER", "User");

            var adminUser = await userManager.FindByNameAsync("admin@localhost.com");
            if (adminUser != null && !await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            foreach (var roleName in new[] { "SuperAdmin", "Administrator", "NonHamyarAdmin", "NONHAMYAADMIN", "NonHamyarUser", "NONHAMYARUSER" })
            {
                var role = await roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    await roleManager.DeleteAsync(role);
                }
            }
        }

        private static async Task EnsureRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        private static async Task MoveUsersToRoleAsync(UserManager<AppUser> userManager, string oldRoleName, string newRoleName)
        {
            var users = await userManager.GetUsersInRoleAsync(oldRoleName);
            foreach (var user in users)
            {
                if (!await userManager.IsInRoleAsync(user, newRoleName))
                {
                    await userManager.AddToRoleAsync(user, newRoleName);
                }

                await userManager.RemoveFromRoleAsync(user, oldRoleName);
            }
        }

        private static async Task SeedMenuAccessAsync(IServiceProvider services)
        {
            var db = services.GetRequiredService<WriteDbContext>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();

            var menuItems = new[]
            {
                new { Name = "BargeAnbar", NameFarsi = "ثبت برگه باسکول", GroupName = "Operations", GroupNameFarsi = "عملیات باسکول" },
                new { Name = "ListBargeAnbar", NameFarsi = "لیست برگه‌های باسکول", GroupName = "Operations", GroupNameFarsi = "عملیات باسکول" },
                new { Name = "UploadExcel", NameFarsi = "ورود اطلاعات از اکسل", GroupName = "Operations", GroupNameFarsi = "عملیات باسکول" },
                new { Name = "SyncServer", NameFarsi = "همگام‌سازی اطلاعات باسکول", GroupName = "Operations", GroupNameFarsi = "عملیات باسکول" },
                new { Name = "ReportSetting", NameFarsi = "تنظیمات چاپ برگه باسکول", GroupName = "Reports", GroupNameFarsi = "گزارش‌ها" },
                new { Name = "SiteList", NameFarsi = "مدیریت محل‌های باسکول", GroupName = "BaseSettings", GroupNameFarsi = "تنظیمات پایه" },
                new { Name = "SelectSite", NameFarsi = "انتخاب باسکول پیش‌فرض", GroupName = "BaseSettings", GroupNameFarsi = "تنظیمات پایه" },
                new { Name = "DataList", NameFarsi = "اطلاعات پایه باسکول", GroupName = "BaseSettings", GroupNameFarsi = "تنظیمات پایه" },
                new { Name = "WindowsAPIs", NameFarsi = "توکن ارتباط با نمایشگر وزن", GroupName = "BaseSettings", GroupNameFarsi = "تنظیمات پایه" },
                new { Name = "ChangePassword", NameFarsi = "تغییر پسورد", GroupName = "Standalone", GroupNameFarsi = "منوی اصلی" },
                new { Name = "UserManager", NameFarsi = "کاربران و دسترسی‌ها", GroupName = "SystemManagement", GroupNameFarsi = "مدیریت سیستم" },
                new { Name = "CodeURLList", NameFarsi = "تنظیمات مرکز باسکول", GroupName = "SystemManagement", GroupNameFarsi = "مدیریت سیستم" }
            };

            foreach (var menuItem in menuItems)
            {
                var objectForm = await db.ObjectForms.FirstOrDefaultAsync(x => x.Name == menuItem.Name);
                if (objectForm == null)
                {
                    db.ObjectForms.Add(new ObjectForm
                    {
                        UserName = menuItem.Name,
                        Name = menuItem.Name,
                        NameFarsi = menuItem.NameFarsi,
                        GroupName = menuItem.GroupName,
                        GroupNameFarsi = menuItem.GroupNameFarsi,
                        Departement = "Weighbridge"
                    });
                }
                else
                {
                    objectForm.UserName = menuItem.Name;
                    objectForm.NameFarsi = menuItem.NameFarsi;
                    objectForm.GroupName = menuItem.GroupName;
                    objectForm.GroupNameFarsi = menuItem.GroupNameFarsi;
                    objectForm.Departement = "Weighbridge";
                }
            }

            await db.SaveChangesAsync();

            var adminUser = await userManager.FindByNameAsync("admin@localhost.com");
            if (adminUser == null)
            {
                return;
            }

            var allMenuIds = await db.ObjectForms.Select(x => x.Id).ToListAsync();
            var adminMenuIds = await db.ObjectFormUsers
                .Where(x => x.User_ == adminUser.Id)
                .Select(x => x.ObjectFormId)
                .ToListAsync();
            var missingAdminMenuIds = allMenuIds.Except(adminMenuIds).ToList();

            if (missingAdminMenuIds.Any())
            {
                db.ObjectFormUsers.AddRange(missingAdminMenuIds.Select(menuId => new ObjectFormUser
                {
                    User_ = adminUser.Id,
                    ObjectFormId = menuId,
                    Supplier_ = adminUser.Id,
                    CreatedDate = DateTime.Now
                }));
                await db.SaveChangesAsync();
            }
        }
    }
}
