using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConditionSeverityExtensionsTests
	{
		private readonly IValidationContext validationContext;

		public ValidationConditionSeverityExtensionsTests()
		{
			validationContext = new ServiceCollection()
				.AddPhemaValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
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

		[Fact]
		public void ErrorSeverity()
		{
			var error = validationContext.When("key", 12)
				.Is(value => value == 12)
				.AddError(() => new ValidationMessage(() => "message"));

			Assert.NotNull(error);

			Assert.Equal("key", error.Key);
			Assert.Equal("message", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}

		[Fact]
		public void ErrorSeverity_OneParameter()
		{
			var error = validationContext.When("key", 12)
				.Is(value => value == 12)
				.AddError(() => new ValidationMessage<int>(one => $"message: {one}"), 11);

			Assert.NotNull(error);

			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}

		[Fact]
		public void Throws_OneParameters()
		{
			Assert.Throws<ArgumentNullException>(() =>
				validationContext.When("key", 12)
					.Is(value => value == 12)
					.Add(() => new ValidationMessage<int>(one => $"message: {one}"), null, ValidationSeverity.Error));
		}

		[Fact]
		public void Throws_OneParameters_ButZero()
		{
			Assert.Throws<ArgumentException>(() =>
				validationContext.When("key", 12)
					.Is(value => value == 12)
					.Add(() => new ValidationMessage<int>(one => $"message: {one}"), Array.Empty<object>(),
						ValidationSeverity.Error));
		}

		[Fact]
		public void ErrorSeverity_TwoParameter()
		{
			var error = validationContext.When("key", 12)
				.Is(value => value == 12)
				.AddError(() => new ValidationMessage<int, int>((one, two) => $"message: {one},{two}"), 11, 22);

			Assert.NotNull(error);

			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}

		[Fact]
		public void Throws_TwoParameters()
		{
			Assert.Throws<ArgumentNullException>(() =>
				validationContext.When("key", 12)
					.Is(value => value == 12)
					.Add(() => new ValidationMessage<int, int>((one, two) => $"message: {one},{two}"), null,
						ValidationSeverity.Error));
		}

		[Fact]
		public void Throws_TwoParameters_ButZero()
		{
			Assert.Throws<ArgumentException>(() =>
				validationContext.When("key", 12)
					.Is(value => value == 12)
					.Add(() => new ValidationMessage<int, int>((one, two) => $"message: {one},{two}"), Array.Empty<object>(),
						ValidationSeverity.Error));
		}

		[Fact]
		public void ErrorSeverity_ThreeParameter()
		{
			var error = validationContext.When("key", 12)
				.Is(value => value == 12)
				.AddError(() => new ValidationMessage<int, int, int>((one, two, three) => $"message: {one},{two},{three}"), 11,
					22, 33);

			Assert.NotNull(error);

			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11,22,33", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}

		[Fact]
		public void Throws_ThreeParameters()
		{
			Assert.Throws<ArgumentNullException>(() =>
				validationContext.When("key", 12)
					.Is(value => value == 12)
					.Add(() => new ValidationMessage<int, int, int>((one, two, three) => $"message: {one},{two},{three}"), null,
						ValidationSeverity.Error));
		}

		[Fact]
		public void Throws_ThreeParameters_ButZero()
		{
			Assert.Throws<ArgumentException>(() =>
				validationContext.When("key", 12)
					.Is(value => value == 12)
					.Add(() => new ValidationMessage<int, int, int>((one, two, three) => $"message: {one},{two},{three}"),
						Array.Empty<object>(), ValidationSeverity.Error));
		}

		[Fact]
		public void FatalSeverity()
		{
			var (key, message, severity) = validationContext.When("key", 12)
				.Is(value => value == 12)
				.Add(() => new ValidationMessage(() => "message"), ValidationSeverity.Fatal);

			Assert.Equal("key", key);
			Assert.Equal("message", message);
			Assert.Equal(ValidationSeverity.Fatal, severity);
		}

		[Fact]
		public void FatalSeverity_OneParameter()
		{
			Assert.Throws<ValidationConditionException>(() =>
				validationContext.When("key", 12)
					.Is(value => value == 12)
					.Throw(() => new ValidationMessage<int>(one => $"message: {one}"), 11));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11", error.Message);
			Assert.Equal(ValidationSeverity.Fatal, error.Severity);
		}

		[Fact]
		public void FatalSeverity_TwoParameters()
		{
			Assert.Throws<ValidationConditionException>(() =>
				validationContext.When("key", 12)
					.Is(value => value == 12)
					.Throw(() => new ValidationMessage<int, int>((one, two) => $"message: {one},{two}"), 11, 22));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Fatal, error.Severity);
		}

		[Fact]
		public void FatalSeverity_ThreeParameters()
		{
			Assert.Throws<ValidationConditionException>(() =>
				validationContext.When("key", 12)
					.Is(value => value == 12)
					.Throw(() => new ValidationMessage<int, int, int>((one, two, three) => $"message: {one},{two},{three}"), 11,
						22, 33));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("key", error.Key);
			Assert.Equal("message: 11,22,33", error.Message);
			Assert.Equal(ValidationSeverity.Fatal, error.Severity);
		}
	}
}