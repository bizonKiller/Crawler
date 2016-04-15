using System;
using Ninject;

namespace BizonCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            IKernel kernal = new StandardKernel();
            kernal.Bind<IMongoRepository>().To<MongoRepository>();
            kernal.Bind<IStringDownloader>().To<StringDownloader>();
            kernal.Bind<IPageParser>().To<PageParser>();

            var crawler = kernal.Get<Crawler>();
            crawler.Run("http://bizonsoftware.pl");

            Console.Out.WriteLine("Press enter to close application...");
            Console.ReadLine();
        }
    }
}
