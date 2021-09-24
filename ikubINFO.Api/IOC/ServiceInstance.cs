using ikubINFO.Api.Logging;
using ikubINFO.DataAccess;
using ikubINFO.Repository.CustomRepositories.Role;
using ikubINFO.Repository.CustomRepositories.User;
using ikubINFO.Repository.GenericRepositoryAndUnitOfWork.GenericRepository;
using ikubINFO.Repository.GenericRepositoryAndUnitOfWork.UnitOfWork;
using ikubINFO.Service.Authorization;
using ikubINFO.Service.Role;
using ikubINFO.Service.User;
using ikubINFO.Utility.PasswordHelper;
using ikubINFO.Utility.StaticData;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ikubINFO.Api.IOC
{
    public static class ServiceInstance
    {
        public static void RegisterIkubInfoServiceInstance(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(nameof(IkubInfoDBContext));
            services.AddDbContext<IkubInfoDBContext>(options => options.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(StaticData.MIGRATION_ASSEMBLY)));
            services.AddScoped<DbContext, IkubInfoDBContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddTransient(provider => configuration);


            services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();



            // generic DI
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}