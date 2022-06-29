

namespace ImplementDAL.Services;

public class OSVService : IOSVService
{
    private readonly IUnitofWork _unitOfWork;
    private readonly ILoggerManager _logger;
    public OSVService(IUnitofWork unitOfWork, ILoggerManager logger)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public async Task<OSVersion> Create(OSVersion model)
    {
        try
        {
            model.Updated_At = null;
           await _unitOfWork.OSVRepository.AddAsync(model);
            await _unitOfWork.CommitAsync();
            return model;
        }

        finally
        {
            _logger.LogExit();
        }
    }

    public async Task<OSVersion> Delete(OSVersion model)
    {
        try
        {
            model.IsDeleted = true;
            await _unitOfWork.CommitAsync();
            return model;
        }
        finally { _logger.LogExit(); }
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
        try
        {
            update.Name = model.Name;
            update.Updated_At = model.Updated_At;
            update.OperatingSystemId = model.OperatingSystemId; ;
            await _unitOfWork.CommitAsync();
            return update;
        }

        finally
        {
            _logger.LogExit();
        }
    }
}
 
