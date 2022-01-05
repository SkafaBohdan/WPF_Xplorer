using Moq;
using NUnit.Framework;
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
        private ApplicationMainWindowViewModel viewModel;
        private OpenFileCommand openCommand;

        private string fileName = "name";

        [SetUp]
        public void SetUp()
        {
            openDialog = new Mock<IDialogOpen>();
            pdfDocProc = new Mock<IPdfDocProc>();
            viewModel = new ApplicationMainWindowViewModel(pdfDocProc.Object);
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

            openCommand.Execute(treeView);

            pdfDocProc.Verify(service => service.OpenFile(It.IsAny<string>(), ref treeView), Times.Once);
        }


        [Test]
        public void Execute_Unsuccessfully_ReturnFileLoaded()
        {
            TreeView treeView = new TreeView();
            openDialog.Setup(command => command.OpenDialog(It.IsAny<object>(), out fileName)).Returns(false);

            openCommand.Execute(treeView);

            pdfDocProc.Verify(service => service.OpenFile(It.IsAny<string>(), ref treeView), Times.Never);
        }
        
    }
}
