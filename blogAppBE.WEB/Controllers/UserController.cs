using blogAppBE.CORE.Enums;
using blogAppBE.CORE.RequestModels.User;
using blogAppBE.SERVICE.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace blogAppBE.WEB.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController:ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserRegisterViewModel request)
        {
            var response = await _userService.CreateUser(request);
            if(response.IsSuccessfull)
            {
                return StatusCode((int)response.StatusCode, response.Errors);
            }
            return StatusCode((int)response.StatusCode);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] AppUserUpdateRequestModel request)
        {
            var response = await _userService.UpdateUser(request);
            if(response.IsSuccessfull)
            {
                return StatusCode((int)response.StatusCode, response.Data);
            }
            return StatusCode((int)response.StatusCode, response.Errors);
        }
    }
}