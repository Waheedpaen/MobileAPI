



using DataAccessLayer.Seeds;
using HelperData;
using ViewModel.ViewModels.UserViewModel;

namespace ImplementDAL.Services;
    public class UserService : IUserService
    {
    private readonly IUnitofWork _unitOfWork;
    private readonly ILoggerManager _logger;
    public UserService(IUnitofWork unitOfWork, ILoggerManager logger)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceResponse<object>> AddUser(UserAddDto model)
    { 
   var data =      await _unitOfWork.IUserRepository.AddUser(model);
        await _unitOfWork.CommitAsync();
        return data;
    }

    public async Task<User> ChangePassword(User update, ChangePasswordDto model)
    {
        byte[] passwordHash, passwordSalt;
        Seed.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);
        update.PasswordHash = passwordHash;
        update.PasswordSalt = passwordSalt;
        await _unitOfWork.CommitAsync();
        return update;
    }

    public async Task<bool> CheckUserNameExistence(string Name)
    {
        var data = await _unitOfWork.IUserRepository.SingleOrDefaultAsync(e => e.Name == Name.Replace("'", "''"));

        if (data == null)
            return false;
        else
            return true;
    }

    public async Task<User> Delete(User model)
    {
        try
        {
            _logger.LogEnter();
            model.IsDeleted = true;
            await _unitOfWork.CommitAsync();
            return model;
        }
        finally { _logger.LogExit(); }
    }

    public async Task<User> GetUser(int userId)
    {
        return await _unitOfWork.IUserRepository.GetUser(userId);
    }

    public async Task<int> GetUserCount()
    {
       return await _unitOfWork.IUserRepository.GetUserCount();
    }
    public async Task<List<User>> GetUsers()
    {
        return await _unitOfWork.IUserRepository.GetUsers();
    }

    public async Task<List<UserTypes>> GetUserTypes()
    {
         return await _unitOfWork.IUserRepository.GetUserTypes();
    }

    public async Task<LoginUserDto> Login(UserDtoLogin model)
    {
     
      var objUser =      await _unitOfWork.IUserRepository.Login(model);
       return objUser;

    }
    public async Task<User> Logout()
    {
        var userData = await _unitOfWork.IUserRepository.Logout();
          await _unitOfWork.CommitAsync();
         return userData;
    }

    public Task<List<User>> SearchingData(string name)
    {
        return _unitOfWork.IUserRepository.SearchingData(name);
    }

    public async Task<User> UpdateUser(User update, User model)
    {
        try
        {
            update.Name = model.Name;
            update.UserName = model.UserName;
            update.ImageUrl = model.ImageUrl;
            await _unitOfWork.CommitAsync();
            return update;
        }
catch (Exception ex)
        {
            throw (ex);
        }

    }

    public async Task<bool> UserAlreadyExit(string Name)
    {
        return await _unitOfWork.IUserRepository.UserAlreadyExit(Name);
    }
    public async Task<bool> UserEmailAlreadyExit(string Name)
    {
       return await _unitOfWork.IUserRepository.UserEmailAlreadyExit(Name);
    }

    public async Task<User> UserEmailAlreadyExitForVerify(string emailAddress)
    {
        return await _unitOfWork.IUserRepository.UserEmailAlreadyExitForVerify(emailAddress);
    }

    public Task<bool> UserNameAlreadyExit(string Name)
    {
        throw new NotImplementedException();
    }
    public Task<bool> UserPhoneAlreadyExit(string Name)
    {
        throw new NotImplementedException();
    }

    public async Task<EmailVerificationCode> VerifyEmailCodeAndEmail(EmailVerificationCode model)
    {
        var userData = await _unitOfWork.IUserRepository.verifyEmailCodeAndEmail(model);
        await _unitOfWork.CommitAsync();
        return model;
    }

    public async Task<EmailVerificationCode> verifyEmailCodeAndEmailCheck(string emailAddress)
    {
       return await _unitOfWork.IUserRepository.verifyEmailCodeAndEmailCheck(emailAddress);
    }

    public async Task<List<ColorProject>> ColorGetAll()
    {
        return await _unitOfWork.IUserRepository.ColorGetAll();
    }
    public async Task<ColorProject> UpdateColor(ColorProject update, ColorProject model)
    {
     
            update.Name = model.Name;
            update.Name = model.Name; 
            await _unitOfWork.CommitAsync();
            return update;
    }

    public async Task<ColorProject> ColorGet(int Id)
    {
        return await _unitOfWork.IUserRepository.ColorGet(Id);
    }
}
 
