using System;
using WPF_Xplorer.Services;
using WPF_Xplorer.Services.Interfaces;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Commands.OpenBookmarkUpdateWindowCommand
{
    public class OpenBookmarkUpdateDeleteBookmarkCommand : BaseCommand
    {
        public BookmarkUpdateViewModel BookmarkUpdateViewModel { get; set; }
        IBookmarksUpdateService bookmarksUpdateService { get; set; }

        public OpenBookmarkUpdateDeleteBookmarkCommand(BookmarkUpdateViewModel bookmarkViewModel, IBookmarksUpdateService bookmarksUpdateService)
        {
            BookmarkUpdateViewModel = bookmarkViewModel;
            this.bookmarksUpdateService = bookmarksUpdateService;
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(BookmarkUpdateViewModel.NameDelete);
        }
         
        public override void Execute(object parameter)
        {
            bookmarksUpdateService.DeleteBookmark(BookmarkUpdateViewModel.NameDelete);
        }
    }
}
