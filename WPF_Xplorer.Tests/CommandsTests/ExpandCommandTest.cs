﻿using Moq;
using NUnit.Framework;
using System.Collections.ObjectModel;
using System.Text;
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
        private Mock<IBookmarksUpdateService> bookUpdateService;
        private BookmarkUpdateViewModel bookmarkUpdateViewModel;
        private ApplicationMainWindowViewModel viewModel;
        private ExpandCommand expandCommand;

        private TreeViewItem treeViewItem;

        [SetUp]
        public void SetUp()
        {
            argsConverter = new Mock<IArgsConverter>();
            pdfDocProc = new Mock<IPdfDocProc>();
            bookUpdateService = new Mock<IBookmarksUpdateService>();
            bookmarkUpdateViewModel = new BookmarkUpdateViewModel(bookUpdateService.Object);
            viewModel = new ApplicationMainWindowViewModel(pdfDocProc.Object, bookmarkUpdateViewModel);
            expandCommand = new ExpandCommand(viewModel);
            expandCommand.argsConverter = argsConverter.Object;
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
            pdfDocProc.Setup(docProc => docProc.GridListItemKey).Returns(new ObservableCollection<StringBuilder>());
            pdfDocProc.Setup(docProc => docProc.GridListItemType).Returns(new ObservableCollection<StringBuilder>());
            pdfDocProc.Setup(docProc => docProc.GridListItemValue).Returns(new ObservableCollection<StringBuilder>());

            expandCommand.Execute(It.IsAny<object>());

            pdfDocProc.Verify(service => service.RelativeLeaveAdd(treeViewItem), Times.Once);
        }

    }
}
