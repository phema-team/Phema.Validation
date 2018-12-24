using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConditionEnumerableExtensionsTests
	{
		private readonly IValidationContext validationContext;

		public ValidationConditionEnumerableExtensionsTests()
		{
			validationContext = new ServiceCollection()
				.AddValidation(c => {})
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}
		
		[Fact]
		public void IsAny()
		{
			var error = validationContext.Validate("key", new [] { 1 })
				.IsAny()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenAny_Empty()
		{
			var error = validationContext.Validate("key", new [] { 1 })
				.WhenAny()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenAny_Added()
		{
			var error = validationContext.Validate("key", new [] { 1 })
				.WhenAny()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);
			
			var @null = validationContext.Validate("key", new [] { 1 })
				.WhenAny()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Null(@null);
			Assert.Single(validationContext.Errors);
			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsAny_Valid()
		{
			validationContext.Validate("key", Array.Empty<int>())
				.IsAny()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void WhenAny_Valid()
		{
			validationContext.Validate("key", Array.Empty<int>())
				.WhenAny()
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void IsAnyWithParameter()
		{
			var error = validationContext.Validate("key", new [] { 1 })
				.IsAny(x => x == 1)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenAnyWithParameter_Empty()
		{
			var error = validationContext.Validate("key", new [] { 1 })
				.WhenAny(x => x == 1)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenAnyWithParameter_Added()
		{
			var error = validationContext.Validate("key", new [] { 1 })
				.WhenAny(x => x == 1)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			var @null = validationContext.Validate("key", new [] { 1 })
				.WhenAny(x => x == 1)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.Null(@null);
			Assert.Single(validationContext.Errors);
			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsAnyWithParameter_Valid()
		{
			validationContext.Validate("key", new [] { 1 })
				.IsAny(x => x == 2)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void WhenAnyWithParameter_Valid()
		{
			validationContext.Validate("key", new [] { 1 })
				.WhenAny(x => x == 2)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void IsAll()
		{
			var error = validationContext.Validate("key", new [] { 1 })
				.IsAll(x => x == 1)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenAll_Empty()
		{
			var error = validationContext.Validate("key", new [] { 1 })
				.WhenAll(x => x == 1)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenAll_Added()
		{
			var error = validationContext.Validate("key", new [] { 1 })
				.WhenAll(x => x == 1)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);
			
			var @null = validationContext.Validate("key", new [] { 1 })
				.WhenAll(x => x == 1)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);
			
			Assert.Null(@null);
			Assert.Single(validationContext.Errors);
			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsAll_Valid()
		{
			validationContext.Validate("key", new [] { 1, 2 })
				.IsAll(x => x == 1)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}
		
		[Fact]
		public void WhenAll_Valid()
		{
			validationContext.Validate("key", new [] { 1, 2 })
				.WhenAll(x => x == 1)
				.Add(() => new ValidationMessage(() => "template"), ValidationSeverity.Error);

			Assert.True(!validationContext.Errors.Any());
		}
	}
}