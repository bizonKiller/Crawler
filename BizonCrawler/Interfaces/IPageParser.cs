using System;

namespace BizonCrawler
{
    public interface IPageParser
    {
        string GetPageTitle(string html);
        IObservable<string> GetPageLinks(Page page);
    }
}