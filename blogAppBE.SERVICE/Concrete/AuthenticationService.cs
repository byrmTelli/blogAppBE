using blogAppBE.CORE.Generics;
using blogAppBE.CORE.ViewModels.AuthenticationViewModels;
using blogAppBE.CORE.ViewModels.TokenVeiwModels;
using blogAppBE.SERVICE.Abstract;
using blogAppBE.CORE.Enums;
using Microsoft.AspNetCore.Identity;
using blogAppBE.CORE.DBModels;
using blogAppBE.DAL.Abstract;
using blogAppBE.CORE.RequestModels.Token;

namespace blogAppBE.SERVICE.Concrete
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenDal _tokenDal;

        public AuthenticationService(UserManager<AppUser> userManager,ITokenDal tokenDal)  
        {
            _userManager = userManager;
            _tokenDal = tokenDal;
        }
        public async Task<Response<TokenVeiwModel>> CreateTokenAsync(LoginRequestModel request)
        {
            if(request == null)
            {
                return Response<TokenVeiwModel>.Fail("The request is invalid.",StatusCode.BadRequest);
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if(user == null)
            {
                return Response<TokenVeiwModel>.Fail("Email or password is wrong.", StatusCode.UnAuthorized);
            }

            var checkPassword = await _userManager.CheckPasswordAsync(user,request.Password);

            if(!checkPassword)
            {
                return Response<TokenVeiwModel>.Fail("Email or password is wrong.", StatusCode.UnAuthorized);
            }

            var newToken = await _tokenDal.CreateToken(user.Email);
            var userRefreshToken = await _tokenDal.GetRefreshToken(user.Id);

            if(userRefreshToken == null)
            {
                await _tokenDal.AddAsync(new UserRefreshToken {UserId = user.Id,Code = newToken.RefreshToken,Expiration = newToken.AccessTokenExpiration });
            }
            else
            {

                userRefreshToken.Code = newToken.RefreshToken;
                userRefreshToken.Expiration  = newToken.AccessTokenExpiration;
                await _tokenDal.UpdateAsync(userRefreshToken);
            }

            return Response<TokenVeiwModel>.Success(newToken, StatusCode.OK);
        }
        public async Task<Response<CreateTokenByRefreshTokenViewModel>> CreateTokenByRefreshToken(CreateTokenByRefreshTokenRequestModel request)
        {
            var result = await _tokenDal.CreateTokenByRefreshToken(request);
            return result;
        }
    }
}