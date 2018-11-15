using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Phema.Validation
{
	public class ValidationMessageTests
	{
		private readonly IValidationContext validationContext;

		public ValidationMessageTests()
		{
			validationContext = new ValidationContext();
		}

		[Fact]
		public void ValidationMessage()
		{
			var message = new ValidationMessage(() => "message");

			Assert.Equal("message", message.Template());
		}

		[Fact]
		public void ValidationMessageWithParameter()
		{
			var message = new ValidationMessage(() => "{0}");

			Assert.Equal("1", message.GetMessage(new object[] { 1 }));
		}

		[Fact]
		public void ValidationMessageWithParameters()
		{
			var message = new ValidationMessage(() => "{0}{1}");

			Assert.Equal("1", message.GetMessage(new object[] { 1, 2 }));
		}

		[Fact]
		public void ValidationMessageWithInvalidParameterCount()
		{
			var message = new ValidationMessage(() => "{0}");

			Assert.Equal("1", message.GetMessage(new object[] { 1, 2 }));
		}

		[Fact]
		public void ValidationMessageWithInvalidParameterCount_1()
		{
			var message = new ValidationMessage<int>(() => "{0}");
			var exception = Assert.Throws<ArgumentException>(() =>  message.GetMessage(new object[] { 1, 2 }));

			Assert.Equal("arguments", exception.Message);
		}

		[Fact]
		public void ValidationMessageWithInvalidParameterCount_2()
		{
			var message = new ValidationMessage<int, int>(() => "{0}{1}");
			var exception = Assert.Throws<ArgumentException>(() => message.GetMessage(new object[] { 1 }));

			Assert.Equal("arguments", exception.Message);
		}
	}
}
