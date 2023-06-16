using DietCode.PublicServices.App;
using DietCode.PublicServices.CoreServices;
using DietCode.PublicServices.Domain.Interfaces.Services;
using DietCode.PublicServices.ViewModel.Interfaces;
using FluxoDeCaixa.App;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DietCode.PublicServices.Ioc
{
    public static class Bootstraper
    {
        public static void Initializer(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<ICardApp, CardApp>();
            services.AddScoped<IDocumentService, IDocumentService>();
            services.AddScoped<IDocumentApp, DocumentApp>();
        }
    }

}