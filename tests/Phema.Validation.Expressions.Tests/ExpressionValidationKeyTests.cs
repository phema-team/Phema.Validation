using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ExpressionValidationKeyTests
	{
		private readonly IValidationContext validationContext;

		public ExpressionValidationKeyTests()
		{
			validationContext = new ServiceCollection()
				.AddPhemaValidation(configuration =>
					configuration.AddComponent<TestModel, TestModelValidationComponent>())
				.ConfigurePhemaValidationExpressions(o => o.UseDataContractPrefix = true)
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void ExpressionEquality()
		{
			Expression<Func<TestModel, string>> e = p => p.String;
			Expression<Func<TestModel, string>> e2 = p => p.String;

			Assert.False(e.Equals(e2));
			Assert.NotEqual(e.GetHashCode(), e2.GetHashCode());
		}
		
		[Fact]
		public void NamedExpression()
		{
			var model = new TestModel();

			var error = validationContext.When(model, m => m.String)
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.NotNull(error);
			Assert.Equal("test:string", error.Key);
			Assert.Equal("template1", error.Message);
		}

		[Fact]
		public void NamedJoinedExpression()
		{
			var model = new TestModel
			{
				Nested = new TestModel()
			};

			var error = validationContext.When(model, m => m.Nested.String)
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.NotNull(error);
			Assert.Equal("test:nested:string", error.Key);
			Assert.Equal("template1", error.Message);
		}

		[Fact]
		public void NamedIndexedListExpression()
		{
			var model = new TestModel
			{
				List = new List<TestModel> { new TestModel() }
			};

			var error = validationContext.When(model, m => m.List[0])
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.NotNull(error);
			Assert.Equal("test:list:0", error.Key);
			Assert.Equal("template1", error.Message);
		}

		[Fact]
		public void NamedIndexedArrayWithJoinedExpression()
		{
			var model = new TestModel
			{
				Array = new[]
				{
					new TestModel()
				}
			};

			var error = validationContext.When(model, m => m.Array[0].String)
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.NotNull(error);
			Assert.Equal("test:array:0:string", error.Key);
			Assert.Equal("template1", error.Message);
		}

		[Fact]
		public void NamedIndexedListWithJoinedExpression()
		{
			var model = new TestModel
			{
				List = new List<TestModel>
				{
					new TestModel()
				}
			};

			var error = validationContext.When(model, m => m.List[0].String)
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.NotNull(error);
			Assert.Equal("test:list:0:string", error.Key);
			Assert.Equal("template1", error.Message);
		}

		[Fact]
		public void NamedIndexedMemberArrayWithJoinedExpression()
		{
			var model = new TestModel
			{
				Array = new[]
				{
					new TestModel(), new TestModel()
				}
			};

			var index = 1;

			var error = validationContext.When(model, m => m.Array[index].String)
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.NotNull(error);
			Assert.Equal("test:array:1:string", error.Key);
			Assert.Equal("template1", error.Message);
		}

		[Fact]
		public void NamedIndexedMemberListWithJoinedExpression()
		{
			var model = new TestModel
			{
				List = new List<TestModel>
				{
					new TestModel(), new TestModel()
				}
			};

			var index = 1;

			var error = validationContext.When(model, p => p.List[index].String)
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.NotNull(error);
			Assert.Equal("test:list:1:string", error.Key);
			Assert.Equal("template1", error.Message);
		}

		[Fact]
		public void NamedIndexedMemberArrayWithJoinedAndIndexedExpression()
		{
			var model = new TestModel
			{
				Array = new[]
				{
					new TestModel
					{
						Nested = new TestModel()
					}
				}
			};

			var error = validationContext.When(model, p => p.Array[0].Nested.String)
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.NotNull(error);
			Assert.Equal("test:array:0:nested:string", error.Key);
			Assert.Equal("template1", error.Message);
		}

		[Fact]
		public void NamedIndexedMemberListWithJoinedAndIndexedExpression()
		{
			var model = new TestModel
			{
				List = new List<TestModel>
				{
					new TestModel
					{
						Nested = new TestModel()
					}
				}
			};

			var error = validationContext.When(model, p => p.List[0].Nested.String)
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.NotNull(error);
			Assert.Equal("test:list:0:nested:string", error.Key);
			Assert.Equal("template1", error.Message);
		}

		[Fact]
		public void NamedIndexedMemberArrayWithJoinedAndIndexedMemberExpression()
		{
			var person = new TestModel
			{
				Array = new[]
				{
					new TestModel(), new TestModel
					{
						Nested = new TestModel()
					}
				}
			};

			var index = 1;

			var error = validationContext.When(person, p => p.Array[index].Nested.String)
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.NotNull(error);
			Assert.Equal("test:array:1:nested:string", error.Key);
			Assert.Equal("template1", error.Message);
		}

		[Fact]
		public void NamedIndexedMemberListWithJoinedAndIndexedMemberExpression()
		{
			var person = new TestModel
			{
				List = new List<TestModel>
				{
					new TestModel(),
					new TestModel
					{
						Nested = new TestModel()
					}
				}
			};

			var index = 1;

			var error = validationContext.When(person, p => p.List[index].Nested.String)
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.NotNull(error);
			Assert.Equal("test:list:1:nested:string", error.Key);
			Assert.Equal("template1", error.Message);
		}

		[Fact]
		public void IndexedListByNestedProperties()
		{
			var person = new TestModel 
			{
				List = new List<TestModel>
				{
					new TestModel(), new TestModel()
				}
			};

			var model = new
			{
				Index = 1
			};

			var error = validationContext.When(person, d => d.List[model.Index])
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.NotNull(error);
			Assert.Equal("test:list:1", error.Key);
			Assert.Equal("template1", error.Message);
		}

		[Fact]
		public void IndexedArrayByNestedProperties()
		{
			var person = new TestModel
			{
				Array = new[]
				{
					new TestModel(), new TestModel()
				}
			};

			var model = new
			{
				Index = 1
			};

			var error = validationContext.When(person, d => d.Array[model.Index])
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.NotNull(error);
			Assert.Equal("test:array:1", error.Key);
			Assert.Equal("template1", error.Message);
		}
		
		
		public class Dimensional
		{
			[DataMember(Name = "array")]
			public int[,] Array { get; set; }
		}

		[Fact]
		public void DimensionalExpression()
		{
			var dimensional = new Dimensional
			{
				Array = new[,]
				{
					{
						1, 2
					},
					{
						1, 2
					}
				}
			};

			var error = validationContext.When(dimensional, d => d.Array[1, 0])
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.NotNull(error);
			Assert.Equal("array:1:0", error.Key);
			Assert.Equal("template1", error.Message);
		}

		[Fact]
		public void LongIndexExpressionWithDoubleConstants()
		{
			var person = new TestModel
			{
				List = new List<TestModel>
				{
					new TestModel
					{
						Nested = new TestModel
						{
							Nested = new TestModel
							{
								List = new List<TestModel>
								{
									new TestModel(), 
									new TestModel
									{
										String = "room"
									}
								}
							}
						}
					}
				}
			};

			var model = new
			{
				Index1 = 0, Index2 = 1
			};

			var error = validationContext.When(person, p => p.List[model.Index1].Nested.Nested.List[model.Index2].String)
				.Is(value => value == "room")
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("test:list:0:nested:nested:list:1:string", error.Key);
		}

		[Fact]
		public void LongIndexExpressionWithDoubleConstantsWithArray()
		{
			var person = new TestModel
			{
				Array = new[]
				{
					new TestModel
					{
						Nested = new TestModel
						{
							Nested = new TestModel
							{
								Array = new[]
								{
									new TestModel(),
									new TestModel
									{
										String = "room"
									}
								}
							}
						}
					}
				}
			};

			var model = new
			{
				Index1 = 0, Index2 = 1
			};

			var error = validationContext.When(person, p => p.Array[model.Index1].Nested.Nested.Array[model.Index2].String)
				.Is(value => value == "room")
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("test:array:0:nested:nested:array:1:string", error.Key);
		}

		[Fact]
		public void ExpressionValidationKeyIndexString()
		{
			var error = validationContext.When("e", p => p[0])
				.Is(value => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

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
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("Name:0", error.Key);
		}

		[Fact]
		public void ExpressionValidationKey()
		{
			var key = new ExpressionValidationKey<string, int>(new ExpressionValidationOptions(), s => s.Length);

			Assert.Equal("Length", key.Key);
		}

		[Fact]
		public void NullExpressionValidationKey()
		{
			var exception = Assert.Throws<ArgumentNullException>(
				() => new ExpressionValidationKey<string, int>(new ExpressionValidationOptions(), null));

			Assert.Equal("expression", exception.ParamName);
		}

		[Fact]
		public void ExpressionValidationKey_NotMemberExpression()
		{
			var key = new ExpressionValidationKey<string, int>(new ExpressionValidationOptions(), s => s.GetHashCode());

			Assert.Equal("", key.Key);
		}
	}
}