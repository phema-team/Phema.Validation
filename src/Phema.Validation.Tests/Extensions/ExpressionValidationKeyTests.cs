using System.Collections.Generic;
using System.Runtime.Serialization;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ExpressionValidationKeyTests
	{
		[DataContract]
		private class Person
		{
			[DataMember(Name = "name")]
			public string Name { get; set; }
			
			[DataMember(Name = "address")]
			public Address Address { get; set; }
			
			[DataMember(Name = "list")]
			public IList<Children> List { get; set; }
			
			[DataMember(Name = "array")]
			public Children[] Array { get; set; }
		}

		[DataContract]
		private class Address
		{
			[DataMember(Name = "street")]
			public string Street { get; set; }
		}

		[DataContract]
		private class Children
		{
			[DataMember(Name = "name")]
			public string Name { get; set; }

			[DataMember(Name = "address")]
			public Address Address { get; set; }
		}

		private readonly IValidationContext validationContext;
		
		public ExpressionValidationKeyTests()
		{
			validationContext = new ValidationContext();
		}

		[Fact]
		public void NamedExpression()
		{
			var person = new Person();

			var error = validationContext.When(person, p => p.Name)
				.Is(() => true)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.NotNull(error);
			Assert.Equal("name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void NamedJoinedExpression()
		{
			var person = new Person
			{
				Address = new Address()
			};

			var error = validationContext.When(person, p => p.Address.Street)
				.Is(() => true)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.NotNull(error);
			Assert.Equal("address:street", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void NamedIndexedListExpression()
		{
			var person = new Person
			{
				List = new List<Children> { new Children() }
			};

			var error = validationContext.When(person, p => p.List[0])
				.Is(() => true)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.NotNull(error);
			Assert.Equal("list:0", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void NamedIndexedListWithJoinedExpression()
		{
			var person = new Person
			{
				List = new List<Children> { new Children() }
			};

			var error = validationContext.When(person, p => p.List[0].Name)
				.Is(() => true)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.NotNull(error);
			Assert.Equal("list:0:name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void NamedIndexedMemberListWithJoinedExpression()
		{
			var person = new Person
			{
				List = new List<Children> { new Children(), new Children() }
			};

			var index = 1;

			var error = validationContext.When(person, p => p.List[index].Name)
				.Is(() => true)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.NotNull(error);
			Assert.Equal("list:1:name", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void NamedIndexedMemberListWithJoinedAndIndexedExpression()
		{
			var person = new Person
			{
				List = new List<Children> { new Children { Address = new Address() } }
			};

			var error = validationContext.When(person, p => p.List[0].Address.Street)
				.Is(() => true)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.NotNull(error);
			Assert.Equal("list:0:address:street", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void NamedIndexedMemberListWithJoinedAndIndexedMemberExpression()
		{
			var person = new Person
			{
				List = new List<Children> { new Children(), new Children { Address = new Address() } }
			};

			var index = 1;
			
			var error = validationContext.When(person, p => p.List[index].Address.Street)
				.Is(() => true)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.NotNull(error);
			Assert.Equal("list:1:address:street", error.Key);
			Assert.Equal("template", error.Message);
		}
	}
}