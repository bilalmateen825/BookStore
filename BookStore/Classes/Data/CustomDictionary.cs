using System.Collections.Generic;

namespace BookStore.Classes.Data
{
    public class CustomDictionary<K, T> : Dictionary<K, T>, IDisposable where T : IKey<K>
    {
        public CustomDictionary()
        {

        }

        public T FindByKey(K key)
        {
            T item = default(T);

            if (base.TryGetValue(key, out item))
                return item;

            return default(T);
        }

        public bool Remove(K key)
        {
            return base.Remove(key);
        }

        public T this[K item]
        {
            get
            {
                return base[item];
            }
        }

        public void Dispose()
        {
            base.Clear();
        }
    }
}
