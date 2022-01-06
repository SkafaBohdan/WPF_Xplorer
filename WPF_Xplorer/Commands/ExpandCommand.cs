using WPF_Xplorer.Converters;
using WPF_Xplorer.Interfaces;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Commands
{
    public class ExpandCommand : BaseCommand
    {
        public ApplicationMainWindowViewModel AppViewModel { get; set; }
        public IArgsConverter ArgsConverter { get; set; }

        public ExpandCommand(ApplicationMainWindowViewModel pdfViewModel)
        {
            AppViewModel = pdfViewModel;
            ArgsConverter = new ArgsConverter();
        }


        public override void Execute(object parameter)
        {
            var treeViewItem = ArgsConverter.ConverterTreeViewItem(parameter);

            if (treeViewItem != null)
            {
                AppViewModel.PdfDocProc.RelativeLeaveAdd(ref treeViewItem);
            }
        }
    }
}
