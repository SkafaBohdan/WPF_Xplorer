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
            var openDialog = new OpenFileDialog();
            openDialog.Filter = "Pdf files (*.pdf)  | *.pdf";

            if (openDialog.ShowDialog() != true) return false;

            fileName = openDialog.FileName;

            return parameter is TreeView;
        }
    }
}
