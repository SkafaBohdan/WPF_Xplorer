using pdftron.Common;
using pdftron.PDF;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;
using WPF_Xplorer.Services.Interfaces;
using static WPF_Xplorer.Models.PdfObj;

namespace WPF_Xplorer.Services
{
    public class PdfDocProc : NotifyPropertyChanged, IPdfDocProc
    {
        private readonly IPdfService service;
        private string docPath;
        private string name;


        public PdfDocProc(IPdfService service)
        {
            this.service = service;
        }

        public string DocPath
        {

            get => docPath;
            set
            {
                docPath = value;
                Name = value;
                OnPropertyChanged(nameof(DocPath));
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public void OpenFile(string path, ref TreeView treeView)
        {
            service.GetDocumentNode(path, treeView);
            DocPath = path;
        }

        public void RelativeLeaveAdd(ref TreeViewItem treeViewItem)
        {
            //var items = treeViewItem.Items;
            //if (items.Count > 1 || items.Count == 0)
            //{
            //    return;
            //}

            treeViewItem.Items.Clear();

            var type = ((BinderObj)treeViewItem.Tag).PdfObj.Type;
            switch (type)
            {
                case PdfType.Document:
                    service.AddCatalogNode(treeViewItem);
                    service.AddInfoNode(treeViewItem);
                    break;

                case PdfType.Info:
                    service.AddInfoStrings(treeViewItem);
                    break;

                case PdfType.String:
                    break;

                default:
                    service.AddKidNodes(treeViewItem);
                    break;
            }
        }


        private ObservableCollection<StringBuilder> gridListItemKey;
        public ObservableCollection<StringBuilder> GridListItemKey
        {
            get
            {   
                gridListItemKey = service.GetGridListItemKey();
                return gridListItemKey;
            }
            set
            {
                gridListItemKey = value;
                OnPropertyChanged(nameof(GridListItemKey));
            }
        }
        private ObservableCollection<StringBuilder> gridListItemType;
        
        public ObservableCollection<StringBuilder> GridListItemType
        {
            get
            {
                gridListItemType = service.GetGridListItemType();
                return gridListItemType;
            }
            set
            {
                gridListItemType = value;
                OnPropertyChanged(nameof(GridListItemType));
            }
        }
        private ObservableCollection<StringBuilder> gridListItemValue;
        public ObservableCollection<StringBuilder> GridListItemValue
        {
            get
            {
                gridListItemValue = service.GetGridListItemValue();
                return gridListItemValue;
            }
            set
            {
                gridListItemValue = value;
                OnPropertyChanged(nameof(GridListItemValue));
            }
        }









        //TODO: TestZone
        //TEST ZONE

        static StringBuilder bookmarks = new StringBuilder();

        static void PrintIndent(Bookmark item)
        {
            int indent = item.GetIndent() - 1;
            for (int i = 0; i < indent; ++i)
                bookmarks.Append("  ");
        }

        // Распечатывает дерево схемы на стандартный вывод
        static StringBuilder PrintOutlineTree(Bookmark item)
        {
            for (; item.IsValid(); item = item.GetNext())
            {
                PrintIndent(item);
                bookmarks.Append($"{(item.IsOpen() ? "- " : "+ ")}{item.GetTitle()} ACTION ->  \n");

                // Print Action
                pdftron.PDF.Action action = item.GetAction();
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

                if (item.HasChildren())  // Рекурсивно печатать дочерние поддеревья
                {
                    PrintOutlineTree(item.GetFirstChild());
                }
            }
            return bookmarks;
        }

        public StringBuilder PrintBookmarks()
        {
            try
            {
                var path = docPath.Replace("\\", "/");
                using (PDFDoc doc = new PDFDoc(path))
                {
                    doc.InitSecurityHandler();

                    Bookmark root = doc.GetFirstBookmark();
                    if (root == null)
                    {
                        System.Windows.MessageBox.Show("No bookmarks found", "Bookmarks");
                        return null;
                    }
                    else
                    {
                        return PrintOutlineTree(root);
                    }
                }
            }
            catch (PDFNetException e)
            {
                System.Windows.MessageBox.Show(e.GetMessage(), "Error");
                return null;
            }
            catch(Exception e)
            {
                System.Windows.MessageBox.Show(e.Message, "Error");
                return null;
            }
        }

        //////
    }
}
