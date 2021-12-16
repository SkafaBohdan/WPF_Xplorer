using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingPDFTron.Models
{
    public class PdfString : PdfObj
    {
        public PdfString()
        {
            Type = PdfType.String;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
