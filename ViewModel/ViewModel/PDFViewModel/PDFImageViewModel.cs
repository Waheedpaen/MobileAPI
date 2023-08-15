using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModel.PDFViewModel
{
    public class PDFImageViewModel
    {
                           
        public string Author { get; set; }
        public IFormFile  ?PdfFile { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
