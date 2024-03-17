using Microsoft.AspNetCore.Mvc;
using uBlog.API.Models.Domain;
using uBlog.API.Models.DTO;
using uBlog.API.Repositories.Interface;

namespace uBlog.API.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase {

        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository) {
            
            this._categoryRepository = categoryRepository;
        }

        //CREATE
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDto request) {

            var category = new Category {

                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };
            
            await _categoryRepository.CreateAsync(category);

            var response = new CategoryDto {

                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };

            return Ok(response);

        }

        //READ ALL
        [HttpGet]
        public async Task<IActionResult> GetAllCategories() {

            var categories = await _categoryRepository.GetAllAsync();

            var response = new List<CategoryDto>();
            foreach (var category in categories) {

                response.Add(new CategoryDto {

                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                });
            }

            return Ok(response);
        }

        //READ BY ID
        //GET : https://localhost:7249/api/categories/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id) {

            var existingCategory = await _categoryRepository.GetById(id);

            if (existingCategory is null) return NotFound();

            var response = new CategoryDto {

                Id = existingCategory.Id,
                Name = existingCategory.Name,
                UrlHandle = existingCategory.UrlHandle
            };

            return Ok(response);
        }

        //UPDATE
        //https://localhost:7249/api/categories/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, UpdateCategoryRequestDto request) {

            var category = new Category {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            category = await _categoryRepository.UpdateAsync(category);

            if (category is null) return NotFound();

            var response = new CategoryDto {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }

        // DELETE
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id) {

            var category = await _categoryRepository.DeleteAsnyc(id);

            if (category is null) return NotFound();

            var response = new CategoryDto {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }
    }
}
