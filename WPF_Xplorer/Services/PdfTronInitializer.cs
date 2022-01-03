using pdftron;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Xplorer.Services.Interfaces;

namespace WPF_Xplorer.Services
{
    public interface IPdfTronInitializer
    {
        void Initialize();
    }
    public class PdfTronInitializer : IPdfTronInitializer
    {
        private readonly IPdfTronConfigurator configurator;

        public PdfTronInitializer(IPdfTronConfigurator configurator)
        {
            this.configurator = configurator;
        }

        public void Initialize()
        {
            PDFNet.Initialize(configurator.Key);
        }
    }
}
