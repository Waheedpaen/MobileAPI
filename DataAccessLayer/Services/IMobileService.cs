 
  


   namespace DataAccessLayer.Services;
   public interface IMobileService
    {
    Task<IEnumerable<Mobile>> Get();
    Task<List<Mobile>> GetMobileByBrand(int Id);
    Task<Mobile> Get(int id);
    Task<Mobile> Create(Mobile model);
    Task<Mobile> Update(Mobile update, Mobile model);
    Task<List<Mobile>> SearchMobileData(string name);
    Task<Mobile> Delete(Mobile model);
    Task<List<Color>> GetColor();
    public Task<List<OSVersion>> GetOSVersion();
    Task<OperatingSystems> GetOperatingSystems(int Id);
    Task<List<OperatingSystems>> GetOperatingSystems();
    Task<List<OSVersion>> GetOSVersionByOperatingSystem(int id);
    Task<OSVersion> GetOSVersion(int id);

    Task<MobileImage> GetMobileImage(int id);
    Task<List<MobileImage>> GetMobileImage();
    Task<MobileImage> CreateMobileImage(MobileImage model);
    Task<MobileImage> CreateMobileImages(MobileImage model);
    Task<MobileImage> UpdateMobileImage(MobileImage update, MobileImage model);
 
    Task<MobileImage> DeleteMobileImage(MobileImage model);
    Task<List<Mobile>> GetMobileListByColor(string name);
    Task<List<Mobile>> GetMobilesByPrice(RangeDto model);

    Task<List<Mobile>> GetMobilesByScreen(RangeScreenSizeDto model);
}
 
