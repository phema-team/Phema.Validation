using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationKeyTests
	{
		[Fact]
		public void ValidationKey()
		{
			var key = new ValidationKey("key");

			Assert.Equal("key", key.Key);
		}

		[Fact]
		public void ValidationKeyEquality()
		{
			var key1 = new ValidationKey("key");
			var key2 = new ValidationKey("key");

			Assert.Equal(key1.Key, key2.Key);
			Assert.Equal(key1, key2);
			Assert.Equal(key1, key1);
			Assert.Equal(key1.GetHashCode(), key2.GetHashCode());
			Assert.True(key1.Equals(key2));
		}

		[Fact]
		public void ValidationKeyNotNull()
		{
			var key = new ValidationKey("key");

			Assert.False(key.Equals(null));
		}
	}
}