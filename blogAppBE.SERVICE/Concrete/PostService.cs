using blogAppBE.CORE.Generics;
using blogAppBE.CORE.RequestModels.Post;
using blogAppBE.CORE.ViewModels;
using blogAppBE.CORE.ViewModels.PostViewModels;
using blogAppBE.DAL.Abstract;
using blogAppBE.SERVICE.Abstract;
using Microsoft.EntityFrameworkCore.Query;

namespace blogAppBE.SERVICE.Concrete
{
    public class PostService:IPostService
    {
        private readonly IPostDal _postDal;
        public PostService(IPostDal postDal)
        {
            _postDal = postDal;
        }

        public async Task<Response<NoDataViewModel>> CreatePost(PostRequestModel request)
        {
            var result = await _postDal.CreatePost(request);
            return result;
        }
        public async Task<Response<List<PostViewModel>>> GetActivePosts()
        {
            var result = await _postDal.GetPublishedPostList();
            return result;
        }

        public async Task<Response<NoDataViewModel>> UpdatePost(PostUpdateRequestModel request)
        {
            var result = await _postDal.UpdatePost(request);
            return result;
        }

        public async Task<Response<NoDataViewModel>> DeletePost(int id)
        {
            var result = await _postDal.DeletePost(id);
            return result;
        }

        public async Task<Response<List<PostViewModel>>> GetAllPosts()
        {
            var result = await _postDal.GetAllPosts();
            return result;
        }

    }
}