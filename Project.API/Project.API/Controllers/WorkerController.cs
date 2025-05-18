using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.BL.DTOs.WorkerDTOs;
using Project.BL.Models;
using Project.BL.Services.InternalServices.Abstractions;

namespace Project.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerService _workerService;

        public WorkerController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        
        
        [HttpPost]
        public async Task<ApiResponse<WorkerCreateResponseDTO>> Create([FromBody] WorkerCreateDTO workerCreateDTO)
        {
            return await _workerService.CreateAsync(workerCreateDTO);
        }

       
        [HttpPut("{id}")]
        public async Task<ApiResponse<bool>> Update(int id, [FromBody] WorkerUpdateDTO workerUpdateDTO)
        {
            return await _workerService.UpdateAsync(id, workerUpdateDTO);
        }

        
        
        [HttpGet]
        public async Task<ApiResponse<PaginatedResponse<WorkerDTO>>> GetWorkers(
            [FromQuery] int pageNo = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string finCode = null,
            [FromQuery] string fullName = null,
            [FromQuery] DateTime? birthDate = null,
            [FromQuery] int? districtId = null)
        {
            return await _workerService.GetWorkersAsync(pageNo, pageSize, finCode, fullName, birthDate, districtId);
        }

       
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ApiResponse<WorkerDTO>> GetById(int id)
        {
            return await _workerService.GetByIdAsync(id);
        }

        /// <summary>
        /// Delete worker (CMS endpoint)
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ApiResponse<bool>> Delete(int id)
        {
            return await _workerService.DeleteAsync(id);
        }

        /// <summary>
        /// Get worker profile (Mobile app endpoint)
        /// </summary>
        [Authorize]
        [HttpGet("profile")]
        public async Task<ApiResponse<WorkerDTO>> GetProfile()
        {
            // Get worker ID from token claims
            var workerIdClaim = User.FindFirst("sub")?.Value;
            if (!int.TryParse(workerIdClaim, out int workerId))
            {
                return ApiResponse<WorkerDTO>.Fail("Invalid token", "Unauthorized");
            }

            return await _workerService.GetByIdAsync(workerId);
        }

        /// <summary>
        /// Update worker profile (Mobile app endpoint)
        /// </summary>
        [Authorize]
        [HttpPut("profile")]
        public async Task<ApiResponse<bool>> UpdateProfile([FromBody] WorkerUpdateDTO workerUpdateDTO)
        {
            // Get worker ID from token claims
            var workerIdClaim = User.FindFirst("sub")?.Value;
            if (!int.TryParse(workerIdClaim, out int workerId))
            {
                return ApiResponse<bool>.Fail("Invalid token", "Unauthorized");
            }

            return await _workerService.UpdateAsync(workerId, workerUpdateDTO);
        }
    }
} 