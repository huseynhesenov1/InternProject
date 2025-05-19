﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BL.DTOs.ProductDTOs;
using Project.BL.DTOs.WorkerDTOs;
using Project.BL.Models;
using Project.BL.Services.InternalServices.Abstractions;
using Project.BL.Services.InternalServices.Implementations;
using Project.Core.Entities.Commons;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpPost]
        public async Task<int> Create([FromBody] ProductCreateDTO productCreateDTO)
        {
            return await _productService.CreateAsync(productCreateDTO);
        }
        [HttpPut("{Id}")]
        public async Task<int> Update(int Id, [FromBody] ProductUpdateDTO productUpdateDTO)
        {
            try
            {
                return await _productService.UpdateAsync(Id, productUpdateDTO);
            }
            catch 
            {
                return -1;
            }
        }
        [HttpGet]
        public async Task<ICollection<ProductReadDTO>> GetAll()
        {
            return await _productService.GetAllAsync();
        }
        [HttpGet("Paginated")]
        public async Task<IActionResult> GetPaginated([FromQuery] PaginationParams @params)
        {
            var result = await _productService.GetPaginatedAsync(@params);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ApiResponse<ProductReadDTO>> GetById(int id)
        {
            return await _productService.GetByIdAsync(id);
        }
        [HttpGet("Search")]
        public async Task<IActionResult> GetSearch([FromQuery] string title)
        {
            var result = await _productService .SearchProductsAsync(title);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<ApiResponse<bool>> Delete(int id)
        {
            return await _productService.DeleteAsync(id);
        }
    }
}
