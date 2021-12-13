using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Xplorer.Services.Interfaces;
using WPF_Xplorer.ViewModels.Commands;

namespace WPF_Xplorer.ViewModels
{
    public class ApplicationMainWindowViewModel : BaseViewModel
    {
        private Tree selectedTree;

        IFileService fileService;
        IDialogService dialogService;

        public ObservableCollection<PDF> pDFs { get; set; }


        private RelayCommand closeApplication;
        public RelayCommand CloseApplication
        {
            get
            {
                return closeApplication ??
                    (closeApplication = new RelayCommand(obj =>
                    {
                        Application.Current.Shutdown();
                    }));
            }
        }
    }
}
