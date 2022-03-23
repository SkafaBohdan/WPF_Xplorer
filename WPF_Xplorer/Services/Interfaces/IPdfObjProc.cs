using pdftron.PDF;
using pdftron.SDF;
using System.Collections.Generic;
using WPF_Xplorer.Models;
using WPF_Xplorer.Services.Interfaces;

namespace WPF_Xplorer.Interfaces
{
    public interface IPdfObjProc
    {
        Dictionary<string, string> GetInfoObj(PDFDocInfo docInfo);
        PdfObj StructObjectBranchOnType(Obj obj, string name, out bool hasKid);
        PdfObj InderectObj(Obj obj, string name);
        PdfObj StringObj(string key, string name);
        PdfObj StreamObj(Obj obj, IStreamService streamServicem, string name);
        PdfObj DocObj(string path);
    }
}
