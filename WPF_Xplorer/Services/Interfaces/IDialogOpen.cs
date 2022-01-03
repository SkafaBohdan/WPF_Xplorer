using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Xplorer.Services.Interfaces
{
    public interface IDialogOpen
    {
        bool OpenDialog(object parameter, out string fileName);
    }
}
