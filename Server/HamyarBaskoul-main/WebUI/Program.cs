using Application.Services;
using Application.Tools;
using System.Text;
using Domain.Models;
using Infra.Data.Context;
using Infra.Ioc;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebUI.Controllers;
using Infra.Data.Classes;

var builder = WebApplication.CreateBuilder(args);

var WriteConnection = builder.Configuration.GetConnectionString("WriteConnection");
builder.Services.AddDbContext<WriteDbContext>(options =>
	options.UseSqlServer(WriteConnection));

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
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

builder.Services.AddProjectServices(builder.Configuration);

builder.Services.AddSignalR();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", builder =>
	{
		builder.AllowAnyOrigin()
			   .AllowAnyMethod()
			   .AllowAnyHeader();
	});
});

// 6️⃣ Form Options
builder.Services.Configure<FormOptions>(options =>
{
	options.MultipartBodyLengthLimit = 20971520; // 20 MB
});

// 7️⃣ Authorization
builder.Services.AddControllersWithViews(options =>
{
	options.Conventions.Add(new AuthorizeAreaConvention("Administrators", "Administrator", "SuperAdmin"));
});

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("AdminOrSuperAdmin", policy =>
		policy.RequireRole("Administrator", "SuperAdmin", "NonHamyarAdmin"));

	options.AddPolicy("ExcludeNonHamayars", policy =>
		policy.RequireAssertion(context =>
			!context.User.IsInRole("NONHAMYARUSER") &&
			!context.User.IsInRole("NONHAMYAADMIN")));

	options.AddPolicy("ExcludeHamayars", policy =>
		policy.RequireAssertion(context =>
			!context.User.IsInRole("Administrator") &&
			!context.User.IsInRole("User")));
});

builder.Services.AddHttpContextAccessor();



builder.Services.AddDataProtection()
	.PersistKeysToDbContext<WriteDbContext>()
	.SetDefaultKeyLifetime(TimeSpan.FromDays(10));
builder.Services.AddScoped<UsersService>();

builder.Services.AddScoped<JwtTokenHelper>();
builder.Services.AddSingleton<WeightService>();


var jwtSettings = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSettings["Key"];

if (string.IsNullOrWhiteSpace(jwtKey))
    throw new Exception("❌ JWT Key is missing! Add 'Jwt:Key' to appsettings.json or IIS environment variables.");

var key = Encoding.UTF8.GetBytes(jwtKey);


builder.Services.AddAuthentication("Bearer")
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

        // REQUIRED for SignalR
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;

                if (!string.IsNullOrEmpty(accessToken) &&
                    path.Value.EndsWith("/hubs/weight", StringComparison.OrdinalIgnoreCase))
                {
                    context.Token = accessToken;
                }

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
	db.Database.Migrate();
}

if (app.Environment.IsProduction())
{
	app.UseHsts();
}

app.UseStatusCodePages(async context =>
{
	var response = context.HttpContext.Response;
	if (response.StatusCode == 404) response.Redirect("Error/NotFound");
	if (response.StatusCode == 500) response.Redirect("Error/ServerError");
});

app.UseStaticFiles();

app.UseCors("AllowAll");

app.UseRouting();

// Middleware to extract dynamic appname
app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value;

    if (!string.IsNullOrEmpty(path))
    {
        var segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (segments.Length > 0)
        {
            context.Items["AppName"] = segments[0];
            context.Request.Path = "/" + string.Join('/', segments.Skip(1));
        }
    }

    await next();
});

app.UseAuthentication();
app.UseAuthorization();

// Map hub after path rewrite
app.MapHub<ReceiveWeightFromScale>("/hubs/weight");


app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "areas",
	pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
