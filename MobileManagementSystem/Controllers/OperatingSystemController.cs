

using ViewModel.ViewModels.BrandViewModel;
using ViewModel.ViewModels.OperatingSystemViewModel;

namespace MobileManagementSystem.Controllers;
 
    [Route("api/[controller]")]
    [ApiController]
    
public class OperatingSystemController : BaseController
    {
    private readonly IMapper _mapper;
    private readonly IOperatingSystemService _operatingSystemService;
    public OperatingSystemController(IMapper mapper, IOperatingSystemService OperatingSystemService)
     { 
        _mapper = mapper;
        _operatingSystemService = OperatingSystemService;
      }

      [HttpGet("OperatingSystemList")]
      public async Task<IActionResult> OperatingSystemList()
      {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var operatingSytemList = await _operatingSystemService.Get();
        var osDto = _mapper.Map<List<OperatingSystemDto>>(operatingSytemList);
        if (osDto != null) {
            _response.Data = osDto;
            _response.Success = true;
            return Ok(_response);
        }
        else
        {
            _response.Success = false;
            _response.Message = CustomMessage.DataNotExit;
            return Ok(_response);
        }
   
      }

      [HttpGet("OperatingSystemDetail/{Id}")]
      public async Task<IActionResult> OperatingSystemDetail(int Id)
       {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var osObj = await _operatingSystemService.Get(Id);
        var osDto = _mapper.Map<OperatingSystemDto>(osObj);
        if (osDto != null)
        {
            _response.Data = osDto;
            _response.Success=true;
            return Ok(_response);
        }
        else
        {
            _response.Data=null;
            _response.Success = false;
            return Ok(_response);
        }

       }
       
      [HttpDelete("DeleteOperatingSystemData/{Id}")]
      public async Task<IActionResult> DeleteOperatingSystemData(int Id)
      {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var osObj = await _operatingSystemService.Get(Id);
        if(osObj != null)
        {
            await _operatingSystemService.Delete(osObj);
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

      [HttpPost("SaveOperatingSystemData")]
      public async Task<IActionResult> SaveOperatingSystemData(OperatingSystemDto operatingSystemDto)
     {
        var operatingSystemalreadyExit = await _operatingSystemService.OperatingSystemAlreadyExit(operatingSystemDto.Name);
        if (operatingSystemalreadyExit != null)
        {
            _response.Success = false;
            _response.Message = operatingSystemalreadyExit.Name + ' ' + "Already Exist";
            return Ok(_response);

        }
        else
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var operatingSystemsDto = _mapper.Map<OperatingSystems>(operatingSystemDto);
            await _operatingSystemService.Create(operatingSystemsDto);
            _response.Success = true;
            _response.Message = CustomMessage.Added;
            return Ok(_response);
        }


     }

      [HttpPut("UpdateOperatingSystemData")]
      public async Task<IActionResult> UpdateOperatingSystemData(OperatingSystemDto OperatingSystemDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var operatingSystemEntity = _mapper.Map<OperatingSystems>(OperatingSystemDto);

        var operatingSystemalreadyExit = await _operatingSystemService.OperatingSystemAlreadyExit(operatingSystemEntity.Name);
        if (operatingSystemalreadyExit != null)
        {
            _response.Success = false;
            _response.Message = operatingSystemalreadyExit.Name + ' ' + "Already Exist";
            return Ok(_response);

        }
        else
        {
            var objOS = await _operatingSystemService.Get(operatingSystemEntity.Id);
            if (objOS != null)
            {
                await _operatingSystemService.Update(objOS, operatingSystemEntity);
                _response.Success = true;
                _response.Message = CustomMessage.Updated; ;
                return Ok(_response);
            }
            else
            {
                _response.Success = false; ;
                _response.Message = CustomMessage.RecordNotFound;
                return Ok(_response);
            }
        }
        
    }
      [HttpGet("SearchData/{Name}")]
      public async Task<IActionResult> SearchOperatingSystem(string Name)
   {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var osSearchData = _operatingSystemService.SearchOperatingSystemsData(Name);
        _response.Data = osSearchData;
        _response.Success = true;
        return Ok(_response);
    }
}
 
