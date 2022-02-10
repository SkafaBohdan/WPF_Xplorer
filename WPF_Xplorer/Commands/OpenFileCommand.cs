using System.Windows.Controls;
using WPF_Xplorer.Services.Interfaces;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Commands
{
    public class OpenFileCommand : BaseCommand
    {
        private readonly IDialogOpen openDialog;
        public ApplicationMainWindowViewModel ViewModel { get; set; }

        public OpenFileCommand(ApplicationMainWindowViewModel viewModel, IDialogOpen openDialog)
        {
            ViewModel = viewModel;
            this.openDialog = openDialog;
        }

        public override void Execute(object parameter)
        {
            if (!openDialog.OpenDialog(parameter, out var fileName)) return;
            if(parameter is TreeView treeView)
            {
                ViewModel.ClosePdfFileCommand.Execute(parameter);
                ViewModel.PdfDocProc.OpenFile(fileName, ref treeView);
                ViewModel.bookmarkUpdateViewModel.BookService.InitPageCount();
               // ViewModel.CreateBookmarks();
            }
        }
    }
}
