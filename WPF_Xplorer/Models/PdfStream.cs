using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Xplorer.Models
{
    public class PdfStream : PdfObj
    {
        public string Path { get; set; }
        public override string DisplayValue => $"{Value} bytes";
        public PdfStream()
        {
            Type = PdfType.Stream;
        }
    }
}
