using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Core.Tests
{
	public class ValidationConditionTests
	{
		private readonly IValidationContext validationContext;

		public ValidationConditionTests()
		{
			validationContext = new ServiceCollection()
				.AddPhemaValidation(configuration =>
					configuration.AddComponent<TestModel, TestModelValidationComponent>())
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}
		
		[Fact]
		public void Is_CompareByValue()
		{
			validationContext.When("key", "value")
				.Is(value => value == "value")
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			var (key, message) = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void Is_WithoutValue()
		{
			validationContext.When("key", "value")
				.Is(() => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);
			
			var (key, message) = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", key);
			Assert.Equal("template1", message);
		}
		
		[Fact]
		public void Is_CompareCombined_True()
		{
			validationContext.When("key", "value")
				.Is(() => true)
				.Is(value => value == "value")
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);
			
			var (key, message) = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", key);
			Assert.Equal("template1", message);
		}
		
		[Fact]
		public void Is_CompareCombined_SomeFalse()
		{
			validationContext.When("key", "value")
				.Is(() => true)
				.Is(value => false)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);
			
			Assert.Empty(validationContext.Errors);
		}

		[Fact]
		public void ConditionsOrder()
		{
			var raises = 0;
			
			validationContext.When("key", "value")
				.Is(() =>
				{
					Assert.Equal(0, raises++);
					return true;
				})
				.Is(value =>
				{
					Assert.Equal(1, raises++);
					return true;
				})
				.Is(value =>
				{
					Assert.Equal(2, raises++);
					return false;
				})
				.Is(() =>
				{
					Assert.False(true);
					return false;
				})
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);
			
			Assert.Equal(3, raises);
		}
		
		[Fact]
		public void AddReturnsSameErrorAsContext()
		{
			var error1 = validationContext.When("key", 10)
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			var error2 = Assert.Single(validationContext.Errors);

			Assert.Same(error1, error2);
			Assert.Equal(error1, error2);
		}

		[Fact]
		public void IfIsConditionIsTrueAddsError()
		{
			var (key, message) = validationContext.When("key", 10)
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("key", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IfNoIsConditionAddsError()
		{
			var (key, message) = validationContext.When("key", 10)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("key", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IfAllIsConditionIsTrueAddsError()
		{
			var (key, message) = validationContext.When("key", 10)
				.Is(value => true)
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("key", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsConditionIsTrueNotAddsError()
		{
			validationContext.When("key", 10)
				.Is(value => false)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Empty(validationContext.Errors);
		}

		[Fact]
		public void ValidationMessageWithParameters()
		{
			var (key, message) = validationContext.When("key", 10)
				.Is(value => true)
				.AddError<TestModelValidationComponent, int, int>(c => c.TestModelTemplate4, 12, 13);

			Assert.Equal("key", key);
			Assert.Equal("template: 12, 13", message);
		}

		[Fact]
		public void ThrowsSameExceptionInIsCondition()
		{
			Assert.Throws<Exception>(() =>
				validationContext.When("key", 10)
					.Is(value => throw new Exception())
					.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1));
		}

		[Fact]
		public void ThrowsSameExceptionInAdd()
		{
			Assert.Throws<Exception>(() =>
				validationContext.When("key", 10)
					.Is(value => true)
					.AddError<TestModelValidationComponent>(c => throw new Exception()));
		}
		
		[Fact]
		public void AspNetErrorSeverity()
		{
			var model = new TestModel();

			var error = validationContext.When("string", model.String)
				.Is(value => value == null)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.NotNull(error);
			Assert.Equal("string", error.Key);
			Assert.Equal("template1", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}

		[Fact]
		public void AspNetErrorSeverity_OneParameter()
		{
			var model = new TestModel();

			var error = validationContext.When("string", model.String)
				.Is(value => value == null)
				.AddError<TestModelValidationComponent, int>(c => c.TestModelTemplate3, 11);

			Assert.NotNull(error);
			Assert.Equal("string", error.Key);
			Assert.Equal("template: 11", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}

		[Fact]
		public void AspNetErrorSeverity_TwoParameters()
		{
			var model = new TestModel();

			var error = validationContext.When("string", model.String)
				.Is(value => value == null)
				.AddError<TestModelValidationComponent, int, int>(c => c.TestModelTemplate4, 11, 22);

			Assert.NotNull(error);
			Assert.Equal("string", error.Key);
			Assert.Equal("template: 11, 22", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}

		[Fact]
		public void AspNetErrorSeverity_ThreeParameters()
		{
			var model = new TestModel();

			var error = validationContext.When("string", model.String)
				.Is(value => value == null)
				.AddError<TestModelValidationComponent, int, int, int>(c => c.TestModelTemplate5, 11, 22, 33);

			Assert.NotNull(error);
			Assert.Equal("string", error.Key);
			Assert.Equal("template: 11, 22, 33", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}

		[Fact]
		public void AspNetWarningSeverity()
		{
			var model = new TestModel();

			var error = validationContext.When("string", model.String)
				.Is(value => value == null)
				.AddWarning<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.NotNull(error);
			Assert.Equal("string", error.Key);
			Assert.Equal("template1", error.Message);
			Assert.Equal(ValidationSeverity.Warning, error.Severity);
		}

		[Fact]
		public void AspNetWarningSeverity_OneParameter()
		{
			var model = new TestModel();

			var error = validationContext.When("string", model.String)
				.Is(value => value == null)
				.AddWarning<TestModelValidationComponent, int>(c => c.TestModelTemplate3, 11);

			Assert.NotNull(error);
			Assert.Equal("string", error.Key);
			Assert.Equal("template: 11", error.Message);
			Assert.Equal(ValidationSeverity.Warning, error.Severity);
		}

		[Fact]
		public void AspNetWarningSeverity_TwoParameters()
		{
			var model = new TestModel();

			var error = validationContext.When("string", model.String)
				.Is(value => value == null)
				.AddWarning<TestModelValidationComponent, int, int>(c => c.TestModelTemplate4, 11, 22);

			Assert.NotNull(error);
			Assert.Equal("string", error.Key);
			Assert.Equal("template: 11, 22", error.Message);
			Assert.Equal(ValidationSeverity.Warning, error.Severity);
		}

		[Fact]
		public void AspNetWarningSeverity_ThreeParameters()
		{
			var model = new TestModel();

			var error = validationContext.When("string", model.String)
				.Is(value => value == null)
				.AddWarning<TestModelValidationComponent, int, int, int>(c => c.TestModelTemplate5, 11, 22, 33);

			Assert.NotNull(error);
			Assert.Equal("string", error.Key);
			Assert.Equal("template: 11, 22, 33", error.Message);
			Assert.Equal(ValidationSeverity.Warning, error.Severity);
		}

		[Fact]
		public void AspNetInformationSeverity()
		{
			var model = new TestModel();

			var error = validationContext.When("string", model.String)
				.Is(value => value == null)
				.AddInformation<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.NotNull(error);
			Assert.Equal("string", error.Key);
			Assert.Equal("template1", error.Message);
			Assert.Equal(ValidationSeverity.Information, error.Severity);
		}

		[Fact]
		public void AspNetInformationSeverity_OneParameter()
		{
			var model = new TestModel();

			var error = validationContext.When("string", model.String)
				.Is(value => value == null)
				.AddInformation<TestModelValidationComponent, int>(c => c.TestModelTemplate3, 11);

			Assert.NotNull(error);
			Assert.Equal("string", error.Key);
			Assert.Equal("template: 11", error.Message);
			Assert.Equal(ValidationSeverity.Information, error.Severity);
		}

		[Fact]
		public void AspNetInformationSeverity_TwoParameters()
		{
			var model = new TestModel();

			var error = validationContext.When("string", model.String)
				.Is(value => value == null)
				.AddInformation<TestModelValidationComponent, int, int>(c => c.TestModelTemplate4, 11, 22);

			Assert.NotNull(error);
			Assert.Equal("string", error.Key);
			Assert.Equal("template: 11, 22", error.Message);
			Assert.Equal(ValidationSeverity.Information, error.Severity);
		}

		[Fact]
		public void AspNetInformationSeverity_ThreeParameters()
		{
			var model = new TestModel();

			var error = validationContext.When("string", model.String)
				.Is(value => value == null)
				.AddInformation<TestModelValidationComponent, int, int, int>(c => c.TestModelTemplate5, 11, 22, 33);

			Assert.NotNull(error);
			Assert.Equal("string", error.Key);
			Assert.Equal("template: 11, 22, 33", error.Message);
			Assert.Equal(ValidationSeverity.Information, error.Severity);
		}

		[Fact]
		public void AspNetDebugSeverity()
		{
			var model = new TestModel();

			var error = validationContext.When("string", model.String)
				.Is(value => value == null)
				.AddDebug<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.NotNull(error);
			Assert.Equal("string", error.Key);
			Assert.Equal("template1", error.Message);
			Assert.Equal(ValidationSeverity.Debug, error.Severity);
		}

		[Fact]
		public void AspNetDebugSeverity_OneParameter()
		{
			var model = new TestModel();

			var error = validationContext.When("string", model.String)
				.Is(value => value == null)
				.AddDebug<TestModelValidationComponent, int>(c => c.TestModelTemplate3, 11);

			Assert.NotNull(error);
			Assert.Equal("string", error.Key);
			Assert.Equal("template: 11", error.Message);
			Assert.Equal(ValidationSeverity.Debug, error.Severity);
		}

		[Fact]
		public void AspNetDebugSeverity_TwoParameters()
		{
			var model = new TestModel();

			var error = validationContext.When("string", model.String)
				.Is(value => value == null)
				.AddDebug<TestModelValidationComponent, int, int>(c => c.TestModelTemplate4, 11, 22);

			Assert.NotNull(error);
			Assert.Equal("string", error.Key);
			Assert.Equal("template: 11, 22", error.Message);
			Assert.Equal(ValidationSeverity.Debug, error.Severity);
		}

		[Fact]
		public void AspNetDebugSeverity_ThreeParameters()
		{
			var model = new TestModel();

			var error = validationContext.When("string", model.String)
				.Is(value => value == null)
				.AddDebug<TestModelValidationComponent, int, int, int>(c => c.TestModelTemplate5, 11, 22, 33);

			Assert.NotNull(error);
			Assert.Equal("string", error.Key);
			Assert.Equal("template: 11, 22, 33", error.Message);
			Assert.Equal(ValidationSeverity.Debug, error.Severity);
		}

		[Fact]
		public void AspNetTraceSeverity()
		{
			var model = new TestModel();

			var error = validationContext.When("string", model.String)
				.Is(value => value == null)
				.AddTrace<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.NotNull(error);
			Assert.Equal("string", error.Key);
			Assert.Equal("template1", error.Message);
			Assert.Equal(ValidationSeverity.Trace, error.Severity);
		}

		[Fact]
		public void AspNetTraceSeverity_OneParameter()
		{
			var model = new TestModel();

			var error = validationContext.When("string", model.String)
				.Is(value => value == null)
				.AddTrace<TestModelValidationComponent, int>(c => c.TestModelTemplate3, 11);

			Assert.NotNull(error);
			Assert.Equal("string", error.Key);
			Assert.Equal("template: 11", error.Message);
			Assert.Equal(ValidationSeverity.Trace, error.Severity);
		}

		[Fact]
		public void AspNetTraceSeverity_TwoParameters()
		{
			var model = new TestModel();

			var error = validationContext.When("string", model.String)
				.Is(value => value == null)
				.AddTrace<TestModelValidationComponent, int, int>(c => c.TestModelTemplate4, 11, 22);

			Assert.NotNull(error);
			Assert.Equal("string", error.Key);
			Assert.Equal("template: 11, 22", error.Message);
			Assert.Equal(ValidationSeverity.Trace, error.Severity);
		}

		[Fact]
		public void AspNetTraceSeverity_ThreeParameters()
		{
			var model = new TestModel();

			var error = validationContext.When("string", model.String)
				.Is(value => value == null)
				.AddTrace<TestModelValidationComponent, int, int, int>(c => c.TestModelTemplate5, 11, 22, 33);

			Assert.NotNull(error);
			Assert.Equal("string", error.Key);
			Assert.Equal("template: 11, 22, 33", error.Message);
			Assert.Equal(ValidationSeverity.Trace, error.Severity);
		}
		
		[Fact]
		public void Throw()
		{
			var exception = Assert.Throws<ValidationConditionException>(() =>
				validationContext.When("age", 10)
					.Is(value => value == 10)
					.Throw<TestModelValidationComponent>(c => c.TestModelTemplate1));

			Assert.Equal("age", exception.Error.Key);
			Assert.Equal("template1", exception.Error.Message);
			Assert.Equal(ValidationSeverity.Fatal, exception.Error.Severity);
		}

		[Fact]
		public void Throw_Valid()
		{
			validationContext.When("age", 10)
				.Is(value => value == 9)
				.Throw<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.True(validationContext.IsValid());
		}

		[Fact]
		public void Throw_OneParameter()
		{
			var exception = Assert.Throws<ValidationConditionException>(() =>
				validationContext.When("age", 10)
					.Is(value => value == 10)
					.Throw<TestModelValidationComponent, int>(c => c.TestModelTemplate3, 11));

			Assert.Equal("age", exception.Error.Key);
			Assert.Equal("template: 11", exception.Error.Message);
			Assert.Equal(ValidationSeverity.Fatal, exception.Error.Severity);
		}

		[Fact]
		public void Throw_OneParameter_Valid()
		{
			validationContext.When("age", 10)
				.Is(value => value == 9)
				.Throw<TestModelValidationComponent, int>(c => c.TestModelTemplate3, 11);

			Assert.True(validationContext.IsValid());
		}

		[Fact]
		public void Throw_TwoParameters()
		{
			var exception = Assert.Throws<ValidationConditionException>(() =>
				validationContext.When("age", 10)
					.Is(value => value == 10)
					.Throw<TestModelValidationComponent, int, int>(c => c.TestModelTemplate4, 11, 22));

			Assert.Equal("age", exception.Error.Key);
			Assert.Equal("template: 11, 22", exception.Error.Message);
			Assert.Equal(ValidationSeverity.Fatal, exception.Error.Severity);
		}

		[Fact]
		public void Throw_TwoParameters_Valid()
		{
			validationContext.When("age", 10)
				.Is(value => value == 9)
				.Throw<TestModelValidationComponent, int, int>(c => c.TestModelTemplate4, 11, 22);

			Assert.True(validationContext.IsValid());
		}

		[Fact]
		public void ThrowAddsSameErrorAsContext()
		{
			var exception = Assert.Throws<ValidationConditionException>(() =>
				validationContext.When("age", 10)
					.Is(value => value == 10)
					.Throw<TestModelValidationComponent>(c => c.TestModelTemplate1));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal(error, exception.Error);
			Assert.Same(error, exception.Error);
		}
	}
}