using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Commands
{
    public class ClosePdfFileCommand : BaseCommand
    {
        public ApplicationMainWindowViewModel ViewModel { get; set; }

        public ClosePdfFileCommand (ApplicationMainWindowViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(ViewModel.PdfDocProc.DocPath);
        } 
        public override void Execute(object parameter)
        {
            if (!(parameter is TreeView treeView)) return;

            treeView.Items.Clear();
            ViewModel.SelectedItemCommand = null;
        }
    }
}
