using WPF_Xplorer.Services.Interfaces;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Commands.OpenBookmarkUpdateWindowCommand
{
    public class OpenBookmarkUpdateAddBookmarkCommand : BaseCommand
    {
        public BookmarkUpdateViewModel bookmarkUpdateViewModel { get; set; }
        IBookmarksUpdateService bookmarksUpdateService { get; set; }

        public OpenBookmarkUpdateAddBookmarkCommand(BookmarkUpdateViewModel bookmarkViewModel, IBookmarksUpdateService bookmarksUpdateService)
        {
            bookmarkUpdateViewModel = bookmarkViewModel;
            this.bookmarksUpdateService = bookmarksUpdateService;
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(bookmarkUpdateViewModel.NameAdd) && (bookmarkUpdateViewModel.NumberPage != 0);
        }

        public override void Execute(object parameter)
        {
            bookmarksUpdateService.AddBookmark(bookmarkUpdateViewModel.NameAdd, bookmarkUpdateViewModel.NumberPage);
        }
    }
}
