
using DataAccessLayer.IRepositories;

namespace DataAccessLayer.IUnitofWork;
    public  interface IUnitofWork  :  IDisposable
{
    IOperatingSytemRepository OperatingSytemRepository { get; }
    IUserRepository IUserRepository { get; }
    IOderRepository IOderRepository { get; }
    IOSVRepository   OSVRepository { get; }
    IMobileRepository MobileRepository { get; }
    IBrandRepository BrandRepository { get; }
    Task<int> CommitAsync();
    public void saveData();
}