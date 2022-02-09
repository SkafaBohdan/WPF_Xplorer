using Microsoft.Win32;
using System.Windows;
using WPF_Xplorer.Services.Interfaces;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Commands
{
    public class SaveFileDialogCommand : BaseCommand
    {
        public BookmarkUpdateViewModel BookmarkUpdateViewModel { get; set; }
        IBookmarksUpdateService bookmarksUpdateService { get; set; }

        public SaveFileDialogCommand(BookmarkUpdateViewModel bookmarkViewModel, IBookmarksUpdateService bookmarksUpdateService)
        {
            BookmarkUpdateViewModel = bookmarkViewModel;
            this.bookmarksUpdateService = bookmarksUpdateService;
        }

        public override void Execute(object parameter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Pdf file  (*.pdf) | *.pdf";
            if (saveFileDialog.ShowDialog() == true)
            {
                bookmarksUpdateService.SaveBookmarks(saveFileDialog.FileName);
                MessageBox.Show("Файл сохранен");
            }
        }
    }
}
