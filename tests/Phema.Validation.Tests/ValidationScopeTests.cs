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

			var (key, _) = withPrefix.When("key", "value").AddValidationError("Error");

			var validationDetail = Assert.Single(validationContext.ValidationDetails);

			Assert.Equal("path.key", key);
			Assert.Equal("path.key", validationDetail.ValidationKey);
		}

		[Fact]
		public void DoubleInnerCreate()
		{
			var withPrefix = validationContext.CreateScope("path1");
			withPrefix = withPrefix.CreateScope("path2");

			var (key, _) = withPrefix.When("key", "value").AddValidationError("Error");

			Assert.Equal("path1.path2.key", key);
		}

		[Fact]
		public void IsValid_InnerPath_InnerValidationContext()
		{
			var withPrefix = validationContext.CreateScope("path");

			withPrefix.When("key", "value").AddValidationError("Error");

			Assert.False(withPrefix.IsValid("key"));
			Assert.False(validationContext.IsValid("path.key"));

			Assert.True(validationContext.IsValid("key"));
		}

		[Fact]
		public void NullPart_PathNotNull_IsValid()
		{
			var validationScope = validationContext.CreateScope("Scope");

			validationScope.When("key").AddValidationError("Error");

			Assert.False(validationScope.IsValid());
		}

		[Fact]
		public void ValidationScope_OwnCollection_AddDetailsToRoot()
		{
			validationContext.When("key").AddValidationError("Error");

			var validationScope = validationContext.CreateScope("Scope");

			Assert.Empty(validationScope.ValidationDetails);

			validationScope.When("Key").AddValidationError("Error");

			Assert.Single(validationScope.ValidationDetails);
			Assert.Equal(2, validationContext.ValidationDetails.Count);
		}

		[Fact]
		public void DoubleValidationScope()
		{
			validationContext.When("key").AddValidationError("Error");

			var validationScope1 = validationContext.CreateScope("Scope1");
			Assert.Empty(validationScope1.ValidationDetails);
			validationScope1.When("Key").AddValidationError("Error");

			var validationScope2 = validationScope1.CreateScope("Scope2");
			Assert.Empty(validationScope2.ValidationDetails);
			validationScope2.When("Key").AddValidationError("Error");

			// Only from scope2
			Assert.Single(validationScope2.ValidationDetails);

			// Scope1 and scope2 - inheritance
			Assert.Equal(2, validationScope1.ValidationDetails.Count);

			// Context, Scope1, Scope2
			Assert.Equal(3, validationContext.ValidationDetails.Count);
		}

		[Fact]
		public void ScopeDoesNotRemoveValidationContextDetails()
		{
			var validationDetail = validationContext.When("key").AddValidationError("Error");

			var scope = validationContext.CreateScope("Scope1");

			scope.ValidationDetails.Remove(validationDetail);

			Assert.Single(validationContext.ValidationDetails);
		}
	}
}