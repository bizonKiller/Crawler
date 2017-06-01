using System;
using System.Net;
using System.Reactive.Linq;

namespace BizonCrawler
{
    public class StringDownloader : IStringDownloader
    {
        public IObservable<string> Get(string url)
        {
            return Observable.FromAsync(() =>
            {
                try
                {
                    return new WebClient().DownloadStringTaskAsync(url);
                }
                catch (Exception e)
                {
                    // ingore exception move on... 
                    Console.WriteLine(e);
                }
                return null;
            });
        }
    }
}