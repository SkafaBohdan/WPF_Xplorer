using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Commands.OpenBookmarkUpdateWindowCommand
{
    public class AddChildBookmarkCommand : BaseCommand
    {
        BookmarkUpdateViewModel bookmarkUpdateViewModel { get; set; }

        public AddChildBookmarkCommand(BookmarkUpdateViewModel bookmarkUpdateViewModel)
        {
            this.bookmarkUpdateViewModel = bookmarkUpdateViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(bookmarkUpdateViewModel.ChildName) && !string.IsNullOrEmpty(bookmarkUpdateViewModel.ParentNameBookmark);
        }

        public override void Execute(object parameter)
        {
            bookmarkUpdateViewModel.BookService.AddChildBookmark(bookmarkUpdateViewModel.SelectedBookmark, bookmarkUpdateViewModel.ChildName, bookmarkUpdateViewModel.NumberChildPage);
            bookmarkUpdateViewModel.ParentNameBookmark = null;
            bookmarkUpdateViewModel.ChildName = null;
            bookmarkUpdateViewModel.NumberChildPage = 0;

            var treeView = bookmarkUpdateViewModel.BookmarkUpdateWindow.tree_bookmarks;
            bookmarkUpdateViewModel.BookService.GetBookmarksTreeViewItem(treeView);
        }
    }
}
