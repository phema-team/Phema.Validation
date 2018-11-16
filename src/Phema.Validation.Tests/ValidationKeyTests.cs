using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationKeyTests
	{
		[Fact]
		public void ValidationKey()
		{
			var key = (ValidationKey)"key";

			Assert.Equal("key", key.Key);
		}

		[Fact]
		public void ValidationKeyEquality()
		{
			var key1 = (ValidationKey)"key";
			var key2 = (ValidationKey)"key";

			Assert.Equal(key1.Key, key2.Key);
			Assert.Equal(key1, key2);
			Assert.Equal(key1, key1);
			Assert.Equal(key1.GetHashCode(), key2.GetHashCode());
			Assert.True(key1.Equals(key2));
		}

		[Fact]
		public void ValidationKeyNotNull()
		{
			var key = (ValidationKey)"key";

			Assert.False(key.Equals(null));
		}

		[Fact]
		public void ExpressionValidationKey()
		{
			var key = (ExpressionValidationKey<string, int>)(Expression<Func<string, int>>)(s => s.Length);

			Assert.Equal("Length", key.Key);
		}

		[Fact]
		public void NullExpressionValidationKey()
		{
			var exception = Assert.Throws<ArgumentNullException>(
				() => (ExpressionValidationKey<string, int>)(Expression<Func<string, int>>)(null));

			Assert.Equal("expression", exception.ParamName);
		}

		[Fact]
		public void ExpressionValidationKey_NotMemberExpression()
		{
			Assert.Throws<KeyNotFoundException>(
				() => (ExpressionValidationKey<string, int>)(Expression<Func<string, int>>)(s => s.GetHashCode()));
		}
	}
}
