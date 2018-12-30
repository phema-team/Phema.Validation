using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConditionExtensions
	{
		private readonly IValidationContext validationContext;

		public ValidationConditionExtensions()
		{
			validationContext = new ServiceCollection()
				.AddPhemaValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void Is()
		{
			var error = validationContext.When("age", 10)
				.Is(() => true)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}

		[Fact]
		public void IsNot()
		{
			var error = validationContext.When("age", 10)
				.IsNot(value => value == 5)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}

		[Fact]
		public void IsNot_Valid()
		{
			validationContext.When("age", 10)
				.IsNot(value => value == 10)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}

		[Fact]
		public void IsNot_Empty()
		{
			var error = validationContext.When("age", 10)
				.IsNot(() => false)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("age", error.Key);
			Assert.Equal("template", error.Message);
		}

		[Fact]
		public void IsNot_Valid_Empty()
		{
			validationContext.When("age", 10)
				.IsNot(() => true)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}

		[Fact]
		public void IsNull()
		{
			var error = validationContext.When((ValidationKey)"name", (string)null)
				.IsNull()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}

		[Fact]
		public void IsNull_Valid()
		{
			validationContext.When((ValidationKey)"name", "")
				.IsNull()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}

		[Fact]
		public void IsNotNull()
		{
			var error = validationContext.When((ValidationKey)"name", "")
				.IsNotNull()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}

		[Fact]
		public void IsNotNull_Valid()
		{
			validationContext.When((ValidationKey)"name", (string)null)
				.IsNotNull()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}

		[Fact]
		public void IsEqual()
		{
			var error = validationContext.When((ValidationKey)"name", "john")
				.IsEqual("john")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}

		[Fact]
		public void IsEqual_Valid()
		{
			validationContext.When((ValidationKey)"name", "john")
				.IsEqual("notjohn")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}

		[Fact]
		public void IsEqualNull()
		{
			var error = validationContext.When((ValidationKey)"name", (string)null)
				.IsEqual(null)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}

		[Fact]
		public void IsNotEqual()
		{
			var error = validationContext.When((ValidationKey)"name", "john")
				.IsNotEqual("notjohn")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}

		[Fact]
		public void IsNotEqual_Valid()
		{
			validationContext.When((ValidationKey)"name", "john")
				.IsNotEqual("john")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(!validationContext.Errors.Any());
		}

		[Fact]
		public void IsNotEqualNull()
		{
			var error = validationContext.When((ValidationKey)"name", (string)null)
				.IsNotEqual("john")
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
	}
}