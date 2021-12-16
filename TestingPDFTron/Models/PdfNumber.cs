using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingPDFTron.Models
{
    public class PdfNumber : PdfObj
    {
        public PdfNumber()
        {
            Type = PdfType.Number;
        }
    }
}
