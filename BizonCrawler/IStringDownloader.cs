using System;

namespace BizonCrawler
{
    public interface IStringDownloader
    {
        IObservable<string> Get(string url);
    }
}