using blogAppBE.CORE.DBModels;
using blogAppBE.CORE.Generics;
using blogAppBE.CORE.RequestModels.User;
using blogAppBE.CORE.ViewModels;
using blogAppBE.DAL.Abstract;
using blogAppBE.DAL.Context;
using Microsoft.AspNetCore.Identity;
using blogAppBE.CORE.Enums;
using blogAppBE.CORE.ViewModels.UserViewModels;

namespace blogAppBE.DAL.Concrete
{
    public class UserDal : IUserDal
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        public UserDal(AppDbContext context, UserManager<AppUser> userManager,RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<Response<NoDataViewModel>> CreateUser(UserRegisterViewModel request)
        {
            var isUserExist = await _userManager.FindByEmailAsync(request.EmailAdress);
            if(isUserExist != null)
            {
                return Response<NoDataViewModel>.Fail("Invalid values.",StatusCode.UnAuthorized);
            }

            var newUser = new AppUser
            {
                UserName = request.Username,
                CreatedDate = DateTime.UtcNow,
                Email = request.EmailAdress,
                PhoneNumber = request.PhoneNumber
            };

            var createResponse = await _userManager.CreateAsync(newUser, request.Password);
            if(!createResponse.Succeeded)
            {
                var errors = createResponse.Errors.Select(er => er.Description).ToList();
                return Response<NoDataViewModel>.Fail(errors,StatusCode.BadRequest);
            }

            return Response<NoDataViewModel>.Success(StatusCode.Created);
        }

        public async Task<Response<AppUserViewModel>> UpdateUser(string userId,AppUserUpdateRequestModel request)
        {
            var isUserExist = await _userManager.FindByIdAsync(userId);

            if(isUserExist == null)
            {
                return Response<AppUserViewModel>.Fail("There is no user matched given values.", StatusCode.NotFound);
            }
            
            isUserExist.UserName = request.UserName;  
            isUserExist.PhoneNumber = request.PhoneNumber;

            var userViewModel = new AppUserViewModel
            {
                Id = isUserExist.Id,
                UserName = isUserExist.UserName,
                PhoneNumber = isUserExist.PhoneNumber,
                CreatedDate = isUserExist.CreatedDate
            };

            _context.Update(isUserExist);
            await _context.SaveChangesAsync();

            return Response<AppUserViewModel>.Success(userViewModel, StatusCode.OK);
        }
    }
}