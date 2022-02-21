using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_Xplorer.Models
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
