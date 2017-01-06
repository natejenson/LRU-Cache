namespace LRU_Cache
{
	public class CacheItem<K,V>
	{
		public K Key { get; }
		public V Value { get; set; }

		public CacheItem(K key, V value)
		{
			this.Key = key;
			this.Value = value;
		}
	}
}
