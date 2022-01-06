using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Xplorer.Models
{
    public class PdfArray : PdfObj
    {

        public override string DisplayValue => $"{Key} {Value} el.";
        public override string DisplayKeyAndValue => $"{Key} {Value} el.";
        public PdfArray()
        {
            Type = PdfType.Array;
        }
    }
}
