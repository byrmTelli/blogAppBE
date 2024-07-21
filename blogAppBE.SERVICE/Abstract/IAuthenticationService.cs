using blogAppBE.CORE.Generics;
using blogAppBE.CORE.ViewModels.AuthenticationViewModels;
using blogAppBE.CORE.ViewModels.TokenVeiwModels;

namespace blogAppBE.SERVICE.Abstract
{
    public interface IAuthenticationService
    {
        Task<Response<TokenVeiwModel>> CreateTokenAsync(LoginRequestModel res);
        Task<Response<TokenVeiwModel>> CreateTokenByRefreshToken(string refreshToken);
        Task<Response<TokenVeiwModel>> RevokeRefreshToken(string refreshToken);
    }
}