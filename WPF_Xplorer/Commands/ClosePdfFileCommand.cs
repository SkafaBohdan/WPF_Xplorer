using System.Windows.Controls;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Commands
{
    public class ClosePdfFileCommand : BaseCommand
    {
        ApplicationMainWindowViewModel viewModel { get; set; }

        public ClosePdfFileCommand (ApplicationMainWindowViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(viewModel.PdfDocProc.DocPath);
        } 
        public override void Execute(object parameter)
        {
            if (!(parameter is TreeView treeView)) return;

            treeView.Items.Clear();
            viewModel.SelectedItemCommand = null;
            viewModel.SelectedObject = null;
            viewModel.PdfDocProc.DocPath = null;
            viewModel.PdfDocProc.GridListItemKey.Clear();
            viewModel.PdfDocProc.GridListItemType.Clear();
            viewModel.PdfDocProc.GridListItemValue.Clear();
        }
    }
}
