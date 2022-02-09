using Moq;
using NUnit.Framework;
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
        private Mock<IBookmarksUpdateService> bookUpdateService;
        private BookmarkUpdateViewModel bookmarkUpdateViewModel;
        private ApplicationMainWindowViewModel viewMainModel;
        private BookmarksViewModel bookmarksViewModel;
        private OpenBookmarkCommand openBookmarkCommand;

        [SetUp]
        public void SetUp()
        {
            pdfDocProc = new Mock<IPdfDocProc>();
            bookUpdateService = new Mock<IBookmarksUpdateService>();
            bookmarkUpdateViewModel = new BookmarkUpdateViewModel(bookUpdateService.Object);
            viewMainModel = new ApplicationMainWindowViewModel(pdfDocProc.Object, bookmarkUpdateViewModel);
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
        public void CanExecute_True()
        {
            pdfDocProc.Setup(service => service.DocPath).Returns("true");

            var result = openBookmarkCommand.CanExecute(It.IsAny<object>());

            Assert.IsTrue(result);
        }

    }
}
