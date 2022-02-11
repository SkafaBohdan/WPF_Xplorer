using System.Windows.Input;
using WPF_Xplorer.Commands;
using WPF_Xplorer.Commands.OpenBookmarkUpdateWindowCommand;
using WPF_Xplorer.Services.Interfaces;


namespace WPF_Xplorer.ViewModels
{
    public class BookmarkUpdateViewModel : NotifyPropertyChanged
    {
        public IBookmarksUpdateService BookService { get; set; }

        private string nameAdd;
        private int numberPage = 1;
        private string nameDelete;
        private string findName;
        private string childName;
        private int numberChildPage = 1;


        public BookmarkUpdateViewModel(IBookmarksUpdateService bookService)
        {
            BookService = bookService;
            CreateCommands();
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
        public string FindName
        {
            get => findName;
            set
            {
                findName = value;
                OnPropertyChanged(nameof(FindName));
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
       

        public ICommand OpenBookmarkUpdateAddBookmarkCommand { get; set; }
        public ICommand OpenBookmarkUpdateDeleteBookmarkCommand { get; set; }
        public ICommand SaveFileCommand { get; set; }
        public ICommand SaveAsFileDialogCommand { get; set; }

     
        public ICommand OpenBookmarkAddChildBookmarkWindowCommand { get; set; }
        public ICommand AddChildBookmarkCommand { get; set; }
        public ICommand ExitWindow { get; set; }

        private void CreateCommands()
        {
            OpenBookmarkUpdateAddBookmarkCommand = new AddBookmarkCommand(this);
            OpenBookmarkUpdateDeleteBookmarkCommand = new DeleteBookmarkCommand(this);
            SaveFileCommand = new SaveFileCommand(this);
            SaveAsFileDialogCommand = new SaveAsFileDialogCommand(this);

            OpenBookmarkAddChildBookmarkWindowCommand = new OpenBookmarkAddChildBookmarkWindowCommand(this);
            AddChildBookmarkCommand = new AddChildBookmarkCommand(this);
            ExitWindow = new ExitWindow();
        }

    }
}
