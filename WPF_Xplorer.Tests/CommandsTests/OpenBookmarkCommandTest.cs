using Moq;
using NUnit.Framework;
using System.Text;
using System.Threading;
using WPF_Xplorer.Commands;
using WPF_Xplorer.Services.Interfaces;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Tests.CommandTests
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
            pdfDocProc.Setup(docProc => docProc.PrintBookmarks()).Returns(new StringBuilder());

            openBookmarkCommand.Execute(It.IsAny<StringBuilder>());

            pdfDocProc.Verify(docProc => docProc.PrintBookmarks(), Times.Once);
        }

    }
}
