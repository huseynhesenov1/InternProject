using Project.BL.DTOs.ProductDTOs;
using Project.BL.DTOs.WorkerDTOs;
using Project.BL.Models;
using Project.Core.Entities;
using Project.Core.Entities.Commons;
using System.Diagnostics;

namespace Project.BL.Services.InternalServices.Abstractions
{
    public interface IWorkerService
    {
        Task<ICollection<Worker>> GetAllAsync();
        Task<ApiResponse<WorkerCreateResponseDTO>> CreateAsync(WorkerCreateDTO workerCreateDTO);
        Task<PagedResult<Worker>> GetPaginatedAsync(PaginationParams @params);
        Task<ApiResponse<bool>> UpdateAsync(int id, WorkerUpdateDTO workerUpdateDTO);
        Task<ApiResponse<bool>> DeleteAsync(int id);
        Task<ApiResponse<WorkerDTO>> GetByIdAsync(int id);
        Task<ICollection<WorkerDTO>> SearchProductsAsync(WorkerSearchDTO workerSearchDTO);
    }
}