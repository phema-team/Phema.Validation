using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationResultTests
	{
		private readonly IValidationContext validationContext;

		public ValidationResultTests()
		{
			validationContext = new ServiceCollection()
				.AddPhemaValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}
		
		[Fact]
		public void ValidationResultMultipleErrors()
		{
			validationContext.When("key1")
				.AddError(() => new ValidationMessage(() => "template1"));
			validationContext.When("key2")
				.AddError(() => new ValidationMessage(() => "template2"));
			
			var result = new ValidationResult(new SimpleValidationOutputFormatter(), validationContext.Errors);

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
			validationContext.When("key")
				.AddError(() => new ValidationMessage(() => "template1"));
			validationContext.When("key")
				.AddError(() => new ValidationMessage(() => "template2"));
			
			var result = new ValidationResult(new SimpleValidationOutputFormatter(), validationContext.Errors);

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
			validationContext.When("key1")
				.AddError(() => new ValidationMessage(() => "template1"));
			validationContext.When("key1")
				.AddError(() => new ValidationMessage(() => "template2"));
			validationContext.When("key2")
				.AddError(() => new ValidationMessage(() => "template1"));
			
			var result = new ValidationResult(new SimpleValidationOutputFormatter(), validationContext.Errors);

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