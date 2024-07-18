using blogAppBE.CORE.Generics;
using blogAppBE.CORE.RequestModels.Post;
using blogAppBE.CORE.ViewModels;
using blogAppBE.CORE.ViewModels.PostViewModels;

namespace blogAppBE.SERVICE.Abstract
{
    public interface IPostService
    {
        Task<Response<NoDataViewModel>> CreatePost(PostRequestModel request);
        Task<Response<List<PostViewModel>>> GetActivePosts();
        Task<Response<NoDataViewModel>> UpdatePost(PostUpdateRequestModel request);
        Task<Response<NoDataViewModel>> DeletePost(int id);
        
    }
}