

using HelperData;
using ViewModel.ViewModels.UserViewModel;

namespace DataAccessLayer.Services;
 
     public  interface IUserService
     {
      Task<User> Logout();
    Task<Message> CreateSingleChatMessage(Message model);

    Task<LoginUserDto> Login(UserDtoLogin model);

    Task<ServiceResponse<object>> AddUser(UserAddDto model);
    Task<bool> UserAlreadyExit(string Name);
    Task<bool> UserNameAlreadyExit(string Name);
    Task<User> ChangePassword(User update, ChangePasswordDto model);
    Task<List<User>> GetUsers();
     Task<int> GetUserCount();
    Task<bool> CheckUserNameExistence(string Name);
    Task<bool> UserEmailAlreadyExit(string Name);
    Task<EmailVerificationCode> VerifyEmailCodeAndEmail(EmailVerificationCode model);

    Task<bool> UserPhoneAlreadyExit(string Name); 
    Task<User> UpdateUser(User update, User model);
    Task<User> GetUser(int userId);
    Task<List<User>> SearchingData(string name);
    Task<User> Delete(User model);
    Task<List<UserTypes>> GetUserTypes();
    Task<User> UserEmailAlreadyExitForVerify(string emailAddress);
    Task<EmailVerificationCode> verifyEmailCodeAndEmailCheck(string emailAddress);
    public Task<ColorProject> UpdateColor(ColorProject update, ColorProject model);
    public Task<List<ColorProject>> ColorGetAll();
    Task<ColorProject> ColorGet(int Id);


}
 
