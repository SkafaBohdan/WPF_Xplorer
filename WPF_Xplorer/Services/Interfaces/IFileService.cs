using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Xplorer.Services.Interfaces
{
    public interface IFileService
    {
        List<PDF> Open(string fileName); // пдф файл который нужно открыть
    }
}
