using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPF_Xplorer.View;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Commands
{
    public class OpenBookmarkCommand : BaseCommand
    {
        public ApplicationMainWindowViewModel ViewModel { get; set; }
        public OpenBookmarkCommand(ApplicationMainWindowViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(ViewModel.PdfDocProc.DocPath);
        }

        public override void Execute(object parameter)
        {
            var stringBookmarks = ViewModel.PdfDocProc.PrintBookmarks();
            if (stringBookmarks != null)
            {
                string bookmarksPrint = stringBookmarks.ToString();

                MessageBox.Show(bookmarksPrint);                
            }
        }
    }
}
