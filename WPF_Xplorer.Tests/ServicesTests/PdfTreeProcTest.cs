using Moq;
using NUnit.Framework;
using pdftron.SDF;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Controls;
using WPF_Xplorer.Interfaces;
using WPF_Xplorer.Models;
using WPF_Xplorer.Services;
using WPF_Xplorer.Services.Interfaces;

namespace WPF_Xplorer.Tests.ServicesTests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class PdfTreeProcTest
    {
        private PdfTreeProc pdfTreeProc;
        private Mock<IPdfObjProc> pdfObjProc;
        private Mock<IEnumerable<KeyValuePair<string, Obj>>> dict;


        [SetUp]
        public void SetUp()
        {
            pdfObjProc = new Mock<IPdfObjProc>();
            pdfTreeProc = new PdfTreeProc(pdfObjProc.Object);
            dict = new Mock<IEnumerable<KeyValuePair<string, Obj>>>();
        }

        [TearDown]
        public void TearDown()
        {
            pdfObjProc = null;
            pdfTreeProc = null;
            dict = null;
        }


        [Test]
        public void GetCatalogNode_Called_ReturnTreeViewItemPdfObj()
        {
            string Key = "Key";
            pdfObjProc.Setup(service => service.InderectObj(It.IsAny<Obj>(), It.IsAny<string>())).Returns(new PdfObj());

            var result = pdfTreeProc.GetCatalogNode(It.IsAny<Obj>(), It.IsAny<Obj>(), Key);

            pdfObjProc.Verify(service => service.InderectObj(It.IsAny<Obj>(), It.IsAny<string>()), Times.Once);
            Assert.IsInstanceOf<TreeViewItem>(result);
        }

        [Test]
        public void GetChildNodes_Called_ReturnIEnumerableTreeViewItems()
        {
            var hasKids = false;
            dict.Setup(dict => dict.GetEnumerator()).Returns(new DictEnumerator(It.IsAny<DictIterator>()));
            pdfObjProc.Setup(service => service.StructObjectBranchOnType(It.IsAny<Obj>(), It.IsAny<string>(), out hasKids));

            var result = pdfTreeProc.GetChildNodes(dict.Object);
            
            Assert.IsInstanceOf<IEnumerable<TreeViewItem>>(result);
        }

        [Test]
        public void GetDocumentNode_Called_ReturnTreeViewItemPdfDocObj()
        {
            string path = "Path";
            pdfObjProc.Setup(service => service.DocObj(path)).Returns(new PdfObj() { Type = PdfObj.PdfType.Document });

            var result = pdfTreeProc.GetDocumentNode(path);

            pdfObjProc.Verify(service => service.DocObj(path), Times.Once);
            Assert.IsInstanceOf<TreeViewItem>(result);
        }

        [Test]
        public void GetInfoNode_Called_ReturnTreeViewItemPdfObjectTypeInfo()
        {
            pdfObjProc.Setup(service => service.InderectObj(It.IsAny<Obj>(), It.IsAny<string>())).Returns(new PdfObj());

            var result = pdfTreeProc.GetInfoNode(It.IsAny<Obj>(), It.IsAny<pdftron.PDF.PDFDocInfo>());
            var pdfObj = ((ObjBinder)result.Tag).PdfObj;

            pdfObjProc.Verify(service => service.InderectObj(It.IsAny<Obj>(), It.IsAny<string>()), Times.Once);
            Assert.That(pdfObj.Type, Is.EqualTo(PdfObj.PdfType.Info));

        }

        [Test]
        public void GetInfoNodes_Called_ReturnsIEnumerableTreeViewItems()
        {
            var key = "Key";
            var value = "Value";

            pdfObjProc.Setup(service => service.GetInfoObj(It.IsAny<pdftron.PDF.PDFDocInfo>())).
                Returns(new Dictionary<string, string> 
            { 
                { key, value } 
            });

            pdfObjProc.Setup(service => service.StringObj(key, value)).Returns(new PdfObj());

            var result = pdfTreeProc.GetInfoNodes(It.IsAny<pdftron.PDF.PDFDocInfo>());
            var resultObj = ((ObjBinder)result.First().Tag).PdfObj;


            pdfObjProc.Verify(service => service.StringObj(key, value), Times.Once);
            pdfObjProc.Verify(service => service.GetInfoObj(It.IsAny<pdftron.PDF.PDFDocInfo>()), Times.Once);
            Assert.IsInstanceOf<IEnumerable<TreeViewItem>>(result);
            Assert.AreEqual(1, result.Count());
            Assert.IsInstanceOf<PdfObj>(resultObj);
        }

        [Test]
        public void GetStream_Called_StreamTreeViewItem()
        {
            var key = "Key";
            pdfObjProc.Setup(service => service.StreamObj(It.IsAny<Obj>(), It.IsAny<IStreamService>(), It.IsAny<string>())).Returns(new PdfStream());

            var result = pdfTreeProc.GetStream(key, It.IsAny<IStreamService>(), It.IsAny<Obj>());
            var resultBind = ((ObjBinder)result.Tag).PdfObj;

            pdfObjProc.Verify(service => service.StreamObj(It.IsAny<Obj>(), It.IsAny<IStreamService>(), It.IsAny<string>()), Times.Once);
            Assert.IsInstanceOf<TreeViewItem>(result);
            Assert.IsInstanceOf<PdfStream>(resultBind);
        }

    }
}
