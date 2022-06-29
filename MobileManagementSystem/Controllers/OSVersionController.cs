 

    namespace MobileManagementSystem.Controllers;
 
    [Route("api/[controller]")]
    [ApiController]
    public class OSVersionController : BaseController
    {
    private readonly IMapper _mapper;
    private readonly IOSVService _oSVService;
 
    public OSVersionController(IMapper mapper, IOSVService oSVService)
    {
          _oSVService = oSVService;
          _mapper = mapper;
    }

    [HttpGet("OSVersionListData")]
    public async Task<IActionResult> OSVersionListData()
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var osvList = await _oSVService.Get();
        var osvListDto = _mapper.Map<List<OSVersionDtoForList>>(osvList);
        _response.Data = osvListDto;
        _response.Success = true;
        return Ok(_response);
    }

    [HttpGet("OSVersionDetailData/{Id}")]
    public async Task<IActionResult> OSVersionDetailData(int Id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var osvData = await _oSVService.Get(Id);
        var osvDto = _mapper.Map<OSVersionDtoForList>(osvData);
        if (osvDto != null)
        {
            _response.Data = osvDto;
            _response.Success = true;
            return Ok(_response);
        }
        else
        {
            _response.Data =null;
            _response.Success = false;
            return Ok(_response);
        }
    }

    [HttpDelete("DeleteOSVersionData/{Id}")]
    public async Task<IActionResult> OSVersionDeleteData(int Id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var obj = await _oSVService.Get(Id);
        if (obj != null)
        {
            await _oSVService.Delete(obj);
            _response.Success = true;
            _response.Message = CustomMessage.Deleted;
            return Ok(_response);
        }
        else
        {
            _response.Success = false;
            _response.Message = CustomMessage.RecordNotFound;
            return Ok(_response);
        }
     
    }

    [HttpPost("SaveOSVersionData")]
    public async Task<IActionResult> SaveOSVersionData(OSVersionDtoForSave oSVersionDtoForSave)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var osvEntity = _mapper.Map<OSVersion>(oSVersionDtoForSave); 
        await _oSVService.Create(osvEntity);
        _response.Success = true;
        _response.Message = CustomMessage.Added;
        return Ok(_response);
    }

    [HttpPut("UpdateOSVersionData")]
    public async Task<IActionResult> UpdateOSVersionData(OSVersionDtoForSave oSVersionDtoForUpdate)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var osvEntity = _mapper.Map<OSVersion>(oSVersionDtoForUpdate);
        var osvId = await _oSVService.Get(osvEntity.Id);
        if(osvId != null)
        {
            await _oSVService.Update(osvId, osvEntity);
            _response.Success = true;
            _response.Message = CustomMessage.Updated;
            return Ok(_response);
        }
        else
        {
            _response.Success = false;
            _response.Message = CustomMessage.RecordNotFound;
            return Ok(_response);
        }

    }
    [HttpGet("SearchData/{Name}")]
    public async Task<IActionResult> SearchData(string Name)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var SearchData =  await _oSVService.SearchOSVersionData(Name);
        _response.Data = SearchData;
        _response.Success = true;
        return Ok(_response);
    }
}
 
