using blogAppBE.CORE.DataAccess;
using blogAppBE.CORE.DBModels;
using blogAppBE.CORE.Generics;
using blogAppBE.CORE.RequestModels.Token;
using blogAppBE.CORE.ViewModels.TokenVeiwModels;

namespace blogAppBE.DAL.Abstract
{
    public interface ITokenDal: IEntityRepository<UserRefreshToken>
    {
        Task<TokenVeiwModel> CreateToken(string email);
        Task<UserRefreshToken> GetRefreshToken(string userId);
        Task<Response<CreateTokenByRefreshTokenViewModel>> CreateTokenByRefreshToken(CreateTokenByRefreshTokenRequestModel request);
    }
}