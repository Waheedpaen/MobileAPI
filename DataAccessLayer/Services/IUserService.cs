﻿

using HelperData;
using ViewModel.ViewModels.UserViewModel;

namespace DataAccessLayer.Services;
 
     public  interface IUserService
     {
      Task<User> Logout();
    Task<LoginUserDto> Login(UserDtoLogin model);
    Task<ServiceResponse<object>> AddUser(UserAddDto model);
    Task<bool> UserAlreadyExit(string Name);
    Task<bool> UserNameAlreadyExit(string Name);
    Task<User> ChangePassword(User update, ChangePasswordDto model);
    Task<List<User>> GetUsers();
     Task<int> GetUserCount();
    Task<bool> CheckUserNameExistence(string Name);
    Task<bool> UserEmailAlreadyExit(string Name);

    Task<bool> UserPhoneAlreadyExit(string Name); 
    Task<User> UpdateUser(User update, User model);
    Task<User> GetUser(int userId);
    Task<List<User>> SearchingData(string name);
    Task<User> Delete(User model);
    Task<List<UserTypes>> GetUserTypes();
}
 
