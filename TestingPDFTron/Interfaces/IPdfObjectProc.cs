using pdftron.PDF;
using pdftron.SDF;
using System.Collections.Generic;
using TestingPDFTron.Models;


namespace TestingPDFTron.Interfaces
{
    public interface IPdfObjectProc
    {
        Dictionary<string, string> GetInfoObj(PDFDocInfo docInfo);
        PdfObj StructObjectBranchOnType(Obj obj, string name, out bool hasKid);
        PdfObj InderectObj(Obj obj, string name);
        PdfObj StringObj(Obj obj, string name);
        PdfObj StreamObj(Obj obj, string name);
        PdfObj DocObj(string path);
    }
}
