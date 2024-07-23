using blogAppBE.CORE.Generics;
using blogAppBE.CORE.RequestModels.User;
using blogAppBE.CORE.ViewModels;
using blogAppBE.CORE.ViewModels.UserViewModels;

namespace blogAppBE.DAL.Abstract
{
    public interface IUserDal
    {
        Task<Response<NoDataViewModel>> CreateUser(UserRegisterViewModel request);
        Task<Response<AppUserViewModel>> UpdateUser(string userId,AppUserUpdateRequestModel request);
    }
}