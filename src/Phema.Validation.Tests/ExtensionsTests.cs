using System.Runtime.Serialization;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ExtensionsTests
	{
		private readonly IValidationContext validationContext;

		public ExtensionsTests()
		{
			validationContext = new ValidationContext(ValidationSeverity.Error);
		}

		[Fact]
		public void IfAnyErrorIsValidIsFalse()
		{
			validationContext.Validate("test", 10)
				.When(value => true)
				.AddError(() => new ValidationMessage(() => "works"));

			Assert.Single(validationContext.Errors);
			Assert.False(validationContext.IsValid());
		}

		[Fact]
		public void IsNotWorksAsNotIs()
		{
			validationContext.Validate("test", 10)
				.WhenNot(value => true)
				.AddError(() => new ValidationMessage(() => "works"));

			Assert.True(validationContext.IsValid());
		}

		[Fact]
		public void ThrowsIfConditionIsTrue()
		{
			var exception = Assert.Throws<ValidationConditionException>(() =>
				validationContext.Validate("test", 10)
					.When(value => true)
					.Throw(() => new ValidationMessage(() => "works")));

			Assert.Equal("test", exception.Error.Key);
			Assert.Equal("works", exception.Error.Message);
		}

		[Fact]
		public void EnsureThrowsIfAnyError()
		{
			validationContext.Validate("test", 10)
				.When(value => true)
				.AddError(() => new ValidationMessage(() => "works"));

			var exception = Assert.Throws<ValidationContextException>(
				() => validationContext.EnsureIsValid());

			var error = Assert.Single(exception.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		public class Stab
		{
			[DataMember(Name = "message")]
			public string Message { get; set; }
		}

		[Fact]
		public void WhenWorksAnsGenericVersion()
		{
			var stab = new Stab();

			validationContext.Validate(stab, s => s.Message)
				.When(value => true)
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("message", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void EnsureIsValidNotThrowsIfValid()
		{
			validationContext.Validate("test", 10)
				.When(value => false)
				.AddError(() => new ValidationMessage(() => "works"));

			validationContext.EnsureIsValid();
		}
		
		[Fact]
		public void IsValidByKey()
		{
			validationContext.Validate("test", 10)
				.When(value => false)
				.AddError(() => new ValidationMessage(() => "works"));

			Assert.True(validationContext.IsValid("test"));
		}
		
		[Fact]
		public void IsInvalidByKey()
		{
			validationContext.Validate("test", 10)
				.When(value => true)
				.AddError(() => new ValidationMessage(() => "works"));

			Assert.False(validationContext.IsValid("test"));
		}
		
		[Fact]
		public void IsValidByKeyExpression()
		{
			var stab = new Stab();
			
			validationContext.Validate(stab, s => s.Message)
				.When(value => false)
				.AddError(() => new ValidationMessage(() => "works"));

			Assert.True(validationContext.IsValid<Stab>(s => s.Message));
		}
		
		[Fact]
		public void IsValidByKeyModelExpression()
		{
			var stab = new Stab();
			
			validationContext.Validate(stab, s => s.Message)
				.When(value => false)
				.AddError(() => new ValidationMessage(() => "works"));

			Assert.True(validationContext.IsValid(stab, s => s.Message));
		}
		
		[Fact]
		public void IsInvalidByKeyExpression()
		{
			var stab = new Stab();
			
			validationContext.Validate(stab, s => s.Message)
				.When(value => true)
				.AddError(() => new ValidationMessage(() => "works"));

			Assert.False(validationContext.IsValid<Stab>(s => s.Message));
		}
		
		[Fact]
		public void IsInvalidByKeyModelExpression()
		{
			var stab = new Stab();
			
			validationContext.Validate(stab, s => s.Message)
				.When(() => true)
				.AddError(() => new ValidationMessage(() => "works"));
			
			validationContext.Validate(stab, s => s.Message)
				.When(() => true)
				.AddError(() => new ValidationMessage(() => "works"));

			Assert.False(validationContext.IsValid(stab, s => s.Message));
			
			Assert.Equal(2, validationContext.Errors.Count);
		}

		[Fact]
		public void EmptyValidate()
		{
			validationContext.Validate()
				.AddError(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("", error.Key);
			Assert.Equal("works", error.Message);
		}
	}
}