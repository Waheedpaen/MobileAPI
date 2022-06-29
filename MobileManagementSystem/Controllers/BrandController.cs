


namespace MobileManagementSystem.Controllers;
 
    [Route("api/[controller]")]
    [ApiController]
public class BrandController : BaseController
{ 
        private readonly IMapper _mapper;
    private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService, IMapper mapper)
        {
              _brandService = brandService; 
             _mapper = mapper;
        }

        [HttpGet("BrandList")]
        public async Task<IActionResult> GetBrandList()
        {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var listBrand = await _brandService.Get();

       var brandDto =      _mapper.Map<List<BrandDto>>(listBrand);

        if (brandDto != null)
        {
            _response.Data = brandDto;
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

        [HttpGet("BrandDetail/{Id}")]
        public async Task<IActionResult> BrandDetail(int Id)
        {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var obj = await _brandService.Get(Id);
        var brandDto = _mapper.Map<BrandDto>(obj);
        if (brandDto != null)
        {
            _response.Data = brandDto; ;
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
     
        [HttpDelete("DeleteBrandData/{Id}")]
        public async Task<IActionResult> DeleteBrandData(int Id)
        {
        if (!ModelState.IsValid) return BadRequest(ModelState);
         var obj = await _brandService.Get(Id);
        if(obj != null)
        {
            await _brandService.Delete(obj);
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

        [HttpPost("SaveBrand")]
        public async Task<IActionResult> SaveBrand(BrandDto brandDto)
        {
        if (!ModelState.IsValid) return BadRequest(ModelState);
          var BrandDto = _mapper.Map<Brand>(brandDto);
        await _brandService.Create(BrandDto); 
         _response.Success=true;
        _response.Message = CustomMessage.Added;

        return Ok(_response);
        }

        [HttpPost("SaveBrandListExcel")]
        public async Task<IActionResult> SaveBrandExcel(List<BrandDto> brands)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var brandsDto = _mapper.Map<List<Brand>>(brands);
        foreach (var item in brandsDto)
        {
            await _brandService.Create(item);
        }
        _response.Success = true;
        _response.Message = CustomMessage.Added;
        return Ok(_response);
    }

        [HttpPut("UpdateBrand")]
        public async Task<IActionResult> UpdateBrand(BrandDto brandDto)
       {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var  BrandEntity = _mapper.Map<Brand>(brandDto);
        var obj = await _brandService.Get(BrandEntity.Id);
        if(obj != null)
        {
            await _brandService.Update(obj, BrandEntity);
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
        public async Task<IActionResult> SearchBrandData(string Name)
       {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var searchData = _brandService.SearchBrandData(Name);
        if (searchData != null)
        {
            _response.Data = searchData;
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





}
 
