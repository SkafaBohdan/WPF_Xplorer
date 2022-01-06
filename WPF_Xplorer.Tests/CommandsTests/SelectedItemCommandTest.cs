using Moq;
using NUnit.Framework;
using System.Threading;
using System.Windows.Controls;
using WPF_Xplorer.Commands;
using WPF_Xplorer.Services;
using WPF_Xplorer.Services.Interfaces;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.Tests.CommandsTests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class SelectedItemCommandTest
    {
        private Mock<IPdfDocProc> pdfDocProc;
        private ApplicationMainWindowViewModel viewModel;
        private SelectedItemCommand selectedItemCommand;

        [SetUp]
        public void SetUp()
        {
            pdfDocProc = new Mock<IPdfDocProc>();
            viewModel = new ApplicationMainWindowViewModel(pdfDocProc.Object);
            selectedItemCommand = new SelectedItemCommand(viewModel);
        }

        [TearDown]
        public void TearDown()
        {
            pdfDocProc = null;
            viewModel = null;
            selectedItemCommand = null;
        }

        [Test]
        public void Execute_SelectedObject()
        {
            TreeViewItem treeViewItem = new TreeViewItem() { Tag = new BinderObj(new Models.PdfObj(), null) };
            var pdfObject = ((BinderObj)treeViewItem.Tag).PdfObj;

            selectedItemCommand.Execute(treeViewItem);

            Assert.AreEqual(viewModel.SelectedObject, pdfObject);
        }
    }
}
