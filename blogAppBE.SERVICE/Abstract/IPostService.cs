using blogAppBE.CORE.Generics;
using blogAppBE.CORE.RequestModels.Post;
using blogAppBE.CORE.ViewModels;

namespace blogAppBE.SERVICE.Abstract
{
    public interface IPostService
    {
        Task<Response<NoDataViewModel>> CreatePost(PostRequestModel request);
    }
}