using blogAppBE.CORE.RequestModels.Role;
using blogAppBE.SERVICE.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace blogAppBE.WEB.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController:ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody]CreateRoleRequestModel request)
        {
            var response = await _roleService.CreateRole(request);
            if(!response.IsSuccessfull)
            {
                return StatusCode((int)response.StatusCode, response.Errors);
            }
            return StatusCode((int)response.StatusCode);
        }
    }
}