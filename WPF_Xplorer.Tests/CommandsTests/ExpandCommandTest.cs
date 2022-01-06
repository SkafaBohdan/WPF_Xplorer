using Moq;
using NUnit.Framework;
using System.Threading;
using System.Windows.Controls;
using WPF_Xplorer.Commands;
using WPF_Xplorer.Interfaces;
using WPF_Xplorer.Services.Interfaces;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Tests.CommandsTests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class ExpandCommandTest
    {
        private Mock<IArgsConverter> argsConverter;
        private Mock<IPdfDocProc> pdfDocProc;
        private ApplicationMainWindowViewModel viewModel;
        private ExpandCommand expandCommand;

        private TreeViewItem treeViewItem;

        [SetUp]
        public void SetUp()
        {
            argsConverter = new Mock<IArgsConverter>();
            pdfDocProc = new Mock<IPdfDocProc>();
            viewModel = new ApplicationMainWindowViewModel(pdfDocProc.Object);
            expandCommand = new ExpandCommand(viewModel);
            expandCommand.ArgsConverter = argsConverter.Object;

            treeViewItem = new TreeViewItem();
        }

        [TearDown]
        public void TearDown()
        {
            argsConverter = null;
            pdfDocProc = null;
            viewModel = null;
            expandCommand = null;
            treeViewItem = null;
        }


        [Test]
        public void Execute_Open_ReturnFileLoaded()
        {
            argsConverter.Setup(conv => conv.ConverterTreeViewItem(It.IsAny<object>())).Returns(treeViewItem);

            expandCommand.Execute(It.IsAny<object>());

            pdfDocProc.Verify(service => service.RelativeLeaveAdd(ref treeViewItem), Times.Once);
        }

    }
}
