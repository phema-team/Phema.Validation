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
			validationContext.Validate("key", Array.Empty<int>())
				.WhenEmpty()
				.AddError(() => new ValidationMessage(() => "message"));

			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message", error.Message);
		}
		
		[Fact]
		public void IsNotEmpty()
		{
			validationContext.Validate("key", new [] { 1, 2, 3 })
				.WhenNotEmpty()
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
			
			validationContext.Validate(stab, s => s.List)
				.WhenEmpty()
				.AddError(() => new ValidationMessage(() => "message"));

			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message", error.Message);
		}
		
		[Fact]
		public void ValueIsNotEmpty()
		{
			var stab = new Stab { List = new List<int> { 1, 2, 3 }};
			
			validationContext.Validate(stab, s => s.List)
				.WhenNotEmpty()
				.AddError(() => new ValidationMessage(() => "message"));

			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", error.Key);
			Assert.Equal("message", error.Message);
		}
	}
}