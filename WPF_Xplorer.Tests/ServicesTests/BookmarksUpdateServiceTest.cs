using Moq;
using NUnit.Framework;
using pdftron;
using pdftron.PDF;
using pdftron.SDF;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;
using WPF_Xplorer.Interfaces;
using WPF_Xplorer.Services;
using WPF_Xplorer.Services.Interfaces;

namespace WPF_Xplorer.Tests.ServicesTests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class BookmarksUpdateServiceTest
    {
        private Mock<IPdfTronService> pdfTronService;
        private Mock<IMessageBox> messageBox;
        private BookmarksUpdateService bookmarksUpdateService;
        string nameTest = "test";
        PDFDoc pdfDoc;


        [SetUp]
        public void SetUp()
        {
            pdfTronService = new Mock<IPdfTronService>();
            messageBox = new Mock<IMessageBox>();
            bookmarksUpdateService = new BookmarksUpdateService(pdfTronService.Object, messageBox.Object);
            PDFNet.Initialize("demo:1638797506699:7b62627f0300000000e7c9c9b56709b97ba1634ef4ec416ab001038e68");
            pdfDoc = new PDFDoc();
        }

        [TearDown]
        public void TearDown()
        {
            pdfTronService = null;
            messageBox = null;
            bookmarksUpdateService = null;
            pdfDoc = null;
        }


        [Test]
        public void InitPageCount_InitialPageCount_ValidPageCount()
        {
            pdfTronService.Setup(service => service.GetDoc()).Returns(It.IsAny<PDFDoc>());

            bookmarksUpdateService.InitPageCount();

            Assert.AreEqual(1, bookmarksUpdateService.PageCount);
        }

        [Test]
        public void AddBookmark_Success()
        {
            CreatePdfDocAndPage();

            bookmarksUpdateService.AddBookmark(nameTest, 1);

            messageBox.Verify(message => message.MessageBoxShow(It.IsAny<string>(), "Ok"), Times.Once);
        }

        [Test]
        public void AddChildBookmark_Success()
        {
            CreatePdfDocAndPage();
            Bookmark bookmark = Bookmark.Create(pdfDoc, nameTest);

            bookmarksUpdateService.AddChildBookmark(bookmark, nameTest, 1);

            messageBox.Verify(message => message.MessageBoxShow(It.IsAny<string>(), "Ok"), Times.Once);
        }

        private void CreatePdfDocAndPage()
        {
            var page = pdfDoc.PageCreate();
            pdfDoc.PagePushFront(page);
            pdfTronService.Setup(service => service.GetDoc()).Returns(pdfDoc);
            bookmarksUpdateService.InitPageCount();
        }

        [Test]
        public void Delete_Unsuccess()
        {
            bookmarksUpdateService.DeleteBookmark(It.IsAny<Bookmark>());

            messageBox.Verify(message => message.MessageBoxShow(It.IsAny<string>(), "NotFound"), Times.Once);
        }

        [Test]
        public void Delete_Success()
        {
            Bookmark bookmark = Bookmark.Create(pdfDoc, nameTest);
            pdfDoc.AddRootBookmark(bookmark);

            bookmarksUpdateService.DeleteBookmark(bookmark);

            messageBox.Verify(message => message.MessageBoxShow(It.IsAny<string>(), "Ok"), Times.Once);
        }


        [Test]
        public void GetBookmarksTreeViewItem_AddItem()
        {
            pdfTronService.Setup(service => service.GetBookmarksTree()).Returns(It.IsAny<IEnumerable<It.IsAny<TreeViewItem>()>>());

            bookmarksUpdateService.GetBookmarksTreeViewItem(It.IsAny<TreeView>());

            pdfTronService.Verify(service => service.GetBookmarksTree(), Times.Once);
            //public void GetBookmarksTreeViewItem(TreeView treeView)
            //{
            //    treeView.Items.Clear();
            //    var children = pdfTronService.GetBookmarksTree();
            //    foreach (var item in children)
            //    {
            //        treeView.Items.Add(item);
            //    }
        }


    }
}
