using System.Runtime.InteropServices;
using CodePluse.API.Data;
using CodePluse.API.Models.Domain;
using CodePluse.API.Models.DTO;
using CodePluse.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePluse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDto request)
        {
            // Map DTO to Domain Model
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            await categoryRepository.CreateAsync(category);

            //Domain Model to DTO
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            return Ok(response);
        }

        //Get All Category
        // GET: https://localhost:7297/api/Category?quer=html&sortBy=name&sortDirection=desc
        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] string? query, [FromQuery] string? sortBy, [FromQuery] string? sortDirection, [FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var categories = await categoryRepository.GetAllAsync(query, sortBy, sortDirection, pageNumber, pageSize);

            //Map Domain Model to DTO
            var response = new List<CategoryDto>();
            foreach (var category in categories)
            {
                response.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                });
            }

            return Ok(response);
        }


        //Get Category By ID
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryByID([FromRoute] Guid id)
        {
            var existingCategory = await categoryRepository.GetById(id);

            if (existingCategory is null)
            {
                return NotFound();
            }

            var response = new CategoryDto
            {
                Id = existingCategory.Id,
                Name = existingCategory.Name,
                UrlHandle = existingCategory.UrlHandle
            };

            return Ok(response);
        }


        // Update Category
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> EditCategoty([FromRoute] Guid id, UpdateCategoryRequestDto request)
        {
            // Convert DTO to Domain Model
            var category = new Category
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };
            await categoryRepository.UpadateAsync(category);

            if (category is null)
            {
                return NotFound();
            }

            // Convert Domain model to DTO
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }


        // Delete Categoty 
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var category = await categoryRepository.DeleteAsync(id);

            if(category is null)
            {
                return NotFound();
            }

            // Convert Domain model to DTO
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }


        // GET https://localhost:7226/api/categories/count
        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> GetCategoriesTotal()
        {
            var count = await categoryRepository.GetCount();
            return Ok(count);
        }
    }
}
