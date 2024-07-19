using blogAppBE.CORE.DataAccess;
using blogAppBE.CORE.DBModels;
using blogAppBE.CORE.Generics;
using blogAppBE.CORE.RequestModels.Post;
using blogAppBE.CORE.ViewModels;
using blogAppBE.CORE.ViewModels.PostViewModels;

namespace blogAppBE.DAL.Abstract
{
    public interface IPostDal:IEntityRepository<Post>
    {
        Task<Response<NoDataViewModel>> CreatePost(PostRequestModel request);
        Task<Response<List<PostViewModel>>> GetPublishedPostList();
        Task<Response<NoDataViewModel>> DeletePost(int id);
        Task<Response<NoDataViewModel>> UpdatePost(PostUpdateRequestModel request);
        Task<Response<List<PostViewModel>>> GetAllPosts();
    }
}