using blogAppBE.CORE.DBModels;
using blogAppBE.CORE.Enums;
using blogAppBE.CORE.Generics;
using blogAppBE.CORE.RequestModels.Role;
using blogAppBE.CORE.ViewModels;
using blogAppBE.DAL.Abstract;
using blogAppBE.DAL.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace blogAppBE.DAL.Concrete
{
    public class RoleDal : IRoleDal
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleDal(AppDbContext context,RoleManager<AppRole> roleManager,UserManager<AppUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<Response<NoDataViewModel>> AssingRoleToUser(AssignRoleToUserRequestModel request)
        {
            try
            {
                var isUserExist = await _userManager.FindByIdAsync(request.UserId);
                if(isUserExist == null)
                {
                    return Response<NoDataViewModel>.Fail("There is no related data given values.", StatusCode.NotFound);
                }

                var isRoleExist = await _roleManager.FindByIdAsync(request.RoleId);
                if(isRoleExist == null)
                {
                    return Response<NoDataViewModel>.Fail("There is no related data given values.", StatusCode.NotFound);
                }

                var result = await _userManager.AddToRoleAsync(isUserExist,isRoleExist.Name);
                if(result.Succeeded)
                {
                    return Response<NoDataViewModel>.Success(StatusCode.OK);
                }

                return Response<NoDataViewModel>.Fail("Something went wrong while granting user's role.", StatusCode.BadRequest);

            }
            catch(Exception ex)
            {
                return Response<NoDataViewModel>.Fail("Something went wrong. Error: " + ex, StatusCode.InternalServerError);
            }
            
            
        }

        public async Task<Response<NoDataViewModel>> CreateRole(CreateRoleRequestModel request)
        {
            try
            {
                var isRoleExist = await _roleManager.RoleExistsAsync(request.Name);
                if (isRoleExist)
                {
                    return Response<NoDataViewModel>.Fail("Role already exist.", StatusCode.Conflict);
                }

                var newRole = new AppRole
                {
                    Name = request.Name
                };

                var result = await _roleManager.CreateAsync(newRole);

                if(!result.Succeeded)
                {
                    var errors = result.Errors.Select(err => err.Description).ToList();
                    return Response<NoDataViewModel>.Fail(errors,StatusCode.BadRequest);
                    
                }

                await _context.SaveChangesAsync();

                return Response<NoDataViewModel>.Success(StatusCode.Created);
            }
            catch(Exception ex)
            {
                return Response<NoDataViewModel>.Fail("Error occured.Error: "+ex, StatusCode.InternalServerError);
            }
        }
    }
}