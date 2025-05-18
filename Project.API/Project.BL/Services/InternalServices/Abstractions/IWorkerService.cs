using Project.BL.DTOs.WorkerDTOs;
using Project.BL.Models;
using Project.Core.Entities;
using System.Diagnostics;

namespace Project.BL.Services.InternalServices.Abstractions
{
    public interface IWorkerService
    {
        Task<ICollection<Worker>> GetAllAsync();
       
       
       
        Task<Worker> SoftDeleteAsync(int id);
        Task<Worker> HardDeleteAsync(int id);
        Task<ApiResponse<WorkerCreateResponseDTO>> CreateAsync(WorkerCreateDTO workerCreateDTO);
        Task<ApiResponse<bool>> UpdateAsync(int id, WorkerUpdateDTO workerUpdateDTO);
        Task<ApiResponse<PaginatedResponse<WorkerDTO>>> GetWorkersAsync(int pageNo, int pageSize, string finCode = null, string fullName = null, DateTime? birthDate = null, int? districtId = null);
        Task<ApiResponse<WorkerDTO>> GetByIdAsync(int id);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }

    public class PaginatedResponse<T>
    {
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
        public int PageCount { get; set; }
        public ICollection<T> Items { get; set; }
    }
}
