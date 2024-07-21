using blogAppBE.CORE.Generics;
using blogAppBE.CORE.RequestModels.Role;
using blogAppBE.CORE.ViewModels;

namespace blogAppBE.SERVICE.Abstract
{
    public interface IRoleService
    {
        Task<Response<NoDataViewModel>> CreateRole(CreateRoleRequestModel request);
    }
}