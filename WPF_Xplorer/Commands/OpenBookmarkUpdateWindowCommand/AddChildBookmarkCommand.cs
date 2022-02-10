using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Commands.OpenBookmarkUpdateWindowCommand
{
    public class AddChildBookmarkCommand : BaseCommand
    {
        public BookmarkUpdateViewModel bookmarkUpdateViewModel { get; set; }

        public AddChildBookmarkCommand(BookmarkUpdateViewModel bookmarkUpdateViewModel)
        {
            this.bookmarkUpdateViewModel = bookmarkUpdateViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(bookmarkUpdateViewModel.ChildName) && !string.IsNullOrEmpty(bookmarkUpdateViewModel.FindName);
        }

        public override void Execute(object parameter)
        {
            bookmarkUpdateViewModel.BookService.AddChildBookmark(bookmarkUpdateViewModel.FindName, bookmarkUpdateViewModel.ChildName, bookmarkUpdateViewModel.NumberChildPage);
        }
    }
}
