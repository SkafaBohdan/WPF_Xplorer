using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Commands.OpenBookmarkUpdateWindowCommand
{
    public class OpenBookmarkUpdateWindowCommand : BaseCommand
    {
        ApplicationMainWindowViewModel AppViewModel { get; set; }
        BookmarkUpdateViewModel bookmarkUpdateViewModel { get; set; }

        public OpenBookmarkUpdateWindowCommand(ApplicationMainWindowViewModel appViewModel, BookmarkUpdateViewModel bookmarkUpdateViewModel)
        {
            AppViewModel = appViewModel;
            this.bookmarkUpdateViewModel = bookmarkUpdateViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(AppViewModel.PdfDocProc.DocPath);
        }

        public override void Execute(object parameter)
        {
            var treeView = bookmarkUpdateViewModel.BookmarkUpdateWindow.tree_bookmarks;
            bookmarkUpdateViewModel.BookService.GetBookmarksTreeViewItem(treeView);
            bookmarkUpdateViewModel.BookmarkUpdateWindow.ShowDialog();
        }
    }
}
