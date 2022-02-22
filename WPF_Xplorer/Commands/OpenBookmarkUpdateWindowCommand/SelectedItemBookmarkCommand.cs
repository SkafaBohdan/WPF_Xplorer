using System.Windows.Controls;
using WPF_Xplorer.Services;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Commands.OpenBookmarkUpdateWindowCommand
{
    public class SelectedItemBookmarkCommand : BaseCommand
    {
        private BookmarkUpdateViewModel bookmarkUpdateViewModel { get; set; }

        public SelectedItemBookmarkCommand(BookmarkUpdateViewModel bookmarkViewModel)
        {
            bookmarkUpdateViewModel = bookmarkViewModel;
        }

        public override void Execute(object parameter)
        {
            if (parameter is TreeViewItem { Tag: BinderObj binder })
            {
                bookmarkUpdateViewModel.SelectedBookmark = binder.BookmarkObj;
                bookmarkUpdateViewModel.NameDelete = binder.PdfBookmarkObj.Title;
                bookmarkUpdateViewModel.PageDelete = binder.PdfBookmarkObj.Page;
                bookmarkUpdateViewModel.ParentNameBookmark = binder.PdfBookmarkObj.Title;
            }
        }
    }
}
