using System;
using Microsoft.Extensions.Options;
using Xunit;

namespace Phema.Validation
{
	public class ValidationMessageTests
	{
		[Fact]
		public void ValidationMessage()
		{
			var message = new ValidationMessage(() => "message");

			Assert.Equal("message", message.TemplateProvider(null));
		}
		
		[Fact]
		public void NullValidationMessage()
		{
			var message = new ValidationMessage(() => null);

			Assert.Null(message.TemplateProvider(null));
		}

		[Fact]
		public void Throws_ValidationMessageWithNullArguments()
		{
			var message = new ValidationMessage(args => $"{args[0]}");

			Assert.Throws<ArgumentNullException>(() => message.GetMessage(null));
		}
		
		[Fact]
		public void ValidationMessageWithParameter()
		{
			var message = new ValidationMessage(args => $"{args[0]}");

			Assert.Equal("1", message.GetMessage(new object[] { 1 }));
		}
		
		[Fact]
		public void ValidationMessageWithParameters()
		{
			var message = new ValidationMessage(args => $"{args[0]}{args[1]}");

			Assert.Equal("12", message.GetMessage(new object[] { 1, 2 }));
		}

		[Fact]
		public void ValidationMessageWithInvalidParameterCount()
		{
			var message = new ValidationMessage(args => $"{args[0]}");

			Assert.Equal("1", message.GetMessage(new object[] { 1, 2 }));
		}

		[Fact]
		public void AddMessageWithInvalidArguments()
		{
			var validationContext = new ValidationContext(null, Options.Create(new ValidationOptions()));

			var error = validationContext.Validate((ValidationKey)"key", "value")
				.Is(value => true)
				.Add(() => new ValidationMessage(args => $"{args[0]}"), new object[]{10}, ValidationSeverity.Error);

			Assert.Equal("key", error.Key);
			Assert.Equal("10", error.Message);
		}
	}
}