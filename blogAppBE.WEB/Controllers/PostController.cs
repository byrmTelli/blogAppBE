using blogAppBE.CORE.RequestModels.Post;
using blogAppBE.SERVICE.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace blogAppBE.WEB.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostController:ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(PostRequestModel request)
        {
            var response = await _postService.CreatePost(request);
            if(response.IsSuccessfull)
            {
                return StatusCode((int)response.StatusCode);
            }

            return StatusCode((int)response.StatusCode,response.Errors);
        }
    }
}