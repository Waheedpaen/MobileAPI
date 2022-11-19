

namespace ImplementDAL.Services;

public class OSVService : IOSVService
{
    private readonly IUnitofWork _unitOfWork;
 
    public OSVService(IUnitofWork unitOfWork )
    {
     
        _unitOfWork = unitOfWork;
    }
    public async Task<OSVersion> Create(OSVersion model)
    {
       
            model.Updated_At = null;
           await _unitOfWork.OSVRepository.AddAsync(model);
            await _unitOfWork.CommitAsync();
            return model;
        

      
    }

    public async Task<OSVersion> Delete(OSVersion model)
    {
      
            model.IsDeleted = true;
            await _unitOfWork.CommitAsync();
            return model;
     
    }

    public async Task<List<OSVersion>> Get()
    {
        return await _unitOfWork.OSVRepository.Get();
    }

    public async Task<OSVersion> Get(int id)
    {
    return await _unitOfWork.OSVRepository.Get(id);
    }

    public async Task<List<OSVersion>> SearchOSVersionData(string name)
    {
        return await _unitOfWork.OSVRepository.SearchingData(name);
    }

    public async Task<OSVersion> Update(OSVersion update, OSVersion model)
    {
     
            update.Name = model.Name;
            update.Updated_At = model.Updated_At;
            update.OperatingSystemId = model.OperatingSystemId; ;
            await _unitOfWork.CommitAsync();
            return update;
       

       
    }
}
 
