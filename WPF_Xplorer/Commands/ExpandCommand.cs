using System.Text;
using WPF_Xplorer.Converters;
using WPF_Xplorer.Interfaces;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Commands
{
    public class ExpandCommand : BaseCommand
    {
        public ApplicationMainWindowViewModel ViewModel { get; set; }
        public IArgsConverter ArgsConverter { get; set; }

        public ExpandCommand(ApplicationMainWindowViewModel pdfViewModel)
        {
            ViewModel = pdfViewModel;
            ArgsConverter = new ArgsConverter();
        }


        public override void Execute(object parameter)
        {
            var treeViewItem = ArgsConverter.ConverterTreeViewItem(parameter);

            if (treeViewItem != null)
            {
                ViewModel.PdfDocProc.GridListItemKey.Clear();
                ViewModel.PdfDocProc.GridListItemType.Clear();
                ViewModel.PdfDocProc.GridListItemValue.Clear();
                ViewModel.PdfDocProc.RelativeLeaveAdd(ref treeViewItem);
            }
        }
    }
}
