using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Phema.Validation.Conditions;

namespace Phema.Validation.Tests
{
	public class ValidationPredicateCollectionExtensionsTests
	{
		private readonly IValidationContext validationContext;

		public ValidationPredicateCollectionExtensionsTests()
		{
			validationContext = new ServiceCollection()
				.AddValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void IsEmpty()
		{
			var message = validationContext.When("key", Array.Empty<int>())
				.IsEmpty()
				.AddError("template1");

			Assert.NotNull(message);
			Assert.Equal("key", message.ValidationKey);
			Assert.Equal("template1", message.ValidationMessage);
		}

		[Fact]
		public void PropertyIsEmpty()
		{
			var (key, message) = validationContext.When("list", new List<int>())
				.IsEmpty()
				.AddError("template1");

			Assert.Equal("list", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotEmpty()
		{
			var (key, message) = validationContext.When("list", new[] { 1 })
				.IsNotEmpty()
				.AddError("template1");

			Assert.Equal("list", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void PropertyIsNotEmpty()
		{
			var (key, message) = validationContext.When("list", new List<int> { 1 })
				.IsNotEmpty()
				.AddError("template1");

			Assert.Equal("list", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void HasCount()
		{
			var (key, message) = validationContext.When("list", new[] { 1 })
				.HasCount(1)
				.AddError("template1");

			Assert.Equal("list", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void PropertyHasCount()
		{
			var (key, message) = validationContext.When("list", new List<int> { 1 })
				.HasCount(1)
				.AddError("template1");

			Assert.Equal("list", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void NotHasCount()
		{
			var (key, message) = validationContext.When("list", new[] { 1 })
				.NotHasCount(2)
				.AddError("template1");

			Assert.Equal("list", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void PropertyNotHasCount()
		{
			var (key, message) = validationContext.When("list", new List<int> { 1 })
				.NotHasCount(2)
				.AddError("template1");
			

			Assert.Equal("list", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsContains()
		{
			var (key, message) = validationContext.When("list", new[] { 1 })
				.IsContains(1)
				.AddError("template1");

			Assert.Equal("list", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void PropertyIsContains()
		{
			var (key, message) = validationContext.When("list", new List<int> { 1 })
				.IsContains(1)
				.AddError("template1");

			Assert.Equal("list", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotContains()
		{
			var (key, message) = validationContext.When("list", new[] { "item1" })
				.IsNotContains("item2")
				.AddError("template1");

			Assert.Equal("list", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void PropertyIsNotContains()
		{
			var (key, message) = validationContext.When("list", new List<int>())
				.IsNotContains(1)
				.AddError("template1");

			Assert.Equal("list", key);
			Assert.Equal("template1", message);
		}
	}
}