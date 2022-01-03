using pdftron.Filters;
using pdftron.PDF;
using pdftron.SDF;

namespace WPF_Xplorer.Interfaces
{
    public interface IPdfObjectValueProc
    {
        // PDFDOC Catalog, element ---------------------
        bool IsIndirect(Obj obj);
        Obj.ObjType GetType(Obj obj);
        int GetGenNumber(Obj obj);
        int GetObjectNumber(Obj obj);
        double GetNumber(Obj obj);
        bool GetBool(Obj obj);
        double GetStreamSize(Filter stream);
        string GetName(Obj obj);
        string GetStringWithBuffer(Obj obj);
        int GetSize(Obj obj);


        // PDFDOC INFO -----------------------
        string GetAuthor(PDFDocInfo docInfo);
        string GetCreater(PDFDocInfo docInfo);
        string GetCreationDate(PDFDocInfo docInfo);
        string GetModDate(PDFDocInfo docInfo);
        string GetProducer(PDFDocInfo docInfo);
        string GetSubject(PDFDocInfo docInfo);
        string GetTitle(PDFDocInfo docInfo);
        string GetKeywords(PDFDocInfo docInfo);
    }
}
