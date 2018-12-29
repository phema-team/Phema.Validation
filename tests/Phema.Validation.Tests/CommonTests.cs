using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class CommonTests
	{
		private readonly IValidationContext validationContext;

		public CommonTests()
		{
			validationContext = new ServiceCollection()
				.AddPhemaValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void AddReturnsSameErrorAsContext()
		{
			var error1 = validationContext.Validate((ValidationKey)"key", 10)
				.Is(value => true)
				.Add(() => new ValidationMessage(() => "template"), Array.Empty<object>(), ValidationSeverity.Error);

			var error2 = Assert.Single(validationContext.Errors);

			Assert.Same(error1, error2);
			Assert.Equal(error1, error2);
		}
		
		[Fact]
		public void IfIsConditionIsTrueAddsError()
		{
			var error = validationContext.Validate((ValidationKey)"key", 10)
				.Is(value => true)
				.Add(() => new ValidationMessage(() => "template"), Array.Empty<object>(), ValidationSeverity.Error);

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}

		[Fact]
		public void IfNoIsConditionAddsError()
		{
			var error =validationContext.Validate((ValidationKey)"key", 10)
				.Add(() => new ValidationMessage(() => "template"), Array.Empty<object>(), ValidationSeverity.Error);

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}

		[Fact]
		public void IfAnyIsConditionIsTrueAddsError()
		{
			var error =validationContext.Validate((ValidationKey)"key", 10)
				.Is(value => false)
				.Is(value => true)
				.Add(() => new ValidationMessage(() => "template"), Array.Empty<object>(), ValidationSeverity.Error);

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}

		[Fact]
		public void IsConditionIsTrueNotAddsError()
		{
			validationContext.Validate((ValidationKey)"key", 10)
				.Is(value => false)
				.Add(() => new ValidationMessage(() => "template"), Array.Empty<object>(), ValidationSeverity.Error);

			Assert.Empty(validationContext.Errors);
		}

		[Fact]
		public void ValidationMessageWithParameters()
		{
			var error = validationContext.Validate((ValidationKey)"key", 10)
				.Is(value => true)
				.Add(() => new ValidationMessage(args => $"template {args[0]}, {args[1]}"), new object[]{12, 13}, ValidationSeverity.Error);

			Assert.Equal("key", error.Key);
			Assert.Equal("template 12, 13", error.Message);
		}

		[Fact]
		public void ThrowsSameExceptionInIsCondition()
		{
			Assert.Throws<Exception>(() =>
				validationContext.Validate((ValidationKey)"key", 10)
					.Is(value => throw new Exception())
					.Add(() => new ValidationMessage(() => "template"), Array.Empty<object>(), ValidationSeverity.Error));
		}

		[Fact]
		public void ThrowsSameExceptionInAdd()
		{
			Assert.Throws<Exception>(() =>
				validationContext.Validate((ValidationKey)"key", 10)
					.Is(value => true)
					.Add(() => throw new Exception(), null, ValidationSeverity.Error));
		}
	}
}