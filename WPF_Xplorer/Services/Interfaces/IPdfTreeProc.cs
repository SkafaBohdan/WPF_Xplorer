using pdftron.Filters;
using pdftron.PDF;
using pdftron.SDF;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;

namespace WPF_Xplorer.Services.Interfaces
{
    public interface IPdfTreeProc
    {
        IEnumerable<TreeViewItem> GetInfoNodes(PDFDocInfo info);
        IEnumerable<TreeViewItem> GetKidNodes(IEnumerable<KeyValuePair<string, Obj>> dictionary);
        TreeViewItem GetStream(string name, IStreamService streamService, Obj value);
        TreeViewItem GetDocumentNode(string path);
        TreeViewItem GetCatalogNode(Obj root, Obj catalogObj, string name);
        TreeViewItem GetInfoNode(Obj infoObj, PDFDocInfo docInfo);
        //TODO: test
        ObservableCollection<StringBuilder> GetKidNodesGRIDDD(Models.PdfObj obj);
        ObservableCollection<StringBuilder> GetGridListItemKey();
    }

    public interface IStreamService
    {
        string Path { get; }
        Filter CreateStream(Obj streamObj, int streamNumber, out string fullPath);
    }
}
