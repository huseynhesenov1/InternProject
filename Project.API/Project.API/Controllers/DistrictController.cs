﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BL.DTOs.DistrictDTOs;
using Project.BL.Services.InternalServices.Abstractions;
using Project.Core.Entities;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly IDistrictService _districtService;
        public DistrictController(IDistrictService districtService)
        {
            _districtService = districtService;
        }

        [HttpGet]
        public async Task<ICollection<DistrictReadDTO>> GetAll()
        {
            return await _districtService.GetAllAsync();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] DistrictCreateDTO reviewDto)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, await _districtService.CreateAsync(reviewDto));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, await _districtService.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        [HttpDelete("{id}Soft")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, await _districtService.SoftDeleteAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] DistrictUpdateDTO reviewUpdateDTO)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, await _districtService.UpdateAsync(id, reviewUpdateDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
       
    }
}
