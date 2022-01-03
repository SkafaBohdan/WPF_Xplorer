using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Xplorer.ViewModels;
using WPF_Xplorer.ViewModels.Commands;


namespace WPF_Xplorer.Commands
{
    public class CloseApplicationCommand : BaseCommand
    {
        public override bool CanExecute(object parameter) => true;
        public override void Execute(object parameter) => Application.Current.Shutdown();
    }
}
