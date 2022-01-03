using pdftron.PDF;
using pdftron.SDF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Xplorer.Models;

namespace WPF_Xplorer.Services
{
    public class ObjBinder
    {
        public Obj Obj { get; set; }
        public PdfObj PdfObj { get; set; }
        public PDFDocInfo InfoDoc { get; set; }
    
        public ObjBinder (PdfObj pdfObject, Obj obj)
        {
            Obj = obj;
            PdfObj = pdfObject;
        }
    
    }
}
