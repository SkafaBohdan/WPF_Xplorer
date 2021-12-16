using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingPDFTron.Models
{
    public class PdfBool : PdfObj
    {
        public PdfBool()
        {
            Type = PdfType.Bool;
        }
    }
}
