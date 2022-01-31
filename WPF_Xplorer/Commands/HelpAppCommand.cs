using System.Windows;

namespace WPF_Xplorer.Commands
{
    public class HelpAppCommand : BaseCommand
    {
        public override void Execute(object parameter)
        {
            MessageBox.Show("Wpf Xplorer \n Version 1.3", "Wpf Xplorer", MessageBoxButton.OK);   
        }
    }
}
