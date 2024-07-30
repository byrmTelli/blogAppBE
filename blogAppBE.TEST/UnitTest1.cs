using blogAppBE.CORE.DBModels;
using blogAppBE.CORE.Enums;
using blogAppBE.CORE.Generics;
using blogAppBE.CORE.RequestModels.User;
using blogAppBE.CORE.ViewModels;
using blogAppBE.CORE.ViewModels.UserViewModels;
using blogAppBE.SERVICE;
using blogAppBE.SERVICE.Abstract;
using Moq;


namespace blogAppBE.TEST;

public class UnitTest1
{
    private readonly IUserService _userService ;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly List<AppUser> _userList;
    public UnitTest1()
    {
        _userServiceMock = new Mock<IUserService>();
        _userService = _userServiceMock.Object;
        _userList = new List<AppUser>()
        {
            new AppUser(){
                Id = "existingUserId", 
                UserName = "existingUserName", 
                Email = "existingmail@test.com",
                PhoneNumber = "555 444 33 22"
                }
        };
    }
    [Fact]
    public async Task UpdateUser_IfIdNotFound_ShouldReturnNotFound()
    {
        //Arange
        var userId = "";

        var updateUserRequestModel = new AppUserUpdateRequestModel() { 
            // Email,UserName,PhoneNumber
            Email = "",
            UserName = "",
            PhoneNumber = "",
        };

        var response = Response<AppUserViewModel>.Fail("There is no user matched given values.", StatusCode.NotFound);

        _userServiceMock.Setup(service => service.UpdateUser(userId, updateUserRequestModel))
                        .ReturnsAsync(response);

        //Act

        var result = await _userService.UpdateUser(userId, updateUserRequestModel);

        //Assert

        Assert.False(result.IsSuccessfull);
        Assert.Contains("There is no user matched given values.",result.Errors.Errors);
        Assert.Null(result.Data);
        Assert.Equal(StatusCode.NotFound,result.StatusCode);
    }

    [Fact]
    public async Task UpdateUser_IfIdMatched_ShouldReturnSuccess()
    {
        //Arrange
        var userId = "existingUserId";

        var updateUserRequestModel = new AppUserUpdateRequestModel()
        {
            Email = "existingmail@test.com",
            UserName = "Testing User",
            PhoneNumber = "555 444 33 22"
        };

        var userViewModel = new AppUserViewModel()
        {
            Id = userId,
            UserName = updateUserRequestModel.UserName,
            PhoneNumber = updateUserRequestModel.PhoneNumber
        };

        var response = Response<AppUserViewModel>.Success(userViewModel,StatusCode.OK);

        _userServiceMock.Setup(service => service.UpdateUser(userId,updateUserRequestModel))
                                                 .ReturnsAsync((string id,AppUserUpdateRequestModel request)=>{
                                                     var isUserExist = _userList.FirstOrDefault(x => x.Id == id);
                                                        if(isUserExist != null)
                                                        {
                                                         var userViewModel = new AppUserViewModel()
                                                         {
                                                             Id = isUserExist.Id,
                                                             UserName = isUserExist.UserName,
                                                             PhoneNumber = isUserExist.PhoneNumber

                                                         };
                                                         return Response<AppUserViewModel>.Success(userViewModel, StatusCode.OK);
                                                     }

                                                     return Response<AppUserViewModel>.Fail("There is no matched user givend values",StatusCode.NotFound);
                                                 });
        //Act

        var result = await _userService.UpdateUser(userId, updateUserRequestModel);

        //Assert

        Assert.True(result.IsSuccessfull);
        Assert.Equal(StatusCode.OK,result.StatusCode);
        Assert.IsType<AppUserViewModel>(result.Data);
    }
}