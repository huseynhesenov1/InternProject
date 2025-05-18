using Project.BL.DTOs.WorkerDTOs;
using Project.Core.Entities;
using System.Diagnostics;

namespace Project.BL.Services.InternalServices.Abstractions
{
    public interface IWorkerService
    {
        Task<ICollection<Worker>> GetAllAsync();
        Task<ICollection<Worker>> GetAllDeletedAsync();
        Task<Worker> RestoreAsync(int id);
        Task<Worker> GetByIdAsync(int id);
        Task<Worker> CreateAsync(WorkerCreateDTO workerCreateDTO);
        Task<Worker> UpdateAsync(int id, WorkerUpdateDTO workerUpdateDTO);
        Task<Worker> SoftDeleteAsync(int id);
        Task<Worker> HardDeleteAsync(int id);
    }
}
