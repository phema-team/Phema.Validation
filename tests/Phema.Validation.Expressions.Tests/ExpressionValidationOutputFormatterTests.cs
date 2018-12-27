using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ExpressionValidationOutputFormatterTests
	{
		[Fact]
		public void GlobalMessage()
		{
			var errors = new List<IValidationError>
			{
				new ValidationError("", "message1", ValidationSeverity.Error),
				new ValidationError("", "message2", ValidationSeverity.Error),
				new ValidationError("", "message3", ValidationSeverity.Error)
			};

			var options = Options.Create(new ValidationOptions());
			
			var result = new ExpressionValidationOutputFormatter(options).FormatOutput(errors);

			var (key, value) = Assert.Single(result);
			
			Assert.Equal("", key);

			var messages = Assert.IsType<List<string>>(value);
			
			Assert.Equal(3, messages.Count);
			
			Assert.Collection(messages,
				m1 => Assert.Equal("message1", m1),
				m2 => Assert.Equal("message2", m2),
				m3 => Assert.Equal("message3", m3));
		}
		
		[Fact]
		public void SimpleNamedMessage()
		{
			var errors = new List<IValidationError>
			{
				new ValidationError("messages", "message1", ValidationSeverity.Error),
				new ValidationError("messages", "message2", ValidationSeverity.Error),
				new ValidationError("messages", "message3", ValidationSeverity.Error)
			};

			var options = Options.Create(new ValidationOptions());
			
			var result = new ExpressionValidationOutputFormatter(options).FormatOutput(errors);

			var (key, value) = Assert.Single(result);
			
			Assert.Equal("messages", key);

			var messages = Assert.IsType<List<string>>(value);
			
			Assert.Equal(3, messages.Count);
			
			Assert.Collection(messages,
				m1 => Assert.Equal("message1", m1),
				m2 => Assert.Equal("message2", m2),
				m3 => Assert.Equal("message3", m3));
		}
		
		[Fact]
		public void SimpleNamedMessagesInSameTier()
		{
			var errors = new List<IValidationError>
			{
				new ValidationError("message1", "message1", ValidationSeverity.Error),
				new ValidationError("message2", "message2", ValidationSeverity.Error),
				new ValidationError("message3", "message3", ValidationSeverity.Error),
				new ValidationError("message3", "message4", ValidationSeverity.Error)
			};

			var options = Options.Create(new ValidationOptions());
			
			var result = new ExpressionValidationOutputFormatter(options).FormatOutput(errors);

			Assert.Equal(3, result.Count);
			
			Assert.Collection(result,
				r =>
				{
					Assert.Equal("message1", r.Key);

					var message = Assert.IsType<string>(r.Value);
					
					Assert.Equal("message1", message);
				},
				r =>
				{
					Assert.Equal("message2", r.Key);

					var message = Assert.IsType<string>(r.Value);
					
					Assert.Equal("message2", message);
				},
				r =>
				{
					Assert.Equal("message3", r.Key);

					var messages = Assert.IsType<List<string>>(r.Value);
					
					Assert.Equal(2, messages.Count);
					
					Assert.Collection(messages,
						m1 => Assert.Equal("message3", m1),
						m2 => Assert.Equal("message4", m2));
				});
		}

		[Fact]
		public void MessageAndNestedMessage_GlobalPrefix()
		{
			var errors = new List<IValidationError>
			{
				new ValidationError("address", "Address is invalid1", ValidationSeverity.Error),
				new ValidationError("address", "Address is invalid2", ValidationSeverity.Error),
				new ValidationError("address:street", "Street is invalid1", ValidationSeverity.Error),
				new ValidationError("address:street", "Street is invalid2", ValidationSeverity.Error)
			};

			var options = Options.Create(new ValidationOptions());
			
			var result = new ExpressionValidationOutputFormatter(options).FormatOutput(errors);
			
			Assert.Equal(2, result.Count);
			
			Assert.Collection(result,
				r =>
				{
					Assert.Equal("", r.Key);

					var messages = Assert.IsType<List<string>>(r.Value);
					
					Assert.Collection(messages,
						m1 => Assert.Equal("Address is invalid1", m1),
						m2 => Assert.Equal("Address is invalid2", m2));
				});

		}
	}
}