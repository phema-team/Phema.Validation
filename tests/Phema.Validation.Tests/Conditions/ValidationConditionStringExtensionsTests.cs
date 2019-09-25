using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Phema.Validation.Conditions;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConditionStringExtensionsTests
	{
		private readonly IValidationContext validationContext;

		public ValidationConditionStringExtensionsTests()
		{
			validationContext = new ServiceCollection()
				.AddValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void IsEmpty()
		{
			var (key, message) = validationContext.When("name", string.Empty)
				.IsEmpty()
				.AddValidationError("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsEmpty_Valid()
		{
			validationContext.When("name", "john")
				.IsEmpty()
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsNotEmpty()
		{
			var (key, message) = validationContext.When("name", "john")
				.IsNotEmpty()
				.AddValidationError("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotEmpty_Valid()
		{
			validationContext.When("name", string.Empty)
				.IsNotEmpty()
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsNullOrWhitespace()
		{
			var (key, message) = validationContext.When("name", " ")
				.IsNullOrWhitespace()
				.AddValidationError("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNullOrWhitespace_Valid()
		{
			validationContext.When("name", "john")
				.IsNullOrWhitespace()
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsNotNullOrWhitespace()
		{
			var (key, message) = validationContext.When("name", "john")
				.IsNotNullOrWhitespace()
				.AddValidationError("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotNullOrWhitespace_Valid()
		{
			validationContext.When("name", " ")
				.IsNotNullOrWhitespace()
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}
		
		[Fact]
		public void IsWhitespace()
		{
			var (key, message) = validationContext.When("name", "   ")
				.IsWhitespace()
				.AddValidationError("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsWhitespace_Valid()
		{
			validationContext.When("name", (string)null)
				.IsWhitespace()
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}
		
		[Fact]
		public void IsNotWhitespace()
		{
			var (key, message) = validationContext.When("name", (string)null)
				.IsNotWhitespace()
				.AddValidationError("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotWhitespace_Valid()
		{
			validationContext.When("name", "  ")
				.IsNotWhitespace()
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsMatch()
		{
			var (key, message) = validationContext.When("name", "abc")
				.IsMatch("[a-c]+")
				.AddValidationError("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsMatch_Valid()
		{
			validationContext.When("name", "def")
				.IsMatch("[a-c]+")
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsNotMatch()
		{
			var (key, message) = validationContext.When("name", "def")
				.IsNotMatch("[a-c]+")
				.AddValidationError("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotMatch_Valid()
		{
			validationContext.When("name", "abc")
				.IsNotMatch("[a-c]+")
				.AddValidationError("template1");

			Assert.True(!validationContext.ValidationDetails.Any());
		}

		[Fact]
		public void IsEmail()
		{
			var (key, message) = validationContext.When("email", "email@email.com")
				.IsEmail()
				.AddValidationError("template1");

			Assert.Equal("email", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsEmail_Valid()
		{
			validationContext.When("email", "email")
				.IsEmail()
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsNotEmail()
		{
			var (key, message) = validationContext.When("email", "email")
				.IsNotEmail()
				.AddValidationError("template1");

			Assert.Equal("email", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotEmail_Valid()
		{
			validationContext.When("email", "email@email.com")
				.IsNotEmail()
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsUrl()
		{
			var (key, message) = validationContext.When("url", "/orders/1")
				.IsUrl(UriKind.Relative)
				.AddValidationError("You should specify absolute url");

			Assert.Equal("url", key);
			Assert.Equal("You should specify absolute url", message);
		}

		[Fact]
		public void IsUrl_Valid()
		{
			validationContext.When("url", "https://www.example.com")
				.IsUrl(UriKind.Relative)
				.AddValidationError("You should specify absolute url");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsNotUrl()
		{
			var (key, message) = validationContext.When("url", "not a valid url")
				.IsNotUrl()
				.AddValidationError("template1");

			Assert.Equal("url", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotUrl_Valid()
		{
			validationContext.When("url", "https://www/example.com")
				.IsNotUrl()
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void HasLength()
		{
			var (key, message) = validationContext.When("name", "john")
				.HasLength(4)
				.AddValidationError("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void HasLength_Valid()
		{
			validationContext.When("name", "john")
				.HasLength(5)
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void HasLengthCallback()
		{
			var (key, message) = validationContext.When("name", "john")
				.HasLength(length => true)
				.AddValidationError("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void HasLengthCallback_Valid()
		{
			validationContext.When("name", "john")
				.HasLength(length => false)
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void HasLengthLess()
		{
			var (key, message) = validationContext.When("name", "john")
				.HasLengthLess(5)
				.AddValidationError("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void HasLengthLess_Valid()
		{
			validationContext.When("name", "john")
				.HasLengthLess(3)
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void HasLengthGreater()
		{
			var (key, message) = validationContext.When("name", "john")
				.HasLengthGreater(3)
				.AddValidationError("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void HasLengthGreater_Valid()
		{
			validationContext.When("name", "john")
				.HasLengthGreater(5)
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}
	}
}