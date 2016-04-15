using System;
using System.Net;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace BizonCrawler
{
    public class StringDownloader : IStringDownloader
    {
        public IObservable<string> Get(string url)
        {
            return Observable.FromAsync(
            () => new WebClient().DownloadStringTaskAsync(url));
        }
    }

    public class Crawler
    {
        private Subject<string> _urlsToCheck;
        private ConcurentHashSet<string> _checkedUrls;
        private StringDownloader _stringDownloader;

        public void Run(string startUrl)
        {
            _urlsToCheck = new Subject<string>();
            _checkedUrls = new ConcurentHashSet<string>();

            var pageParser = new PageParser();
            var mongoRepository = new MongoRepository();
            _stringDownloader = new StringDownloader();

            var pagesStream = from url in _urlsToCheck
                              from data in _stringDownloader.Get(url)
                              select new Page(data, url, pageParser.GetPageTitle(data));

            pagesStream.Subscribe(mongoRepository);

            var linkStream = from page in pagesStream
                             from link in pageParser.GetPageLinks(page)
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