using pdftron.PDF;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;

namespace WPF_Xplorer.Services.Interfaces
{
    public interface IPdfService
    {
        void GetDocumentNode(string path, TreeView parent, out bool boolPath);
        void AddCatalogNode(TreeViewItem parent);
        void AddInfoNode(TreeViewItem parent);
        void AddInfoStrings(TreeViewItem parent);
        void AddKidNodes(TreeViewItem parent);
        ObservableCollection<StringBuilder> GetGridListItemKey();
        ObservableCollection<StringBuilder> GetGridListItemType();
        ObservableCollection<StringBuilder> GetGridListItemValue();
     //   StringBuilder PrintOutlineTree(Bookmark bookItem);
        StringBuilder PrintBookmarks();
    }
}
