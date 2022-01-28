using pdftron.Common;
using pdftron.PDF;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WPF_Xplorer.Services.Interfaces;


namespace WPF_Xplorer.Services
{
    public class PdfService : IPdfService
    {
        private readonly IPdfTronService pdfTronService;
        private readonly IPdfTreeProc pdfTreeProc;


        public PdfService(IPdfTronService pdfTronService, IPdfTreeProc pdfTreeProc)
        {
            this.pdfTronService = pdfTronService;
            this.pdfTreeProc = pdfTreeProc;
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
            catch (PDFNetException e)
            {
                MessageBox.Show(e.GetMessage(), "Error!");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error!");
            }
        }

     
        public ObservableCollection<StringBuilder> GetGridListItemKey()
        {
            return pdfTreeProc.GetGridListItemKey();
        }
        public ObservableCollection<StringBuilder> GetGridListItemType()
        {
            return pdfTreeProc.GetGridListItemType();
        }
        public ObservableCollection<StringBuilder> GetGridListItemValue()
        {
            return pdfTreeProc.GetGridListItemValue();
        }


        StringBuilder bookmarks = new StringBuilder();
        public StringBuilder PrintBookmarks()
        {
            var doc = pdfTronService.GetDoc();
            Bookmark root = doc.GetFirstBookmark();

            if (root == null)
            {
                return bookmarks.Append("No Bookmarks!");
            }

            return PrintOutlineTree(root);
        }

        void PrintIndent(Bookmark item)
        {
            int indent = item.GetIndent() - 1;
            for (int i = 0; i < indent; ++i)
                bookmarks.Append("  ");
        }

       
        StringBuilder PrintOutlineTree(Bookmark bookItem)
        {
            for (; bookItem.IsValid(); bookItem = bookItem.GetNext())
            {
                PrintIndent(bookItem);
                bookmarks.Append($"{(bookItem.IsOpen() ? "- " : "+ ")}{bookItem.GetTitle()} ACTION ->  ");

                pdftron.PDF.Action action = bookItem.GetAction();
                if (action.IsValid())
                {
                    if (action.GetType() == pdftron.PDF.Action.Type.e_GoTo)
                    {
                        Destination dest = action.GetDest();
                        if (dest.IsValid())
                        {
                            pdftron.PDF.Page page = dest.GetPage();
                            bookmarks.Append($"GoTo Page {page.GetIndex()} \n");
                        }
                    }
                    else
                    {
                        bookmarks.Append("Not a 'GoTo' action  \n");
                    }
                }
                else
                {
                    bookmarks.Append("NULL \n");
                }

                if (bookItem.HasChildren())  
                {
                    PrintOutlineTree(bookItem.GetFirstChild());
                }
            }
            return bookmarks;
        }

    }
}
