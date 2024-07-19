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
using blogAppBE.CORE.ViewModels.CategoryViewModels;

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

                    if (isCategoryExist == null)
                    {
                        return Response<NoDataViewModel>.Fail("There is no category matched given values", StatusCode.NotFound);
                    }

                    var postQuery = (from post in context.Posts
                                     where post.Title == request.Title &&
                                     post.Content == request.Content
                                     select post);

                    var isPostExist = await postQuery.FirstOrDefaultAsync();

                    if (isPostExist != null)
                    {
                        return Response<NoDataViewModel>.Fail("There is already a post which is same. ", StatusCode.Conflict);
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
                catch (Exception ex)
                {
                    return Response<NoDataViewModel>.Fail("Error occured: " + ex, StatusCode.InternalServerError);
                }
            }
        }
        public async Task<Response<List<PostViewModel>>> GetPublishedPostList()
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var publishedPostQuery = (from post in context.Posts
                                              join category in context.Categories on post.CategoryId equals category.Id
                                              where post.IsPublished == true
                                              select new PostViewModel
                                              {
                                                  Id = post.Id,
                                                  Title = post.Title,
                                                  Content = post.Content,
                                                  Category = new CategoryViewModel
                                                  {
                                                      Id = category.Id,
                                                      Name = category.Name,
                                                      CreatedDate = category.CreatedDate
                                                  },
                                                  CreatedDate = post.CreatedDate,
                                              });

                    if (publishedPostQuery == null)
                    {
                        return Response<List<PostViewModel>>.Fail("There is no active post.", StatusCode.NotFound);
                    }

                    var publishedPostList = await publishedPostQuery.ToListAsync();

                    return Response<List<PostViewModel>>.Success(publishedPostList, StatusCode.OK);


                }
                catch (Exception ex)
                {
                    return Response<List<PostViewModel>>.Fail("Error occured. Error: " + ex, StatusCode.InternalServerError);
                }
            }
        }
        public async Task<Response<NoDataViewModel>> DeletePost(int id)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var postQuery = (from post in context.Posts
                                     where post.Id == id
                                     select post);

                    var isPostExist = await postQuery.FirstOrDefaultAsync();

                    if (isPostExist == null)
                    {
                        return Response<NoDataViewModel>.Fail("There is no post matched given values", StatusCode.NotFound);
                    }

                    context.Posts.Remove(isPostExist);
                    await context.SaveChangesAsync();

                    return Response<NoDataViewModel>.Success(StatusCode.OK);
                }
                catch (Exception ex)
                {
                    return Response<NoDataViewModel>.Fail("Something went wrong. Error: " + ex, StatusCode.InternalServerError);
                }
            }
        }
        public async Task<Response<NoDataViewModel>> UpdatePost(PostUpdateRequestModel request)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var postQuery = (from post in context.Posts
                                     where post.Id == request.Id
                                     select post);

                    var isPostExist = await postQuery.FirstOrDefaultAsync();
                    if (isPostExist == null)
                    {
                        return Response<NoDataViewModel>.Fail("There is no post matched given values.", StatusCode.NotFound);
                    }

                    var categoryQuery = (from category in context.Categories
                                         where category.Id == request.CategoryId
                                         select category);

                    var isCategoryExist = await categoryQuery.FirstOrDefaultAsync();

                    if (isCategoryExist == null)
                    {
                        return Response<NoDataViewModel>.Fail("There is no category matched given values", StatusCode.NotFound);
                    }


                    isPostExist.Title = request.Title;
                    isPostExist.Content = request.Content;
                    isPostExist.PostCategory = isCategoryExist;

                    context.Update(isPostExist);
                    context.SaveChanges();

                    return Response<NoDataViewModel>.Success(StatusCode.OK);
                }
                catch (Exception ex)
                {
                    return Response<NoDataViewModel>.Fail("While updating data an error occured. Error: " + ex, StatusCode.InternalServerError);
                }

            }
        }
        public async Task<Response<List<PostViewModel>>> GetAllPosts()
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var publishedPostQuery = (from post in context.Posts
                                              join category in context.Categories on post.CategoryId equals category.Id
                                              select new PostViewModel
                                              {
                                                  Id = post.Id,
                                                  Title = post.Title,
                                                  Content = post.Content,
                                                  Category = new CategoryViewModel
                                                  {
                                                      Id = category.Id,
                                                      Name = category.Name,
                                                      CreatedDate = category.CreatedDate
                                                  },
                                                  CreatedDate = post.CreatedDate,
                                              });

                    if (publishedPostQuery == null)
                    {
                        return Response<List<PostViewModel>>.Fail("There is no active post.", StatusCode.NotFound);
                    }

                    var publishedPostList = await publishedPostQuery.ToListAsync();

                    return Response<List<PostViewModel>>.Success(publishedPostList, StatusCode.OK);


                }
                catch (Exception ex)
                {
                    return Response<List<PostViewModel>>.Fail("Error occured. Error: " + ex, StatusCode.InternalServerError);
                }
            }

        }
    }
}