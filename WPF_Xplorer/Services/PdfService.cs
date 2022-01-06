using System;
using System.Windows;
using System.Windows.Controls;
using WPF_Xplorer.Services.Interfaces;


namespace WPF_Xplorer.Services
{
    public class PdfService : IPdfService
    {
        private readonly IPdfTronService pdfTronService;
        private readonly IPdfTreeProc pdfTreeProc;


        public PdfService(IPdfTronService pdfTronService, IPdfTreeProc pdfTreeProcessor)
        {
            this.pdfTronService = pdfTronService;
            this.pdfTreeProc = pdfTreeProcessor;
        }

        public void AddCatalogNode(TreeViewItem parent)
        {
            var catalogNode = pdfTronService.GetCatalogNode();

            if (catalogNode.Tag is BinderObj)
            {
                catalogNode.Items.Add(null); 
                parent.Items.Add(catalogNode);
            }
        }

        public void AddKidNodes(TreeViewItem parent)
        {
            if (!(parent.Tag is BinderObj binder)) return;

            var children = pdfTronService.GetKidNodes(binder.Obj, binder.PdfObj.Key);

            foreach (var treeViewItem in children)
            {
                parent.Items.Add(treeViewItem);
            }
        }

        public void AddInfoNode(TreeViewItem parent)
        {
            var infoNode = pdfTronService.GetInfoNode();

            if (infoNode.Tag is BinderObj)
            {
                infoNode.Items.Add(null);
                parent.Items.Add(infoNode);
            }
        }

        public void AddInfoStrings(TreeViewItem parent)
        {
            if (!(parent.Tag is BinderObj binder)) return;

            var children = pdfTronService.GetInfoStrings(binder);

            foreach (var treeViewItem in children)
            {
                parent.Items.Add(treeViewItem);
            }
        }

        public void GetDocumentNode(string path, TreeView parent)
        {
            try
            {
                pdfTronService.LoadDoc(path);

                var documentNode = pdfTreeProc.GetDocumentNode(path);
                documentNode.Items.Add(null); 
                parent.Items.Add(documentNode);
            }
            catch (Exception e)
            {
                MessageBox.Show("Invalid document\n" + e.Message, "Error!");
                
            }
        }
    }
}
