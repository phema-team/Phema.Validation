using System;
using System.Globalization;
using Xunit;

namespace Phema.Validation
{
	public class ValidationMessageTests
	{
		[Fact]
		public void ValidationMessage()
		{
			var message = new ValidationMessage(() => "message");

			Assert.Equal("message", message.TemplateProvider());
		}
		
		[Fact]
		public void NullValidationMessage()
		{
			var message = new ValidationMessage(() => null);

			Assert.Null(message.TemplateProvider());
		}

		[Fact]
		public void ValidationMessageWithParameter()
		{
			var message = new ValidationMessage(() => "{0}");

			Assert.Equal("1", message.GetMessage(new object[]
			{
				1
			}, CultureInfo.InvariantCulture));
		}

		[Fact]
		public void ValidationMessageWithParameters()
		{
			var message = new ValidationMessage(() => "{0}{1}");

			Assert.Equal("12", message.GetMessage(new object[]
			{
				1, 2
			}, CultureInfo.InvariantCulture));
		}

		[Fact]
		public void ValidationMessageWithInvalidParameterCount()
		{
			var message = new ValidationMessage(() => "{0}");

			Assert.Equal("1", message.GetMessage(new object[]
			{
				1, 2
			}, CultureInfo.InvariantCulture));
		}

		[Fact]
		public void AddMessageWithInvalidArguments()
		{
			var validationContext = new ValidationContext();

			var error = validationContext.When("key", "value")
				.Is(value => true)
				.Add(() => new ValidationMessage(() => "{0}"), new object[]{10}, ValidationSeverity.Error);

			Assert.Equal("key", error.Key);
			Assert.Equal("10", error.Message);
		}
	}
}