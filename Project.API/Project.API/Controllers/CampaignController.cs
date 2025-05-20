using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BL.DTOs.CampaignDTOs;
using Project.BL.DTOs.ProductDTOs;
using Project.BL.Services.InternalServices.Abstractions;
using Project.BL.Services.InternalServices.Implementations;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignService _campaignService;
        public CampaignController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }
        [HttpPost]
        
        public async Task<IActionResult> Create([FromBody] CampaignCreateDTO campaignCreateDTO)
        {
            var result = await _campaignService.CreateAsync(campaignCreateDTO);
            if (!result.IsSuccess)
            {
                return BadRequest(new
                {
                    error = result.Message,
                    detail = result.ErrorDetail
                });
            }

            return Ok(new
            {
                id = result.Data,
                message = result.Message
            });
        }


        [HttpPut("{Id}")]
        public async Task<int> Update(int Id, [FromBody] CampaignUpdateDTO productUpdateDTO)
        {
            try
            {
                return await _campaignService.UpdateAsync(Id, productUpdateDTO);
            }
            catch
            {
                return -1;
            }
        }
        [HttpGet]
        public async Task<ICollection<CampaignReadDTO>> GetAll()
        {
            return await _campaignService.GetAllAsync();
        }


    }
}
