using System.Windows.Controls;
using WPF_Xplorer.Services;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Commands
{
    public class SelectedItemCommand : BaseCommand
    {
        ApplicationMainWindowViewModel viewModel { get; set; }
   
        public SelectedItemCommand(ApplicationMainWindowViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute (object parameter)
        {
            if (parameter is TreeViewItem {Tag: BinderObj binder})
            {
                viewModel.SelectedObject = binder.PdfObj;
            }
        }
    }
}
