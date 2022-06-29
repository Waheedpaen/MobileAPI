

using HelperData;
using ViewModel.ViewModels.UserViewModel;

namespace DataAccessLayer.IRepositories;
 
  public  interface IUserRepository : IRepository<User, int>
    { 
    Task<User> Logout();
    Task<LoginUserDto> Login(UserDtoLogin model);

    Task<ServiceResponse<object>> AddUser(UserAddDto model);
    Task<bool> UserAlreadyExit(string Name);
    Task<bool> UserNameAlreadyExit(string Name);

    Task<bool> UserEmailAlreadyExit(string Name);

    Task<List<User>> GetUsers();
    Task<List<UserTypes>> GetUserTypes();

    Task<int> GetUserCount();
    Task<User> GetUser(int userId);
    Task<List<User>> SearchingData(string name);

    Task<bool> UserPhoneAlreadyExit(string Name);

}
 
