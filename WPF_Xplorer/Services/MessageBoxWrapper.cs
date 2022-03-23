using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Xplorer.Interfaces;

namespace WPF_Xplorer.Services
{
    public class MessageBoxWrapper : IMessageBox
    {
        public void MessageBoxShow(string messageBoxText, string title)
        {
            MessageBox.Show(messageBoxText, title);
        }
    }
}
