using System.Windows;

namespace WPF_Xplorer.Commands.OpenBookmarkUpdateWindowCommand
{
    public class ExitWindow : BaseCommand
    {
        public override void Execute(object parameter)
        {
            if (parameter is Window) 
                ((Window)parameter).Close();
        }
    }
}
