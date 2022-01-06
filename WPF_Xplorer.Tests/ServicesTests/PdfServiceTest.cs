using Moq;
using NUnit.Framework;
using pdftron.SDF;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;
using WPF_Xplorer.Models;
using WPF_Xplorer.Services;
using WPF_Xplorer.Services.Interfaces;

namespace WPF_Xplorer.Tests.ServicesTests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class PdfServiceTest
    {
        private PdfService pdfService;
        private Mock<IPdfTronService> pdfTronServiceMock;
        private Mock<IPdfTreeProc> pdfTreeProcMock;

        private TreeView treeView;
        private TreeViewItem treeViewItem;
        private IEnumerable<TreeViewItem> kids;
        private TreeViewItem kid;


        [SetUp]
        public void SetUp()
        {
            pdfTreeProcMock = new Mock<IPdfTreeProc>();
            pdfTronServiceMock = new Mock<IPdfTronService>();

            pdfService = new PdfService(pdfTronServiceMock.Object, pdfTreeProcMock.Object);

            treeView = new TreeView();
            treeViewItem = new TreeViewItem
            {
                Tag = new BinderObj(new PdfObj(), It.IsAny<Obj>())
            };
            kid = new TreeViewItem
            {
                Tag = new BinderObj(new PdfObj(), It.IsAny<Obj>())
            };
            kids = new List<TreeViewItem>
            {
                new TreeViewItem(),
                new TreeViewItem()
            };
        }
        
        [TearDown]
        public void TearDown()
        {
            pdfTreeProcMock = null;
            pdfTronServiceMock = null;
            pdfService = null;
            treeView = null;
            treeViewItem = null;
            kid = null;
            kids = null;
        }


        [Test]
        public void GetDocumentNode_Called_AddDummyItem()
        {
            string path = "TestPath";
            pdfTreeProcMock.Setup(service => service.GetDocumentNode(path)).Returns(treeViewItem);

            pdfService.GetDocumentNode(path, treeView);

            pdfTreeProcMock.Verify(service => service.GetDocumentNode(path), Times.Once);
            Assert.AreEqual(1, treeView.Items.Count);
            Assert.AreEqual(1, treeViewItem.Items.Count);
        }

        [Test]
        public void AddInfoString_ValidChild_AddNode()
        {
            pdfTronServiceMock.Setup(service => service.GetInfoStrings(It.IsAny<BinderObj>())).Returns(kids);

            pdfService.AddInfoStrings(treeViewItem);

            pdfTronServiceMock.Verify(service => service.GetInfoStrings(It.IsAny<BinderObj>()), Times.Once);
            Assert.AreEqual(2, treeViewItem.Items.Count);
        }

        [Test]
        public void AddInfoString_InvalidValidChild_AddNode()
        {
            treeViewItem.Tag = null;

            pdfService.AddInfoStrings(treeViewItem);

            pdfTronServiceMock.Verify(service => service.GetInfoStrings(It.IsAny<BinderObj>()), Times.Never);
        }

        [Test]
        public void AddInfoNode_ValidInfoNode_AddDummyItem_()
        {
            pdfTronServiceMock.Setup(service => service.GetInfoNode()).Returns(kid);

            pdfService.AddInfoNode(treeViewItem);

            pdfTronServiceMock.Verify(service => service.GetInfoNode(), Times.Once);
            Assert.AreEqual(kid, treeViewItem.Items[0]);
            Assert.AreEqual(1, treeViewItem.Items.Count);
        }

        [Test]
        public void AddInfoNode_InvalidValidInfoNode_NotAddItem()
        {
            kid = new TreeViewItem(); 
            pdfTronServiceMock.Setup(service => service.GetInfoNode()).Returns(kid);

            pdfService.AddInfoNode(treeViewItem);

            pdfTronServiceMock.Verify(service => service.GetInfoNode(), Times.Once);
            Assert.AreEqual(0, treeViewItem.Items.Count);
        }

        [Test]
        public void AddChildNodes_ValidParent_AddNode()
        {
            pdfTronServiceMock.Setup(service => service.GetKidNodes(It.IsAny<Obj>(), It.IsAny<string>())).Returns(kids);

            pdfService.AddKidNodes(treeViewItem);

            pdfTronServiceMock.Verify(service => service.GetKidNodes(It.IsAny<Obj>(), It.IsAny<string>()), Times.Once);
            Assert.AreEqual(2, treeViewItem.Items.Count);
        }

        [Test]
        public void AddCatalogNode_Invalidparent_NotAddNode()
        {
            treeViewItem.Tag = null;

            pdfService.AddKidNodes(treeViewItem);

            pdfTronServiceMock.Verify(service => service.GetKidNodes(It.IsAny<Obj>(), It.IsAny<string>()), Times.Never);
            Assert.AreEqual(0, treeViewItem.Items.Count);
        }
    }
}
