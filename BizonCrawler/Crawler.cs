using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace BizonCrawler
{
    public class Crawler
    {
        private IStringDownloader _stringDownloader;
        private IMongoRepository _mongoRepository;
        private IPageParser _pageParser;

        private Subject<string> _urlsToCheck;
        private ConcurentHashSet<string> _checkedUrls;

        public Crawler(IStringDownloader stringDownloader, IMongoRepository mongoRepository, IPageParser pageParser)
        {
            _stringDownloader = stringDownloader;
            _mongoRepository = mongoRepository;
            _pageParser = pageParser;
        }

        public void Run(string startUrl)
        {
            _urlsToCheck = new Subject<string>();
            _checkedUrls = new ConcurentHashSet<string>();
            
            var pagesStream = from url in _urlsToCheck
                              from data in _stringDownloader.Get(url)
                              select new Page(data, url, _pageParser.GetPageTitle(data));

            pagesStream.Subscribe(_mongoRepository);

            var linkStream = from page in pagesStream
                             from link in _pageParser.GetPageLinks(page)
                             where !IsLinkChecked(link)
                             select link;

            linkStream.Subscribe(_urlsToCheck);
            

            AddUrlToCrawl(startUrl);
        }

        private bool IsLinkChecked(string url)
        {
            if (_checkedUrls.Conains(url))
            {
                return true;
            }
            _checkedUrls.Add(url);
            Console.Out.WriteLine(url);
            return false;
        }

        private void AddUrlToCrawl(string url)
        {
            _checkedUrls.Add(url);
            _urlsToCheck.OnNext(url);
        }
    }
}