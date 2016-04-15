using System;

namespace BizonCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            new Crawler().Run("http://wykop.pl");

            Console.Out.WriteLine("Press enter to close application...");
            Console.ReadLine();
        }
    }
}
