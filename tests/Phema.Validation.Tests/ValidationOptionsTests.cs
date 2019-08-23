using System;
using Microsoft.Extensions.DependencyInjection;
using Phema.Validation.Conditions;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationOptionsTests
	{
		[Fact]
		public void ValidationPathOverride()
		{
			var validationContext = CreateValidationContext(o => o.ValidationPath = "context");

			var model = new TestModel
			{
				Model = new TestModel()
			};

			var details = validationContext.When(model, m => m.Model.Model)
				.IsNull()
				.AddError("Inner model is null");

			Assert.Equal("context.Model.Model", details.ValidationKey);
		}

		[Fact]
		public void ValidationPathOverride_CreateScope()
		{
			var validationContext = CreateValidationContext(o => o.ValidationPath = "context");

			var forKey = validationContext.CreateScope("key");

			Assert.Equal("context.key", forKey.ValidationPath);
		}

		[Fact]
		public void ValidationPathSeparatorOverride()
		{
			var validationContext = CreateValidationContext(o => o.ValidationPartSeparator = ":");

			var model = new TestModel
			{
				Model = new TestModel()
			};

			var details = validationContext.When(model, m => m.Model.Model)
				.IsNull()
				.AddError("Inner model is null");

			Assert.Equal("Model:Model", details.ValidationKey);
		}

		[Fact]
		public void ValidationPathSeparatorOverride_CreateScope()
		{
			var validationContext = CreateValidationContext(o => o.ValidationPartSeparator = ":");

			var model = new TestModel
			{
				Model = new TestModel()
			};

			var forModel = validationContext.CreateScope(model, m => m.Model.Model);

			Assert.Equal("Model:Model", forModel.ValidationPath);
		}

		[Theory]
		[InlineData(ValidationSeverity.Warning, true)]
		[InlineData(ValidationSeverity.Information, false)]
		public void ValidationSeverityOverride(ValidationSeverity validationSeverity, bool isValid)
		{
			var validationContext = CreateValidationContext(o => o.ValidationSeverity = validationSeverity);

			var model = new TestModel
			{
				Model = new TestModel()
			};

			var details = validationContext.When(model, m => m.Model.Model)
				.IsNull()
				.AddInformation("Inner model is null");

			Assert.Equal("Model.Model", details.ValidationKey);
			Assert.Equal(isValid, validationContext.IsValid());
		}

		private static IValidationContext CreateValidationContext(Action<ValidationOptions> validationOptions)
		{
			return new ServiceCollection()
				.AddValidation(validationOptions)
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		private class TestModel
		{
			public TestModel Model { get; set; }
		}
	}
}