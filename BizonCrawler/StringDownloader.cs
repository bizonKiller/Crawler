using System;
using System.Net;
using System.Reactive.Linq;

namespace BizonCrawler
{
    public class StringDownloader : IStringDownloader
    {
        public IObservable<string> Get(string url)
        {
            return Observable.FromAsync(() => new WebClient().DownloadStringTaskAsync(url));
        }
    }
}