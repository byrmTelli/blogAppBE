using blogAppBE.CORE.DataAccess.EntityFramework;
using blogAppBE.CORE.DBModels;
using blogAppBE.CORE.Generics;
using blogAppBE.CORE.RequestModels.Post;
using blogAppBE.CORE.ViewModels;
using blogAppBE.CORE.ViewModels.PostViewModels;
using blogAppBE.DAL.Abstract;
using blogAppBE.DAL.Context;
using Microsoft.EntityFrameworkCore;
using blogAppBE.CORE.Enums;

namespace blogAppBE.DAL.Concrete
{
    public class PostDal : EfEntityRepositoryBase<Post, AppDbContext>, IPostDal
    {
        public async Task<Response<NoDataViewModel>> CreatePost(PostRequestModel request)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var categoryQuery = (from category in context.Categories
                    where category.Id == request.CategoryId
                    select category);

                    var isCategoryExist = await categoryQuery.FirstOrDefaultAsync();

                    if(isCategoryExist == null)
                    {
                        return Response<NoDataViewModel>.Fail("There is no category matched given values",StatusCode.NotFound);
                    }

                    var postQuery = (from post in context.Posts
                                    where post.Title == request.Title &&
                                    post.Content == request.Content
                                    select post);
                    
                    var isPostExist = await postQuery.FirstOrDefaultAsync();

                    if(isPostExist != null)
                    {
                        return Response<NoDataViewModel>.Fail("There is already a post which is same. ",StatusCode.Conflict);
                    }

                    var newPost = new Post
                    {
                        Title = request.Title,
                        Content = request.Content,
                        PostCategory = isCategoryExist,
                        CreatedDate = DateTime.UtcNow
                    };
                    
                    await context.Posts.AddAsync(newPost);
                    await context.SaveChangesAsync();

                    return Response<NoDataViewModel>.Success(StatusCode.Created);
                }
                catch(Exception ex)
                {
                    return Response<NoDataViewModel>.Fail("Error occured: "+ex,StatusCode.InternalServerError);
                }
            }
        }
        public Task<List<PostViewModel>> GetActivePostList()
        {
            throw new NotImplementedException();
        }

        public Task<List<PostViewModel>> GetActivePostsByCategoryName(string categoryName)
        {
            throw new NotImplementedException();
        }

        public Task<List<PostViewModel>> GetAllPosts()
        {
            throw new NotImplementedException();
        }
    }
}