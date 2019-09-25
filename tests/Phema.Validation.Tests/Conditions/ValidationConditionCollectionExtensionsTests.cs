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
			var validationDetail = validationContext.When("key", Array.Empty<int>())
				.IsEmpty()
				.AddValidationError("template1");

			Assert.NotNull(validationDetail);
			Assert.Equal("key", validationDetail.ValidationKey);
			Assert.Equal("template1", validationDetail.ValidationMessage);
		}

		[Fact]
		public void PropertyIsEmpty()
		{
			var (key, message) = validationContext.When("list", new List<int>())
				.IsEmpty()
				.AddValidationError("template1");

			Assert.Equal("list", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotEmpty()
		{
			var (key, message) = validationContext.When("list", new[] {1})
				.IsNotEmpty()
				.AddValidationError("template1");

			Assert.Equal("list", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void PropertyIsNotEmpty()
		{
			var (key, message) = validationContext.When("list", new List<int> {1})
				.IsNotEmpty()
				.AddValidationError("template1");

			Assert.Equal("list", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void HasCount()
		{
			var (key, message) = validationContext.When("list", new[] {1})
				.HasCount(1)
				.AddValidationError("template1");

			Assert.Equal("list", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void PropertyHasCount()
		{
			var (key, message) = validationContext.When("list", new List<int> {1})
				.HasCount(1)
				.AddValidationError("template1");

			Assert.Equal("list", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void NotHasCount()
		{
			var (key, message) = validationContext.When("list", new[] {1})
				.HasCountNot(2)
				.AddValidationError("template1");

			Assert.Equal("list", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void PropertyNotHasCount()
		{
			var (key, message) = validationContext.When("list", new List<int> {1})
				.HasCountNot(2)
				.AddValidationError("template1");


			Assert.Equal("list", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsContains()
		{
			var (key, message) = validationContext.When("list", new[] {1})
				.IsContains(1)
				.AddValidationError("template1");

			Assert.Equal("list", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void PropertyIsContains()
		{
			var (key, message) = validationContext.When("list", new List<int> {1})
				.IsContains(1)
				.AddValidationError("template1");

			Assert.Equal("list", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsNotContains()
		{
			var (key, message) = validationContext.When("list", new[] {"item1"})
				.IsNotContains("item2")
				.AddValidationError("template1");

			Assert.Equal("list", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void PropertyIsNotContains()
		{
			var (key, message) = validationContext.When("list", new List<int>())
				.IsNotContains(1)
				.AddValidationError("template1");

			Assert.Equal("list", key);
			Assert.Equal("template1", message);
		}
	}
}