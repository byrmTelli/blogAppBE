using blogAppBE.CORE.Generics;
using blogAppBE.CORE.RequestModels;
using blogAppBE.CORE.ViewModels;
using blogAppBE.CORE.ViewModels.CategoryViewModels;
using blogAppBE.SERVICE.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blogAppBE.WEB.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController:ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryRequestModel request)
        {
            var response = await _categoryService.CreateCategory(request);

            if(response.IsSuccessfull)
            {
                return Created();
            }

            return StatusCode((int)response.StatusCode,response.Errors);

        }
        
        
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            var response = await _categoryService.GetCategoryByIdAsync(categoryId);
            if(response.IsSuccessfull)
            {
                return StatusCode((int)response.StatusCode,response.Data);
            }

            return StatusCode((int)response.StatusCode,response.Errors);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCategoryList()
        {
            var response =  await _categoryService.GetCategoryListAsync();
            if(response.IsSuccessfull)
            {
                return StatusCode((int)response.StatusCode,response.Data);
            }

            return StatusCode((int)response.StatusCode,response.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var response =  await _categoryService.DeleteCategory(id);
            if(response.IsSuccessfull)
            {
                return StatusCode((int)response.StatusCode,response.Data);
            }

            return StatusCode((int)response.StatusCode,response.Errors);
        }

    }
}