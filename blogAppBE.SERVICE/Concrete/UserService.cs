using blogAppBE.CORE.Generics;
using blogAppBE.CORE.RequestModels.User;
using blogAppBE.CORE.ViewModels;
using blogAppBE.CORE.ViewModels.UserViewModels;
using blogAppBE.DAL.Abstract;
using blogAppBE.SERVICE.Abstract;

namespace blogAppBE.SERVICE.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;
        public UserService(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public async Task<Response<NoDataViewModel>> CreateUser(UserRegisterViewModel request)
        {
            var result = await _userDal.CreateUser(request);
            return result;
        }

        public async Task<Response<AppUserViewModel>> UpdateUser(AppUserUpdateRequestModel request)
        {
            var result = await _userDal.UpdateUser(request);
            return result;
        }
    }
}