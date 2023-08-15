using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesClasses.Entities
{
    public  class PdfDocumentImage
    {
        public int Id { get; set; }
      
 



        public string Author { get; set; }

        public string? FileNamePDF { get; set; }
        public string? FilePathPDF { get; set; }

        public string FileNameImage { get; set; }
        public string FilePathImage { get; set; }

    

    }
}
