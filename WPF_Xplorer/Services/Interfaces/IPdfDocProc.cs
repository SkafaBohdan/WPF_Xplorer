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
        ObservableCollection<StringBuilder> GridListItemKey { get; set; }
        ObservableCollection<StringBuilder> GridListItemType { get; set; }
        ObservableCollection<StringBuilder> GridListItemValue { get; set; }
        void OpenFile(string path, ref TreeView treeView);
        void RelativeLeaveAdd(ref TreeViewItem treeViewItem);
    }
}
