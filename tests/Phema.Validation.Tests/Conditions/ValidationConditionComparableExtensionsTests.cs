using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Phema.Validation.Conditions;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConditionComparableExtensionsTests
	{
		private readonly IValidationContext validationContext;

		public ValidationConditionComparableExtensionsTests()
		{
			validationContext = new ServiceCollection()
				.AddValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void IsGreater()
		{
			var (key, message) = validationContext.When("age", 11)
				.IsGreater(10)
				.AddValidationDetail("template1");

			Assert.Equal("age", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsGreater_Valid()
		{
			validationContext.When("age", 10)
				.IsGreater(10)
				.AddValidationDetail("template1");

			Assert.True(!validationContext.ValidationDetails.Any());
		}

		[Fact]
		public void IsGreaterOrEqual()
		{
			var (key, message) = validationContext.When("age", 10)
				.IsGreaterOrEqual(10)
				.AddValidationDetail("template1");

			Assert.Equal("age", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsGreaterOrEqual_Valid()
		{
			validationContext.When("age", 9)
				.IsGreaterOrEqual(10)
				.AddValidationDetail("template1");

			Assert.True(!validationContext.ValidationDetails.Any());
		}

		[Fact]
		public void IsLess()
		{
			var (key, message) = validationContext.When("age", 11)
				.IsLess(12)
				.AddValidationDetail("template1");

			Assert.Equal("age", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsLess_Valid()
		{
			validationContext.When("age", 10)
				.IsLess(10)
				.AddValidationDetail("template1");

			Assert.True(!validationContext.ValidationDetails.Any());
		}

		[Fact]
		public void IsLessOrEqual()
		{
			var (key, message) = validationContext.When("age", 12)
				.IsLessOrEqual(12)
				.AddValidationDetail("template1");

			Assert.Equal("age", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsLessOrEqual_Valid()
		{
			validationContext.When("age", 11)
				.IsLessOrEqual(10)
				.AddValidationDetail("template1");

			Assert.True(!validationContext.ValidationDetails.Any());
		}

		[Fact]
		public void IsInRange()
		{
			var (key, message) = validationContext.When("age", 11)
				.IsInRange(10, 12)
				.AddValidationDetail("template1");

			Assert.Equal("age", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsInRange_Less_Valid()
		{
			validationContext.When("age", 9)
				.IsInRange(10, 12)
				.AddValidationDetail("template1");

			Assert.True(!validationContext.ValidationDetails.Any());
		}

		[Fact]
		public void IsInRange_Greater_Valid()
		{
			validationContext.When("age", 13)
				.IsInRange(10, 12)
				.AddValidationDetail("template1");

			Assert.True(!validationContext.ValidationDetails.Any());
		}

		[Fact]
		public void IsNotInRange_Valid()
		{
			validationContext.When("age", 11)
				.IsNotInRange(10, 12)
				.AddValidationDetail("template1");

			validationContext.EnsureIsValid();
		}

		[Fact]
		public void IsNotInRange_Less_Invalid()
		{
			validationContext.When("age", 9)
				.IsNotInRange(10, 12)
				.AddValidationDetail("template1");

			Assert.Single(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsNotInRange_Greater_Invalid()
		{
			validationContext.When("age", 13)
				.IsNotInRange(10, 12)
				.AddValidationDetail("template1");

			Assert.Single(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsLess_DateTime()
		{
			validationContext.When("age", new DateTime(2011, 11, 11))
				.IsLess(new DateTime(2012, 12, 12))
				.AddValidationDetail("template1");

			Assert.Single(validationContext.ValidationDetails);
		}
	}
}