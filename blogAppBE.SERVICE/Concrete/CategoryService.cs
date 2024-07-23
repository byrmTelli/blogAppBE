using blogAppBE.CORE.Enums;
using blogAppBE.CORE.Generics;
using blogAppBE.CORE.RequestModels;
using blogAppBE.CORE.RequestModels.Category;
using blogAppBE.CORE.ViewModels;
using blogAppBE.CORE.ViewModels.CategoryViewModels;
using blogAppBE.DAL.Abstract;
using blogAppBE.SERVICE.Abstract;
using Microsoft.VisualBasic;

namespace blogAppBE.SERVICE.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        public CategoryService(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal; 
        }
        public async Task<Response<CategoryViewModel>> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryDal.GetAsync(x => x.Id == id);

            var categoryViewModel = new CategoryViewModel (){
                Id  =category.Id, 
                Name = category.Name,
                CreatedDate = category.CreatedDate
                };
            
            return Response<CategoryViewModel>.Success(categoryViewModel,StatusCode.OK);
        }

        public async Task<Response<NoDataViewModel>> CreateCategory(CategoryRequestModel request)
        {
            var result = await _categoryDal.Create(request);
            return result;
        }

        public async Task<Response<List<CategoryViewModel>>> GetCategoryListAsync()
        {
            var result = await _categoryDal.GetCategoryList();
            return result;
        }

        public async Task<Response<NoDataViewModel>> DeleteCategory(int id)
        {
            var result = await _categoryDal.DeleteCategory(id);
            return result;
        }


        public async Task<Response<NoDataViewModel>> UpdateCategory(CategoryRequestModel request)
        {
            var result = await _categoryDal.UpdateCategory(request);
            return result;
        }
    }
}