using pdftron.Filters;
using pdftron.PDF;
using pdftron.SDF;
using System.Text;
using WPF_Xplorer.Interfaces;

namespace WPF_Xplorer.Services
{
    public class PdfObjectValueProc : IPdfObjectValueProc
    {
        // PDFDOC Catalog, element ---------------------
        public bool GetBool(Obj obj)
        {
            return obj.GetBool();
        }

        public string GetStringWithBuffer(Obj obj)
        {
            var buffer = obj.GetBuffer();

            return Encoding.Default.GetString(buffer);
        }

        public int GetGenNumber(Obj obj)
        {
            return obj.GetGenNum();
        }

        public string GetName(Obj obj)
        {
            return obj.GetName();
        }

        public double GetNumber(Obj obj)
        {
            return obj.GetNumber();
        }

        public int GetObjectNumber(Obj obj)
        {
            return obj.GetObjNum();
        }

        public int GetSize(Obj obj)
        {
            return obj.Size();
        }

        public double GetStreamSize(Filter stream)
        {
            return stream.Size();
        }

        public Obj.ObjType GetType(Obj obj)
        {
            try
            {
                return obj.GetType();
            }
            catch
            {
                return Obj.ObjType.e_null;
            }
        }

        public bool IsIndirect(Obj obj)
        {
            return obj.IsIndirect();
        }


        // PDFDOC INFO -----------------------

        public string GetAuthor(PDFDocInfo docInfo) 
        {
            return docInfo.GetAuthor();
        }
        public string GetCreater(PDFDocInfo docInfo)
        {
            return docInfo.GetCreator();
        }
        public string GetCreationDate(PDFDocInfo docInfo)
        {
            return docInfo.GetCreationDate().ToString();
        }
        public string GetModDate(PDFDocInfo docInfo)
        {
            return docInfo.GetModDate().ToString();
        }
        public string GetProducer(PDFDocInfo docInfo)
        {
            return docInfo.GetProducer();
        }
        public string GetSubject(PDFDocInfo docInfo)
        {
            return docInfo.GetSubject();
        }
        public string GetTitle(PDFDocInfo docInfo)
        {
            return docInfo.GetTitle();
        }
        public string GetKeywords(PDFDocInfo docInfo)
        {
            return docInfo.GetKeywords();
        }

    }
}
