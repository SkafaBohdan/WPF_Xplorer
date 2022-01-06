using System.Windows;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Commands
{
    public class HelpAppCommand : BaseCommand
    {
        public override void Execute(object parameter)
        {
            MessageBox.Show("Wpf Xplorer \n Version 0.1", "Wpf Xplorer", MessageBoxButton.OK);
            
        }
    }
}
