using blogAppBE.CORE.DataAccess;
using blogAppBE.CORE.DBModels;
using blogAppBE.CORE.Generics;
using blogAppBE.CORE.RequestModels.Category;
using blogAppBE.CORE.ViewModels;
using blogAppBE.CORE.ViewModels.CategoryViewModels;

namespace blogAppBE.DAL.Abstract
{
    public interface ICategoryDal:IEntityRepository<Category>
    {
     Task<Response<NoDataViewModel>> Create(CategoryRequestModel request);
     Task<Response<List<CategoryViewModel>>> GetCategoryList();
     Task<Response<NoDataViewModel>> DeleteCategory(int id);
     Task<Response<NoDataViewModel>> UpdateCategory(CategoryRequestModel request);
    }
}