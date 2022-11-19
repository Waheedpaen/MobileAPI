



using ClosedXML.Excel;
using HelperData;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;

namespace MobileManagementSystem.Controllers;
 
    [Route("api/[controller]")]
    [ApiController]
public class BrandController : BaseController
{ 
        private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IBrandService _brandService;
    private readonly IWebHostEnvironment _hostEnvironment;
    public BrandController(IBrandService brandService, IMapper mapper, IWebHostEnvironment HostEnvironment, IConfiguration configuration)
        {
              _brandService = brandService;
        _configuration = configuration;
        _mapper = mapper;
        _hostEnvironment = HostEnvironment;
    }

        [HttpGet("BrandList")]
        public async Task<IActionResult> GetBrandList()
        {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var listBrand = await _brandService.Get();
        //List<BrandDto> brands = new  ();
        //foreach (var item in listBrand)
        //{
        //    var obj = new  BrandDto();
        //    obj.Name = item.Name;
        //    obj.FullPath =    "images" + '/' + item.ImageUrl;
        //    brands.Add(obj);
         
        //}

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
        var BrandDto = _mapper.Map<Brand>(brandDto);
        var brandNameAlreadyExit = await _brandService.BrandNameAlreadyExit(brandDto.Name);
        if (brandNameAlreadyExit != null)
        {
            _response.Success = false;
            _response.Message = brandNameAlreadyExit.Name + ' ' + "Already Exist";
            return Ok(_response);

        }
        else
        {
            await _brandService.Create(BrandDto);
            _response.Success = true;
            _response.Message = CustomMessage.Added;

            return Ok(_response);
        }
    }
    [HttpPost("SaveBrands")]
    public async Task<IActionResult> SaveBrands([FromForm] BrandDto brandDto)
    {
    
     
        var brandNameAlreadyExit = await _brandService.BrandNameAlreadyExit(brandDto.Name);
        if (brandNameAlreadyExit != null)
        {
            _response.Success = false;
            _response.Message = brandNameAlreadyExit.Name + ' ' + "Already Exist";
            return Ok(_response);

        }
        else
        {
            //string uniqueFileName = null;
            //var BrandDto = _mapper.Map<Brand>(brandDto);
            //string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
            //uniqueFileName = Guid.NewGuid() + Path.GetExtension(brandDto.Photo.FileName);
            //string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            //using (var fileStream = new FileStream(filePath, FileMode.Create))
            //{
            //    brandDto.Photo.CopyTo(fileStream);
            //}
            //var objEntity = new Brand();
            //objEntity.Name = brandDto.Name;
            //objEntity.ImageUrl = uniqueFileName;
            //objEntity.Created_At = brandDto.Created_At;
            //await _brandService.Create(objEntity);
            //_response.Success = true;
            //_response.Message = CustomMessage.Added;

            return Ok(_response);
        }
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
        var brandNameAlreadyExit = await _brandService.BrandNameAlreadyExit(brandDto.Name);
        if(brandNameAlreadyExit != null)
        {
            _response.Success = false;
            _response.Message = brandNameAlreadyExit.Name + ' ' + "Already Exist";
            return Ok(_response);
        }
        else
        {
            var obj = await _brandService.Get(BrandEntity.Id);
            if (obj != null)
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
    [HttpGet("GetUserCount")]
    public async Task<IActionResult> GetUserCount()
    {
        using var workbook = new XLWorkbook();
        var listBrand = await _brandService.Get();
        var worksheet = workbook.Worksheets.Add("Users");
        var currentRow = 1;
      
        worksheet.Row(currentRow).Height = 25.0;
        worksheet.Row(currentRow).Style.Font.Bold = true;
        worksheet.Row(currentRow).Style.Fill.BackgroundColor = XLColor.Red;
        worksheet.Row(currentRow).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

        worksheet.Cell(currentRow, 1).Value = "Id";
        worksheet.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

        worksheet.Cell(currentRow, 2).Value = "Name";
       

      

        foreach (var user in listBrand)
        {
            currentRow++;

            worksheet.Row(currentRow).Height = 20.0;
            worksheet.Row(currentRow).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            worksheet.Cell(currentRow, 1).Value = user.Id;
            worksheet.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell(currentRow,1).Style.Font.FontColor = XLColor.Blue;

            worksheet.Cell(currentRow, 2).Value = user.Name;
            worksheet.Cell(currentRow,2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Columns().AdjustToContents();
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();

        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "users.xlsx");
    }



}
 
