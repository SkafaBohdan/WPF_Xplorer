using System.Windows.Controls;
using WPF_Xplorer.Services;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Commands
{
    public class SelectedItemCommand : BaseCommand
    {
        public ApplicationMainWindowViewModel ViewModel { get; set; }

        public SelectedItemCommand(ApplicationMainWindowViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public override void Execute (object parameter)
        {
            if (parameter is TreeViewItem {Tag: BinderObj binder})
            {
                ViewModel.SelectedObject = binder.PdfObj;
                ViewModel.PdfDocProc.GridListItemKey.Clear();
                ViewModel.PdfDocProc.GridListItemType.Clear();
                ViewModel.PdfDocProc.GridListItemValue.Clear();
            }
        }
    }
}
