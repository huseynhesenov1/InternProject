using Microsoft.Extensions.DependencyInjection;
using Project.BL.Services.InternalServices.Abstractions;
using Project.BL.Services.InternalServices.Implementations;
using Project.DAL.Repositories.Abstractions;
using Project.DAL.Repositories.Implementations;

namespace Project.BL.RegistrationBL
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            //// Register repositories
            //services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            //services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

            // Register services
            services.AddScoped<IWorkerService, WorkerService>();
            services.AddScoped<IJwtService, JwtService>();
        }
    }
}
