 
   namespace DataAccessLayer.IRepositories;

public interface IMobileRepository:   IRepository<Mobile, int>
{ 
    Task<Mobile> Get(int id);
    public Task<List<Mobile>> Get();
    Task<OperatingSystems> GetOperatingSystems(int Id);
    Task<List<OperatingSystems>> GetOperatingSystems();
    public Task<List<OSVersion>> GetOSVVersion();
    public Task<List<Color>> GetColor();

    Task<OSVersion> GetOSVersionbyID(int id);
    public Task<List<Mobile>> SearchingData(string name);
    public Task<List<OSVersion>> GetOSVersionByOperatingSystem(int id);
    Task<MobileImage> GetMobileImage(int id);
    Task<MobileImage> SaveMobileImage(MobileImage model);
    Task<MobileImage> SaveMobileImages(MobileImage model);
    Task<List<MobileImage>> GetMobileImage();

}
