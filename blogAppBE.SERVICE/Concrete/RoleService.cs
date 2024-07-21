using blogAppBE.CORE.Generics;
using blogAppBE.CORE.RequestModels.Role;
using blogAppBE.CORE.ViewModels;
using blogAppBE.DAL.Abstract;
using blogAppBE.SERVICE.Abstract;

namespace blogAppBE.SERVICE.Concrete
{
    public class RoleService : IRoleService
    {
        private readonly IRoleDal _roleDal;
        public RoleService(IRoleDal roleDal)
        {
            _roleDal = roleDal;
        }
        public async Task<Response<NoDataViewModel>> CreateRole(CreateRoleRequestModel request)
        {
            var result = await _roleDal.CreateRole(request);
            return result;
        }
    }
}