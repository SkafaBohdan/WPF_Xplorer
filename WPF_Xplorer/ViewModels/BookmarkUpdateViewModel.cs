using pdftron.PDF;
using System.Windows.Input;
using WPF_Xplorer.Commands;
using WPF_Xplorer.Commands.OpenBookmarkUpdateWindowCommand;
using WPF_Xplorer.Services;
using WPF_Xplorer.Services.Interfaces;
using WPF_Xplorer.View;

namespace WPF_Xplorer.ViewModels
{
    public class BookmarkUpdateViewModel : NotifyPropertyChanged
    {
        public IBookmarksUpdateService BookService { get; set; }
        static BookmarkUpdateWindow bookmarkUpdateWindow { get; set; }


        private string nameAdd;
        private int numberPage = 1;
        private string nameDelete;
        private int pageDelete;
        private string parentNameBookmark;
        private string childName;
        private int numberChildPage = 1;
        private Bookmark selectedBookmark;
        
        public BookmarkUpdateViewModel(IBookmarksUpdateService bookService)
        {
            BookService = bookService;
            CreateCommands();
            BookmarkUpdateWindow = new BookmarkUpdateWindow(this);
        }

        public BookmarkUpdateWindow BookmarkUpdateWindow
        {
            get => bookmarkUpdateWindow;
            set
            {
                bookmarkUpdateWindow = value;
                OnPropertyChanged(nameof(BookmarkUpdateWindow));
            }
        }

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
        public int PageDelete
        {
            get => pageDelete;
            set
            {
                pageDelete = value;
                OnPropertyChanged(nameof(PageDelete));
            }
        }
        
        public string ParentNameBookmark
        {
            get => parentNameBookmark;
            set
            {
                parentNameBookmark = value;
                OnPropertyChanged(nameof(ParentNameBookmark));
            }
        }
        public string ChildName
        {
            get => childName;
            set
            {
                childName = value;
                OnPropertyChanged(nameof(ChildName));
            }
        }
        public int NumberChildPage
        {
            get => numberChildPage;
            set
            {
                numberChildPage = value;
                OnPropertyChanged(nameof(NumberChildPage));
            }
        }

        public Bookmark SelectedBookmark
        {
            get => selectedBookmark;
            set
            {
                selectedBookmark = value;
                OnPropertyChanged(nameof(SelectedBookmark));
            }
        }
       

        public ICommand OpenBookmarkUpdateAddBookmarkCommand { get; set; }
        public ICommand OpenBookmarkUpdateDeleteBookmarkCommand { get; set; }
        public ICommand SaveFileCommand { get; set; }
        public ICommand SaveAsFileDialogCommand { get; set; }
        public ICommand SelectedItemBookmark { get; set; }
        public ICommand ExpandBookmarkCommand { get; set; }


        public ICommand OpenBookmarkAddChildBookmarkWindowCommand { get; set; }
        public ICommand AddChildBookmarkCommand { get; set; }
        public ICommand ExitWindow { get; set; }

        private void CreateCommands()
        {
            OpenBookmarkUpdateAddBookmarkCommand = new AddBookmarkCommand(this);
            OpenBookmarkUpdateDeleteBookmarkCommand = new DeleteBookmarkCommand(this);
            SaveFileCommand = new SaveFileCommand(this, new MessageBoxWrapper());
            SaveAsFileDialogCommand = new SaveAsFileDialogCommand(this, new MessageBoxWrapper());
            SelectedItemBookmark = new SelectedItemBookmarkCommand(this);
            ExpandBookmarkCommand = new ExpandBookmarkCommand(this);

            OpenBookmarkAddChildBookmarkWindowCommand = new OpenBookmarkAddChildBookmarkWindowCommand(this);
            AddChildBookmarkCommand = new AddChildBookmarkCommand(this);
            ExitWindow = new ExitWindow();
        }

    }
}
