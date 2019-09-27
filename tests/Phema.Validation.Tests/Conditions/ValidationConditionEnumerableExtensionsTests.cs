using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Phema.Validation.Conditions;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConditionEnumerableExtensionsTests
	{
		private readonly IValidationContext validationContext;

		public ValidationConditionEnumerableExtensionsTests()
		{
			validationContext = new ServiceCollection()
				.AddValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void IsAny()
		{
			var (key, message) = validationContext.When("key", new[] {1})
				.IsAny()
				.AddValidationDetail("template1");

			Assert.Equal("key", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsAny_Valid()
		{
			validationContext.When("key", Array.Empty<int>())
				.IsAny()
				.AddValidationDetail("template1");

			Assert.False(validationContext.ValidationDetails.Any());
		}

		[Fact]
		public void IsAnyWithParameter()
		{
			var (key, message) = validationContext.When("key", new[] {1})
				.IsAny(x => x == 1)
				.AddValidationDetail("template1");

			Assert.Equal("key", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsAnyWithParameter_Valid()
		{
			validationContext.When("key", new[] {1})
				.IsAny(x => x == 2)
				.AddValidationDetail("template1");

			Assert.False(validationContext.ValidationDetails.Any());
		}

		[Fact]
		public void IsNotAny()
		{
			var (key, message) = validationContext.When("key", Array.Empty<int>())
				.IsNotAny()
				.AddValidationDetail("template1");

			Assert.Equal("key", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotAny_Valid()
		{
			validationContext.When("key", new[] {1})
				.IsNotAny()
				.AddValidationDetail("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsNotAnyWithParameter()
		{
			var (key, message) = validationContext.When("key", new[] {1})
				// When no any 2s in array
				.IsNotAny(x => x == 2)
				.AddValidationDetail("template1");

			Assert.Equal("key", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotAnyWithParameter_Valid()
		{
			validationContext.When("key", new[] {1})
				.IsNotAny(x => x == 1)
				.AddValidationDetail("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsAll()
		{
			var (key, message) = validationContext.When("key", new[] {1})
				.IsAll(x => x == 1)
				.AddValidationDetail("template1");

			Assert.Equal("key", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsAll_Valid()
		{
			validationContext.When("key", new[] {1, 2})
				.IsAll(x => x == 1)
				.AddValidationDetail("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsNotAll()
		{
			var (key, message) = validationContext.When("key", new[] {1, 2})
				// When array has element != 1
				.IsNotAll(x => x == 1)
				.AddValidationDetail("template1");

			Assert.Equal("key", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotAll_Valid()
		{
			validationContext.When("key", new[] {1})
				.IsNotAll(x => x == 1)
				.AddValidationDetail("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}
	}
}