using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationResultTests
	{
		[Fact]
		public void ValidationResultMultipleErrors()
		{
			var errors = new KeyValidationOutputFormatter().FormatOutput(new List<IValidationError>
			{
				new ValidationError("key1", "template1", ValidationSeverity.Debug),
				new ValidationError("key2", "template2", ValidationSeverity.Debug),
			});
			
			var result = new ValidationResult(errors);

			Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);

			var dictionary = Assert.IsType<Dictionary<string, object>>(result.Value);
			Assert.Equal(2, dictionary.Count);

			Assert.Collection(dictionary,
				item =>
				{
					Assert.Equal("key1", item.Key);
					Assert.Equal("template1", Assert.IsType<string>(item.Value));
				},
				item =>
				{
					Assert.Equal("key2", item.Key);
					Assert.Equal("template2", Assert.IsType<string>(item.Value));
				});
		}

		[Fact]
		public void ValidationResultMultipleErrorsSameKey()
		{
			var errors = new KeyValidationOutputFormatter().FormatOutput(new List<IValidationError>
			{
				new ValidationError("key", "template1", ValidationSeverity.Debug),
				new ValidationError("key", "template2", ValidationSeverity.Debug),
			});
			
			var result = new ValidationResult(errors);

			Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);

			var dictionary = Assert.IsType<Dictionary<string, object>>(result.Value);
			Assert.Single(dictionary);

			Assert.Collection(dictionary,
				item =>
				{
					Assert.Equal("key", item.Key);

					var messages = Assert.IsType<string[]>(item.Value);

					Assert.Equal(2, messages.Length);
					Assert.Collection(messages,
						message => Assert.Equal("template1", message),
						message => Assert.Equal("template2", message));
				});
		}
		
		[Fact]
		public void ValidationResultCombinationOfMultipleErrorsSameKeyAndSingleError()
		{
			var errors = new KeyValidationOutputFormatter().FormatOutput(new List<IValidationError>
			{
				new ValidationError("key1", "template1", ValidationSeverity.Debug),
				new ValidationError("key1", "template2", ValidationSeverity.Debug),
				new ValidationError("key2", "template1", ValidationSeverity.Debug),
			});
			
			var result = new ValidationResult(errors);

			Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);

			var dictionary = Assert.IsType<Dictionary<string, object>>(result.Value);
			Assert.Equal(2, dictionary.Count);

			Assert.Collection(dictionary,
				item =>
				{
					Assert.Equal("key1", item.Key);

					var messages = Assert.IsType<string[]>(item.Value);

					Assert.Equal(2, messages.Length);
					Assert.Collection(messages,
						message => Assert.Equal("template1", message),
						message => Assert.Equal("template2", message));
				},
				item =>
				{
					Assert.Equal("key2", item.Key);
					Assert.Equal("template1", Assert.IsType<string>(item.Value));
				});
		}
	}
}