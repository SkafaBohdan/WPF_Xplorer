using System.Windows.Input;
using WPF_Xplorer.Commands;
using WPF_Xplorer.Models;
using WPF_Xplorer.Services;
using WPF_Xplorer.Services.Interfaces;


namespace WPF_Xplorer.ViewModels
{
    public class ApplicationMainWindowViewModel : BaseViewModel
    {
        private PdfObj selectedItem;
        private bool isStream;

        public IPdfDocProc PdfDocProc { get; set; }

        public ApplicationMainWindowViewModel(IPdfDocProc pdfDocProc)
        {
            CreateCommands();

            PdfDocProc = pdfDocProc;
        }

        //TODO: после закрытия файла не очищаеться табличка, нейм, и стрим
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
                isStream = !string.IsNullOrEmpty(selectedItem?.StreamStringValue);
                OnPropertyChanged(nameof(SelectedObject));
            }
        }

        public bool IsStreamNotEmpty
        {
            get => isStream;
            set
            {
                isStream = value;
                OnPropertyChanged(nameof(IsStreamNotEmpty));
            }
        }

    }
}
