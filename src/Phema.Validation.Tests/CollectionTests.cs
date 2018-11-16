using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Xunit;

namespace Phema.Validation.Tests
{
	public class CollectionTests
	{
		private readonly IValidationContext validationContext;

		public CollectionTests()
		{
			validationContext = new ValidationContext();
		}

		[Fact]
		public void IsEmpty()
		{
			validationContext
				.When("key", Array.Empty<int>())
				.IsEmpty()
				.AddError(() => new ValidationMessage(() => "message"));

			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message", error.Message);
		}
		
		[Fact]
		public void IsNotEmpty()
		{
			validationContext.When("key", new [] { 1, 2, 3 })
				.IsNotEmpty()
				.AddError(() => new ValidationMessage(() => "message"));

			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message", error.Message);
		}
		
		[Fact]
		public void IsCount()
		{
			validationContext.When("key", new [] { 1, 2, 3 })
				.IsCount(3)
				.AddError(() => new ValidationMessage(() => "message"));

			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message", error.Message);
		}
		
		[Fact]
		public void IsNotCount()
		{
			validationContext.When("key", new [] { 1, 2, 3 })
				.IsNotCount(2)
				.AddError(() => new ValidationMessage(() => "message"));

			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message", error.Message);
		}
		
		[Fact]
		public void IsContains()
		{
			validationContext.When("key", new [] { 1, 2, 3 })
				.IsContains(2)
				.AddError(() => new ValidationMessage(() => "message"));

			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message", error.Message);
		}
		
		[Fact]
		public void IsNotContains()
		{
			validationContext.When("key", new [] { 1, 2, 3 })
				.IsNotContains(4)
				.AddError(() => new ValidationMessage(() => "message"));

			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message", error.Message);
		}
		
		public class Stab
		{
			[DataMember(Name = "key")]
			public List<int> List { get; set; }
		}
		
		[Fact]
		public void ValueIsEmpty()
		{
			var stab = new Stab { List = new List<int>()};
			
			validationContext.When(stab, s => s.List)
				.IsEmpty()
				.AddError(() => new ValidationMessage(() => "message"));

			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message", error.Message);
		}
		
		[Fact]
		public void ValueIsNotEmpty()
		{
			var stab = new Stab { List = new List<int> { 1, 2, 3 }};
			
			validationContext.When(stab, s => s.List)
				.IsNotEmpty()
				.AddError(() => new ValidationMessage(() => "message"));

			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message", error.Message);
		}
	}
}