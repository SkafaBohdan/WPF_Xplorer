using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Commands.OpenBookmarkUpdateWindowCommand
{
    public class DeleteBookmarkCommand : BaseCommand
    {
        public BookmarkUpdateViewModel BookmarkUpdateViewModel { get; set; }
        public DeleteBookmarkCommand(BookmarkUpdateViewModel bookmarkViewModel)
        {
            BookmarkUpdateViewModel = bookmarkViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(BookmarkUpdateViewModel.NameDelete);
        }

        public override void Execute(object parameter)
        {
            BookmarkUpdateViewModel.BookService.DeleteBookmark(BookmarkUpdateViewModel.SelectedBookmark);
            BookmarkUpdateViewModel.NameDelete = null;
            BookmarkUpdateViewModel.PageDelete = 0;
            BookmarkUpdateViewModel.ParentNameBookmark = null;
           
        }
    }
}
