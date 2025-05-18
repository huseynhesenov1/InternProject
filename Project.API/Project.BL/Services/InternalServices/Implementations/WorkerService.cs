using Microsoft.EntityFrameworkCore;
using Project.BL.DTOs.WorkerDTOs;
using Project.BL.Models;
using Project.BL.Services.InternalServices.Abstractions;
using Project.Core.Entities;
using Project.DAL.Repositories.Abstractions;

namespace Project.BL.Services.InternalServices.Implementations
{
    public class WorkerService : IWorkerService
    {
        private readonly IReadRepository<Worker> _workerReadRepository;
        private readonly IWriteRepository<Worker> _workerWriteRepository;
        private readonly IReadRepository<District> _districtReadRepository;
        private readonly IJwtService _jwtService;

        public WorkerService(
            IReadRepository<Worker> workerReadRepository,
            IWriteRepository<Worker> workerWriteRepository,
            IReadRepository<District> districtReadRepository,
            IJwtService jwtService)
        {
            _workerReadRepository = workerReadRepository;
            _workerWriteRepository = workerWriteRepository;
            _districtReadRepository = districtReadRepository;
            _jwtService = jwtService;
        }

        public async Task<ApiResponse<WorkerCreateResponseDTO>> CreateAsync(WorkerCreateDTO workerCreateDTO)
        {
            try
            {
                // Check if district exists
                var district = await _districtReadRepository.GetByIdAsync(workerCreateDTO.DistrictId, true);
                if (district == null)
                {
                    return ApiResponse<WorkerCreateResponseDTO>.Fail("District not found", "Invalid district ID");
                }

                // Check if worker with same FinCode already exists
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

                // Generate JWT token
                var token = _jwtService.GenerateToken(worker);
                worker.WorkerToken = token;
                _workerWriteRepository.Update(worker);
                await _workerWriteRepository.SaveChangeAsync();

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

                // Check if district exists
                var district = await _districtReadRepository.GetByIdAsync(workerUpdateDTO.DistrictId, true);
                if (district == null)
                {
                    return ApiResponse<bool>.Fail("District not found", "Invalid district ID");
                }

                // Check if another worker with same FinCode exists
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
                worker.UpdatedAt = DateTime.UtcNow;

                _workerWriteRepository.Update(worker);
                await _workerWriteRepository.SaveChangeAsync();

                return ApiResponse<bool>.Success(true, "Worker updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail(ex.Message, "Error updating worker");
            }
        }

        public async Task<ApiResponse<PaginatedResponse<WorkerDTO>>> GetWorkersAsync(
            int pageNo, int pageSize, string finCode = null, string fullName = null, 
            DateTime? birthDate = null, int? districtId = null)
        {
            try
            {
                var query = _workerReadRepository.Table
                    .Include(w => w.District)
                    .Where(w => !w.IsDeleted);

                // Apply filters
                if (!string.IsNullOrWhiteSpace(finCode))
                    query = query.Where(w => w.FinCode.Contains(finCode));
                if (!string.IsNullOrWhiteSpace(fullName))
                    query = query.Where(w => w.FullName.Contains(fullName));
                if (birthDate.HasValue)
                    query = query.Where(w => w.BirthDate.Date == birthDate.Value.Date);
                if (districtId.HasValue)
                    query = query.Where(w => w.DistrictId == districtId.Value);

                var totalCount = await query.CountAsync();
                var filteredCount = totalCount;

                // Apply pagination
                var items = await query
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .Select(w => new WorkerDTO
                    {
                        WorkerId = w.Id,
                        WorkerToken = w.WorkerToken,
                        FinCode = w.FinCode,
                        FullName = w.FullName,
                        BirthDate = w.BirthDate,
                        DistrictId = w.DistrictId,
                        DistrictName = w.District.Name
                    })
                    .ToListAsync();

                var pageCount = (int)Math.Ceiling(totalCount / (double)pageSize);

                return ApiResponse<PaginatedResponse<WorkerDTO>>.Success(
                    new PaginatedResponse<WorkerDTO>
                    {
                        TotalCount = totalCount,
                        FilteredCount = filteredCount,
                        PageCount = pageCount,
                        Items = items
                    },
                    "Workers retrieved successfully"
                );
            }
            catch (Exception ex)
            {
                return ApiResponse<PaginatedResponse<WorkerDTO>>.Fail(ex.Message, "Error retrieving workers");
            }
        }

        public async Task<ApiResponse<WorkerDTO>> GetByIdAsync(int id)
        {
            try
            {
                var worker = await _workerReadRepository.GetByIdAsync(id, true, "District");
                if (worker == null || worker.IsDeleted)
                {
                    return ApiResponse<WorkerDTO>.Fail("Worker not found", "Invalid worker ID");
                }

                var workerDto = new WorkerDTO
                {
                    WorkerId = worker.Id,
                    WorkerToken = worker.WorkerToken,
                    FinCode = worker.FinCode,
                    FullName = worker.FullName,
                    BirthDate = worker.BirthDate,
                    DistrictId = worker.DistrictId,
                    DistrictName = worker.District.Name
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

        // Implementing the old interface methods for backward compatibility
        public async Task<ICollection<Worker>> GetAllAsync()
        {
            return await _workerReadRepository.GetAllAsync(false);
        }

        public async Task<ICollection<Worker>> GetAllDeletedAsync()
        {
            return await _workerReadRepository.GetAllAsync(true);
        }

        public async Task<Worker> RestoreAsync(int id)
        {
            var worker = await _workerReadRepository.GetByIdAsync(id, true);
            if (worker == null)
                throw new Exception("Worker not found");

            _workerWriteRepository.Restore(worker);
            await _workerWriteRepository.SaveChangeAsync();
            return worker;
        }

        public async Task<Worker> GetByIdAsync(int id)
        {
            return await _workerReadRepository.GetByIdAsync(id, true);
        }

        public async Task<Worker> CreateAsync(WorkerCreateDTO workerCreateDTO)
        {
            var worker = new Worker
            {
                FinCode = workerCreateDTO.FinCode,
                FullName = workerCreateDTO.FullName,
                BirthDate = workerCreateDTO.BirthDate,
                DistrictId = workerCreateDTO.DistrictId
            };

            await _workerWriteRepository.CreateAsync(worker);
            await _workerWriteRepository.SaveChangeAsync();
            return worker;
        }

        public async Task<Worker> UpdateAsync(int id, WorkerUpdateDTO workerUpdateDTO)
        {
            var worker = await _workerReadRepository.GetByIdAsync(id, true);
            if (worker == null)
                throw new Exception("Worker not found");

            worker.FinCode = workerUpdateDTO.FinCode;
            worker.FullName = workerUpdateDTO.FullName;
            worker.BirthDate = workerUpdateDTO.BirthDate;
            worker.DistrictId = workerUpdateDTO.DistrictId;
            worker.UpdatedAt = DateTime.UtcNow;

            _workerWriteRepository.Update(worker);
            await _workerWriteRepository.SaveChangeAsync();
            return worker;
        }

        public async Task<Worker> SoftDeleteAsync(int id)
        {
            var worker = await _workerReadRepository.GetByIdAsync(id, true);
            if (worker == null)
                throw new Exception("Worker not found");

            _workerWriteRepository.SoftDelete(worker);
            await _workerWriteRepository.SaveChangeAsync();
            return worker;
        }

        public async Task<Worker> HardDeleteAsync(int id)
        {
            var worker = await _workerReadRepository.GetByIdAsync(id, true);
            if (worker == null)
                throw new Exception("Worker not found");

            _workerWriteRepository.HardDelete(worker);
            await _workerWriteRepository.SaveChangeAsync();
            return worker;
        }
    }
}
