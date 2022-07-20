



namespace MobileManagementSystem.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MobileController : BaseController
    {
    private readonly IMapper _mapper;
    private readonly IMobileService _mobileService;
    public MobileController(IMobileService mobileService, IMapper mapper)
    {
        _mobileService = mobileService;
        _mapper = mapper;
    }

    [HttpGet("MobileList")]
    public async Task<IActionResult> GetMobileList()
    {
         var mobileList = await _mobileService.Get();
         var mobileListDto = _mapper.Map<List<MobileDtoForList>>(mobileList);
         _response.Data = mobileListDto;
         _response.Success = true;
          return Ok(_response);
    }

    [HttpGet("MobileDetail/{Id}")]
    public async Task<IActionResult>  MobileDetail(int Id)
    {
        var mobileDetail = await _mobileService.Get(Id);
        var mobileDetailDto =  _mapper.Map<MobileDtoForList>(mobileDetail);
        if (mobileDetailDto != null)
        {
            _response.Data = mobileDetailDto;
            _response.Success = true;
            return Ok(_response);
        }
        else
        {
            _response.Data = null;
            _response.Success = false;
            return Ok(_response);
        }
    }
    [HttpPut("MobileUpdate")]
    public async Task<IActionResult> UpdateMobile (MobileDtoForSave updateMobile)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var mobileObjEntity =    _mapper.Map<Mobile>(updateMobile);
        if (mobileObjEntity != null)
        {
            var mobileDetail = await _mobileService.Get(mobileObjEntity.Id);
            await _mobileService.Update(mobileDetail,mobileObjEntity);
            //foreach (var item in mobileDetail.MobileImages)
            //{
            //    var dataMobileObj = await _mobileService.GetMobileImage(item.Id);
            //    await _mobileService.UpdateMobileImage(item, dataMobileObj);
            //}
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
 
    [HttpPost("MobileImageSave")]
    public async Task<IActionResult> MobileImageSave(List<MobileImageDtoForSave> saveMobile)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var mobileEntity = _mapper.Map<List<MobileImage>>(saveMobile);
        
        foreach (var item in mobileEntity)
        {
            await _mobileService.CreateMobileImage(item);
        }
        _response.Success = true;
        _response.Message = CustomMessage.Added;
        return Ok(_response);
    }
    [HttpPost("SaveMobile")]
    public async Task<IActionResult> SaveMobile(MobileDtoForSave saveMobile)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
  
           var mobileEntity = _mapper.Map<Mobile>(saveMobile);
            await _mobileService.Create(mobileEntity);
            foreach (var item in mobileEntity.MobileImages)
            { 
                await _mobileService.CreateMobileImages(item);
            }
            _response.Success = true;
            _response.Message = CustomMessage.Added; 
            return Ok(_response); 
    }

    [HttpDelete("DeleteMobileData/{Id}")]
    public async Task<IActionResult> DeleteMobileData(int Id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var obj = await _mobileService.Get(Id);
        if (obj != null)
        {
            await  _mobileService.Delete(obj);
            foreach(var item in obj.MobileImages)
            {
                await _mobileService.DeleteMobileImage(item);
            }
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

    [HttpDelete("DeleteSingleMobileImageData/{Id}")]
    public async Task<IActionResult> DeleteSingleMobileImageData(int Id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var obj = await _mobileService.GetMobileImage(Id);
        if(obj != null)
        {
            await _mobileService.DeleteMobileImage(obj);
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

    [HttpGet("SearchMobileData/{Name}")]
    public async Task<IActionResult> SearchMobileData(string Name)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var SearchData = await _mobileService.SearchMobileData(Name);
        _response.Data = SearchData;
        _response.Success = true;
        return Ok(_response);
    }
    [HttpGet("GetOSVByOperatingSystemId/{Id}")]
    public async Task<IActionResult> GetOSVByOperatingSystemId(int Id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var obj = await _mobileService.GetOSVersionByOperatingSystem(Id);
        _response.Data = obj;
        _response.Success = true;
        return Ok(_response);
    }
    [HttpGet("GetColor")]
    public async Task<IActionResult> GetColor()
    {
        var colorList = await _mobileService.GetColor();
        var colorListDto = _mapper.Map<List<ColorDto>>(colorList);
        _response.Data = colorListDto;
        _response.Success = true;
        return Ok(_response);
    }
    [HttpGet("GetMobileByBrand/{Id}")]
    public async Task<IActionResult> GetMobileByBrand(int Id)
    {
        var obj = _mobileService.GetMobileByBrand(Id);
        var objDto = _mapper.Map<MobileDtoForList>(obj);

        if (objDto != null)
        {
            _response.Data = objDto;
            _response.Success = true;
            return Ok(_response);
        }
        else
        {
            _response.Data = null;
            _response.Success = false;
            return Ok(_response);
        }
    }
    [HttpGet("GetMobileByColor/{Name}")]
    public async Task<IActionResult> GetMobileByColor(string Name)
    {
        var mobileDetail = await _mobileService.GetMobileListByColor(Name);
        var mobileDetailDto = _mapper.Map<List<MobileDtoForList>>(mobileDetail);
        if (mobileDetailDto != null)
        {
            _response.Data = mobileDetailDto;
            _response.Success = true;
            return Ok(_response);
        }
        else
        {
            _response.Data = null;
            _response.Success = false;
            return Ok(_response);
        }
    }

    [HttpPost("GetMobilesByPrice")]
    public async Task<IActionResult> GetMobilesByPrice(RangeDto model)
    {
        var mobileDetail = await _mobileService.GetMobilesByPrice(model);
        var mobileDetailDto = _mapper.Map<List<MobileDtoForList>>(mobileDetail);
        if (mobileDetailDto != null)
        {
            _response.Data = mobileDetailDto;
            _response.Success = true;
            return Ok(_response);
        }
        else
        {
            _response.Data = null;
            _response.Success = false;
            return Ok(_response);
        }
    }
    [HttpPost("GetMobilesByScreen")]
    public async Task<IActionResult> GetMobilesByScreen(RangeScreenSizeDto screenSize)
    {
        var mobileDetail = await _mobileService.GetMobilesByScreen(screenSize);
        var mobileDetailDto = _mapper.Map<List<MobileDtoForList>>(mobileDetail);
        if (mobileDetailDto != null)
        {
            _response.Data = mobileDetailDto;
            _response.Success = true;
            return Ok(_response);
        }
        else
        {
            _response.Data = null;
            _response.Success = false;
            return Ok(_response);
        }
    }

}
 
