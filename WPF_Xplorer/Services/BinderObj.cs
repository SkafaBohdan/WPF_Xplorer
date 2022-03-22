using pdftron.PDF;
using pdftron.SDF;
using WPF_Xplorer.Models;

namespace WPF_Xplorer.Services
{
    public class BinderObj
    {
        public Obj Obj { get; set; }
        public PdfObj PdfObj { get; set; }
        public PDFDocInfo InfoDoc { get; set; }

        public BinderObj(PdfObj pdfObject, Obj obj)
        {
            Obj = obj;
            PdfObj = pdfObject;
        }


        public Bookmark BookmarkObj { get; set; }
        public PdfBookmark PdfBookmarkObj { get; set; }

        public BinderObj(PdfBookmark pdfBookmarkObj, Bookmark bookmarkObj)
        {
            BookmarkObj = bookmarkObj;
            PdfBookmarkObj = pdfBookmarkObj;
        }

    }
}
