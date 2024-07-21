using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using blogAppBE.CORE.DBModels;
using blogAppBE.CORE.Options;
using blogAppBE.DAL.Abstract;
using Microsoft.AspNetCore.Identity;
using blogAppBE.CORE.ViewModels.TokenVeiwModels;
using blogAppBE.DAL.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using blogAppBE.CORE.DataAccess.EntityFramework;
using Microsoft.Extensions.Options;
using blogAppBE.DAL.Context;


namespace blogAppBE.DAL.Concrete
{
    public class TokenDal : EfEntityRepositoryBase<UserRefreshToken, AppDbContext>, ITokenDal
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly CustomTokenOptions _tokenOptions;
        public TokenDal(UserManager<AppUser> userManager, IOptions<CustomTokenOptions> tokenOptions)
        {
            _userManager = userManager;
            _tokenOptions = tokenOptions.Value;
        }

        private string CreateRefreshToken()
        {
            var numberByte = new Byte[32];

            using var random = RandomNumberGenerator.Create();
            random.GetBytes(numberByte);


            return Convert.ToBase64String(numberByte);
        }
        private async Task<IEnumerable<Claim>> GetClaims(AppUser user, List<string> audiences)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var userClaimList = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            userClaimList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            userClaimList.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));
            return userClaimList;

        }
        public async Task<TokenVeiwModel> CreateToken(string email)
        {
            var isUserExist = await _userManager.FindByEmailAsync(email);

            if (isUserExist != null)
            {
                var accessTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOptions.AccessTokenExpiration);
                var refreshTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOptions.AccessTokenExpiration);
                var securityKey = SignInService.GetSymmetricSecurityKey(_tokenOptions.SecurityKey);
                SigningCredentials signInCredential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);


                JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                    //Bu tokeni yayınlayan kim ?
                    issuer: _tokenOptions.Issuer,
                    //Token süresi
                    expires: accessTokenExpiration,
                    //expires ile not Before arasında belirtilen alanda geçerli olur.
                    notBefore: DateTime.UtcNow,
                    //
                    claims: await GetClaims(isUserExist, _tokenOptions.Audience),
                    signingCredentials: signInCredential
                    );

                var handler = new JwtSecurityTokenHandler();


                var token = handler.WriteToken(jwtSecurityToken);

                var tokenDto = new TokenVeiwModel()
                {
                    AccessToken = token,
                    RefreshToken = CreateRefreshToken(),
                    AccessTokenExpiration = accessTokenExpiration,
                    RefreshTokenExpiration = refreshTokenExpiration
                };

                return tokenDto;

            }
            return new TokenVeiwModel { AccessToken = "", RefreshToken = "" };
        }

        //Getting users refresh token by user's id value
        public async Task<UserRefreshToken> GetRefreshToken(string userId)
        {
            using(var context = new AppDbContext())
            {

                    var tokenQuery = (from refreshToken in context.RefreshTokens
                                      where refreshToken.UserId == userId
                                      select refreshToken);

                    var userRefreshToken = await tokenQuery.SingleOrDefaultAsync();

                    return userRefreshToken;
            }
        }
    }
}