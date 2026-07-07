using Application.Tools;
using Infra.Data.Context;
using Infra.Data.Seed;
using Microsoft.EntityFrameworkCore;

namespace WebUI.Common.Configuration
{
    public static class WebApplicationExtensions
    {
        public static async Task MigrateAndSeedDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<WriteDbContext>();

            await db.Database.MigrateAsync();
            await ApplicationStartupSeeder.SeedAsync(scope.ServiceProvider);
        }

        public static WebApplication UseWebUiPipeline(this WebApplication app)
        {
            if (app.Environment.IsProduction())
            {
                app.UseHsts();
            }

            app.UseStatusCodePages(context =>
            {
                var response = context.HttpContext.Response;
                if (response.StatusCode == 404)
                {
                    response.Redirect("/Error/NotFound");
                }
                else if (response.StatusCode == 500)
                {
                    response.Redirect("/Error/ServerError");
                }

                return Task.CompletedTask;
            });

            app.UseStaticFiles();
            app.UseCors("AllowAll");
            app.UseRouting();
            app.UseDynamicAppName();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapHub<ReceiveWeightFromScale>("/{appName}/hubs/weight");
            app.MapHub<ReceiveWeightFromScale>("/hubs/weight");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            return app;
        }

        private static IApplicationBuilder UseDynamicAppName(this IApplicationBuilder app)
        {
            return app.Use(async (context, next) =>
            {
                var path = context.Request.Path.Value;

                if (!string.IsNullOrEmpty(path))
                {
                    var segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
                    if (segments.Length > 0)
                    {
                        context.Items["OriginalRequestPath"] = $"{context.Request.Path}{context.Request.QueryString}";
                        context.Items["AppName"] = segments[0];
                        context.Request.Path = "/" + string.Join('/', segments.Skip(1));
                    }
                }

                await next();
            });
        }
    }
}
