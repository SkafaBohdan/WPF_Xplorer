using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingPDFTron.Models
{
    public class PdfArray : PdfObj
    {

        public override string DisplayValue => $"{Key} {Value} elements";
        public override string DisplayKeyAndValue => $"{Key} {Value} elements";
        public PdfArray()
        {
            Type = PdfType.Array;
        }
    }
}
