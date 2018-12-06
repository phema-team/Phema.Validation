using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Microsoft.Extensions.Options;
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

			[DataMember(Name = "floor")]
			public Floor Floor { get; set; }
		}
		
		[DataContract]
		public class Floor
		{
			[DataMember(Name = "list")]
			public IList<Room> List { get; set; }
			
			[DataMember(Name = "array")]
			public Room[] Array { get; set; }
		}
		
		[DataContract]
		public class Room
		{
			[DataMember(Name = "name")]
			public string Name { get; set; }
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
			validationContext = new ValidationContext(null, Options.Create(new ValidationOptions()));
		}

		[Fact]
		public void ExpressionEquality()
		{
			Expression<Func<Person, string>> e = p => p.Name;

			Expression<Func<Person, string>> e2 = p => p.Address.Street;
			
			Assert.False(e.Equals(e2));
			Assert.NotEqual(e.GetHashCode(), e2.GetHashCode());
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

			Expression<Func<Person, Children>> e = p => p.List[0];

			var error = validationContext.When(person, e)
				.Is(() => true)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.NotNull(error);
			Assert.Equal("list:0", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void NamedIndexedArrayWithJoinedExpression()
		{
			var person = new Person
			{
				Array = new[] { new Children() }
			};

			var error = validationContext.When(person, p => p.Array[0].Name)
				.Is(() => true)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.NotNull(error);
			Assert.Equal("array:0:name", error.Key);
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
		public void NamedIndexedMemberArrayWithJoinedExpression()
		{
			var person = new Person
			{
				Array = new[] { new Children(), new Children() }
			};

			var index = 1;

			var error = validationContext.When(person, p => p.Array[index].Name)
				.Is(() => true)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.NotNull(error);
			Assert.Equal("array:1:name", error.Key);
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
		public void NamedIndexedMemberArrayWithJoinedAndIndexedExpression()
		{
			var person = new Person
			{
				Array = new [] { new Children { Address = new Address() } }
			};

			var error = validationContext.When(person, p => p.Array[0].Address.Street)
				.Is(() => true)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.NotNull(error);
			Assert.Equal("array:0:address:street", error.Key);
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
		public void NamedIndexedMemberArrayWithJoinedAndIndexedMemberExpression()
		{
			var person = new Person
			{
				Array = new []{ new Children(), new Children { Address = new Address() } }
			};

			var index = 1;
			
			var error = validationContext.When(person, p => p.Array[index].Address.Street)
				.Is(() => true)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.NotNull(error);
			Assert.Equal("array:1:address:street", error.Key);
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
		
		public class Dimensional
		{
			[DataMember(Name = "array")]
			public int[,] Array { get; set; }
		}
		
		[Fact]
		public void IndexedListByNestedProperties()
		{
			var person = new Person
			{
				List = new[] { new Children(), new Children() }
			};

			var model = new
			{
				Index = 1
			};
			
			var error = validationContext.When(person, d => d.List[model.Index])
				.Is(() => true)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.NotNull(error);
			Assert.Equal("list:1", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void IndexedArrayByNestedProperties()
		{
			var person = new Person
			{
				Array = new[] { new Children(), new Children() }
			};

			var model = new
			{
				Index = 1
			};
			
			var error = validationContext.When(person, d => d.Array[model.Index])
				.Is(() => true)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.NotNull(error);
			Assert.Equal("array:1", error.Key);
			Assert.Equal("template", error.Message);
		}
		
		[Fact]
		public void DimensionalExpression()
		{
			var dimensional = new Dimensional
			{
				Array = new[,]
				{
					{1, 2},
					{1, 2}
				}
			};
			
			var error = validationContext.When(dimensional, d => d.Array[1, 0])
				.Is(() => true)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.NotNull(error);
			Assert.Equal("array:1:0", error.Key);
			Assert.Equal("template", error.Message);
		}

		[Fact]
		public void LongIndexExpressionWithDoubleConstants()
		{
			var person = new Person
			{
				List = new List<Children>
				{
					new Children
					{
						Address = new Address
						{
							Floor = new Floor
							{
								List = new[]
								{
									new Room(), 
									new Room
									{
										Name = "room"
									}
								}
							}
						}
					}
				}
			};

			var model = new
			{
				Index1 = 0,
				Index2 = 1
			};

			var error = validationContext.When(person, p => p.List[model.Index1].Address.Floor.List[model.Index2].Name)
				.Is(value => value == "room")
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.Equal("list:0:address:floor:list:1:name", error.Key);
		}
		
		[Fact]
		public void LongIndexExpressionWithDoubleConstantsWithArray()
		{
			var person = new Person
			{
				Array = new[]
				{
					new Children
					{
						Address = new Address
						{
							Floor = new Floor
							{
								Array = new[]
								{
									new Room(), 
									new Room
									{
										Name = "room"
									}
								}
							}
						}
					}
				}
			};

			var model = new
			{
				Index1 = 0,
				Index2 = 1
			};

			var error = validationContext.When(person, p => p.Array[model.Index1].Address.Floor.Array[model.Index2].Name)
				.Is(value => value == "room")
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.Equal("array:0:address:floor:array:1:name", error.Key);
		}
		
		[Fact]
		public void ExpressionValidationKeyIndexString()
		{
			var error = validationContext.When("e", p => p[0])
				.Is(value => true)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.Equal("0", error.Key);
		}
		
		[Fact]
		public void ExpressionValidationKeyModelIndexString()
		{
			var model = new
			{
				Name = "Sarah"
			};
			
			var error = validationContext.When(model, m => m.Name[0])
				.Is(value => true)
				.AddError(() => new ValidationMessage(() => "template"));
			
			Assert.Equal("Name:0", error.Key);
		}
		
		[Fact]
		public void ExpressionValidationKey()
		{
			var key = (ExpressionValidationKey<string, int>)(Expression<Func<string, int>>)(s => s.Length);

			Assert.Equal("Length", key.Key);
		}
		
		[Fact]
		public void NullExpressionValidationKey()
		{
			var exception = Assert.Throws<ArgumentNullException>(
				() => (ExpressionValidationKey<string, int>)(Expression<Func<string, int>>)(null));

			Assert.Equal("expression", exception.ParamName);
		}

		[Fact]
		public void ExpressionValidationKey_NotMemberExpression()
		{
			var key = (ExpressionValidationKey<string, int>)(Expression<Func<string, int>>)(s => s.GetHashCode());
			
			Assert.Equal("", key.Key);
		}
	}
}