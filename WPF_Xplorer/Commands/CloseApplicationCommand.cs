using System;
using System.IO;
using System.Windows;


namespace WPF_Xplorer.Commands
{
    public class CloseApplicationCommand : BaseCommand
    {
        public override void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
