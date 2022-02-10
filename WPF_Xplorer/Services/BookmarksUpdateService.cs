using pdftron.PDF;
using System.Windows;
using WPF_Xplorer.Services.Interfaces;


namespace WPF_Xplorer.Services
{
    public class BookmarksUpdateService : NotifyPropertyChanged, IBookmarksUpdateService
    {
        private readonly IPdfTronService pdfTronService;
        private int pageCount = 0;
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

            MessageBox.Show("Закладка добавлена");
        }

        public void DeleteBookmark(string name)
        {
            Bookmark bookmark = doc.GetFirstBookmark().Find(name);
            if (bookmark == null)
            {
                MessageBox.Show("Закладка не найдена");
            }
            else
            {
                bookmark.Delete();
                MessageBox.Show("Закладка удалена");
            }
        }

        public void AddChildBookmark(string findName, string name, int page)
        {
            Bookmark bookmark = doc.GetFirstBookmark().Find(findName);
            if (bookmark == null)
            {
                MessageBox.Show("Закладка не найдена");
            }
            else
            {
                Bookmark childBookmark = bookmark.AddChild(name);
                childBookmark.SetAction(pdftron.PDF.Action.CreateGoto(Destination.CreateFit(doc.GetPage(page))));

                MessageBox.Show("Закладка добавлена");
            }
        }
        

        public void SaveBookmarks(string path)
        {
            doc.Save(path, 0);
        }
    } 
}
