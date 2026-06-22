using Application.Common.Behaviors;
using Application.Interfaces;
using Application.Profiles;
using Application.Services;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Infra.Data.Context;
using Infra.Data.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProCodeGuide.Samples.FileUpload.Services;

namespace Infra.Ioc
{

	public static class DependencyInjection
	{
		public static IServiceCollection AddProjectServices(this IServiceCollection services, IConfiguration configuration)
		{
			// -------------------------------
			// DbContexts
			// -------------------------------
			services.AddDbContext<WriteDbContext>(opt =>
				opt.UseSqlServer(configuration.GetConnectionString("WriteConnection")));

			// -------------------------------
			// AutoMapper
			// -------------------------------
			services.AddAutoMapper(_ => { }, typeof(MappingProfile).Assembly);

			// -------------------------------
			// MediatR + FluentValidation + Behaviors (برای CQRS)
			// -------------------------------
			services.AddMediatR(cfg =>
				cfg.RegisterServicesFromAssembly(typeof(MappingProfile).Assembly));
			services.AddValidatorsFromAssembly(typeof(MappingProfile).Assembly);
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

			services.AddScoped<IBaseTableService, BaseTableService>();
			services.AddScoped<IUsersRepository, UsersRepository>();
			services.AddScoped<IBaseTableRepository, BaseTableRepository>();

			services.AddScoped<IBaskoulService, BaskoulService>();
			services.AddScoped<IBaskoulRepository, BaskoulRepository>();

			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

			//Application LAYER
			services.AddScoped<IUsersService, UsersService>();

			services.AddScoped<IBargeBaskoul, BargeBaskoulService>();
			services.AddScoped<IBargeBaskoulRepository, BargeBaskoulRepository>();

			services.AddScoped<ISite, SiteService>();
			services.AddScoped<ISiteRepository, SiteRepository>();

			services.AddScoped<ICodeMarkaz, CodeMarkazService>();
			services.AddScoped<ICodeMarkazRepository, CodeMarkazRepository>();

			services.AddScoped<IReports, ReportService>();
			services.AddScoped<IReportsRepository, ReportsRepository>();

			services.AddScoped<IAPIService, APIService>();
			services.AddScoped<IAPIsRepository, APIsRepository>();

			services.AddScoped<IBaseService, BaseDataService>();
			services.AddScoped<IBaseRepository, BaseRepository>();
			// -------------------------------
			// سایر سرویس‌ها
			// -------------------------------
			services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, AppUserClaimsPrincipalFactory>();
			services.AddScoped<IBufferedFileUploadService, BufferedFileUploadLocalService>();
			services.AddScoped<IUsersService, UsersService>();

			services.AddHttpContextAccessor();


			return services;
		}
	}
}


