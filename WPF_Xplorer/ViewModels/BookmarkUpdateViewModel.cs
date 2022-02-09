using System.Windows.Input;
using WPF_Xplorer.Commands;
using WPF_Xplorer.Commands.OpenBookmarkUpdateWindowCommand;
using WPF_Xplorer.Services.Interfaces;


namespace WPF_Xplorer.ViewModels
{
    public class BookmarkUpdateViewModel : NotifyPropertyChanged
    {
        public IBookmarksUpdateService BookService { get; set; }

        public BookmarkUpdateViewModel(IBookmarksUpdateService bookService)
        {
            BookService = bookService;
            CreateCommands();
        }

        private string nameAdd;
        private int numberPage = 1;
        private string nameDelete;


        public string NameAdd
        {
            get => nameAdd;
            set
            {
                nameAdd = value;
                OnPropertyChanged(nameof(NameAdd));
            }
        }
        public int NumberPage
        {
            get => numberPage;
            set
            {
                numberPage = value;
                OnPropertyChanged(nameof(NumberPage));
            }
        }
        public string NameDelete
        {
            get => nameDelete;
            set
            {
                nameDelete = value;
                OnPropertyChanged(nameof(NameDelete));
            }
        }


        public ICommand OpenBookmarkUpdateAddBookmarkCommand { get; set; }
        public ICommand OpenBookmarkUpdateDeleteBookmarkCommand { get; set; }
        public ICommand SaveFileDialogCommand { get; set; }

        private void CreateCommands()
        {
            OpenBookmarkUpdateAddBookmarkCommand = new OpenBookmarkUpdateAddBookmarkCommand(this, BookService);
            OpenBookmarkUpdateDeleteBookmarkCommand = new OpenBookmarkUpdateDeleteBookmarkCommand(this, BookService);
            SaveFileDialogCommand = new SaveFileDialogCommand(this, BookService);
        }

    }
}
