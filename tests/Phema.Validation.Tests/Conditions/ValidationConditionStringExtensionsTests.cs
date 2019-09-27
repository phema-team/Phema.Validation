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
				.AddValidationDetail("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsEmpty_Valid()
		{
			validationContext.When("name", "john")
				.IsEmpty()
				.AddValidationDetail("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsNotEmpty()
		{
			var (key, message) = validationContext.When("name", "john")
				.IsNotEmpty()
				.AddValidationDetail("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotEmpty_Valid()
		{
			validationContext.When("name", string.Empty)
				.IsNotEmpty()
				.AddValidationDetail("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsNullOrWhitespace()
		{
			var (key, message) = validationContext.When("name", " ")
				.IsNullOrWhitespace()
				.AddValidationDetail("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNullOrWhitespace_Valid()
		{
			validationContext.When("name", "john")
				.IsNullOrWhitespace()
				.AddValidationDetail("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsNotNullOrWhitespace()
		{
			var (key, message) = validationContext.When("name", "john")
				.IsNotNullOrWhitespace()
				.AddValidationDetail("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotNullOrWhitespace_Valid()
		{
			validationContext.When("name", " ")
				.IsNotNullOrWhitespace()
				.AddValidationDetail("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}
		
		[Fact]
		public void IsWhitespace()
		{
			var (key, message) = validationContext.When("name", "   ")
				.IsWhitespace()
				.AddValidationDetail("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsWhitespace_Valid()
		{
			validationContext.When("name", (string)null)
				.IsWhitespace()
				.AddValidationDetail("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}
		
		[Fact]
		public void IsNotWhitespace()
		{
			var (key, message) = validationContext.When("name", (string)null)
				.IsNotWhitespace()
				.AddValidationDetail("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotWhitespace_Valid()
		{
			validationContext.When("name", "  ")
				.IsNotWhitespace()
				.AddValidationDetail("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsMatch()
		{
			var (key, message) = validationContext.When("name", "abc")
				.IsMatch("[a-c]+")
				.AddValidationDetail("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsMatch_Valid()
		{
			validationContext.When("name", "def")
				.IsMatch("[a-c]+")
				.AddValidationDetail("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsNotMatch()
		{
			var (key, message) = validationContext.When("name", "def")
				.IsNotMatch("[a-c]+")
				.AddValidationDetail("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotMatch_Valid()
		{
			validationContext.When("name", "abc")
				.IsNotMatch("[a-c]+")
				.AddValidationDetail("template1");

			Assert.True(!validationContext.ValidationDetails.Any());
		}

		[Fact]
		public void IsEmail()
		{
			var (key, message) = validationContext.When("email", "email@email.com")
				.IsEmail()
				.AddValidationDetail("template1");

			Assert.Equal("email", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsEmail_Valid()
		{
			validationContext.When("email", "email")
				.IsEmail()
				.AddValidationDetail("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsNotEmail()
		{
			var (key, message) = validationContext.When("email", "email")
				.IsNotEmail()
				.AddValidationDetail("template1");

			Assert.Equal("email", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotEmail_Valid()
		{
			validationContext.When("email", "email@email.com")
				.IsNotEmail()
				.AddValidationDetail("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsUrl()
		{
			var (key, message) = validationContext.When("url", "/orders/1")
				.IsUrl(UriKind.Relative)
				.AddValidationDetail("You should specify absolute url");

			Assert.Equal("url", key);
			Assert.Equal("You should specify absolute url", message);
		}

		[Fact]
		public void IsUrl_Valid()
		{
			validationContext.When("url", "https://www.example.com")
				.IsUrl(UriKind.Relative)
				.AddValidationDetail("You should specify absolute url");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsNotUrl()
		{
			var (key, message) = validationContext.When("url", "not a valid url")
				.IsNotUrl()
				.AddValidationDetail("template1");

			Assert.Equal("url", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotUrl_Valid()
		{
			validationContext.When("url", "https://www/example.com")
				.IsNotUrl()
				.AddValidationDetail("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void HasLength()
		{
			var (key, message) = validationContext.When("name", "john")
				.HasLength(4)
				.AddValidationDetail("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void HasLength_Valid()
		{
			validationContext.When("name", "john")
				.HasLength(5)
				.AddValidationDetail("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void HasLengthCallback()
		{
			var (key, message) = validationContext.When("name", "john")
				.HasLength(length => true)
				.AddValidationDetail("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void HasLengthCallback_Valid()
		{
			validationContext.When("name", "john")
				.HasLength(length => false)
				.AddValidationDetail("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void HasLengthLess()
		{
			var (key, message) = validationContext.When("name", "john")
				.HasLengthLess(5)
				.AddValidationDetail("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void HasLengthLess_Valid()
		{
			validationContext.When("name", "john")
				.HasLengthLess(3)
				.AddValidationDetail("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void HasLengthGreater()
		{
			var (key, message) = validationContext.When("name", "john")
				.HasLengthGreater(3)
				.AddValidationDetail("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void HasLengthGreater_Valid()
		{
			validationContext.When("name", "john")
				.HasLengthGreater(5)
				.AddValidationDetail("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}
	}
}