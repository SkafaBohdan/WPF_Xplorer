using Moq;
using NUnit.Framework;
using System;
using System.Text;
using System.Threading;
using WPF_Xplorer.Commands;
using WPF_Xplorer.Services.Interfaces;
using WPF_Xplorer.View;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Tests.BookmarksTests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class OpenBookmarkCommandTest
    {
        private Mock<IPdfDocProc> pdfDocProc;
        private ApplicationMainWindowViewModel viewMainModel;
        private BookmarksViewModel bookmarksViewModel;
        private OpenBookmarkCommand openBookmarkCommand;

        [SetUp]
        public void SetUp()
        {
            pdfDocProc = new Mock<IPdfDocProc>();
            viewMainModel = new ApplicationMainWindowViewModel(pdfDocProc.Object);
            bookmarksViewModel = new BookmarksViewModel();
            openBookmarkCommand = new OpenBookmarkCommand(viewMainModel, bookmarksViewModel);
        }

        [TearDown]
        public void TearDown()
        {
            pdfDocProc = null;
            viewMainModel = null;
            bookmarksViewModel = null;
            openBookmarkCommand = null;
        }

        
        [Test]
        public void Execute_Verify()
        {
            openBookmarkCommand.Execute(It.IsAny<StringBuilder>());

            pdfDocProc.Verify(docProc => docProc.PrintBookmarks(), Times.Once);
        }

        [Test]
        public void Execute_Exception()
        {
            pdfDocProc.Setup(DocProc => DocProc.PrintBookmarks()).Throws(new Exception());

            openBookmarkCommand.Execute(It.IsAny<StringBuilder>());

            pdfDocProc.Verify(docProc => docProc.PrintBookmarks(), Times.Once);
        }

    }
}
