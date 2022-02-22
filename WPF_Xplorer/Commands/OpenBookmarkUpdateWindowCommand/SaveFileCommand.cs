using System.Windows;
using WPF_Xplorer.ViewModels;


namespace WPF_Xplorer.Commands.OpenBookmarkUpdateWindowCommand
{
    public class SaveFileCommand : BaseCommand
    {
        BookmarkUpdateViewModel bookmarkUpdateViewModel { get; set; }

        public SaveFileCommand(BookmarkUpdateViewModel bookmarkViewModel)
        {
            bookmarkUpdateViewModel = bookmarkViewModel;
        }
        public override void Execute(object parameter)
        {
            bookmarkUpdateViewModel.BookService.SaveBookmarks(null);
            MessageBox.Show("Файл перезаписан", "Ok");
        }
    }
}
