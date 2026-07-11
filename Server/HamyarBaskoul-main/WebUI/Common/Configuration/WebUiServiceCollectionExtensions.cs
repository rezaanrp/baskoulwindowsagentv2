using System.Text;
using Application.Services;
using Application.Tools;
using Domain.Models;
using Infra.Data.Classes;
using Infra.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using WebUI.Common.Mvc.Filters;
using WebUI.Controllers;
using Application.Common.Interfaces;
using WebUI.Common.Security;

namespace WebUI.Common.Configuration
{
    public static class WebUiServiceCollectionExtensions
    {
        public static IServiceCollection AddWebUiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.AllowedForNewUsers = true;
            })
                .AddEntityFrameworkStores<WriteDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI()
                .AddRoles<IdentityRole>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(14);
                options.Events.OnRedirectToLogin = context =>
                {
                    var returnUrl = context.HttpContext.Items.TryGetValue("OriginalRequestPath", out var originalPath) &&
                        originalPath is string originalPathText &&
                        !string.IsNullOrWhiteSpace(originalPathText)
                            ? originalPathText
                            : $"{context.Request.PathBase}{context.Request.Path}{context.Request.QueryString}";

                    var loginPath = options.LoginPath.Value ?? "/Identity/Account/Login";
                    var pathBase = context.Request.PathBase.Value;
                    if (string.IsNullOrWhiteSpace(pathBase) &&
                        context.HttpContext.Items.TryGetValue("AppName", out var appNameValue) &&
                        appNameValue is string appName &&
                        !string.IsNullOrWhiteSpace(appName))
                    {
                        pathBase = "/" + appName.Trim('/');
                    }

                    var loginUrl = QueryHelpers.AddQueryString(
                        $"{pathBase}{loginPath}",
                        options.ReturnUrlParameter,
                        returnUrl);

                    context.Response.Redirect(loginUrl);
                    return Task.CompletedTask;
                };
            });

            services.AddSignalR();

            services.AddControllersWithViews(options =>
            {
                options.Conventions.Add(new AuthorizeAreaConvention("Administrators", "Admin"));
                options.Filters.Add<PageTitleFilter>();
            })
                .AddRazorOptions(options =>
                {
                    options.ViewLocationFormats.Insert(0, "/Modules/Baskoul/Views/{1}/{0}.cshtml");
                    options.ViewLocationFormats.Insert(1, "/Modules/Baskoul/Views/Shared/{0}.cshtml");
                })
                .AddRazorRuntimeCompilation();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 20 * 1024 * 1024;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireRole("Admin"));

                options.AddPolicy("ExcludeNonHamayars", policy =>
                    policy.RequireAssertion(context =>
                        context.User.IsInRole("Admin") ||
                        context.User.IsInRole("User")));

                options.AddPolicy("ExcludeHamayars", policy =>
                    policy.RequireRole("Admin"));
            });

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentBaskoulUser, CurrentBaskoulUser>();

            services.AddDataProtection()
                .PersistKeysToDbContext<WriteDbContext>()
                .SetDefaultKeyLifetime(TimeSpan.FromDays(10));

            services.AddScoped<UsersService>();
            services.AddScoped<JwtTokenHelper>();
            services.AddSingleton<WeightService>();
            services.AddJwtAuthentication(configuration);

            return services;
        }

        private static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt");
            var jwtKey = jwtSettings["Key"];

            if (string.IsNullOrWhiteSpace(jwtKey))
            {
                throw new InvalidOperationException("JWT key is missing. Add 'Jwt:Key' to appsettings.json or environment variables.");
            }

            var key = Encoding.UTF8.GetBytes(jwtKey);

            services.AddAuthentication()
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path.Value;

                            if (!string.IsNullOrEmpty(accessToken) &&
                                path?.EndsWith("/hubs/weight", StringComparison.OrdinalIgnoreCase) == true)
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }
    }
}
