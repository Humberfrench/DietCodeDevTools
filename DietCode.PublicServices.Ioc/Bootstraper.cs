using DietCode.PublicServices.CoreServices;
using DietCode.PublicServices.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DietCode.PublicServices.Ioc
{
    public static class Bootstraper
    {
        public static void Initializer(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IValidatorService, ValidatorService>();
        }
    }

}