using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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

			var address = Assert.IsType<Dictionary<string, object>>(Assert.Single(result).Value);
			
			Assert.Collection(address,
				a =>
				{
					Assert.Equal("", a.Key);

					var messages = Assert.IsType<List<string>>(a.Value);
					
					Assert.Collection(messages,
						m1 => Assert.Equal("Address is invalid1", m1),
						m2 => Assert.Equal("Address is invalid2", m2));
				},
				a =>
				{
					Assert.Equal("street", a.Key);

					var messages = Assert.IsType<List<string>>(a.Value);
					
					Assert.Collection(messages,
						m1 => Assert.Equal("Street is invalid1", m1),
						m2 => Assert.Equal("Street is invalid2", m2));
				});
		}
		
		[Fact]
		public void SingleMessageAndNestedMessage_GlobalPrefix()
		{
			var errors = new List<IValidationError>
			{
				new ValidationError("address", "Address is invalid1", ValidationSeverity.Error),
				new ValidationError("address:street", "Street is invalid1", ValidationSeverity.Error),
				new ValidationError("address:street", "Street is invalid2", ValidationSeverity.Error)
			};

			var options = Options.Create(new ValidationOptions());
			
			var result = new ExpressionValidationOutputFormatter(options).FormatOutput(errors);

			var address = Assert.IsType<Dictionary<string, object>>(Assert.Single(result).Value);
			
			Assert.Collection(address,
				a =>
				{
					Assert.Equal("", a.Key);

					var message = Assert.IsType<string>(a.Value);
					
					Assert.Equal("Address is invalid1", message);
				},
				a =>
				{
					Assert.Equal("street", a.Key);

					var messages = Assert.IsType<List<string>>(a.Value);
					
					Assert.Collection(messages,
						m1 => Assert.Equal("Street is invalid1", m1),
						m2 => Assert.Equal("Street is invalid2", m2));
				});
		}
		
		[Fact]
		public void SingleMessageAndNestedMessage_GlobalPrefix_Doubled()
		{
			var errors = new List<IValidationError>
			{
				new ValidationError("address", "Address is invalid1", ValidationSeverity.Error),
				new ValidationError("address:street", "Street is invalid1", ValidationSeverity.Error),
				new ValidationError("address:street", "Street is invalid2", ValidationSeverity.Error),
				new ValidationError("address:road", "Road is invalid1", ValidationSeverity.Error)
			};

			var options = Options.Create(new ValidationOptions());
			
			var result = new ExpressionValidationOutputFormatter(options).FormatOutput(errors);

			var address = Assert.IsType<Dictionary<string, object>>(Assert.Single(result).Value);
			
			Assert.Collection(address,
				a =>
				{
					Assert.Equal("", a.Key);

					var message = Assert.IsType<string>(a.Value);
					
					Assert.Equal("Address is invalid1", message);
				},
				a =>
				{
					Assert.Equal("street", a.Key);

					var messages = Assert.IsType<List<string>>(a.Value);
					
					Assert.Collection(messages,
						m1 => Assert.Equal("Street is invalid1", m1),
						m2 => Assert.Equal("Street is invalid2", m2));
				},
				a =>
				{
					Assert.Equal("road", a.Key);

					var message = Assert.IsType<string>(a.Value);
					
					Assert.Equal("Road is invalid1", message);
				});
		}
		
		[Fact]
		public void Multiple_MessageAndNestedMessage_GlobalPrefix_Doubled()
		{
			var errors = new List<IValidationError>
			{
				new ValidationError("address", "Address is invalid1", ValidationSeverity.Error),
				new ValidationError("address", "Address is invalid2", ValidationSeverity.Error),
				new ValidationError("address:street", "Street is invalid1", ValidationSeverity.Error),
				new ValidationError("address:street", "Street is invalid2", ValidationSeverity.Error),
				new ValidationError("address:road", "Road is invalid1", ValidationSeverity.Error),
				new ValidationError("address:road", "Road is invalid2", ValidationSeverity.Error)
			};

			var options = Options.Create(new ValidationOptions());
			
			var result = new ExpressionValidationOutputFormatter(options).FormatOutput(errors);

			var address = Assert.IsType<Dictionary<string, object>>(Assert.Single(result).Value);
			
			Assert.Collection(address,
				a =>
				{
					Assert.Equal("", a.Key);

					var messages = Assert.IsType<List<string>>(a.Value);
					
					Assert.Collection(messages,
						m1 => Assert.Equal("Address is invalid1", m1),
						m2 => Assert.Equal("Address is invalid2", m2));
				},
				a =>
				{
					Assert.Equal("street", a.Key);

					var messages = Assert.IsType<List<string>>(a.Value);
					
					Assert.Collection(messages,
						m1 => Assert.Equal("Street is invalid1", m1),
						m2 => Assert.Equal("Street is invalid2", m2));
				},
				a =>
				{
					Assert.Equal("road", a.Key);

					var messages = Assert.IsType<List<string>>(a.Value);
					
					Assert.Collection(messages,
						m1 => Assert.Equal("Road is invalid1", m1),
						m2 => Assert.Equal("Road is invalid2", m2));
				});
		}
		
		[Fact]
		public void MessageAndNestedMessage_GlobalPrefix_Reversed()
		{
			var errors = new List<IValidationError>
			{
				new ValidationError("address:street", "Street is invalid1", ValidationSeverity.Error),
				new ValidationError("address:street", "Street is invalid2", ValidationSeverity.Error),
				new ValidationError("address", "Address is invalid1", ValidationSeverity.Error),
				new ValidationError("address", "Address is invalid2", ValidationSeverity.Error),
			};

			var options = Options.Create(new ValidationOptions());
			
			var result = new ExpressionValidationOutputFormatter(options).FormatOutput(errors);

			var address = Assert.IsType<Dictionary<string, object>>(Assert.Single(result).Value);
			
			Assert.Collection(address,
				a =>
				{
					Assert.Equal("street", a.Key);

					var messages = Assert.IsType<List<string>>(a.Value);
					
					Assert.Collection(messages,
						m1 => Assert.Equal("Street is invalid1", m1),
						m2 => Assert.Equal("Street is invalid2", m2));
				},
				a =>
				{
					Assert.Equal("", a.Key);
	
					var messages = Assert.IsType<List<string>>(a.Value);
						
					Assert.Collection(messages,
						m1 => Assert.Equal("Address is invalid1", m1),
						m2 => Assert.Equal("Address is invalid2", m2));
				});
		}

		[Fact]
		public void MultipleNested_Global_Simple()
		{
			var errors = new List<IValidationError>
			{
				new ValidationError("", "Global is invalid", ValidationSeverity.Error),
				new ValidationError("nested", "Nested is invalid", ValidationSeverity.Error),
				new ValidationError("address:street", "Street is invalid1", ValidationSeverity.Error),
				new ValidationError("address:street", "Street is invalid2", ValidationSeverity.Error),
				new ValidationError("address", "Address is invalid1", ValidationSeverity.Error),
				new ValidationError("address", "Address is invalid2", ValidationSeverity.Error),
				new ValidationError("address:city", "City is invalid", ValidationSeverity.Error),
			};
			
			var options = Options.Create(new ValidationOptions());
			
			var result = new ExpressionValidationOutputFormatter(options).FormatOutput(errors);
			
			Assert.Equal(3, result.Count);
			
			Assert.Collection(result,
				r =>
				{
					Assert.Equal("", r.Key);

					var message = Assert.IsType<string>(r.Value);
					
					Assert.Equal("Global is invalid", message);
				},
				r =>
				{
					Assert.Equal("nested", r.Key);

					var city = Assert.IsType<string>(r.Value);
							
					Assert.Equal("Nested is invalid", city);
				},
				r =>
				{
					Assert.Equal("address", r.Key);

					var address = Assert.IsType<Dictionary<string, object>>(r.Value);
					
					Assert.Equal(3, address.Count);
					
					Assert.Collection(address,
						a =>
						{
							Assert.Equal("street", a.Key);

							var street = Assert.IsType<List<string>>(a.Value);
							
							Assert.Collection(street,
								s => Assert.Equal("Street is invalid1", s),
								s => Assert.Equal("Street is invalid2", s));
						},
						a =>
						{
							Assert.Equal("", a.Key);

							var addresses = Assert.IsType<List<string>>(a.Value);
							
							Assert.Collection(addresses,
								s => Assert.Equal("Address is invalid1", s),
								s => Assert.Equal("Address is invalid2", s));
						},
						a =>
						{
							Assert.Equal("city", a.Key);

							var city = Assert.IsType<string>(a.Value);
							
							Assert.Equal("City is invalid", city);
						});
				});
			
		}
	}
}