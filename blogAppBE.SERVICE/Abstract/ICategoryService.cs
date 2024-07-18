using blogAppBE.CORE.Generics;
using blogAppBE.CORE.RequestModels;
using blogAppBE.CORE.ViewModels;
using blogAppBE.CORE.ViewModels.CategoryViewModels;

namespace blogAppBE.SERVICE.Abstract
{
    public interface ICategoryService
    {
        Task<Response<CategoryViewModel>> GetCategoryByIdAsync(int id);
        Task<Response<NoDataViewModel>> CreateCategory(CategoryRequestModel request);
        Task<Response<List<CategoryViewModel>>> GetCategoryListAsync();
        Task<Response<NoDataViewModel>> DeleteCategory(int id);
    }
}