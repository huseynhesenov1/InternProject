using Project.Core.Entities;

namespace Project.BL.Services.ExternalServices.Abstractions
{
    public interface IJwtService
    {
        string GenerateToken(Worker worker);
    }
}
