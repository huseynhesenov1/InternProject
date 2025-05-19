using Microsoft.EntityFrameworkCore;
using Project.BL.DTOs.WorkerDTOs;
using Project.BL.Models;
using Project.BL.Services.ExternalServices.Abstractions;
using Project.BL.Services.InternalServices.Abstractions;
using Project.Core.Entities;
using Project.Core.Entities.Commons;
using Project.DAL.Repositories.Abstractions;
using Project.DAL.Repositories.Abstractions.Worker;

namespace Project.BL.Services.InternalServices.Implementations
{
    public class WorkerService : IWorkerService
    {
        private readonly IWorkerReadRepository _workerReadRepository;
        private readonly IWorkerWriteRepository _workerWriteRepository;
      
        private readonly IJwtService _jwtService;

        public WorkerService(IJwtService jwtService, IWorkerWriteRepository workerWriteRepository, IWorkerReadRepository workerReadRepository)
        {
            _jwtService = jwtService;
            _workerWriteRepository = workerWriteRepository;
            _workerReadRepository = workerReadRepository;
        }

        public async Task<ApiResponse<WorkerCreateResponseDTO>> CreateAsync(WorkerCreateDTO workerCreateDTO)
        {
            try
            {
                var existingWorker = await _workerReadRepository.Table
                    .FirstOrDefaultAsync(w => w.FinCode == workerCreateDTO.FinCode && !w.IsDeleted);
                if (existingWorker != null)
                {
                    return ApiResponse<WorkerCreateResponseDTO>.Fail("Worker with this FinCode already exists", "Duplicate FinCode");
                }

                var worker = new Worker
                {
                    FinCode = workerCreateDTO.FinCode,
                    FullName = workerCreateDTO.FullName,
                    BirthDate = workerCreateDTO.BirthDate,
                    DistrictId = workerCreateDTO.DistrictId
                };
                await _workerWriteRepository.CreateAsync(worker);
                await _workerWriteRepository.SaveChangeAsync();
                var token = _jwtService.GenerateToken(worker);
                return ApiResponse<WorkerCreateResponseDTO>.Success(
                    new WorkerCreateResponseDTO
                    {
                        WorkerId = worker.Id,
                        WorkerToken = token
                    },
                    "Worker created successfully"
                );
            }
            catch (Exception ex)
            {
                return ApiResponse<WorkerCreateResponseDTO>.Fail(ex.Message, "Error creating worker");
            }
        }

        public async Task<ApiResponse<bool>> UpdateAsync(int id, WorkerUpdateDTO workerUpdateDTO)
        {
            try
            {
                var worker = await _workerReadRepository.GetByIdAsync(id, true);
                if (worker == null || worker.IsDeleted)
                {
                    return ApiResponse<bool>.Fail("Worker not found", "Invalid worker ID");
                }

                var existingWorker = await _workerReadRepository.Table
                    .FirstOrDefaultAsync(w => w.FinCode == workerUpdateDTO.FinCode && w.Id != id && !w.IsDeleted);
                if (existingWorker != null)
                {
                    return ApiResponse<bool>.Fail("Worker with this FinCode already exists", "Duplicate FinCode");
                }
                worker.FinCode = workerUpdateDTO.FinCode;
                worker.FullName = workerUpdateDTO.FullName;
                worker.BirthDate = workerUpdateDTO.BirthDate;
                worker.DistrictId = workerUpdateDTO.DistrictId;
                worker.UpdatedAt = DateTime.UtcNow.AddHours(4);

                _workerWriteRepository.Update(worker);
                await _workerWriteRepository.SaveChangeAsync();

                return ApiResponse<bool>.Success(true, "Worker updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail(ex.Message, "Error updating worker");
            }
        }
        public async Task<ICollection<Worker>> GetAllAsync()
        {
            return await _workerReadRepository.GetAllAsync(false);
        }
        public async Task<PagedResult<Worker>> GetPaginatedAsync(PaginationParams @params)
        {
            var allCategories = await _workerReadRepository.GetAllAsync(false);

            var filtered = allCategories
                //.OrderByDescending(c => c.CreateAt)
                .Skip((@params.PageNumber - 1) * @params.PageSize)
                .Take(@params.PageSize)
                .ToList();

            int totalCount = allCategories.Count;

            return new PagedResult<Worker>(filtered, totalCount, @params.PageNumber, @params.PageSize);
        }



       

        public async Task<ApiResponse<WorkerDTO>> GetByIdAsync(int id)
        {
            try
            {
                var worker = await _workerReadRepository.GetByIdAsync(id, true);
                if (worker == null || worker.IsDeleted)
                {
                    return ApiResponse<WorkerDTO>.Fail("Worker not found", "Invalid worker ID");
                }

                var workerDto = new WorkerDTO
                {
                    WorkerId = worker.Id,
                    FinCode = worker.FinCode,
                    FullName = worker.FullName,
                    BirthDate = worker.BirthDate,
                    DistrictId = worker.DistrictId,
                };

                return ApiResponse<WorkerDTO>.Success(workerDto, "Worker retrieved successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<WorkerDTO>.Fail(ex.Message, "Error retrieving worker");
            }
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                var worker = await _workerReadRepository.GetByIdAsync(id, true);
                if (worker == null || worker.IsDeleted)
                {
                    return ApiResponse<bool>.Fail("Worker not found", "Invalid worker ID");
                }

                _workerWriteRepository.SoftDelete(worker);
                await _workerWriteRepository.SaveChangeAsync();

                return ApiResponse<bool>.Success(true, "Worker deleted successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail(ex.Message, "Error deleting worker");
            }
        }

       
        
    }
}
