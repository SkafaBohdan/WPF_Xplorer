using Moq;
using NUnit.Framework;
using System;
using System.Text;
using System.Threading;
using WPF_Xplorer.Services;
using WPF_Xplorer.Services.Interfaces;

namespace WPF_Xplorer.Tests.BookmarksTests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class PdfDocProcBookmarksTest
    {
        private Mock<IPdfService> pdfService;
        private PdfDocProc pdfDocProc;

        [SetUp]
        public void SetUp()
        {
            pdfService = new Mock<IPdfService>();
            pdfDocProc = new PdfDocProc(pdfService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            pdfService = null;
            pdfDocProc = null;
        }


        [Test]
        public void PrintBookmarks_Called_ReturnStringBuilder()
        {
            pdfService.Setup(service => service.PrintBookmarks()).Returns(new StringBuilder());

            var result = pdfDocProc.PrintBookmarks();

            pdfService.Verify(service => service.PrintBookmarks(), Times.Once);
            Assert.IsInstanceOf<StringBuilder>(result);
        }

        [Test]
        public void PrintBookmarks_ArgumentException()
        {
            pdfService.Setup(service => service.PrintBookmarks()).Throws(new ArgumentException());

            pdfService.Verify(service => service.PrintBookmarks(), Times.Never);
            Assert.Throws<ArgumentException>(() => pdfDocProc.PrintBookmarks());
        }

        [Test]
        public void PrintBookmarks_Exception()
        {
            pdfService.Setup(service => service.PrintBookmarks()).Throws(new Exception());

            pdfService.Verify(service => service.PrintBookmarks(), Times.Never);
            Assert.Throws<Exception>(() => pdfDocProc.PrintBookmarks());
        }
    }
}
