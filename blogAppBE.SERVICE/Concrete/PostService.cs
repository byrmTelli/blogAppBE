using blogAppBE.CORE.Generics;
using blogAppBE.CORE.RequestModels.Post;
using blogAppBE.CORE.ViewModels;
using blogAppBE.DAL.Abstract;
using blogAppBE.SERVICE.Abstract;

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
    }
}