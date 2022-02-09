using WPF_Xplorer.View;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Commands.OpenBookmarkUpdateWindowCommand
{
    public class OpenBookmarkUpdateWindowCommand : BaseCommand
    {
        public ApplicationMainWindowViewModel AppViewModel { get; set; }
        public BookmarkUpdateViewModel bookmarkUpdateViewModel { get; set; }
        static BookmarkUpdateWindow bookmarkUpdateWindow { get; set; } 

        public OpenBookmarkUpdateWindowCommand(ApplicationMainWindowViewModel appViewModel, BookmarkUpdateViewModel bookmarkUpdateViewModel)
        {
            AppViewModel = appViewModel;
            this.bookmarkUpdateViewModel = bookmarkUpdateViewModel;
            bookmarkUpdateWindow = new BookmarkUpdateWindow(this.bookmarkUpdateViewModel);

        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(AppViewModel.PdfDocProc.DocPath);
        }

        public override void Execute(object parameter)
        {
            bookmarkUpdateWindow.ShowDialog();
        }
    }
}
