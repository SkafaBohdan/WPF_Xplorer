using System.Windows;
using System.Windows.Controls;
using WPF_Xplorer.Interfaces;

namespace WPF_Xplorer.Converters
{
    public class ArgsConverter: IArgsConverter
    {
        public TreeViewItem ConverterTreeViewItem(object args)
        {
            return args is RoutedEventArgs { Source: TreeViewItem treeViewItem } ? treeViewItem : null;
        }
    }
}
