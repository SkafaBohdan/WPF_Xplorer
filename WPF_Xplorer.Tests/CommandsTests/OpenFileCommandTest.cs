using Moq;
using NUnit.Framework;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using WPF_Xplorer.Commands;
using WPF_Xplorer.Services.Interfaces;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Tests.CommandsTests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class OpenFileCommandTest
    {
        private Mock<IDialogOpen> openDialog;
        private Mock<IPdfDocProc> pdfDocProc;
        private Mock<IBookmarksUpdateService> bookUpdateService;
        private BookmarkUpdateViewModel bookmarkUpdateViewModel;
        private ApplicationMainWindowViewModel viewModel;
        private OpenFileCommand openCommand;

        private string fileName = "name";

        [SetUp]
        public void SetUp()
        {
            openDialog = new Mock<IDialogOpen>();
            pdfDocProc = new Mock<IPdfDocProc>();
            bookUpdateService = new Mock<IBookmarksUpdateService>();
            bookmarkUpdateViewModel = new BookmarkUpdateViewModel(bookUpdateService.Object);
            viewModel = new ApplicationMainWindowViewModel(pdfDocProc.Object, bookmarkUpdateViewModel);
            openCommand = new OpenFileCommand(viewModel, openDialog.Object);
        }

        [TearDown]
        public void TearDown()
        {
            openDialog = null;
            pdfDocProc = null;
            viewModel = null;
            openCommand = null;
        }

        [Test]
        public void Execute_Open_ReturnFileLoaded()
        {
            TreeView treeView = new TreeView();
            openDialog.Setup(command => command.OpenDialog(It.IsAny<object>(), out fileName)).Returns(true);
            pdfDocProc.Setup(docProc => docProc.GridListItemKey).Returns(new ObservableCollection<StringBuilder>());
            pdfDocProc.Setup(docProc => docProc.GridListItemType).Returns(new ObservableCollection<StringBuilder>());
            pdfDocProc.Setup(docProc => docProc.GridListItemValue).Returns(new ObservableCollection<StringBuilder>());

            openCommand.Execute(treeView);

            pdfDocProc.Verify(service => service.OpenFile(It.IsAny<string>(), treeView), Times.Once);
        }


        [Test]
        public void Execute_Unsuccessfully_ReturnFileLoaded()
        {
            TreeView treeView = new TreeView();
            openDialog.Setup(command => command.OpenDialog(It.IsAny<object>(), out fileName)).Returns(false);

            openCommand.Execute(treeView);

            pdfDocProc.Verify(service => service.OpenFile(It.IsAny<string>(), treeView), Times.Never);
        }
        
    }
}
