using pdftron.PDF;
using pdftron.SDF;
using System.Collections.Generic;
using System.Windows.Controls;
using WPF_Xplorer.Models;

namespace WPF_Xplorer.Services.Interfaces
{
    public interface IPdfTronService
    {
        void LoadDoc(string path);
        public TreeViewItem GetCatalogNode();
        public TreeViewItem GetInfoNode();
        IEnumerable<TreeViewItem> GetInfoStrings(BinderObj binder);
        IEnumerable<TreeViewItem> GetKidNodes(Obj obj, string name);
        PDFDoc GetDoc();
        public IEnumerable<TreeViewItem> GetBookmarksTree();
    }
}
