using System.Net.Mime;
using blogAppBE.CORE.DataAccess.EntityFramework;
using blogAppBE.CORE.DBModels;
using blogAppBE.CORE.Enums;
using blogAppBE.CORE.Generics;
using blogAppBE.CORE.RequestModels;
using blogAppBE.CORE.ViewModels;
using blogAppBE.CORE.ViewModels.CategoryViewModels;
using blogAppBE.DAL.Abstract;
using blogAppBE.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace blogAppBE.DAL.Concrete
{
    public class CategoryDal : EfEntityRepositoryBase<Category, AppDbContext>, ICategoryDal
    {
        public async Task<Response<NoDataViewModel>> Create(CategoryRequestModel request)
        {
            using(var context = new AppDbContext())
            {
                try
                {
                    var isCategoryExist = await context.Categories
                                                .Where(c => c.Name == request.Name)
                                                .FirstOrDefaultAsync();

                    if(isCategoryExist != null)
                    {
                        return Response<NoDataViewModel>.Fail("There is a category given category name",StatusCode.Conflict);
                    }

                    var dbModel = new Category
                    {
                        Name = request.Name,
                        CreatedDate = DateTime.UtcNow
                    };

                    await context.AddAsync(dbModel);
                    await context.SaveChangesAsync();

                    return Response<NoDataViewModel>.Success(StatusCode.Created);

                }
                catch(Exception ex)
                {
                    return Response<NoDataViewModel>.Fail("Bir hata meydana geldi: "+ex.Message,StatusCode.InternalServerError);
                }
            }
        }
        public async Task<Response<List<CategoryViewModel>>> GetCategoryList()
        {
             using(var context = new AppDbContext())
             {
                try
                {
                    var categoryList = await (from category in context.Categories
                                                select new CategoryViewModel(){
                                                    Id = category.Id,
                                                    Name = category.Name,
                                                    CreatedDate = category.CreatedDate
                                                }).ToListAsync();

                    return Response<List<CategoryViewModel>>.Success(categoryList,StatusCode.OK);
                }
                catch(Exception ex)
                {
                    return Response<List<CategoryViewModel>>.Fail("Bir hata meydana geldi. Hata: "+ ex,StatusCode.InternalServerError);
                }
             }
        }
        public async Task<Response<NoDataViewModel>> DeleteCategory(int id)
        {
            using(var context = new AppDbContext())
            {
                try
                {
                    var categoryQuery = from category in context.Categories
                                                where category.Id == id
                                                select category
                                                ;

                    var categoryResult = await categoryQuery.FirstOrDefaultAsync();

                    if(categoryResult == null)
                    {
                        return Response<NoDataViewModel>.Fail("There is no mathed category found given id.",StatusCode.NotFound);
                    }

                    context.Categories.Remove(categoryResult);
                    await context.SaveChangesAsync();
                    
                    return Response<NoDataViewModel>.Success(StatusCode.OK);

                }
                catch(Exception ex)
                {
                    return Response<NoDataViewModel>.Fail("Something went wrong.Error: "+ex,StatusCode.InternalServerError);
                }
            }
        }
    }
}