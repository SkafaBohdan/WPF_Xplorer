using pdftron.PDF;
using pdftron.SDF;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using WPF_Xplorer.Interfaces;
using WPF_Xplorer.Services.Interfaces;

namespace WPF_Xplorer.Services
{
    public class PdfTreeProc : IPdfTreeProc
    {
        private readonly IPdfObjProc pdfObjectProc;

        public PdfTreeProc(IPdfObjProc pdfObjectProc)
        {
            this.pdfObjectProc = pdfObjectProc;
        }


        public TreeViewItem GetCatalogNode(Obj root, Obj catalogObj, string name)
        {
            var pdfObj = pdfObjectProc.InderectObj(catalogObj, name);

            return new TreeViewItem
            {
                Header = pdfObj.DisplayKey,
                Tag = new ObjBinder(pdfObj, root)
            };
        }

        public IEnumerable<TreeViewItem> GetChildNodes(IEnumerable<KeyValuePair<string, Obj>> dictionary)
        {
            var child = dictionary.Select(item => GetChild(item.Key, item.Value)).Where(child => child.Tag is ObjBinder);

            return child;
        }

        public TreeViewItem GetDocumentNode(string path)
        {
            var pdfObject = pdfObjectProc.DocObj(path);

            return new TreeViewItem
            {
                Header = path,
                Tag = new ObjBinder(pdfObject, null)
            };
        }

        public TreeViewItem GetInfoNode(Obj infoObj, PDFDocInfo docInfo)
        {
            var pdfObject = pdfObjectProc.InderectObj(infoObj, "Info");
            pdfObject.Type = Models.PdfObj.PdfType.Info;

            return new TreeViewItem
            {
                Header = pdfObject.DisplayKeyAndValue,
                Tag = new ObjBinder(pdfObject, infoObj)
                {
                    InfoDoc = docInfo
                }
            };
        }

        public IEnumerable<TreeViewItem> GetInfoNodes(PDFDocInfo info)
        {
            foreach (var (key, value) in pdfObjectProc.GetInfoObj(info))
            {
                if (string.IsNullOrEmpty(value)) continue;

                var pdfString = pdfObjectProc.StringObj(key, value);

                yield return new TreeViewItem
                {
                    Header = pdfString.DisplayKeyAndValue,
                    Tag = new ObjBinder(pdfString, null)
                };
            }
        }

        public TreeViewItem GetStream(string name, IStreamService streamService, Obj value)
        {
            var pdfStream = pdfObjectProc.StreamObj(value, streamService, name);

            return new TreeViewItem
            {
                Header = pdfStream.DisplayKeyAndValue,
                Tag = new ObjBinder(pdfStream, null)
            };
        }


        private TreeViewItem GetChild(string key, Obj value)
        {
            var pdfObject = pdfObjectProc.StructObjectBranchOnType(value, key, out var hasChild);

            var treeViewItem = new TreeViewItem
            {
                Header = pdfObject.DisplayKeyAndValue,
                Tag = new ObjBinder(pdfObject, value)
            };

            if (hasChild)
            {
                treeViewItem.Items.Add(null);
            }

            return treeViewItem;
        }
    }
}
