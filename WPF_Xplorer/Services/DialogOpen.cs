using Microsoft.Win32;
using System.Windows.Controls;
using WPF_Xplorer.Services.Interfaces;


namespace WPF_Xplorer.Services
{
    public class DialogOpen: IDialogOpen
    {
        public bool OpenDialog(object parameter, out string fileName)
        {
            fileName = "";
            var openDialogFile = new OpenFileDialog();
            openDialogFile.Filter = "Pdf files (*.pdf)  | *.pdf";

            if (openDialogFile.ShowDialog() != true) return false;

            fileName = openDialogFile.FileName;

            return parameter is TreeView;
        }
    }
}
