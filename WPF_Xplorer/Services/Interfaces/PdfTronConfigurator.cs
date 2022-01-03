using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Xplorer.Services.Interfaces
{
    public interface IPdfTronConfigurator
    {
        string Key { get; }
    }

    public class PdfTronConfigurator : IPdfTronConfigurator
    {
        public string Key { get; }

        public PdfTronConfigurator(string key)
        {
            Key = key;
        }
    }
}
