using System;
using System.Text;
using WPF_Xplorer.Services.Interfaces;
using WPF_Xplorer.View;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Commands
{
    public class OpenBookmarkCommand : BaseCommand
    {
        public ApplicationMainWindowViewModel ViewModel { get; set; }
        public BookmarksViewModel BookmarksViewModel { get; set; }

        public OpenBookmarkCommand(ApplicationMainWindowViewModel viewModel, BookmarksViewModel bookmarksViewModel)
        {
            ViewModel = viewModel;
            BookmarksViewModel = bookmarksViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(ViewModel.PdfDocProc.DocPath);
        }

        public override void Execute(object parameter)
        {
            StringBuilder stringBookmarks;

            stringBookmarks = ViewModel.PdfDocProc.PrintBookmarks();

            string bookmarksPrint = stringBookmarks.ToString();
            BookmarksViewModel.TextBookmarks = bookmarksPrint;
            BookmarkListWindow bookmarkList = new BookmarkListWindow(BookmarksViewModel);

            bookmarkList.Show();
            stringBookmarks.Clear();
        }
    }
}
