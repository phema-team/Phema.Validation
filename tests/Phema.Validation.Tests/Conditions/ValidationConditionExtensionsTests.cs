using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Phema.Validation.Conditions;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConditionExtensionsTests
	{
		private readonly IValidationContext validationContext;

		public ValidationConditionExtensionsTests()
		{
			validationContext = new ServiceCollection()
				.AddValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void Is()
		{
			var (key, message) = validationContext.When("age", 10)
				.Is(() => true)
				.AddValidationError("template1");

			Assert.Equal("age", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNot()
		{
			var (key, message) = validationContext.When("age", 10)
				.IsNot(value => value == 5)
				.AddValidationError("template1");

			Assert.Equal("age", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNot_Valid()
		{
			validationContext.When("age", 10)
				.IsNot(value => value == 10)
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsDefault()
		{
			var (key, message) = validationContext.When("age", 0)
				.IsDefault()
				.AddValidationError("template1");

			Assert.Equal("age", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsDefault_Valid()
		{
			validationContext.When("age", 10)
				.IsDefault()
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}
		
		[Fact]
		public void IsNotDefault()
		{
			var (key, message) = validationContext.When("age", 10)
				.IsNotDefault()
				.AddValidationError("template1");

			Assert.Equal("age", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotDefault_Valid()
		{
			validationContext.When("age", 0)
				.IsNotDefault()
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsNot_Empty()
		{
			var (key, message) = validationContext.When("age", 10)
				.IsNot(() => false)
				.AddValidationError("template1");

			Assert.Equal("age", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNot_Valid_Empty()
		{
			validationContext.When("age", 10)
				.IsNot(() => true)
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsNull()
		{
			var (key, message) = validationContext.When("name", (string) null)
				.IsNull()
				.AddValidationError("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNull_Valid()
		{
			validationContext.When("name", "")
				.IsNull()
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsNotNull()
		{
			var (key, message) = validationContext.When("name", "")
				.IsNotNull()
				.AddValidationError("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotNull_Valid()
		{
			validationContext.When("name", (string) null)
				.IsNotNull()
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsIn()
		{
			var (key, message) = validationContext.When("name", 2)
				.IsIn(1, 2, 3)
				.AddValidationError("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);

			(key, message) = validationContext.When("name", 2)
				.IsIn(new List<int> { 1, 2, 3 })
				.AddValidationError("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsIn_Valid()
		{
			validationContext.When("name", 2)
				.IsIn(1, 3)
				.AddValidationError("template1");

			validationContext.When("name", 2)
				.IsIn(new List<int> { 1, 3 })
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsNotIn()
		{
			var (key, message) = validationContext.When("name", 2)
				.IsNotIn(1, 3)
				.AddValidationError("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
			
			(key, message) = validationContext.When("name", 2)
				.IsNotIn(new List<int> { 1, 3 })
				.AddValidationError("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotIn_Valid()
		{
			validationContext.When("name", 2)
				.IsNotIn(1, 2, 3)
				.AddValidationError("template1");

			validationContext.When("name", 2)
				.IsNotIn(new List<int> { 1, 2, 3 })
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsEqual()
		{
			var (key, message) = validationContext.When("name", "john")
				.IsEqual("john")
				.AddValidationError("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsEqual_Valid()
		{
			validationContext.When("name", "john")
				.IsEqual("notjohn")
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsEqualNull()
		{
			var (key, message) = validationContext.When("name", (string) null)
				.IsEqual(null)
				.AddValidationError("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotEqual()
		{
			var (key, message) = validationContext.When("name", "john")
				.IsNotEqual("notjohn")
				.AddValidationError("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotEqual_Valid()
		{
			validationContext.When("name", "john")
				.IsNotEqual("john")
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsNotEqualNull()
		{
			var (key, message) = validationContext.When("name", (string) null)
				.IsNotEqual("john")
				.AddValidationError("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}
	}
}