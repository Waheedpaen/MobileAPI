
namespace ImplementDAL.Services;
public class MobileService : IMobileService
{
    private readonly IUnitofWork _unitOfWork;
    private readonly ILoggerManager _logger;
    public MobileService(IUnitofWork unitOfWork, ILoggerManager logger)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public async Task<Mobile> Create(Mobile model)
    {
        
        model.Updated_At = null;
            await  _unitOfWork.MobileRepository.AddAsync(model);
        await _unitOfWork.CommitAsync();
        return model;

    }

    public async Task<Mobile> Delete(Mobile model)
    {
        model.IsDeleted = true;
        await _unitOfWork.CommitAsync();
        return model;
    }

    public async Task<IEnumerable<Mobile>> Get()
    {
        return await _unitOfWork.MobileRepository.Get();
    }

    public async Task<Mobile> Get(int id)
    {
        return await _unitOfWork.MobileRepository.Get(id);
    }

    public async Task<OperatingSystems> GetOperatingSystems(int Id)
    {
        return await _unitOfWork.MobileRepository.GetOperatingSystems(Id);
    }

    public async Task<List<OperatingSystems>> GetOperatingSystems()
    {
        return await _unitOfWork.MobileRepository.GetOperatingSystems();
    }

    public async Task<OSVersion> GetOSVersion(int id)
    {
        return await _unitOfWork.MobileRepository.GetOSVersionbyID(id);
    }

    public async Task<List<OSVersion>> GetOSVersionByOperatingSystem(int id)
    {
        return await _unitOfWork.MobileRepository.GetOSVersionByOperatingSystem(id);
    }

    public async Task<List<OSVersion>> GetOSVersion()
    {
        return await _unitOfWork.MobileRepository.GetOSVVersion();
    }

    public async Task<List<Mobile>> SearchMobileData(string name)
    {
        return await _unitOfWork.MobileRepository.SearchingData(name);
    }

    public async Task<Mobile> Update(Mobile update, Mobile model)
    {
        update.Sell=model.Sell;
        update.Wifi=model.Wifi;
        update.Weight=model.Weight;
      
        update.Quantity=model.Quantity;
        update.Bluetooth=   model.Bluetooth;
        update.BatteryMah=model.BatteryMah;
        update.Camera=model.Camera;
        update.Charger=model.Charger;
        update.USBConnector = model.USBConnector;
        update.Storage=model.Storage;
        update.BrandId=model.BrandId;
        update.ColorId=model.ColorId;   
        update.OSVersionId=model.OSVersionId;
        update.CPU=model.CPU; 
        update.LaunchDate=model.LaunchDate; 
        update.HeadPhoneJack=model.HeadPhoneJack;
        update.Ram =    model.Ram; 
        update.MobileWeight=model.MobileWeight;
        update.MobilePrice  =model.MobilePrice;
        update.Description=model.Description;
        update.Name=model.Name;
        update.Processor=model.Processor;
        update.HeadPhoneJack= model.HeadPhoneJack;
        update.MobilePrice=model.MobilePrice;
        update.ScreenSize = update.ScreenSize;
        update.ScreenType=model.ScreenType;
        update.Updated_At=model.Updated_At;
        update.StockAvailiability = model.StockAvailiability;
        await _unitOfWork.CommitAsync();
        return update;
         
    }

    public async Task<MobileImage> GetMobileImage(int id)
    {
        return await _unitOfWork.MobileRepository.GetMobileImage(id);
    }

    public async Task<List<MobileImage>> GetMobileImage()
    {
        return await  _unitOfWork.MobileRepository.GetMobileImage();
    }

    public async Task<MobileImage> CreateMobileImage(MobileImage model)
    {
        model.Updated_At = null;
        await _unitOfWork.MobileRepository.SaveMobileImage(model);
        await _unitOfWork.CommitAsync();
        return model;
    }
    public async Task<MobileImage> CreateMobileImages(MobileImage model)
    {
        model.Updated_At = null;
    await    _unitOfWork.MobileRepository.SaveMobileImages(model);
        await _unitOfWork.CommitAsync();
        return model;
    }

    public async Task<MobileImage> UpdateMobileImage(MobileImage update, MobileImage model)
    {
        update.MobileId= model.MobileId;
        update.ImageUrl= model.ImageUrl;
        update.Updated_At = model.Updated_At;
        await _unitOfWork.CommitAsync();
        return update;
    }

    public async Task<List<Mobile>> SearchMobileImageData(string name)
    {
         return await _unitOfWork.MobileRepository.SearchingData(name);
    }

    public async Task<MobileImage> DeleteMobileImage(MobileImage model)
    {
        model.IsDeleted = true;
        await    _unitOfWork.CommitAsync();
        return model;
         
    }
    public async Task<List<Color>> GetColor()
    {
        var color = await _unitOfWork.MobileRepository.GetColor();
        return color;
    }
    Task<List<MobileImage>> IMobileService.SearchMobileImageData(string name)
    {
        throw new NotImplementedException();
    }
}
 
