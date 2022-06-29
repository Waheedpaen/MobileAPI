
using ImplementDAL.Reporsitory;

namespace ImplementDAL.UnitWorks;
 
     public  class UnitWork : IUnitofWork
    {
    public readonly DataContexts _context;
    public UnitWork(DataContexts context)
    {
        _context = context;
    }
    private BrandRepository _brandRepository;
    private UserRepository _userRepository;
    private MobileRepository _mobileRepository;
    private OSVRepository _oSVRepository;
    private OperatingSytemRepository _operatingSytemRepository;
    public IBrandRepository BrandRepository => _brandRepository ??= new  (_context);

    public IOperatingSytemRepository OperatingSytemRepository => _operatingSytemRepository ??= new   (_context);

    public IOSVRepository OSVRepository => _oSVRepository ??new  (_context);

    public IMobileRepository MobileRepository => _mobileRepository ?? new (_context);

    public IUserRepository IUserRepository => _userRepository ??  new(_context);

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public void saveData()
    {
        
    }
}
 