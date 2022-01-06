using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WPF_Xplorer.Commands;
using WPF_Xplorer.Models;
using WPF_Xplorer.Services;
using WPF_Xplorer.Services.Interfaces;


namespace WPF_Xplorer.ViewModels
{
    public class ApplicationMainWindowViewModel : INotifyPropertyChanged
    {
        private PdfObj selectedItem;

        public IPdfDocProc PdfDocProc { get; set; }

        public ApplicationMainWindowViewModel(IPdfDocProc pdfDocProc)
        {
            CreateCommands();

            PdfDocProc = pdfDocProc;
        }

       
        #region Command

        public ICommand OpenFileCommand { get; set; }
        public ICommand ClosePdfFileCommand { get; set; }
        public ICommand SelectedItemCommand { get; set; }
        public ICommand ExpandCommand { get; set; }

        #endregion

        private void CreateCommands()
        {
            OpenFileCommand = new OpenFileCommand(this, new DialogOpen());
            ClosePdfFileCommand = new ClosePdfFileCommand(this);
            SelectedItemCommand = new SelectedItemCommand(this);
            ExpandCommand = new ExpandCommand(this);
        }


        public PdfObj SelectedObject
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedObject));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
