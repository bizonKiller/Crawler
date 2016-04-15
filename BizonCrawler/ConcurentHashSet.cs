using System.Collections.Generic;

namespace BizonCrawler
{
    public class ConcurentHashSet<T> // this could be done better like in http://stackoverflow.com/a/18923091/406531
    {
        private readonly HashSet<T> _hashSet = new HashSet<T>();
        private readonly object _locker = new object();

        public void Add(T value)
        {
            lock (_locker)
            {
                _hashSet.Add(value);
            }
        }

        public bool Conains(T value)
        {
            lock (_locker)
            {
                return _hashSet.Contains(value);
            }
        }
    }
}