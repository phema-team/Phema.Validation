using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationScopeTests
	{
		private readonly IValidationContext validationContext;

		public ValidationScopeTests()
		{
			validationContext = new ServiceCollection()
				.AddValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void InnerPath()
		{
			var withPrefix = validationContext.CreateScope("path");

			var (key, _) = withPrefix.When("key", "value").AddError("Error");

			var validationMessage = Assert.Single(validationContext.ValidationDetails);

			Assert.Equal("path.key", key);
			Assert.Equal("path.key", validationMessage.ValidationKey);
		}

		[Fact]
		public void DoubleInnerCreate()
		{
			var withPrefix = validationContext.CreateScope("path1");
			withPrefix = withPrefix.CreateScope("path2");

			var (key, _) = withPrefix.When("key", "value").AddError("Error");

			Assert.Equal("path1.path2.key", key);
		}

		[Fact]
		public void IsValid_InnerPath_InnerValidationContext()
		{
			var withPrefix = validationContext.CreateScope("path");

			withPrefix.When("key", "value").AddError("Error");

			Assert.False(withPrefix.IsValid("key"));
			Assert.False(validationContext.IsValid("path.key"));

			Assert.True(validationContext.IsValid("key"));
		}

		[Fact]
		public void NullPart_PathNotNull_IsValid()
		{
			var validationScope = validationContext.CreateScope("Scope");

			validationScope.When("key")
				.Is(() => true)
				.AddError("Error");

			Assert.False(validationScope.IsValid());
		}
	}
}