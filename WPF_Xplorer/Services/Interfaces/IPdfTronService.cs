using pdftron.SDF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF_Xplorer.Services.Interfaces
{
    public interface IPdfTronService
    {
        void LoadDocument(string path);
        public TreeViewItem GetCatalogNode();
        public TreeViewItem GetInfoNode();
        IEnumerable<TreeViewItem> GetInfoStrings(ObjBinder binder);
        IEnumerable<TreeViewItem> GetChildNodes(Obj obj, string name);
    }
}
