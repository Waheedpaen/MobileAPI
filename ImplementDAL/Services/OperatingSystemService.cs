

namespace ImplementDAL.Services;
public class OperatingSystemService : IOperatingSystemService
{
    private readonly IUnitofWork _unitOfWork;
 
    public OperatingSystemService(IUnitofWork unitOfWork )
    {
        
        _unitOfWork = unitOfWork;
    }
    public async Task<OperatingSystems> Create(OperatingSystems model)
    {
      
            model.Updated_At = null;
         await   _unitOfWork.OperatingSytemRepository.AddAsync(model);
            await _unitOfWork.CommitAsync();
            return model;
         
     
    }
    public async Task<OperatingSystems> Delete(OperatingSystems model)
    {
         
            model.IsDeleted = true;
         await   _unitOfWork.CommitAsync();
            return model;
         
    }
    public async Task<IEnumerable<OperatingSystems>> Get()
    {
       return await _unitOfWork.OperatingSytemRepository.GetAllAsync();
    }
    public async Task<OperatingSystems> Get(int id)
    {
        return await _unitOfWork.OperatingSytemRepository.GetByIdAsync(id);
    }

    public async Task<OperatingSystems> OperatingSystemAlreadyExit(string name)
    {
        return await _unitOfWork.OperatingSytemRepository.OperatingSystemAlreadyExit(name);
    }

    public async Task<List<OperatingSystems>> SearchOperatingSystemsData(string name)
    {
       return await _unitOfWork.OperatingSytemRepository.SearchOperatingSystemData(name);
    }
    public async Task<OperatingSystems> Update(OperatingSystems update, OperatingSystems model)
    {
       
        update.Name = model.Name;
        update.Updated_At = model.Updated_At;
       await  _unitOfWork.CommitAsync();
       return update;
        
 
    }
}
 
