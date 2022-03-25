using pdftron.Common;
using WPF_Xplorer.Interfaces;
using WPF_Xplorer.ViewModels;


namespace WPF_Xplorer.Commands.OpenBookmarkUpdateWindowCommand
{
    public class SaveFileCommand : BaseCommand
    {
        BookmarkUpdateViewModel bookmarkUpdateViewModel { get; set; }
        IMessageBox messageBox { get; set; }

        public SaveFileCommand(BookmarkUpdateViewModel bookmarkViewModel, IMessageBox messageBox)
        {
            bookmarkUpdateViewModel = bookmarkViewModel;
            this.messageBox = messageBox;
        }
        public override void Execute(object parameter)
        {
            try
            {
                bookmarkUpdateViewModel.BookService.SaveBookmarks(null);
                messageBox.MessageBoxShow("Файл перезаписан", "Ok");
            }
            catch (PDFNetException e)
            {
                messageBox.MessageBoxShow("Невозможно перезаписать, нет доступа к файлу\n -Переместите файл в другое место и попробуйте снова\n " +
                    "-Сохраните с помощью 'Сохранить как'", "Error");
            }
        }
    }
}
