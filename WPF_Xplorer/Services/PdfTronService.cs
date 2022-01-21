using pdftron.PDF;
using pdftron.SDF;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using WPF_Xplorer.Models;
using WPF_Xplorer.Services.Interfaces;


namespace WPF_Xplorer.Services
{
    public class PdfTronService : IPdfTronService
    {
        private PDFDoc doc;
        private static bool isPdfTronInit;
        private readonly IPdfTreeProc pdfTreeProc;
        private readonly IStreamService streamService;
        
        public PdfTronService(IPdfTronInitializer initializer, IPdfTreeProc treeProcessor, IStreamService streamService)
        {
            if (!isPdfTronInit)
            {
                initializer.Initialize();
                isPdfTronInit = true;
            }

            this.streamService = streamService;
            pdfTreeProc = treeProcessor;
        }

        public TreeViewItem GetCatalogNode()
        {

            var catalogTreeView = new TreeViewItem();

            var root = doc.GetRoot();
            var catalogObj = root.FindObj("Type");

            if (catalogObj != null)
            {
                catalogTreeView = pdfTreeProc.GetCatalogNode(root, catalogObj, catalogObj.GetName());
            }

            return catalogTreeView;
        }

        public IEnumerable<TreeViewItem> GetKidNodes(Obj obj, string name)
        {
            if(obj == null) return new List<TreeViewItem>();   
            switch (obj.GetType())
            {
                case Obj.ObjType.e_dict:
                    var dictionary = new DictObjCollection(obj.GetDictIterator());
                    return pdfTreeProc.GetKidNodes(dictionary);

                case Obj.ObjType.e_array:
                    var array = new ObjectArrayCollection(obj, name);
                    return pdfTreeProc.GetKidNodes(array);

                case Obj.ObjType.e_stream:
                    var dict = new DictObjCollection(obj.GetDictIterator());
                    var children = pdfTreeProc.GetKidNodes(dict).ToList();
                    children.Add(pdfTreeProc.GetStream(name, streamService, obj));
                    return children;

                default:
                    return new List<TreeViewItem>();
            }
        }

        public TreeViewItem GetInfoNode()
        {
            var infoObj = doc.GetTrailer().FindObj("Info");
            if (infoObj == null)
            {
                return new TreeViewItem();
            }

            var docInfo = doc.GetDocInfo();

            return pdfTreeProc.GetInfoNode(infoObj, docInfo);
        }
        
        public IEnumerable<TreeViewItem> GetInfoStrings(BinderObj binder)
        {
            var docInfo = binder.InfoDoc;

            return pdfTreeProc.GetInfoNodes(docInfo);
        }

        public void LoadDoc(string path)
        {
            doc = new PDFDoc(path);
            doc.InitSecurityHandler();
        }

    }
}
