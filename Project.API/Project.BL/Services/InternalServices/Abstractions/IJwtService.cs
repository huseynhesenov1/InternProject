using Project.Core.Entities;

namespace Project.BL.Services.InternalServices.Abstractions
{
    public interface IJwtService
    {
        string GenerateToken(Worker worker);
    }
} 