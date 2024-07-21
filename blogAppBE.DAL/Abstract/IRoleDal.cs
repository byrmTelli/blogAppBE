using blogAppBE.CORE.Generics;
using blogAppBE.CORE.RequestModels.Role;
using blogAppBE.CORE.ViewModels;

namespace blogAppBE.DAL.Abstract
{
    public interface IRoleDal
    {
        Task<Response<NoDataViewModel>> AssingRoleToUser(AssignRoleToUserRequestModel request);
        Task<Response<NoDataViewModel>> CreateRole(CreateRoleRequestModel request);

    }
}