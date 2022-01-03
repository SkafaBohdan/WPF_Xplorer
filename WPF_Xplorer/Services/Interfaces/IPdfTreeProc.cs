using pdftron.Filters;
using pdftron.PDF;
using pdftron.SDF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF_Xplorer.Services.Interfaces
{
    public interface IPdfTreeProc
    {
        TreeViewItem GetDocumentNode(string path);
        TreeViewItem GetCatalogNode(Obj root, Obj catalogObj, string name);
        TreeViewItem GetInfoNode(Obj infoObj, PDFDocInfo docInfo);
        TreeViewItem GetStream(string name, IStreamService streamService, Obj value);
        IEnumerable<TreeViewItem> GetInfoNodes(PDFDocInfo info);
        IEnumerable<TreeViewItem> GetChildNodes(IEnumerable<KeyValuePair<string, Obj>> dictionary);
    }

    public interface IStreamService
    {
        string Path { get; }
        Filter CreateStream(Obj streamObj, int streamNumber, out string fullPath);
    }
}
