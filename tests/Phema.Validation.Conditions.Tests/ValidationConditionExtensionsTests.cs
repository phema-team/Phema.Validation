using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConditionExtensionsTests
	{
		private readonly IValidationContext validationContext;

		public ValidationConditionExtensionsTests()
		{
			validationContext = new ServiceCollection()
				.AddPhemaValidation(configuration =>
					configuration.AddValidationComponent<TestModel, TestModelValidation, TestModelValidationComponent>())
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void Is()
		{
			var (key, message) = validationContext.When("age", 10)
				.Is(() => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("age", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNot()
		{
			var (key, message) = validationContext.When("age", 10)
				.IsNot(value => value == 5)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("age", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNot_Valid()
		{
			validationContext.When("age", 10)
				.IsNot(value => value == 10)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Empty(validationContext.Errors);
		}

		[Fact]
		public void IsNot_Empty()
		{
			var (key, message) = validationContext.When("age", 10)
				.IsNot(() => false)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("age", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNot_Valid_Empty()
		{
			validationContext.When("age", 10)
				.IsNot(() => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Empty(validationContext.Errors);
		}

		[Fact]
		public void IsNull()
		{
			var (key, message) = validationContext.When("name", (string)null)
				.IsNull()
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNull_Valid()
		{
			validationContext.When("name", "")
				.IsNull()
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Empty(validationContext.Errors);
		}

		[Fact]
		public void IsNotNull()
		{
			var (key, message) = validationContext.When("name", "")
				.IsNotNull()
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotNull_Valid()
		{
			validationContext.When("name", (string)null)
				.IsNotNull()
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Empty(validationContext.Errors);
		}

		[Fact]
		public void IsEqual()
		{
			var (key, message) = validationContext.When("name", "john")
				.IsEqual("john")
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsEqual_Valid()
		{
			validationContext.When("name", "john")
				.IsEqual("notjohn")
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Empty(validationContext.Errors);
		}

		[Fact]
		public void IsEqualNull()
		{
			var (key, message) = validationContext.When("name", (string)null)
				.IsEqual(null)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotEqual()
		{
			var (key, message) = validationContext.When("name", "john")
				.IsNotEqual("notjohn")
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotEqual_Valid()
		{
			validationContext.When("name", "john")
				.IsNotEqual("john")
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Empty(validationContext.Errors);
		}

		[Fact]
		public void IsNotEqualNull()
		{
			var (key, message) = validationContext.When("name", (string)null)
				.IsNotEqual("john")
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}
	}
}