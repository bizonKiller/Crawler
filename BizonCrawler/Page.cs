using System;
using System.Threading;

namespace BizonCrawler
{
    public struct Page
    {
        private readonly string _html;

        public string GetHtml()
        {
            return _html;
        }

        public string Url { get; set; }
        public string Title { get; set; }
        public DateTime TimeStamp { get; set; }
        public int WorkerThreadId { get; set; }

        public Page(string html, string url, string title) : this()
        {
            _html          = html;
            Url            = url;
            Title          = title;
            TimeStamp      = DateTime.Now;
            WorkerThreadId = Thread.CurrentThread.ManagedThreadId;
        }
    }
}