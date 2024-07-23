using blogAppBE.CORE.Generics;
using blogAppBE.CORE.RequestModels.User;
using blogAppBE.CORE.ViewModels;
using blogAppBE.CORE.ViewModels.UserViewModels;

namespace blogAppBE.SERVICE.Abstract
{
    public interface IUserService
    {
        Task<Response<NoDataViewModel>> CreateUser(UserRegisterViewModel request);
        Task<Response<AppUserViewModel>> UpdateUser(string userId,AppUserUpdateRequestModel request);

    }
}