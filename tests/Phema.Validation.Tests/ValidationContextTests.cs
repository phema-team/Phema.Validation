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
				.AddPhemaValidation()
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
	}
}