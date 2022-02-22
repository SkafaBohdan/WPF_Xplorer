using WPF_Xplorer.Converters;
using WPF_Xplorer.Interfaces;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Commands
{
    public class ExpandCommand : BaseCommand
    {
        ApplicationMainWindowViewModel viewModel { get; set; }
        public IArgsConverter argsConverter { get; set; }

        public ExpandCommand(ApplicationMainWindowViewModel pdfViewModel)
        {
            viewModel = pdfViewModel;
            argsConverter = new ArgsConverter();
        }


        public override void Execute(object parameter)
        {
            var treeViewItem = argsConverter.ConverterTreeViewItem(parameter);

            if (treeViewItem != null)
            {
                viewModel.PdfDocProc.GridListItemKey.Clear();
                viewModel.PdfDocProc.GridListItemType.Clear();
                viewModel.PdfDocProc.GridListItemValue.Clear();
                viewModel.PdfDocProc.RelativeLeaveAdd(treeViewItem);
            }
        }
    }
}
