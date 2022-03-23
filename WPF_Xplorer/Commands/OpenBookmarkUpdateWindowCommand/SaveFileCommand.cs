using WPF_Xplorer.Interfaces;
using WPF_Xplorer.Services;
using WPF_Xplorer.ViewModels;


namespace WPF_Xplorer.Commands.OpenBookmarkUpdateWindowCommand
{
    public class SaveFileCommand : BaseCommand
    {
        BookmarkUpdateViewModel bookmarkUpdateViewModel { get; set; }
        IMessageBox messageBox;

        public SaveFileCommand(BookmarkUpdateViewModel bookmarkViewModel)
        {
            bookmarkUpdateViewModel = bookmarkViewModel;
            messageBox = new MessageBoxWrapper();
        }
        public override void Execute(object parameter)
        {
            bookmarkUpdateViewModel.BookService.SaveBookmarks(null);
            messageBox.MessageBoxShow("Файл перезаписан", "Ok");
        }
    }
}
