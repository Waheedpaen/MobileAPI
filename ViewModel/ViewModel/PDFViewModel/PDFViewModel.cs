using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModel.PDFViewModel
{
    public class PDFViewModel
    {
        public IFormFile PdfFile { get; set; }
        public  string? Author { get; set; }
    }
}
