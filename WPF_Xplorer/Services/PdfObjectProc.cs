using pdftron.PDF;
using pdftron.SDF;
using System.Collections.Generic;
using WPF_Xplorer.Interfaces;
using WPF_Xplorer.Models;
using WPF_Xplorer.Services.Interfaces;

namespace WPF_Xplorer.Services
{
    public class PdfObjectProc: IPdfObjProc
    {
        private IPdfObjectValueProc objValueProc;
        private int streamNumb = 1;

        public PdfObjectProc(IPdfObjectValueProc objValue)
        {
            objValueProc = objValue;
        }

        public PdfObj StructObjectBranchOnType(Obj obj, string name, out bool hasKid)
        {
            hasKid = false;
            if (objValueProc.IsIndirect(obj))
            {
                hasKid = true;
                return InderectObj(obj, name);  // call func case inderect(dict) 
            }

            switch (objValueProc.GetType(obj))
            {
                case Obj.ObjType.e_name:
                    return NameObj(obj, name);

                case Obj.ObjType.e_dict:
                    hasKid = true;
                    return DictObj(obj, name);

                case Obj.ObjType.e_array:
                    hasKid = true;
                    return ArrayObj(obj, name);

                case Obj.ObjType.e_string:
                    return StringObj(obj, name);

                case Obj.ObjType.e_bool:
                    return BoolObj(obj, name);

                case Obj.ObjType.e_number:
                    return NumberObj(obj, name);

                case Obj.ObjType.e_null:
                    return NullObj(obj, name);

                default:
                    return null;
            }
        }

        public Dictionary<string, string> GetInfoObj(PDFDocInfo docInfo)
        {
            return new Dictionary<string, string>
            {
                {"Title", objValueProc.GetTitle(docInfo)},
                {"Author", objValueProc.GetAuthor(docInfo)},
                {"Creator", objValueProc.GetCreater(docInfo)},
                {"Producer", objValueProc.GetProducer(docInfo)},
                {"CreationDate", objValueProc.GetCreationDate(docInfo)},
                {"ModeData", objValueProc.GetModDate(docInfo)},
                {"Keywords", objValueProc.GetKeywords(docInfo)},
                {"Subject", objValueProc.GetSubject(docInfo)}
            };
        }

        public PdfObj DocObj(string path)
        {
            return new PdfObj
            {
                Key = path,
                Type = PdfObj.PdfType.Document,
                Value = path
             };
        }

        public PdfObj InderectObj(Obj obj, string name)
        {
            return new PdfIndirectObj()
            {
                Key = name,
                GenNumber = objValueProc.GetGenNumber(obj),
                ObjNum = objValueProc.GetObjectNumber(obj)
            };
        }
        public PdfObj NameObj(Obj obj, string name)
        {
            var num = objValueProc.GetName(obj);

            return new PdfName()
            {
                Key = name,
                Value = num
            };
        }
        public PdfObj DictObj(Obj obj, string name)
        {
            var size = objValueProc.GetSize(obj);

            return new PdfDict()
            {
                Key = name,
                Value = size
            };
        }
        public PdfObj StreamObj(Obj obj, IStreamService streamService, string name)
        {
            var stream = streamService.CreateStream(obj, streamNumb++, out var fullPath);

            return new PdfStream()
            {
                Key = name,
                Path = fullPath,
                Value = objValueProc.GetStreamSize(stream)
            };
        }

        public PdfObj StringObj(string key, string value)
        {
            return new PdfString
            {
                Key = key,
                Value = value
            };
        }

        public PdfObj BoolObj(Obj obj, string name)
        {
            var valueBool = objValueProc.GetBool(obj);

            return new PdfBool()
            {
                Key = name,
                Value = valueBool

            };
        }

        public PdfObj ArrayObj(Obj obj, string name)
        {
            var size = objValueProc.GetSize(obj);

            return new PdfArray()
            {
                Key = name,
                Value = size
            };
        }

        public PdfObj NumberObj(Obj obj, string name)
        {
            var number = objValueProc.GetNumber(obj);

            return new PdfNumber()
            {
                Key = name,
                Value = number
            };
        }

        private PdfObj StringObj(Obj obj, string name)
        {
            var valueString = objValueProc.GetStringWithBuffer(obj);

            return new PdfString()
            {
                Key = name,
                Value = valueString
            };
        }

        private PdfObj NullObj(Obj obj, string name)
        {
            return new PdfObj()
            {
                Key = "<null>",
                Type = PdfObj.PdfType.Null,
                Value = "null"
            };
        }

    }
}
