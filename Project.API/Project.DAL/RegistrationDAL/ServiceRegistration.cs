using Microsoft.Extensions.DependencyInjection;
using Project.DAL.Repositories.Abstractions.Worker;
using Project.DAL.Repositories.Implementations.Worker;

namespace Project.DAL.RegistrationDAL
{
    public static class ServiceRegistration
    {
        public static void AddRepos(this IServiceCollection services)
        {

            services.AddScoped<IWorkerReadRepository, WorkerReadRepository>();
            services.AddScoped<IWorkerWriteRepository, WorkerWriteRepository>();

        }
    }
}
