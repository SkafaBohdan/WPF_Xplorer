using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Xplorer.Models
{
    public class PdfIndirectObj : PdfObj
    {
        public int GenNumber { get; set; }
        public int ObjNum { get; set; }
        public override string DisplayKey => $"/{Key} {ObjNum} {GenNumber} obj";
        public override string DisplayValue => $"{Key} {ObjNum} {GenNumber} R";
        public override string DisplayKeyAndValue => DisplayKey;
        public PdfIndirectObj()
        {
            Type = PdfType.Indirect;
        }
    }
}
