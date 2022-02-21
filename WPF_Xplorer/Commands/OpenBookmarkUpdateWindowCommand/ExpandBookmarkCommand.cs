using WPF_Xplorer.Converters;
using WPF_Xplorer.Interfaces;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Commands.OpenBookmarkUpdateWindowCommand
{
    public class ExpandBookmarkCommand : BaseCommand
    {
        public BookmarkUpdateViewModel bookmarkViewModel { get; set; }
        public IArgsConverter ArgsConverter { get; set; }

        public ExpandBookmarkCommand(BookmarkUpdateViewModel pdfViewModel)
        {
            bookmarkViewModel = pdfViewModel;
            ArgsConverter = new ArgsConverter();
        }

        public override void Execute(object parameter)
        {
            var treeViewItem = ArgsConverter.ConverterTreeViewItem(parameter);

        }
    }
}
