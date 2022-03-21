using pdftron.PDF;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WPF_Xplorer.Services.Interfaces;


namespace WPF_Xplorer.Services
{
    public class BookmarksUpdateService : NotifyPropertyChanged, IBookmarksUpdateService
    {
        private readonly IPdfTronService pdfTronService;
        private int pageCount = 1;
        PDFDoc doc;

        
        public BookmarksUpdateService(IPdfTronService pdfTronService)
        {
            this.pdfTronService = pdfTronService;
        }

        public int PageCount
        {
            get
            {
                return pageCount;
            }
            set
            {
                pageCount = value;
                OnPropertyChanged(nameof(PageCount));
            }
        }

        public void InitPageCount()
        {
            doc = pdfTronService.GetDoc();
            if (doc == null) return;

            PageCount = doc.GetPageCount();
        }

        public void AddBookmark(string name, int page)
        {
            Bookmark bookmark = Bookmark.Create(doc, name);
            doc.AddRootBookmark(bookmark);

            Destination bookmarkDestination = Destination.CreateFit(doc.GetPage(page));
            bookmark.SetAction(pdftron.PDF.Action.CreateGoto(bookmarkDestination));

            MessageBox.Show("Закладка добавлена", "Ok");
        }
     
        public void  GetBookmarksTreeViewItem(TreeView treeView)
        {
            treeView.Items.Clear();
            var children = pdfTronService.GetBookmarksTree();
            foreach (var item in children)
            {
                treeView.Items.Add(item);
            }
        }
        
        public void DeleteBookmark(Bookmark bookmarkObj)
        {
            if (bookmarkObj == null)
            {
                MessageBox.Show("Закладка не найдена", "Ne Ok");
            }
            else
            {
                bookmarkObj.Delete();
                MessageBox.Show("Закладка удалена", "Ok");
            }
        }

        public void AddChildBookmark(Bookmark parentBookmark, string name, int page)
        {

            Bookmark childBookmark = parentBookmark.AddChild(name);
            childBookmark.SetAction(Action.CreateGoto(Destination.CreateFit(doc.GetPage(page))));

            MessageBox.Show("Закладка добавлена", "Ok");
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

                Action action = bookItem.GetAction();
                if (action.IsValid())
                {
                    if (action.GetType() == Action.Type.e_GoTo)
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

        public void SaveBookmarks(string path)
        {
            if (path == null) path = doc.GetFileName();
            doc.Save(path, 0);
        }
    } 
}
