using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ViewModel.ViewModel.PDFViewModel;

namespace MobileManagementSystems.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFController : ControllerBase
    {
        private readonly DataContexts _dbContext;
        private readonly IMapper _mapper; 
        private readonly IConfiguration _configuration;
        private readonly IBrandService _brandService;
        private readonly IWebHostEnvironment _hostEnvironment;
        public PDFController(DataContexts dbContext, IBrandService brandService, IMapper mapper, IWebHostEnvironment HostEnvironment, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _brandService = brandService;
            _configuration = configuration;
            _mapper = mapper;
            _hostEnvironment = HostEnvironment;
        }


        [HttpPost("upload")]
        public async Task<IActionResult> UploadPdf(IFormFile pdfFile)
        {
            if (pdfFile == null || pdfFile.Length <= 0)
            {
                return BadRequest("No file or empty file provided.");
            }

            using (var memoryStream = new MemoryStream())
            {
                await pdfFile.CopyToAsync(memoryStream);
                var pdfContent = memoryStream.ToArray();

                var pdfDocument = new PDF
                {
                    FileName = pdfFile.FileName,
                    Content = pdfContent
                };

                _dbContext.PDFs.Add(pdfDocument);
                await _dbContext.SaveChangesAsync();

                return Ok("PDF file uploaded and saved successfully.");
            }
        }







         



        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetPdf(int id)
        //{
        //    var pdfDocument = await _dbContext.PDFs.FindAsync(id);

        //    if (pdfDocument == null)
        //    {
        //        return NotFound();
        //    }

        //    return File(pdfDocument.Content, "application/pdf", pdfDocument.FileName);
        //}





        [HttpPost("uploads")]
        public async Task<IActionResult> UploadPdf([FromForm] PDFViewModel request)
        {
            if (request.PdfFile == null || request.PdfFile.Length <= 0)
            {
                return BadRequest("No file or empty file provided.");
            }

            var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "pdfs");
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.PdfFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.PdfFile.CopyToAsync(stream);
            }

            // Save additional metadata to the database
            var pdfDocument = new PdfDocument
            {
                FileName = fileName,
                Author = request.Author, // Assuming 'Author' is a property in the UploadPdfRequest class
                Content = System.IO.File.ReadAllBytes(filePath) // Read the content back from the saved file
            };

            _dbContext.PdfDocuments.Add(pdfDocument);
            await _dbContext.SaveChangesAsync();

            return Ok("PDF file uploaded and saved successfully.");
        }


        [HttpGet("{id}")]
        public IActionResult GetPdfById(int id)
        {
            var pdfDocument = _dbContext.PdfDocuments.Find(id);

            if (pdfDocument == null)
            {
                return NotFound();
            }

            var filePath = Path.Combine(_hostEnvironment.WebRootPath, "pdfs", pdfDocument.FileName);
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
             
         return File(fileStream, "application/pdf", pdfDocument.FileName);
        }


    }











    
}















 