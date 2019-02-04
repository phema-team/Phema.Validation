using System;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationErrorTests
	{
		[Fact]
		public void Equal()
		{
			var error1 = new ValidationError("key", "template", ValidationSeverity.Error);
			var error2 = new ValidationError("key", "template", ValidationSeverity.Error);

			Assert.Equal(error1, error2);
			Assert.Equal(error1.GetHashCode(), error2.GetHashCode());
		}

		[Fact]
		public void NotEqual()
		{
			var error1 = new ValidationError("key1", "template", ValidationSeverity.Error);
			var error2 = new ValidationError("key2", "template", ValidationSeverity.Error);

			Assert.NotEqual(error1, error2);
			Assert.NotEqual(error1.GetHashCode(), error2.GetHashCode());
		}

		[Fact]
		public void KeyThrows()
		{
			Assert.Throws<ArgumentNullException>(() => new ValidationError(null, "template", ValidationSeverity.Error));
		}

		[Fact]
		public void MessageThrows()
		{
			Assert.Throws<ArgumentNullException>(() => new ValidationError("key", null, ValidationSeverity.Error));
		}

		[Fact]
		public void Deconstruct()
		{
			var (key, message) = new ValidationError("key", "template", ValidationSeverity.Error);

			Assert.Equal("key", key);
			Assert.Equal("template", message);
		}

		[Fact]
		public void DeconstructSeverity()
		{
			var (key, message, severity) = new ValidationError("key", "template", ValidationSeverity.Error);

			Assert.Equal("key", key);
			Assert.Equal("template", message);
			Assert.Equal(ValidationSeverity.Error, severity);
		}
	}
}