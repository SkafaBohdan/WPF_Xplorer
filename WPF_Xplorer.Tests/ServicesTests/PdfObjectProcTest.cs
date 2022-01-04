using Moq;
using NUnit.Framework;
using pdftron.Filters;
using pdftron.PDF;
using pdftron.SDF;
using System.Collections.Generic;
using System.Threading;
using WPF_Xplorer.Interfaces;
using WPF_Xplorer.Models;
using WPF_Xplorer.Services;
using WPF_Xplorer.Services.Interfaces;

namespace WPF_Xplorer.Tests.ServicesTests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class PdfObjectProcTest
    {
        private Mock<IPdfObjectValueProc> pdfObjValueProc;
        private Mock<IStreamService> streamService;
        private PdfObjectProc pdfObjectProc;

        private bool hasKid;
        private readonly Obj obj = It.IsAny<Obj>();
        private readonly string key = "Key";
        private readonly string value = "Value";

        [SetUp]
        public void SetUp()
        {
            pdfObjValueProc = new Mock<IPdfObjectValueProc>();
            pdfObjectProc = new PdfObjectProc(pdfObjValueProc.Object);

            streamService = new Mock<IStreamService>();
        }

        [TearDown]
        public void TearDown()
        {
            pdfObjValueProc = null;
            pdfObjectProc = null;
            streamService = null;
        }

        #region StructObjectBranchOnTypeTESTS

        [Test]
        public void StructObjectBranchOnType_TypeName_ReturnNameObj()
        {
            pdfObjValueProc.Setup(service => service.GetType(obj)).Returns(Obj.ObjType.e_name);
            pdfObjValueProc.Setup(service => service.GetName(obj)).Returns(value);

            var result = pdfObjectProc.StructObjectBranchOnType(obj, key, out hasKid);

            pdfObjValueProc.Verify(service => service.GetName(obj), Times.Once);
            Assert.AreEqual(PdfObj.PdfType.Name, result.Type);
            Assert.IsInstanceOf<PdfName>(result);
            Assert.AreEqual(result.Key, key);
            Assert.AreEqual(result.Value, value);
        }

        [Test]
        public void StructObjectBranchOnType_TypeDict_ReturnDictObj()
        {
            int valueDictSize = 3;
            pdfObjValueProc.Setup(service => service.GetType(obj)).Returns(Obj.ObjType.e_dict);
            pdfObjValueProc.Setup(service => service.GetSize(obj)).Returns(valueDictSize);

            var result = pdfObjectProc.StructObjectBranchOnType(obj, key, out hasKid);

            pdfObjValueProc.Verify(service => service.GetSize(obj), Times.Once);
            Assert.AreEqual(PdfObj.PdfType.Dictionary, result.Type);
            Assert.IsInstanceOf<PdfDict>(result);
            Assert.AreEqual(result.Key, key);
            Assert.AreEqual(result.Value, valueDictSize);
        }

        [Test]
        public void StructObjectBranchOnType_TypeArray_ReturnArrayObj()
        {
            int valueArrSize = 3;
            pdfObjValueProc.Setup(service => service.GetType(obj)).Returns(Obj.ObjType.e_array);
            pdfObjValueProc.Setup(service => service.GetSize(obj)).Returns(valueArrSize);

            var result = pdfObjectProc.StructObjectBranchOnType(obj, key, out hasKid);

            Assert.AreEqual(PdfObj.PdfType.Array, result.Type);
            Assert.IsInstanceOf<PdfArray>(result);
            Assert.AreEqual(result.Key, key);
            Assert.AreEqual(result.Value, valueArrSize);
        }

        [Test]
        public void StructObjectBranchOnType_TypeString_ReturnStringObj()
        {
            pdfObjValueProc.Setup(service => service.GetType(obj)).Returns(Obj.ObjType.e_string);
            pdfObjValueProc.Setup(service => service.GetStringWithBuffer(obj)).Returns(value);

            var result = pdfObjectProc.StructObjectBranchOnType(obj, key, out hasKid);

            Assert.AreEqual(PdfObj.PdfType.String, result.Type);
            Assert.IsInstanceOf<PdfString>(result);
            Assert.AreEqual(result.Key, key);
            Assert.AreEqual(result.Value, value);
        }

        [Test]
        public void StructObjectBranchOnType_TypeBool_ReturnBoolObj()
        {
            pdfObjValueProc.Setup(service => service.GetType(obj)).Returns(Obj.ObjType.e_bool);
            pdfObjValueProc.Setup(service => service.GetBool(obj)).Returns(true);

            var result = pdfObjectProc.StructObjectBranchOnType(obj, key, out hasKid);

            Assert.AreEqual(PdfObj.PdfType.Bool, result.Type);
            Assert.IsInstanceOf<PdfBool>(result);
            Assert.AreEqual(result.Key, key);
            Assert.AreEqual(result.Value, true);
        }

        [Test]
        public void StructObjectBranchOnType_TypeNumber_ReturnNumberObj()
        {
            int valueNumber = 3;
            pdfObjValueProc.Setup(service => service.GetType(obj)).Returns(Obj.ObjType.e_number);
            pdfObjValueProc.Setup(service => service.GetNumber(obj)).Returns(valueNumber);

            var result = pdfObjectProc.StructObjectBranchOnType(obj, key, out hasKid);

            Assert.AreEqual(PdfObj.PdfType.Number, result.Type);
            Assert.IsInstanceOf<PdfNumber>(result);
            Assert.AreEqual(result.Key, key);
            Assert.AreEqual(result.Value, valueNumber);
        }

        [Test]
        public void StructObjectBranchOnType_TypeDefault_ReturnNull()
        {
            pdfObjValueProc.Setup(service => service.GetType(obj)).Returns(It.IsAny<Obj.ObjType>());

            var result = pdfObjectProc.StructObjectBranchOnType(obj, key, out hasKid);

            Assert.AreEqual(null, result);
        }

        [Test]
        public void StructObjectBranchOnType_IndirectObj_ReturnIndirectObj()
        {
            pdfObjValueProc.Setup(service => service.GetGenNumber(obj)).Returns(1);
            pdfObjValueProc.Setup(service => service.GetObjectNumber(obj)).Returns(1);
            pdfObjValueProc.Setup(service => service.IsIndirect(obj)).Returns(true);

            var result = pdfObjectProc.StructObjectBranchOnType(obj, key, out hasKid);

            pdfObjValueProc.Verify(service => service.IsIndirect(obj), Times.Once);
            Assert.AreEqual(PdfObj.PdfType.Indirect, result.Type);
            Assert.IsInstanceOf<PdfIndirectObj>(result);
            Assert.AreEqual(true, hasKid);
        }
        #endregion

        [Test]
        public void GetInfoObj_Called_ReturnDictionaryInfoTypeDocument()
        {
            int countDictionary = 8;
            pdfObjValueProc.SetReturnsDefault(countDictionary);

            var result = pdfObjectProc.GetInfoObj(It.IsAny<PDFDocInfo>());

            Assert.IsInstanceOf<Dictionary<string, string>>(result);
            Assert.AreEqual(countDictionary, result.Count);
        }

        [Test]
        public void DocObj_Called_ReturnPdfObjTypeDocument()
        {
            string path = "path";

            var result = pdfObjectProc.DocObj(path);

            Assert.AreEqual(PdfObj.PdfType.Document, result.Type);
            Assert.AreEqual(path, result.Key);
            Assert.AreEqual(path, result.Value);
        }

        [Test]
        public void StreamObj_Called_ReturnPdfObjStreamTypeStream()
        {
            string path = "path";
            int size = 1;
            streamService.Setup(service => service.CreateStream(It.IsAny<Obj>(), It.IsAny<int>(), out path));
            pdfObjValueProc.Setup(service => service.GetStreamSize(It.IsAny<Filter>())).Returns(size);

            var result = pdfObjectProc.StreamObj(obj, streamService.Object, key);

            streamService.Verify(service => service.CreateStream(It.IsAny<Obj>(), It.IsAny<int>(), out path), Times.Once);
            Assert.AreEqual(PdfObj.PdfType.Stream, result.Type);
            Assert.AreEqual(size, result.Value);
            Assert.AreEqual(key, result.Key);
            Assert.AreEqual(path, ((PdfStream)result).Path);
        }

        
    }
}
