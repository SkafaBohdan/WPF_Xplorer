using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using WPF_Xplorer.Services;
using WPF_Xplorer.Services.Interfaces;

namespace WPF_Xplorer.Tests.BookmarksTests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class PdfServiceBookmarksTest
    {
        private Mock<IPdfTronService> pdfTronService;
        private Mock<IPdfTreeProc> pdfTreeProc;
        private PdfService pdfService;
    

        [SetUp]
        public void SetUp()
        {
            pdfTronService = new Mock<IPdfTronService>();
            pdfTreeProc = new Mock<IPdfTreeProc>();
            pdfService = new PdfService(pdfTronService.Object, pdfTreeProc.Object);
        }

        [TearDown]
        public void TearDown()
        {
            pdfTronService = null;
            pdfTreeProc = null;
            pdfService = null;
        }


     
        [Test]
        public void PrintBookmarks_ArgumentException()
        {
            pdfTronService.Setup(tronService => tronService.GetDoc()).Throws(new ArgumentException());

            Assert.Throws<ArgumentException>(() => pdfService.PrintBookmarks());
        }

        [Test]
        public void PrintBookmarks_Exception()
        {
            Assert.Throws<Exception>(() => pdfService.PrintBookmarks());
        }
     
    }
}
