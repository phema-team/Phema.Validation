using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationContextTests
	{
		private readonly IValidationContext validationContext;

		public ValidationContextTests()
		{
			validationContext = new ServiceCollection()
				.AddValidation(c => {})
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}
		
		[Fact]
		public void EmptyAddWorks()
		{
			var error1 = validationContext.Add(() => new ValidationMessage(args => $"template{args[0]}"), new object[]{10}, ValidationSeverity.Error);

			var error2 = Assert.Single(validationContext.Errors);
		
			Assert.Same(error1, error2);
			Assert.Equal("", error1.Key);
			Assert.Equal("template10", error1.Message);
			Assert.Equal(ValidationSeverity.Error, error1.Severity);
		}
		
		[Fact]
		public void EmptyAddDebugWorks()
		{
			var error = validationContext.AddDebug(() => new ValidationMessage(() => "template"));

			Assert.Equal("", error.Key);
			Assert.Equal("template", error.Message);
			Assert.Equal(ValidationSeverity.Debug, error.Severity);
		}
		
		[Fact]
		public void EmptyAddErrorWorks()
		{
			var error = validationContext.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("", error.Key);
			Assert.Equal("template", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}
		
		[Fact]
		public void EmptyAddTraceWorks()
		{
			var error = validationContext.AddTrace(() => new ValidationMessage(() => "template"));

			Assert.Equal("", error.Key);
			Assert.Equal("template", error.Message);
			Assert.Equal(ValidationSeverity.Trace, error.Severity);
		}
		
		[Fact]
		public void EmptyAddInformationWorks()
		{
			var error = validationContext.AddInformation(() => new ValidationMessage(() => "template"));

			Assert.Equal("", error.Key);
			Assert.Equal("template", error.Message);
			Assert.Equal(ValidationSeverity.Information, error.Severity);
		}
		
		[Fact]
		public void EmptyAddWarningWorks()
		{
			var error = validationContext.AddWarning(() => new ValidationMessage(() => "template"));

			Assert.Equal("", error.Key);
			Assert.Equal("template", error.Message);
			Assert.Equal(ValidationSeverity.Warning, error.Severity);
		}
		
		[Fact]
		public void EmptyThrowsWorks()
		{
			var exception = Assert.Throws<ValidationConditionException>(() =>
				validationContext.Throw(() => new ValidationMessage(() => "template")));

			Assert.Equal("", exception.Error.Key);
			Assert.Equal("template", exception.Error.Message);
			Assert.Equal(ValidationSeverity.Fatal, exception.Error.Severity);
		}
	}
}