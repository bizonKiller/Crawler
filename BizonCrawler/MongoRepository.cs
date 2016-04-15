using System;
using MongoDB.Driver;

namespace BizonCrawler
{
    public class MongoRepository : IObserver<Page>
    {
        private readonly IMongoCollection<Page> _collection;

        public MongoRepository()
        {
            var client = new MongoClient();
            var database = client.GetDatabase("BizonCrawler");
            _collection = database.GetCollection<Page>("crawledData");
        }

        public void OnNext(Page page)
        {
            _collection.InsertOneAsync(page);
        }

        public void OnError(Exception error)
        {
        }

        public void OnCompleted()
        {
        }
    }
}