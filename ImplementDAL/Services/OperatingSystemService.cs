

namespace ImplementDAL.Services;
public class OperatingSystemService : IOperatingSystemService
{
    private readonly IUnitofWork _unitOfWork;
    private readonly ILoggerManager _logger;
    public OperatingSystemService(IUnitofWork unitOfWork, ILoggerManager logger)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public async Task<OperatingSystems> Create(OperatingSystems model)
    {
        try
        {
            model.Updated_At = null;
         await   _unitOfWork.OperatingSytemRepository.AddAsync(model);
            await _unitOfWork.CommitAsync();
            return model;
        }
        finally
        {
            _logger.LogExit();
        }
     
    }
    public async Task<OperatingSystems> Delete(OperatingSystems model)
    {
        try
        {
            model.IsDeleted = true;
         await   _unitOfWork.CommitAsync();
            return model;
        }
        finally { _logger.LogExit(); }
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
        try { 
        update.Name = model.Name;
        update.Updated_At = model.Updated_At;
       await  _unitOfWork.CommitAsync();
       return update;
        }

        finally
        {
            _logger.LogExit();
        }
    }
}
 
