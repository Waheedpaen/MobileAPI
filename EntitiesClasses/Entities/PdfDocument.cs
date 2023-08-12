using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesClasses.Entities
{
    public class PdfDocument
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public string ? Author { get; set; } // New property for the author's name
    }
}
