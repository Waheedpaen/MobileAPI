using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModel.ViewModel.PDFViewModel;

namespace MobileManagementSystems.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfDocumentImageController : ControllerBase
    {
        private readonly DataContexts _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IBrandService _brandService;
        private readonly IWebHostEnvironment _hostEnvironment;
        public PdfDocumentImageController(DataContexts dbContext, IBrandService brandService, IMapper mapper, IWebHostEnvironment HostEnvironment, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _brandService = brandService;
            _configuration = configuration;
            _mapper = mapper;
            _hostEnvironment = HostEnvironment;
        }


        [HttpPost("uploads")]
        public async Task<IActionResult> UploadPdf([FromForm] PDFImageViewModel request)
        {
            if (request.PdfFile == null || request.PdfFile.Length <= 0 || request.ImageFile == null || request.ImageFile.Length <= 0)
            {
                return BadRequest("No file or empty file provided.");
            }

            var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
            var pdfFileName = Guid.NewGuid().ToString() + Path.GetExtension(request.PdfFile.FileName);
            var imageFileName = Guid.NewGuid().ToString() + Path.GetExtension(request.ImageFile.FileName);
            var pdfFilePath = Path.Combine(uploadsFolder, pdfFileName);
            var imageFilePath = Path.Combine(uploadsFolder, imageFileName);

            using (var pdfStream = new FileStream(pdfFilePath, FileMode.Create))
            {
                await request.PdfFile.CopyToAsync(pdfStream);
            }

            using (var imageStream = new FileStream(imageFilePath, FileMode.Create))
            {
                await request .ImageFile.CopyToAsync(imageStream);
            }

            var pdfDocumentImage = new PdfDocumentImage
            {
                FileNamePDF = pdfFileName, 
                FilePathPDF = pdfFilePath,
                Author = request.Author,
                FilePathImage = imageFilePath ,
                FileNameImage = imageFileName,
                
                // Assuming your PdfDocument model has a property for the image path
            };

            _dbContext.PdfDocumentImages.Add(pdfDocumentImage);
            await _dbContext.SaveChangesAsync();
            return Ok("PDF file uploaded and saved successfully.");
        }


        [HttpGet("image/{id}")]
        public async Task<IActionResult> GetImageById(int id)
        {
            var pdfDocumentImage = await _dbContext.PdfDocumentImages.FindAsync(id);

            if (pdfDocumentImage == null)
            {
                return NotFound();
            }

            var imageUrl = pdfDocumentImage.FilePathImage;

            return Ok(new { ImageUrl = imageUrl });
        }
    }
}
