﻿using Moq;
using NUnit.Framework;
using pdftron.SDF;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using WPF_Xplorer.Models;
using WPF_Xplorer.Services;
using WPF_Xplorer.Services.Interfaces;

namespace WPF_Xplorer.Tests.ServicesTests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class PdfDocProcTest
    {
        private Mock<IPdfService> pdfService;
        private PdfDocProc pdfDocProc;

        private TreeView treeView;
        private TreeViewItem treeViewItem;


        [SetUp]
        public void SetUp()
        {
            pdfService = new Mock<IPdfService>();
            pdfDocProc = new PdfDocProc(pdfService.Object);

            treeView = new TreeView();
            treeViewItem = new TreeViewItem();
            treeViewItem.Items.Add(null);
        }

        [TearDown]
        public void TearDown()
        {
            pdfService = null;
            pdfDocProc = null;
            treeView = null;
            treeViewItem = null;
        }


        [TestCase("path", true)]
        [TestCase(null, false)]
        public void OpenFile_Called_SetPath(string path, bool boolTest)
        {
            pdfService.Setup(service => service.GetDocumentNode(It.IsAny<string>(), It.IsAny<TreeView>(), out boolTest));

            pdfDocProc.OpenFile(path, treeView);

            pdfService.Verify(service => service.GetDocumentNode(path, treeView, out boolTest), Times.Once);
            Assert.AreEqual(path, pdfDocProc.DocPath);
        }

        [Test]
        public void AddRelativeLeaves_TypeIsDocument_AddCatalogNodeAndAddInfoNode()
        {
            treeViewItem.Tag = new BinderObj(new PdfObj() { Type = PdfObj.PdfType.Document }, It.IsAny<Obj>());

            pdfDocProc.RelativeLeaveAdd(treeViewItem);

            pdfService.Verify(service => service.AddCatalogNode(treeViewItem), Times.Once);
            pdfService.Verify(service => service.AddInfoNode(treeViewItem), Times.Once);

        }

        [Test]
        public void AddRelativeLeaves_TypeIsInfo_AddInfoStrings()
        {
            treeViewItem.Tag = new BinderObj(new PdfObj() { Type = PdfObj.PdfType.Info }, It.IsAny<Obj>());

            pdfDocProc.RelativeLeaveAdd(treeViewItem);

            pdfService.Verify(service => service.AddInfoStrings(treeViewItem), Times.Once);
        }

        [TestCase(PdfObj.PdfType.Array)]
        [TestCase(PdfObj.PdfType.Bool)]
        [TestCase(PdfObj.PdfType.Dictionary)]
        [TestCase(PdfObj.PdfType.Indirect)]
        [TestCase(PdfObj.PdfType.Stream)]
        public void AddRelativeLeaves_DefaultCase_AddChildNodes(PdfObj.PdfType pdfType)
        {
            treeViewItem.Tag = new BinderObj(new PdfObj() { Type = pdfType }, It.IsAny<Obj>());

            pdfDocProc.RelativeLeaveAdd(treeViewItem);

            pdfService.Verify(service => service.AddKidNodes(treeViewItem), Times.Once);
        }


        [Test]
        public void PrintBookmarks_Called_ReturnStringBuilder()
        {
            pdfService.Setup(service => service.PrintBookmarks()).Returns(new StringBuilder());

            var result = pdfDocProc.PrintBookmarks();

            pdfService.Verify(service => service.PrintBookmarks(), Times.Once);
            Assert.IsInstanceOf<StringBuilder>(result);
        }
    }
}
