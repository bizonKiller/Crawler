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
        public void GetPageTitle_For()
        {
            // Arange
            
            // Act
            var title = parser.GetPageTitle("<html><head><title>test</title></head><body>...");

            // Assert
            Assert.That(title, Is.EqualTo("test"));
        }
    }
}
