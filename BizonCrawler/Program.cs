using System;
using System.Linq;
using Ninject;

namespace BizonCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernal = ConfiguraIoC();

            var crawler = kernal.Get<Crawler>();
            crawler.Run(args.Any() ? args[0] : "http://bizonsoftware.pl");

            Console.Out.WriteLine("Press enter to close application...");
            Console.ReadLine();
        }

        private static IKernel ConfiguraIoC()
        {
            IKernel kernal = new StandardKernel();
            kernal.Bind<IMongoRepository>().To<MongoRepository>();
            kernal.Bind<IStringDownloader>().To<StringDownloader>();
            kernal.Bind<IPageParser>().To<PageParser>();
            return kernal;
        }
    }
}
