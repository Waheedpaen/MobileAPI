



using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using HelperData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MobileManagementSystem.Controllers;
 
    [Route("api/[controller]")]
    [ApiController]
public class BrandController : BaseController
{
    private readonly DataContexts _dbContext;

        private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IBrandService _brandService;
    private readonly IWebHostEnvironment _hostEnvironment;
    public BrandController(DataContexts dbContext,IBrandService brandService, IMapper mapper, IWebHostEnvironment HostEnvironment, IConfiguration configuration)
        {
        _dbContext = dbContext;
        _brandService = brandService;
        _configuration = configuration;
        _mapper = mapper;
        _hostEnvironment = HostEnvironment;
    }
    [HttpGet("{id}/download")]
    public IActionResult DownloadPdfFile(int id)
    {
        var pdfFile = _dbContext.Brands.Find(id);

        if (pdfFile == null)
        {
            return NotFound();
        }

        var filePath = Path.Combine(pdfFile.FilePath, pdfFile.FileName);
        var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

        return new FileStreamResult(stream, "application/pdf");
    }
    [HttpGet("BrandList")]
        public async Task<IActionResult> GetBrandList()
        {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var listBrand = await _brandService.Get();
        List<BrandDto> brands = new();
        foreach (var item in listBrand)
        {
            var obj = new BrandDto();
            obj.Name = item.Name;
            obj.FullPath = _configuration.GetSection("AppSettings:SiteUrl").Value + item.FilePath + '/' + item.FileName;  
            brands.Add(obj);

        }

        

        if (brands != null)
        {
            _response.Data = brands;
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
        public async Task<IActionResult> SaveBrand([FromForm] BrandDto brandDto)
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
            if (brandDto.Photo.Length > 0)
            {
                var pathToSave = Path.Combine(_hostEnvironment.WebRootPath, "Cusine");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(brandDto.Photo.FileName);
                var fullPath = Path.Combine(pathToSave);
                brandDto.FilePath = "Cusine";
                brandDto.FileName = fileName;
                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }
                var filePath = Path.Combine(_hostEnvironment.WebRootPath, "Cusine", fileName);
                //string pathString = filePath.LastIndexOf("/") + 1;

                using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await brandDto.Photo.CopyToAsync(stream);
                }
                var objDishTag = new  Brand
                {
                    Name = brandDto.Name, 
                    
                    FileName = brandDto.FileName,
                    FilePath = brandDto.FilePath,
                    ImageUrl = "dara"
                    
                };
                await _brandService.Create(objDishTag);
                _response.Success = true;
                _response.Message = CustomMessage.Added;

                return Ok(_response);
            }
       
        }
        return Ok(_response);
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
 
