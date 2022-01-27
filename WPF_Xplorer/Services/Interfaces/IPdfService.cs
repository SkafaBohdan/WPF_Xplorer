using pdftron.PDF;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;

namespace WPF_Xplorer.Services.Interfaces
{
    public interface IPdfService
    {
        void GetDocumentNode(string path, TreeView ancestor);
        void AddCatalogNode(TreeViewItem ancestor);
        void AddInfoNode(TreeViewItem ancestor);
        void AddInfoStrings(TreeViewItem ancestor);
        void AddKidNodes(TreeViewItem ancestor);
        ObservableCollection<StringBuilder> GetGridListItemKey();
        ObservableCollection<StringBuilder> GetGridListItemType();
        ObservableCollection<StringBuilder> GetGridListItemValue();
     //   StringBuilder PrintOutlineTree(Bookmark bookItem);
        StringBuilder PrintBookmarks();
    }
}
