using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConditionStringExtensionsTests
	{
		private readonly IValidationContext validationContext;

		public ValidationConditionStringExtensionsTests()
		{
			validationContext = new ServiceCollection()
				.AddPhemaValidation(configuration =>
					configuration.AddComponent<TestModelValidationComponent>())
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void IsEmpty()
		{
			var (key, message) = validationContext.When("name", string.Empty)
				.IsEmpty()
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsEmpty_Valid()
		{
			validationContext.When("name", "john")
				.IsEmpty()
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Empty(validationContext.Errors);
		}

		[Fact]
		public void IsNotEmpty()
		{
			var (key, message) = validationContext.When("name", "john")
				.IsNotEmpty()
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotEmpty_Valid()
		{
			validationContext.When("name", string.Empty)
				.IsNotEmpty()
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Empty(validationContext.Errors);
		}

		[Fact]
		public void IsNullOrWhitespace()
		{
			var error = validationContext.When("name", " ")
				.IsNullOrWhitespace()
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("name", error.Key);
			Assert.Equal("template1", error.Message);
		}

		[Fact]
		public void IsNullOrWhitespace_Valid()
		{
			validationContext.When("name", "john")
				.IsNullOrWhitespace()
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Empty(validationContext.Errors);
		}

		[Fact]
		public void IsMatch()
		{
			var (key, message) = validationContext.When("name", "abc")
				.IsMatch("[a-c]+")
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsMatch_Valid()
		{
			validationContext.When("name", "def")
				.IsMatch("[a-c]+")
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Empty(validationContext.Errors);
		}

		[Fact]
		public void IsNotMatch()
		{
			var (key, message) = validationContext.When("name", "def")
				.IsNotMatch("[a-c]+")
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotMatch_Valid()
		{
			validationContext.When("name", "abc")
				.IsNotMatch("[a-c]+")
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.True(!validationContext.Errors.Any());
		}

		[Fact]
		public void IsNotEmail()
		{
			var (key, message) = validationContext.When("email", "email")
				.IsNotEmail()
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("email", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotEmail_Valid()
		{
			validationContext.When("email", "email@email.com")
				.IsNotEmail()
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.True(!validationContext.Errors.Any());
		}

		[Fact]
		public void HasLength()
		{
			var (key, message) = validationContext.When("name", "john")
				.HasLength(4)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void HasLength_Valid()
		{
			validationContext.When("name", "john")
				.HasLength(5)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Empty(validationContext.Errors);
		}
		
		[Fact]
		public void HasLengthLess()
		{
			var (key, message) = validationContext.When("name", "john")
				.HasLengthLess(5)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void HasLengthLess_Valid()
		{
			validationContext.When("name", "john")
				.HasLengthLess(3)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Empty(validationContext.Errors);
		}
		
		[Fact]
		public void HasLengthGreater()
		{
			var (key, message) = validationContext.When("name", "john")
				.HasLengthGreater(3)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void HasLengthGreater_Valid()
		{
			validationContext.When("name", "john")
				.HasLengthGreater(5)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Empty(validationContext.Errors);
		}
	}
}