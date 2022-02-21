using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF_Xplorer.Services.Interfaces
{
    public interface IPdfDocProc
    {
        string DocPath { get; set; }
        string Name { get; }
        ObservableCollection<StringBuilder> GridListItemKey { get; }
        ObservableCollection<StringBuilder> GridListItemType { get; }
        ObservableCollection<StringBuilder> GridListItemValue { get; }
        void OpenFile(string path, TreeView treeView);
        void RelativeLeaveAdd(TreeViewItem treeViewItem);
        StringBuilder PrintBookmarks();
    }
}
