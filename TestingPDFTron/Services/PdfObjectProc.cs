using pdftron.PDF;
using pdftron.SDF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPDFTron.Interfaces;
using TestingPDFTron.Models;

namespace TestingPDFTron.Services
{
    public class PdfObjectProc: IPdfObjectProc
    {
        private IPdfObjectValueProc objValueProc;

        public PdfObjectProc(IPdfObjectValueProc objValue)
        {
            this.objValueProc = objValue;
        }

        public PdfObj StructObjectBranchOnType(Obj obj, string name, out bool hasKid)
        {
            hasKid = false;
            if (obj.IsIndirect())
            {
                hasKid = true;
                return InderectObj(obj, name);  // call func case inderect(dict) 
            }

            switch (objValueProc.GetType(obj))
            {
                case Obj.ObjType.e_name:
                    return NameObj(obj, name);

                case Obj.ObjType.e_dict:
                    return DictObj(obj, name);

                case Obj.ObjType.e_stream:
                    return StreamObj(obj, name);

                case Obj.ObjType.e_string:
                    return StringObj(obj, name);

                case Obj.ObjType.e_bool:
                    return BoolObj(obj, name);

                case Obj.ObjType.e_array:
                    return ArrayObj(obj, name);

                case Obj.ObjType.e_number:
                    return NumberObj(obj, name);
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
            var num = objValueProc.GetNumber(obj);

            return new PdfNumber()
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
        public PdfObj StreamObj(Obj obj, string name)
        {
            var stream = obj.GetDecodedStream();
            var streamValue = objValueProc.GetStreamSize(stream);
            return new PdfStream()
            {
                Key = name,
                Value = streamValue
            };
        }
        public PdfObj StringObj(Obj obj, string name)
        {
            var valueString = objValueProc.GetBufferToString(obj);

            return new PdfString()
            {
                Key = name,
                Value = valueString
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
    }
}
