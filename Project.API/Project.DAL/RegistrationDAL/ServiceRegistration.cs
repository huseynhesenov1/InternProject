using Microsoft.Extensions.DependencyInjection;
using Project.DAL.Repositories.Abstractions;
using Project.DAL.Repositories.Abstractions.Campaign;
using Project.DAL.Repositories.Abstractions.District;
using Project.DAL.Repositories.Abstractions.Order;
using Project.DAL.Repositories.Abstractions.Product;
using Project.DAL.Repositories.Abstractions.ProductDistrictPrice;
using Project.DAL.Repositories.Abstractions.Worker;
using Project.DAL.Repositories.Implementations.Campaign;
using Project.DAL.Repositories.Implementations.District;
using Project.DAL.Repositories.Implementations.Order;
using Project.DAL.Repositories.Implementations.Product;
using Project.DAL.Repositories.Implementations.ProductDistrictPrice;
using Project.DAL.Repositories.Implementations.Worker;

namespace Project.DAL.RegistrationDAL
{
    public static class ServiceRegistration
    {
        public static void AddRepos(this IServiceCollection services)
        {
            services.AddScoped<IWorkerReadRepository, WorkerReadRepository>();
            services.AddScoped<IWorkerWriteRepository, WorkerWriteRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IDistrictReadRepository, DistrictReadRepository>();
            services.AddScoped<IDistrictWriteRepository, DistrictWriteRepository>();
            services.AddScoped<ICampaignWriteRepository, CampaignWriteRepository>();
            services.AddScoped<ICampaignReadRepository, CampaignReadRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IProductDistrictPriceWriteRepository, ProductDistrictPriceWriteRepository>();
            services.AddScoped<IProductDistrictPriceReadRepository, ProductDistrictPriceReadRepository>();
        }
    }
}
