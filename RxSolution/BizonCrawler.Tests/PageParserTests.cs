using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace BizonCrawler.Tests
{
    [TestFixture]
    public class PageParserTests
    {
        private PageParser parser = new PageParser();

        [Test]
        public void GetPageTitle_ForNormalPage_ReturnTitle()
        {
            // Arange
            
            // Act
            var title = parser.GetPageTitle("<html><head><title>test</title></head><body>...");

            // Assert
            Assert.That(title, Is.EqualTo("test"));
        }


        [Test]
        public void GetPageTitle_ForWrongPage_ReturnEmpty()
        {
            // Arange

            // Act
            var title = parser.GetPageTitle("asdfasdfkasdfkjhasdkfha...");

            // Assert
            Assert.That(title, Is.Empty);
        }

        // TODO: more test especialy for page links
    }
}
