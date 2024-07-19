using blogAppBE.CORE.Generics;
using blogAppBE.CORE.RequestModels.Post;
using blogAppBE.CORE.ViewModels;
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
        public async Task<IActionResult> CreatePost([FromBody]PostRequestModel request)
        {
            var response = await _postService.CreatePost(request);
            if(response.IsSuccessfull)
            {
                return StatusCode((int)response.StatusCode);
            }

            return StatusCode((int)response.StatusCode,response.Errors);
        }

        [HttpGet]
        public async Task<IActionResult> GetPublishedPostList()
        {
            var response = await _postService.GetActivePosts();
            if(response.IsSuccessfull)
            {
                return StatusCode((int)response.StatusCode, response.Data);
            }

            return StatusCode((int)response.StatusCode, response.Errors);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost([FromBody]PostUpdateRequestModel request)
        {
            var response = await _postService.UpdatePost(request);
            if(response.IsSuccessfull)
            {
                return StatusCode((int)response.StatusCode);
            }

            return StatusCode((int)response.StatusCode, response.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost([FromRoute]int id)
        {
            var response = await _postService.DeletePost(id);
            if(response.IsSuccessfull)
            {
             return StatusCode((int)response.StatusCode);
            }

            return StatusCode((int)response.StatusCode, response.Errors);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var response = await _postService.GetAllPosts();
            if(response.IsSuccessfull)
            {
                return StatusCode((int)response.StatusCode,response.Data);
            }

            return StatusCode((int)response.StatusCode,response.Errors);
        }
    }
}