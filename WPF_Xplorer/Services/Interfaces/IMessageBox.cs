using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_Xplorer.Services.Interfaces
{
    public interface IMessageBox
    {
        public void ShowMessage(Exception exception)
        {
            MessageBox.Show(exception.Message, "Error");
        } 

        public void ShowMessage(string description)
        {
            MessageBox.Show(description, "Error");
        }
    }
}
