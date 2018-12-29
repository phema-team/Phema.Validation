using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ConditionValidation
	{
		private readonly IValidationContext validationContext;

		public ConditionValidation()
		{
			validationContext = new ServiceCollection()
				.AddPhemaValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void NullBuilderIsThrows()
		{
			Assert.Throws<ArgumentNullException>(() => ((IValidationCondition<object>)null).Is(value => true));
			Assert.Throws<ArgumentNullException>(() => validationContext.Validate(new ValidationKey(""), "").Is(null));
		}
		
		[Fact]
		public void NullBuilderWhenThrows()
		{
			Assert.Throws<ArgumentNullException>(() => ((IValidationCondition<object>)null).When(value => true));
			Assert.Throws<ArgumentNullException>(() => validationContext.Validate(new ValidationKey(""), "").When(null));
		}
		
		[Fact]
		public void ConditionAddSingle()
		{
			var error1 = validationContext.Validate(new ValidationKey("key"), "value")
				.Condition((value, added) =>
				{
					Assert.Equal("value", value);
					Assert.False(added);

					return true;
				})
				.Add(() => new ValidationMessage(() => "template"), Array.Empty<object>(), ValidationSeverity.Error);
			
			Assert.NotNull(error1);

			var error2 = Assert.Single(validationContext.Errors);
			Assert.Equal(error1, error2);
			
			Assert.Equal("key", error1.Key);
			Assert.Equal("template", error1.Message);
		}
		
		[Fact]
		public void ConditionAddTwo()
		{
			var error1 = validationContext.Validate(new ValidationKey("key"), "value")
				.Condition((_, __) => true)
				.Add(() => new ValidationMessage(() => "template1"), Array.Empty<object>(), ValidationSeverity.Error);
			
			var error2 = validationContext.Validate(new ValidationKey("key"), "value")
				.Condition((value, added) =>
				{
					Assert.Equal("value", value);
					Assert.True(added);

					return true;
				})
				.Add(() => new ValidationMessage(() => "template2"), Array.Empty<object>(), ValidationSeverity.Error);
			
			Assert.Equal("key", error1.Key);
			Assert.Equal("template1", error1.Message);
			
			Assert.Equal("key", error2.Key);
			Assert.Equal("template2", error2.Message);
			
			Assert.Collection(validationContext.Errors,
				e => Assert.Equal(e, error1),
				e => Assert.Equal(e, error2));
		}
	}
}