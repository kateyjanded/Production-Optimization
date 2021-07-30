using Application.Common.Interfaces;
using Application.ModelBackGround.Interfaces;
using Application.PVT.Interfaces;
using Application.SystemAnalysisModels.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repository.EntityFrameWork_Repository;
using Infrastructure.Repository.EntityFrameWork_Repository;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddScoped<IWellModelRepository, WellModelRepository>();
            services.AddScoped<IModelBackgroundRepository, ModelBackgroundRepository>();
            services.AddScoped<IPVTRepository, PVTRepository>();
            services.AddScoped<IParamEntryRepository, ParamEntryRepository>();
            services
                .AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddTransient<IIdentityService, IdentityService>();

            //services.AddIdentityServer()
            //    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();


            services.AddTransient<IDateTime, DateTimeService>();
            return services;
        }
    }
}
