using System;
using System.Linq;
using System.Reactive.Linq;
using System.Text.RegularExpressions;

namespace BizonCrawler
{
    public class PageParser
    {
        private static readonly Regex _linkParser = new Regex(@"<a\s*.*href=\""([^\""]+)\""", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex _titleParser = new Regex(@"<title>\s*(.+?)\s*</title>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
 
        public string GetPageTitle(string html)
        {
            var match = _titleParser.Match(html);
            return match.Success ? match.Groups[1].Value : "";
        }

        public IObservable<string> GetPageLinks(Page page)
        {
            string host = null;

            return _linkParser.Matches(page.GetHtml())
                              .Cast<Match>()
                              .Select(m => GetNextUrl(m, page, ref host))
                              .ToObservable();
        }

        private string GetNextUrl(Match m, Page data, ref string host)
        {
            var nextUrl = m.Groups[1].Value;

            if (!nextUrl.StartsWith("http", StringComparison.InvariantCultureIgnoreCase))
            {
                if (host == null)
                {
                    var uri = new Uri(data.Url);
                    host = uri.Scheme + "://" + uri.Host + ":" + uri.Port;
                }
                if (nextUrl[0] == '/')
                {
                    nextUrl = host + nextUrl;
                }
                else
                {
                    nextUrl = host + '/' + nextUrl;
                }
            }
            return nextUrl;
        }
    }
}