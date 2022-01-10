using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Xplorer.Models
{
    public class PdfDict : PdfObj
    {
        public int Size { get; set; }
        public override string DisplayKey => $"/{Key} {Size} entity";
        public override string DisplayValue => $"/{Size} entity";
        public PdfDict()
        {
            Type = PdfType.Dictionary;
        }
    }
}
