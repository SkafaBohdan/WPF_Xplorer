using System.Windows.Controls;

namespace WPF_Xplorer.Interfaces
{
    public interface IArgsConverter
    {
        TreeViewItem ConverterTreeViewItem(object args);
    }
}
