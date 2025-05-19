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
       
    }

    //public class PaginatedResponse<T>
    //{
    //    public int TotalCount { get; set; }
    //    public int FilteredCount { get; set; }
    //    public int PageCount { get; set; }
    //    public ICollection<T> Items { get; set; }
    //}
}
