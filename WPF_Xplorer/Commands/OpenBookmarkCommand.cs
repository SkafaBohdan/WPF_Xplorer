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
            try
            {
                stringBookmarks = ViewModel.PdfDocProc.PrintBookmarks();
                if (stringBookmarks != null)
                {
                    string bookmarksPrint = stringBookmarks.ToString();
                    BookmarksViewModel.TextBookmarks = bookmarksPrint;
                    BookmarkListWindow bookmarkList = new BookmarkListWindow(BookmarksViewModel);

                    bookmarkList.Show();
                    stringBookmarks.Clear();
                }
            }
            catch (ArgumentException e)
            {
                //TODO: обернуть мессадж бокс, и возможно весь тру-кетч обернуть куда-то в интерфейс
                System.Windows.MessageBox.Show(e.Message, "Bookmarks");
               
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message, "Error");
            }
        }
    }
}
