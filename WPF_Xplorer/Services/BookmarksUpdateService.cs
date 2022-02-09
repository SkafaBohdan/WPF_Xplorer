using pdftron.PDF;
using System.Windows;
using WPF_Xplorer.Services.Interfaces;


namespace WPF_Xplorer.Services
{
    public class BookmarksUpdateService : NotifyPropertyChanged, IBookmarksUpdateService
    {
        private readonly IPdfTronService pdfTronService;
        private int pageCount;
        PDFDoc doc;


        public BookmarksUpdateService(IPdfTronService pdfTronService)
        {
            this.pdfTronService = pdfTronService;
        }

        public int PageCount
        {
            get
            {
                doc = pdfTronService.GetDoc();
                if (doc == null) return 0;
               
                pageCount = doc.GetPageCount();
                return pageCount;
            }
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

        public void SaveBookmarks(string path)
        {
            doc.Save(path, 0);
        }
    } 
}
