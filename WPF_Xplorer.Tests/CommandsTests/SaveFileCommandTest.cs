using Moq;
using NUnit.Framework;
using pdftron.Common;
using System.Threading;
using WPF_Xplorer.Commands.OpenBookmarkUpdateWindowCommand;
using WPF_Xplorer.Interfaces;
using WPF_Xplorer.Services.Interfaces;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Tests.CommandsTests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class SaveFileCommandTest
    {
        private Mock<IMessageBox> messageBox;
        private Mock<IBookmarksUpdateService> bookService;
        private BookmarkUpdateViewModel bookmarkUpdateViewModel;
        private SaveFileCommand saveFileCommand;

        [SetUp]
        public void SetUp()
        {
            messageBox = new Mock<IMessageBox>();
            bookService = new Mock<IBookmarksUpdateService>();
            bookmarkUpdateViewModel = new BookmarkUpdateViewModel(bookService.Object);
            saveFileCommand = new SaveFileCommand(bookmarkUpdateViewModel, messageBox.Object);
        }

        [TearDown]
        public void TearDown()
        {
            messageBox = null;
            bookService = null;
            bookmarkUpdateViewModel = null;
            saveFileCommand = null;
        }

        [Test]
        public void Execute_Success()
        {
            string titleMessageBox = "Ok";
            bookService.Setup(service => service.SaveBookmarks(It.IsAny<string>()));

            saveFileCommand.Execute(null);

            bookService.Verify(service => service.SaveBookmarks(It.IsAny<string>()), Times.Once);
            messageBox.Verify(message => message.MessageBoxShow(It.IsAny<string>(), titleMessageBox), Times.Once);
        }

        [Test]
        public void Execute_PDFNetException()
        {
            string titleMessageBox = "Error";
            bookService.Setup(service => service.SaveBookmarks(It.IsAny<string>())).Throws(new PDFNetException());

            saveFileCommand.Execute(null);

            messageBox.Verify(message => message.MessageBoxShow(It.IsAny<string>(), titleMessageBox), Times.Once);
        }
    }
}
