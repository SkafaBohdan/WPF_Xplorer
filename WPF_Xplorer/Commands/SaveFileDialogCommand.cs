using Microsoft.Win32;
using System.Windows;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Commands
{
    public class SaveFileDialogCommand : BaseCommand
    {
        public BookmarkUpdateViewModel BookmarkUpdateViewModel { get; set; }

        public SaveFileDialogCommand(BookmarkUpdateViewModel bookmarkViewModel)
        {
            BookmarkUpdateViewModel = bookmarkViewModel;
        }

        public override void Execute(object parameter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Pdf file  (*.pdf) | *.pdf";
            if (saveFileDialog.ShowDialog() == true)
            {
                BookmarkUpdateViewModel.BookService.SaveBookmarks(saveFileDialog.FileName);
                MessageBox.Show("Файл сохранен");
            }
        }
    }
}
