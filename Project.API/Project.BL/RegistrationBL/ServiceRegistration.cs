using Microsoft.Extensions.DependencyInjection;
using Project.BL.Services.ExternalServices.Abstractions;
using Project.BL.Services.ExternalServices.Implementations;
using Project.BL.Services.InternalServices.Abstractions;
using Project.BL.Services.InternalServices.Implementations;

namespace Project.BL.RegistrationBL
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {

            services.AddScoped<IWorkerService, WorkerService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IDistrictService, DistrictService>();
            services.AddScoped<ICampaignService, CampaignService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IJwtService, JwtService>();
        }
    }
}
