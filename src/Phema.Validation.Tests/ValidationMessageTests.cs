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
		public void ValidationMessageWithInvalidParameterCount_1()
		{
			var message = new ValidationMessage<int>(() => "{0}");
			var exception = Assert.Throws<ArgumentException>(() => message.GetMessage(new object[]
			{
				1, 2
			}, CultureInfo.InvariantCulture));

			Assert.Equal("arguments", exception.Message);
		}

		[Fact]
		public void ValidationMessageWithInvalidParameterCount_2()
		{
			var message = new ValidationMessage<int, int>(() => "{0}{1}");
			var exception = Assert.Throws<ArgumentException>(() => message.GetMessage(new object[]
			{
				1
			}, CultureInfo.InvariantCulture));

			Assert.Equal("arguments", exception.Message);
		}

		[Fact]
		public void AddMessageWithInvalidArguments()
		{
			var validationContext = new ValidationContext();

			var error = validationContext.When("key", "value")
				.Is(() => true)
				.AddError(() => new ValidationMessage<int>(() => "{0}"), 10);
			// .AddError(() => new ValidationMessage<int>(() => "{0}"), 10L);
			// .AddError(() => new ValidationMessage<int>(() => "{0}"), "");

			Assert.Equal("key", error.Key);
			Assert.Equal("10", error.Message);
		}
	}
}