using ViewModel.ViewModel.PDFViewModel;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using PuppeteerSharp;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ModelBinding;
 
 
using PuppeteerSharp; 
using MobileManagementSystems.GlobalReferences;

namespace MobileManagementSystems.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfDocumentImageController : ControllerBase
    {
        private readonly  PdfGeneratorData _pdfGenerator;
    
        private readonly DataContexts _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IBrandService _brandService;
        private readonly IWebHostEnvironment _hostEnvironment;
        public PdfDocumentImageController(DataContexts dbContext,
            IBrandService brandService, IMapper mapper,
            IWebHostEnvironment HostEnvironment, IConfiguration configuration,
      

       PdfGeneratorData pdfGenerator
            )
        {
            _dbContext = dbContext;
            _brandService = brandService;
            _configuration = configuration;
            _mapper = mapper;
            _hostEnvironment = HostEnvironment;
        
            _pdfGenerator = pdfGenerator;
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
                await request.ImageFile.CopyToAsync(imageStream);
            }

            var pdfDocumentImage = new PdfDocumentImage
            {
                FileNamePDF = pdfFileName,
                FilePathPDF = pdfFilePath,
                Author = request.Author,
                FilePathImage = imageFilePath,
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




          // how to generate pdf in .net core
        // using PuppeteerSharp.Media;
        //using PuppeteerSharp; 

        // 1: install using PuppeteerSharp
        // 2: generate class that name (PdfGeneratorData)
        // 3: builder.Services.AddTransient<PdfGeneratorData>() add it in program that services ;
        // 4:  call it in controller ;

        [HttpGet("wag")]
        public async Task<IActionResult> GeneratePdf()
        {
            var list = _dbContext.OperatingSystems.ToList();
            var loopContent = "";
            foreach (var item in list)
            {
                loopContent += $@"
    <div class=""col-4 mb-4"">
        {item.Name}
    </div>";
            }

            var htmlContent = $@"<!DOCTYPE html>
<html>
<head>
    <link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.1/dist/css/bootstrap.min.css"">
    <style>
        /* Your custom CSS styles here */
        body {{
            background-color: #f2f2f2;
            font-family: Arial, sans-serif;
        }}
        /* Additional custom styles */
    </style>
</head>
<body>
    <div class=""container"">
        <h1 class=""text-danger"">Custom PDF Example</h1>
        <p class=""lead"">This PDF was generated using PuppeteerSharp with Bootstrap and custom styles.  </p>
        <button class=""btn btn-primary"">Click me</button>
    </div>
  <div class=""container-fluid"">
<div class=""row"">
    {loopContent}

</div>
        <h1 class=""text-danger"">Custom PDF Example</h1>
        <p class=""lead"">This PDF was generated using PuppeteerSharp with Bootstrap and custom styles. </p>
        <button class=""btn btn-primary"">Click me</button>
    </div>
</body>
</html>";

            var pdfBytes = await   _pdfGenerator.GeneratePdfFromHtml(htmlContent);

            return File(pdfBytes, "application/pdf", "output.pdf");
        }
    }

}
 
