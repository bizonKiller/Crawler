using System.Reactive.Linq;
using Moq;
using NUnit.Framework;

namespace BizonCrawler.Tests
{
    [TestFixture]
    public class CrawlerTests
    {
        private Mock<IStringDownloader> _stringDownloadaderMock;
        private Mock<IMongoRepository>  _mongoRepositoryMock;
        private Mock<IPageParser>       _pageParserMock;

        private Crawler _crawler;

        [SetUp]
        public void Setup()
        {
            _stringDownloadaderMock = new Mock<IStringDownloader>();
            _mongoRepositoryMock = new Mock<IMongoRepository>();
            _pageParserMock = new Mock<IPageParser>();
            
            _crawler = new Crawler(_stringDownloadaderMock.Object, _mongoRepositoryMock.Object, _pageParserMock.Object);
        }
        
        [Test]
        public void Run_ForNormalInstance_SaveEverything()
        {
            // Arrange
            const string url1 = "http://test.pl";
            const string url2 = "http://test.pl/1";
            const string html1 = "some html with 1 link";
            const string html2 = "some html without any link";
            const string title1 = "Title1";
            const string title2 = "Title2";

            _stringDownloadaderMock.Setup(downloader => downloader.Get(url1)).Returns(() => new[] { html1 }.ToObservable());
            _stringDownloadaderMock.Setup(downloader => downloader.Get(url2)).Returns(() => new[] { html2 }.ToObservable());
            _pageParserMock.Setup(parser => parser.GetPageTitle(html1)).Returns(title1);
            _pageParserMock.Setup(parser => parser.GetPageTitle(html2)).Returns(title2);
            _pageParserMock.Setup(parser => parser.GetPageLinks(It.Is<Page>(page => page.Url == url1))).Returns(() => new [] { url2 }.ToObservable());
            _pageParserMock.Setup(parser => parser.GetPageLinks(It.Is<Page>(page => page.Url == url2))).Returns(Observable.Empty<string>);

            _mongoRepositoryMock.Setup(repository => repository.OnNext(It.Is<Page>(page => page.Url == url1 && page.Title == title1)));
            _mongoRepositoryMock.Setup(repository => repository.OnNext(It.Is<Page>(page => page.Url == url2 && page.Title == title2)));
            
            // Act
            _crawler.Run(url1);

            // Assert
            _stringDownloadaderMock.VerifyAll();
            _pageParserMock.VerifyAll();
            _mongoRepositoryMock.VerifyAll();
        }

        // TODO: A lot more testing
    }
}