

using ImplementDAL.LoggerManager;

namespace MobileManagementSystem.Extension
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection Services)
        {
            Services.AddTransient<IBrandService, BrandService>();
            Services.AddTransient<IMobileService, MobileService>();
            Services.AddTransient<IOSVService, OSVService>();
            Services.AddScoped<IUnitofWork, UnitWork>();
            Services.AddTransient<IOderService, OderService>();
            Services.AddTransient<IUserService, UserService>();
            Services.AddTransient<IOperatingSystemService, OperatingSystemService>();
          Services.AddTransient<ILoggerManager, LoggerManager>();
            Services.AddAutoMapper(typeof(AutoMappers));

            return Services;


            return Services;
        }
    }
}
