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
        Task<List<PostViewModel>> GetActivePostList();
        Task<List<PostViewModel>> GetAllPosts();
        Task<List<PostViewModel>> GetActivePostsByCategoryName(string categoryName);
    }
}