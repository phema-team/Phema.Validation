using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationContextExpressionTests
	{
		private readonly IValidationContext validationContext;
		
		public ValidationContextExpressionTests()
		{
			validationContext = new ServiceCollection()
				.AddValidation(c => {})
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}
		
			
		[Fact]
		public void IsWorksAnsGenericVersion()
		{
			var stab = new TestModel();

			validationContext.When(stab, s => s.Name)
				.Is(value => true)
				.AddError(() => new ValidationMessage(() => "template"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void WhenWithFuncSelector()
		{
			var stab = new TestModel();

			validationContext.When(stab, s => s.Name, s => s.Name)
				.Is(value => true)
				.AddError(() => new ValidationMessage(() => "template"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IsValidByKeyExpression()
		{
			var model = new TestModel();
			
			validationContext.When(model, s => s.Name)
				.Is(value => false)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(validationContext.IsValid<TestModel>(s => s.Name));
		}
		
		[Fact]
		public void IsInvalidByKeyExpression()
		{
			var model = new TestModel();
			
			validationContext.When(model, s => s.Name)
				.Is(value => true)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.False(validationContext.IsValid<TestModel>(s => s.Name));
		}
		
		[Fact]
		public void IsValidByKeyModelExpression()
		{
			var model = new TestModel();
			
			validationContext.When(model, s => s.Name)
				.Is(value => false)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.True(validationContext.IsValid(model, s => s.Name));
		}
		
		[Fact]
		public void IsInvalidByKeyModelExpression()
		{
			var model = new TestModel();
			
			validationContext.When(model, s => s.Name)
				.Is(() => false)
				.AddError(() => new ValidationMessage(() => "template1"));
			
			validationContext.When(model, s => s.Name)
				.Is(() => true)
				.AddError(() => new ValidationMessage(() => "template2"));

			Assert.False(validationContext.IsValid(model, s => s.Name));
			
			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("name", error.Key);
			Assert.Equal("template2", error.Message);
		}
	}
}