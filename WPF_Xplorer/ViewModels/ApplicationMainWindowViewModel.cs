using System.Windows.Input;
using WPF_Xplorer.Commands;
using WPF_Xplorer.Commands.OpenBookmarkUpdateWindowCommand;
using WPF_Xplorer.Models;
using WPF_Xplorer.Services;
using WPF_Xplorer.Services.Interfaces;


namespace WPF_Xplorer.ViewModels
{
    public class ApplicationMainWindowViewModel : NotifyPropertyChanged
    {
        public  BookmarkUpdateViewModel bookmarkUpdateViewModel;
        private PdfObj selectedItem;
        public IPdfDocProc PdfDocProc { get; set; }
    

        public ApplicationMainWindowViewModel(IPdfDocProc pdfDocProc, BookmarkUpdateViewModel viewModel)
        {
            bookmarkUpdateViewModel = viewModel;
            PdfDocProc = pdfDocProc;
            CreateCommands();
        }
        
        #region Command

        public ICommand OpenFileCommand { get; set; }
        public ICommand ClosePdfFileCommand { get; set; }
        public ICommand SelectedItemCommand { get; set; }
        public ICommand ExpandCommand { get; set; }
        public ICommand OpenBookmarkCommand { get; set; }
        public ICommand OpenBookmarkUpdateWindowCommand { get; set; }


        private void CreateCommands()
        {
            OpenFileCommand = new OpenFileCommand(this, new DialogOpen());
            ClosePdfFileCommand = new ClosePdfFileCommand(this);
            SelectedItemCommand = new SelectedItemCommand(this);
            ExpandCommand = new ExpandCommand(this);
            OpenBookmarkCommand = new OpenBookmarkCommand(this, new BookmarksViewModel());
            OpenBookmarkUpdateWindowCommand = new OpenBookmarkUpdateWindowCommand(this, bookmarkUpdateViewModel);
        }

        #endregion

        public PdfObj SelectedObject
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedObject));
            }
        }

    }
}


