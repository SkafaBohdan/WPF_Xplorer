using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Xplorer.Models
{
    public class PdfDict : PdfObj
    {
        public override string DisplayKey => $"/{Key} entitries";
        public override string DisplayValue => $"/{Value} entity";
        public PdfDict()
        {
            Type = PdfType.Dictionary;
        }
    }
}
