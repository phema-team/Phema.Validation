using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Phema.Validation.Tests
{
	public class HierarchicalValidationOutputFormatterTests
	{
		private readonly IValidationContext validationContext;

		public HierarchicalValidationOutputFormatterTests()
		{
			validationContext = new ServiceCollection()
				.AddPhemaValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}
		
		[Fact]
		public void GlobalMessage()
		{
			validationContext.AddError(() => new ValidationMessage(() => "message1"));
			validationContext.AddError(() => new ValidationMessage(() => "message2"));
			validationContext.AddError(() => new ValidationMessage(() => "message3"));
			
			var options = Options.Create(new ValidationOptions());
			
			var result = new HierarchicalValidationOutputFormatter(options).FormatOutput(validationContext.Errors);

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
			validationContext.When("messages")
				.AddError(() => new ValidationMessage(() => "message1"));
			validationContext.When("messages")
				.AddError(() => new ValidationMessage(() => "message2"));
			validationContext.When("messages")
				.AddError(() => new ValidationMessage(() => "message3"));
			
			var options = Options.Create(new ValidationOptions());
			
			var result = new HierarchicalValidationOutputFormatter(options).FormatOutput(validationContext.Errors);

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
			validationContext.When("message1")
				.AddError(() => new ValidationMessage(() => "message1"));
			validationContext.When("message2")
				.AddError(() => new ValidationMessage(() => "message2"));
			validationContext.When("message3")
				.AddError(() => new ValidationMessage(() => "message3"));
			validationContext.When("message3")
				.AddError(() => new ValidationMessage(() => "message4"));
			
			var options = Options.Create(new ValidationOptions());
			
			var result = new HierarchicalValidationOutputFormatter(options).FormatOutput(validationContext.Errors);

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
			validationContext.When("address")
				.AddError(() => new ValidationMessage(() => "Address is invalid1"));
			validationContext.When("address")
				.AddError(() => new ValidationMessage(() => "Address is invalid2"));
			validationContext.When("address:street")
				.AddError(() => new ValidationMessage(() => "Street is invalid1"));
			validationContext.When("address:street")
				.AddError(() => new ValidationMessage(() => "Street is invalid2"));
			
			var options = Options.Create(new ValidationOptions());
			
			var result = new HierarchicalValidationOutputFormatter(options).FormatOutput(validationContext.Errors);

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
			validationContext.When("address")
				.AddError(() => new ValidationMessage(() => "Address is invalid1"));
			validationContext.When("address:street")
				.AddError(() => new ValidationMessage(() => "Street is invalid1"));
			validationContext.When("address:street")
				.AddError(() => new ValidationMessage(() => "Street is invalid2"));

			var options = Options.Create(new ValidationOptions());
			
			var result = new HierarchicalValidationOutputFormatter(options).FormatOutput(validationContext.Errors);

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
			validationContext.When("address")
				.AddError(() => new ValidationMessage(() => "Address is invalid1"));
			validationContext.When("address:street")
				.AddError(() => new ValidationMessage(() => "Street is invalid1"));
			validationContext.When("address:street")
				.AddError(() => new ValidationMessage(() => "Street is invalid2"));
			validationContext.When("address:road")
				.AddError(() => new ValidationMessage(() => "Road is invalid1"));
			
			var options = Options.Create(new ValidationOptions());
			
			var result = new HierarchicalValidationOutputFormatter(options).FormatOutput(validationContext.Errors);

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
			validationContext.When("address")
				.AddError(() => new ValidationMessage(() => "Address is invalid1"));
			validationContext.When("address")
				.AddError(() => new ValidationMessage(() => "Address is invalid2"));
			validationContext.When("address:street")
				.AddError(() => new ValidationMessage(() => "Street is invalid1"));
			validationContext.When("address:street")
				.AddError(() => new ValidationMessage(() => "Street is invalid2"));
			validationContext.When("address:road")
				.AddError(() => new ValidationMessage(() => "Road is invalid1"));
			validationContext.When("address:road")
				.AddError(() => new ValidationMessage(() => "Road is invalid2"));

			var options = Options.Create(new ValidationOptions());
			
			var result = new HierarchicalValidationOutputFormatter(options).FormatOutput(validationContext.Errors);

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
			validationContext.When("address:street")
				.AddError(() => new ValidationMessage(() => "Street is invalid1"));
			validationContext.When("address:street")
				.AddError(() => new ValidationMessage(() => "Street is invalid2"));
			validationContext.When("address")
				.AddError(() => new ValidationMessage(() => "Address is invalid1"));
			validationContext.When("address")
				.AddError(() => new ValidationMessage(() => "Address is invalid2"));
			
			var options = Options.Create(new ValidationOptions());
			
			var result = new HierarchicalValidationOutputFormatter(options).FormatOutput(validationContext.Errors);

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
			validationContext.When()
				.AddError(() => new ValidationMessage(() => "Global is invalid"));
			validationContext.When("nested")
				.AddError(() => new ValidationMessage(() => "Nested is invalid"));
			validationContext.When("address:street")
				.AddError(() => new ValidationMessage(() => "Street is invalid1"));
			validationContext.When("address:street")
				.AddError(() => new ValidationMessage(() => "Street is invalid2"));
			validationContext.When("address")
				.AddError(() => new ValidationMessage(() => "Address is invalid1"));
			validationContext.When("address")
				.AddError(() => new ValidationMessage(() => "Address is invalid2"));
			validationContext.When("address:city")
				.AddError(() => new ValidationMessage(() => "City is invalid"));
			
			var options = Options.Create(new ValidationOptions());
			
			var result = new HierarchicalValidationOutputFormatter(options).FormatOutput(validationContext.Errors);
			
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