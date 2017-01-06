using System;
using Xunit;
using LRUCache;

namespace LRUCache.Tests
{
	public class CacheTests
	{
		[Fact]
		public void LRUCache_GivenNoCapacity_ThrowsException()
		{
			Assert.Throws(typeof (ArgumentException), () => new Cache<int,int>(0));
		}

		[Fact]
		public void Get_GivenNewKey_ReturnsDefault()
		{
			var cache = new Cache<int, string>(10);
			var val = cache.Get(1);
			Assert.Equal(val, default(string));
		}

		[Fact]
		public void Get_GivenExistingKey_ReturnsGivenValue()
		{
			var cache = new Cache<int, string>(10);
			var expectedVal = "natejenson";
			cache.Set(1, expectedVal);
			var val = cache.Get(1);
			Assert.Equal(expectedVal, val);
		}

		[Fact]
		public void Set_GivenExistingKey_ReplacesExistingValue()
		{
			var cache = new Cache<int, string>(10);
			cache.Set(1, "first");
			cache.Set(1, "second");
			Assert.Equal("second", cache.Get(1));
		}

		[Fact]
		public void Set_GivenNewKeyFullCache_AddsValueAndRemovesOldest()
		{
			var cache = new Cache<int, string>(1);
			cache.Set(1, "first");
			cache.Set(2, "second");
			Assert.Equal(default(string), cache.Get(1));
			Assert.Equal("second", cache.Get(2));
		}
	}
}
