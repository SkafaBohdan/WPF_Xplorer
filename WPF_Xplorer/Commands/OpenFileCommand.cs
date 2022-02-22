using System.Windows.Controls;
using WPF_Xplorer.Services.Interfaces;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Commands
{
    public class OpenFileCommand : BaseCommand
    {
        private readonly IDialogOpen openDialog;
        ApplicationMainWindowViewModel viewModel { get; set; }

        public OpenFileCommand(ApplicationMainWindowViewModel viewModel, IDialogOpen openDialog)
        {
            this.viewModel = viewModel;
            this.openDialog = openDialog;
        }

        public override void Execute(object parameter)
        {
            if (!openDialog.OpenDialog(parameter, out var fileName)) return;
            if(parameter is TreeView treeView)
            {
                viewModel.ClosePdfFileCommand.Execute(parameter);
                viewModel.PdfDocProc.OpenFile(fileName, treeView);
                viewModel.bookmarkUpdateViewModel.BookService.InitPageCount();
            }
        }
    }
}
