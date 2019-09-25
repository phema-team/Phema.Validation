using System;
using Microsoft.Extensions.DependencyInjection;
using Phema.Validation.Conditions;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConditionIsTypeExtensionTests
	{
		private readonly IValidationContext validationContext;

		public ValidationConditionIsTypeExtensionTests()
		{
			validationContext = new ServiceCollection()
				.AddValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void IsType_StringType_Invalid()
		{
			var (key, message) = validationContext.When("name", "john")
				.IsType(typeof(string))
				.AddValidationError("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsIsType_IntType_Valid()
		{
			validationContext.When("name", "john")
				.IsType(typeof(int))
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsTypeOfString_NextChecksValid_Invalid()
		{
			var (key, message) = validationContext.When("name", (object)"john")
				.IsType<string>(typed => typed.Is(value => value.Length == 4))
				.AddValidationError("template1");

			Assert.Equal("name", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsTypeOfTType_TypeChecks_Valid()
		{
			validationContext.When("name", (object)"john")
				// Never called because type is string
				.IsType<int>(typed => typed.Is(value => throw new Exception()))
				.AddValidationError("template1");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void IsType_StringAndIntType_AndJoin_Invalid()
		{
			var condition = validationContext.When("name", "john");

			var stringCondition = condition.IsType<string>();
			Assert.False(stringCondition.IsValid);

			var intCondition = stringCondition.IsType<int>();
			Assert.True(intCondition.IsValid);
		}

		[Fact]
		public void IsTypeOfTType_AllConditionsPassed_Invalid()
		{
			var validationDetail = validationContext.When("name", "john")
				.IsEqual("john")
				.IsType<string>()
				.IsEqual("john")
				.AddValidationError("error");

			// Because all checks passed
			Assert.NotNull(validationDetail);
		}

		[Fact]
		public void IsTypeOfTType_TypeConditionsFailed_Valid()
		{
			var validationDetail = validationContext.When("name", "john")
				.IsEqual("john")
				.IsType<int>(typed => typed.Is(() => throw new Exception()))
				.AddValidationError("error");

			// Because string is not of type int
			Assert.Null(validationDetail);
		}

		[Fact]
		public void IsTypeOfTType_NextConditionsFailed_Valid()
		{
			var validationDetail = validationContext.When("name", "john")
				.IsEqual("john")
				.IsType<string>()
				.IsEqual("sarah")
				.AddValidationError("error");

			// Because sarah != john
			Assert.Null(validationDetail);
		}

		[Fact]
		public void IsTypeOfTType_NoPrecondition_NextConditionsFailed_Valid()
		{
			var validationDetail = validationContext.When("name", "john")
				.IsType<string>()
				.IsEqual("sarah")
				.AddValidationError("error");

			// Because sarah != john
			Assert.Null(validationDetail);
		}

		[Fact]
		public void IsTypeOfTType_NoPrecondition_TypeChecksFailed_Valid()
		{
			var validationDetail = validationContext.When("name", "john")
				.IsType<int>()
				.AddValidationError("error");

			// Because string is not typeof int
			Assert.Null(validationDetail);
		}

		[Fact]
		public void IsTypeOfTType_NePrecondition_TypeChecksPassed_Invalid()
		{
			var validationDetail = validationContext.When("name", "john")
				.IsType<string>()
				.AddValidationError("error");

			Assert.NotNull(validationDetail);
		}
	}
}