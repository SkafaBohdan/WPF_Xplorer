using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Commands.OpenBookmarkUpdateWindowCommand
{
    public class AddBookmarkCommand : BaseCommand
    {
        public BookmarkUpdateViewModel bookmarkUpdateViewModel { get; set; }

        public AddBookmarkCommand(BookmarkUpdateViewModel bookmarkViewModel)
        {
            bookmarkUpdateViewModel = bookmarkViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(bookmarkUpdateViewModel.NameAdd) && (bookmarkUpdateViewModel.NumberPage != 0);
        }

        public override void Execute(object parameter)
        {
            bookmarkUpdateViewModel.BookService.AddBookmark(bookmarkUpdateViewModel.NameAdd, bookmarkUpdateViewModel.NumberPage);
            bookmarkUpdateViewModel.NameAdd = null;
            bookmarkUpdateViewModel.NumberPage = 0;
        }
    }
}
