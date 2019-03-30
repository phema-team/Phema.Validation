using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Core.Tests
{
	public class ValidationContextExtensionsTests
	{
		private readonly IValidationContext validationContext;

		public ValidationContextExtensionsTests()
		{
			validationContext = new ServiceCollection()
				.AddValidation(configuration =>
					configuration.AddComponent<TestModel, TestModelValidationComponent>())
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void IsWorksAnsGenericVersion()
		{
			var stab = new TestModel();

			validationContext.When(stab, s => s.String)
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			var (key, message) = Assert.Single(validationContext.Errors);

			Assert.Equal("test.string", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void WhenWithFuncSelector()
		{
			var stab = new TestModel();

			validationContext.When(stab, s => s.String, s => s.String)
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			var (key, message, _) = Assert.Single(validationContext.Errors);

			Assert.Equal("test.string", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsValidByKeyExpression()
		{
			var model = new TestModel();

			validationContext.When(model, s => s.String)
				.Is(value => false)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.True(validationContext.IsValid<TestModel>(s => s.String));
		}

		[Fact]
		public void IsInvalidByKeyExpression()
		{
			var model = new TestModel();

			validationContext.When(model, s => s.String)
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.False(validationContext.IsValid<TestModel>(s => s.String));
		}

		[Fact]
		public void IsValidByKeyModelExpression()
		{
			var model = new TestModel();

			validationContext.When(model, s => s.String)
				.Is(value => false)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.True(validationContext.IsValid(model, s => s.String));
		}

		[Fact]
		public void EnsureIsValidByKeyModelExpression_ByType()
		{
			var model = new TestModel();

			validationContext.When(model, s => s.String)
				.Is(value => false)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			validationContext.EnsureIsValid<TestModel>(s => s.String);
		}

		[Fact]
		public void EnsureIsValidByKeyModelExpression_ByModel()
		{
			var model = new TestModel();

			validationContext.When(model, s => s.String)
				.Is(value => false)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			validationContext.EnsureIsValid(model, s => s.String);
		}

		[Fact]
		public void IsInvalidByKeyModelExpression()
		{
			var model = new TestModel();

			validationContext.When(model, s => s.String)
				.Is(value => false)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			validationContext.When(model, s => s.String)
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate2);

			Assert.False(validationContext.IsValid(model, s => s.String));

			var (key, message, _) = Assert.Single(validationContext.Errors);

			Assert.Equal("test.string", key);
			Assert.Equal("template2", message);
		}
	}
}