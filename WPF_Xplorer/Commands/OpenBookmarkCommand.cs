using System.Text;
using WPF_Xplorer.View;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Commands
{
    public class OpenBookmarkCommand : BaseCommand
    {
        ApplicationMainWindowViewModel viewModel { get; set; }
        BookmarksViewModel bookmarksViewModel { get; set; }
        static BookmarkListWindow bookmarkListView;

        public OpenBookmarkCommand(ApplicationMainWindowViewModel viewModel, BookmarksViewModel bookmarksViewModel)
        {
            this.viewModel = viewModel;
            this.bookmarksViewModel = bookmarksViewModel;
            bookmarkListView = new BookmarkListWindow(bookmarksViewModel);
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(viewModel.PdfDocProc.DocPath);
        }

        public override void Execute(object parameter)
        {
            StringBuilder stringBookmarks;
            stringBookmarks = viewModel.PdfDocProc.PrintBookmarks();
            string bookmarksPrint = stringBookmarks.ToString();
            bookmarksViewModel.TextBookmarks = bookmarksPrint;

            bookmarkListView.ShowDialog();

            stringBookmarks.Clear();
        }
    }
}
