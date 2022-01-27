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
    public class ClosePdfFileCommandTest
    {
        private Mock<IPdfDocProc> pdfDocProc;
        private ApplicationMainWindowViewModel viewModel;
        private ClosePdfFileCommand closeCommand;

        [SetUp]
        public void SetUp()
        {
            pdfDocProc = new Mock<IPdfDocProc>();
            viewModel = new ApplicationMainWindowViewModel(pdfDocProc.Object);
            closeCommand = new ClosePdfFileCommand(viewModel);
        }

        [TearDown]
        public void TearDown()
        {
            pdfDocProc = null;
            viewModel = null;
            closeCommand = null;
        }


        [Test]
        public void CanExecute_ValidDocPath_ReturnTrue()
        {
            pdfDocProc.Setup(service => service.DocPath).Returns("path");

            var result = closeCommand.CanExecute(null);

            Assert.IsTrue(result);
        }

        [Test]
        public void CanExecute_InvalidDocPath_ReturnFalse()
        {
            var result = closeCommand.CanExecute(null);

            Assert.IsFalse(result);
        }

        [Test]
        public void Execute_CloseFile_ClearItems()
        {
            TreeView treeView = new TreeView();
            treeView.Items.Add(null);
            pdfDocProc.Setup(docProc => docProc.GridListItemKey).Returns(new ObservableCollection<StringBuilder>());
            pdfDocProc.Setup(docProc => docProc.GridListItemType).Returns(new ObservableCollection<StringBuilder>());
            pdfDocProc.Setup(docProc => docProc.GridListItemValue).Returns(new ObservableCollection<StringBuilder>());

            closeCommand.Execute(treeView);

            Assert.AreEqual(0, treeView.Items.Count);   
        }

        [Test]
        public void Execute_AbsentFile_InactiveFileClosed()
        {
            TreeView treeView = new TreeView();
            treeView.Items.Add(null);
            pdfDocProc.Setup(docProc => docProc.GridListItemKey).Returns(new ObservableCollection<StringBuilder>());
            pdfDocProc.Setup(docProc => docProc.GridListItemType).Returns(new ObservableCollection<StringBuilder>());
            pdfDocProc.Setup(docProc => docProc.GridListItemValue).Returns(new ObservableCollection<StringBuilder>());

            closeCommand.Execute(treeView);

            Assert.AreEqual(0, treeView.Items.Count);
        }

    }
}
