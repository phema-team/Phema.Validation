using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConditionCollectionExtensionsTests
	{
		private readonly IValidationContext validationContext;

		public ValidationConditionCollectionExtensionsTests()
		{
			validationContext = new ServiceCollection()
				.AddPhemaValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}
		
		[Fact]
		public void IsEmpty()
		{
			var error = validationContext.Validate("key", Array.Empty<int>())
				.IsEmpty()
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.NotNull(error);
			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenEmpty_Empty()
		{
			var error = validationContext.Validate("key", Array.Empty<int>())
				.WhenEmpty()
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.NotNull(error);
			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenEmpty_Added()
		{
			var error = validationContext.Validate("key", Array.Empty<int>())
				.WhenEmpty()
				.AddError(() => new ValidationMessage(() => "template"));
			
			var @null = validationContext.Validate("key", Array.Empty<int>())
				.WhenEmpty()
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.Null(@null);
			Assert.Single(validationContext.Errors);
			Assert.NotNull(error);
			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void PropertyIsEmpty()
		{
			var model = new TestModel { Statistics = new List<int>()};
			
			var error = validationContext.Validate(nameof(model.Statistics), model.Statistics)
				.IsEmpty()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("Statistics", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void PropertyWhenEmpty()
		{
			var model = new TestModel { Statistics = new List<int>()};
			
			var error = validationContext.Validate(nameof(model.Statistics), model.Statistics)
				.WhenEmpty()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("Statistics", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNotEmpty()
		{
			var error = validationContext.Validate("key", new [] { 1 })
				.IsNotEmpty()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNotEmpty()
		{
			var error = validationContext.Validate("key", new [] { 1 })
				.IsNotEmpty()
				.AddError(() => new ValidationMessage(() => "template"));
			
			var @null = validationContext.Validate("key", new [] { 1 })
				.WhenNotEmpty()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Null(@null);
			Assert.Single(validationContext.Errors);
			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void PropertyIsNotEmpty()
		{
			var model = new TestModel { Statistics = new List<int> { 1 }};
			
			var error = validationContext.Validate(nameof(model.Statistics), model.Statistics)
				.IsNotEmpty()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("Statistics", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void PropertyWhenNotEmpty()
		{
			var model = new TestModel { Statistics = new List<int> { 1 }};
			
			var error = validationContext.Validate(nameof(model.Statistics), model.Statistics)
				.WhenNotEmpty()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("Statistics", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void HasCount()
		{
			var error = validationContext.Validate("key", new [] { 1 })
				.HasCount(1)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenHasCount_Empty()
		{
			var error = validationContext.Validate("key", new [] { 1 })
				.WhenHasCount(1)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenHasCount_Added()
		{
			var error = validationContext.Validate("key", new [] { 1 })
				.WhenHasCount(1)
				.AddError(() => new ValidationMessage(() => "template"));
			
			var @null = validationContext.Validate("key", new [] { 1 })
				.WhenHasCount(1)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Null(@null);
			Assert.Single(validationContext.Errors);
			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void PropertyHasCount()
		{
			var model = new TestModel { Statistics = new List<int> { 1 }};
			
			var error = validationContext.Validate(nameof(model.Statistics), model.Statistics)
				.HasCount(1)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("Statistics", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void PropertyWhenHasCount()
		{
			var model = new TestModel { Statistics = new List<int> { 1 }};
			
			var error = validationContext.Validate(nameof(model.Statistics), model.Statistics)
				.WhenHasCount(1)
				.AddError(() => new ValidationMessage(() => "template"));
			
			var @null = validationContext.Validate(nameof(model.Statistics), model.Statistics)
				.WhenHasCount(1)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Null(@null);
			Assert.Single(validationContext.Errors);
			Assert.Equal("Statistics", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void NotHasCount()
		{
			var error = validationContext.Validate("key", new [] { 1 })
				.NotHasCount(2)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNotHasCount_Empty()
		{
			var error = validationContext.Validate("key", new [] { 1 })
				.WhenNotHasCount(2)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNotHasCount_Added()
		{
			var error = validationContext.Validate("key", new [] { 1 })
				.WhenNotHasCount(2)
				.AddError(() => new ValidationMessage(() => "template"));
			
			var @null = validationContext.Validate("key", new [] { 1 })
				.WhenNotHasCount(2)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Null(@null);
			Assert.Single(validationContext.Errors);
			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void PropertyNotHasCount()
		{
			var model = new TestModel { Statistics = new List<int> { 1 }};
			
			var error = validationContext.Validate(nameof(model.Statistics), model.Statistics)
				.NotHasCount(2)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("Statistics", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void PropertyWhenNotHasCount()
		{
			var model = new TestModel { Statistics = new List<int> { 1 }};
			
			var error = validationContext.Validate(nameof(model.Statistics), model.Statistics)
				.WhenNotHasCount(2)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("Statistics", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsContains()
		{
			var error = validationContext.Validate("key", new [] { 1 })
				.IsContains(1)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenContains_Empty()
		{
			var error = validationContext.Validate("key", new [] { 1 })
				.WhenContains(1)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenContains_Added()
		{
			var error = validationContext.Validate("key", new [] { 1 })
				.WhenContains(1)
				.AddError(() => new ValidationMessage(() => "template"));
			
			var @null = validationContext.Validate("key", new [] { 1 })
				.WhenContains(1)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Null(@null);
			Assert.Single(validationContext.Errors);
			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void PropertyIsContains()
		{
			var model = new TestModel { Statistics = new List<int> { 1 }};
			
			var error = validationContext.Validate(nameof(model.Statistics), model.Statistics)
				.IsContains(1)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("Statistics", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void PropertyWhenContains()
		{
			var model = new TestModel { Statistics = new List<int> { 1 }};
			
			var error = validationContext.Validate(nameof(model.Statistics), model.Statistics)
				.WhenContains(1)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("Statistics", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsNotContains()
		{
			var error = validationContext.Validate("key", new [] { 1 })
				.IsNotContains(2)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNotContains_Empty()
		{
			var error = validationContext.Validate("key", new [] { 1 })
				.WhenNotContains(2)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenNotContains_Added()
		{
			var error = validationContext.Validate("key", new [] { 1 })
				.WhenNotContains(2)
				.AddError(() => new ValidationMessage(() => "template"));
			
			var @null = validationContext.Validate("key", new [] { 1 })
				.WhenNotContains(2)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Null(@null);
			Assert.Single(validationContext.Errors);
			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void PropertyIsNotContains()
		{
			var model = new TestModel { Statistics = new List<int> { 1 }};
			
			var error = validationContext.Validate(nameof(model.Statistics), model.Statistics)
				.IsNotContains(2)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("Statistics", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void PropertyWhenNotContains()
		{
			var model = new TestModel { Statistics = new List<int> { 1 }};
			
			var error = validationContext.Validate(nameof(model.Statistics), model.Statistics)
				.WhenNotContains(2)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("Statistics", error.Key);
			Assert.Equal("template", error.Message);
		}
	}
}