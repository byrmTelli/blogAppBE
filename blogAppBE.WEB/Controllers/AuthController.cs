using blogAppBE.CORE.RequestModels.Token;
using blogAppBE.CORE.ViewModels.AuthenticationViewModels;
using blogAppBE.SERVICE.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace blogAppBE.WEB.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        private IAuthenticationService _authService;
        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginRequestModel request)
        {
            var response = await _authService.CreateTokenAsync(request);
            if(response.IsSuccessfull)
            {
                return StatusCode((int)response.StatusCode,response.Data);
            }
            return StatusCode((int)response.StatusCode,response.Errors);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(CreateTokenByRefreshTokenRequestModel request)
        {
            var response = await _authService.CreateTokenByRefreshToken(request);
            if (response.IsSuccessfull)
            {
                return StatusCode((int)response.StatusCode, response.Data);
            }
            return StatusCode((int)response.StatusCode, response.Errors);
        }
    }
}