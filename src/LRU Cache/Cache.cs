using System;
using System.Collections.Generic;
using System.Linq;
using LRU_Cache;

namespace LRUCache
{
	public class Cache<K,V>
	{
		private readonly int _capacity;
		private readonly LinkedList<CacheItem<K,V>> _lruList = new LinkedList<CacheItem<K, V>>();
		private readonly Dictionary<K, LinkedListNode<CacheItem<K, V>>> _map =
			new Dictionary<K, LinkedListNode<CacheItem<K, V>>>();

		public Cache(int capacity)
		{
			if (capacity < 1)
			{
				throw new ArgumentException("Capacity must be greater than zero.");
			}
			_capacity = capacity;
		}

		/// <summary>
		/// Search the cache for the value associated with a given key.
		/// </summary>
		/// <param name="key">The key to search for in the cache.</param>
		/// <returns>The value associated with the given key, or the default value for the
		/// value's type if the key cannot be found.</returns>
		public V Get(K key)
		{
			LinkedListNode<CacheItem<K,V>> node;
			if (_map.TryGetValue(key, out node))
			{
				// Put this node at the end of the LRU list.
				_lruList.Remove(node);
				_lruList.AddLast(node);

				return node.Value.Value;
			}
			return default(V);
		}

		/// <summary>
		/// Insert the value if the key is not already present. Overwrite the value if the key is present.
		/// </summary>
		/// <param name="key">The key</param>
		/// <param name="value">The value</param>
		public void Set(K key, V value)
		{
			LinkedListNode<CacheItem<K, V>> node;
			if (_map.TryGetValue(key, out node))
			{
				node.Value.Value = value;
				_lruList.Remove(node);
			}
			else
			{
				if (_capacity == _lruList.Count)
				{
					var lastNode = _lruList.First;
					_map.Remove(lastNode.Value.Key);
					_lruList.RemoveFirst();
				}
				node = new LinkedListNode<CacheItem<K, V>>(new CacheItem<K, V>(key,value));
				_map.Add(key, node);
			}

			// Put the node at the end of the LRU list.
			_lruList.AddLast(node);
		}
	}
}
