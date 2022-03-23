using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Xplorer.Interfaces
{
    public interface IMessageBox
    {
        void MessageBoxShow(string messageBoxText, string title);
    }
}
