using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Phema.Validation.Conditions;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationExpressionTests
	{
		private readonly IValidationContext validationContext;

		public ValidationExpressionTests()
		{
			validationContext = new ServiceCollection()
				.AddValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void ValueProvidedKey_Valid()
		{
			var model = new TestModel
			{
				Property = 12
			};

			validationContext.When(model, m => m.Property, () => 13)
				.IsEqual(13)
				.AddError("Error");

			Assert.False(validationContext.IsValid(model, m => m.Property));
		}

		[Fact]
		public void ValueProvidedKey_Invalid()
		{
			var model = new TestModel
			{
				Property = 13
			};

			validationContext.When(model, m => m.Property, () => 12)
				.IsEqual(13)
				.AddError("Error");

			Assert.Empty(validationContext.ValidationDetails);
		}

		[Fact]
		public void ExpressionKey_IsValid_SameKey()
		{
			var model = new TestModel
			{
				Property = 12
			};

			validationContext.When(model, m => m.Property).AddError("Error");

			Assert.False(validationContext.IsValid(model, m => m.Property));
		}

		[Fact]
		public void ExpressionKey_IsValid_KeyNotPresent()
		{
			var model = new TestModel
			{
				Property = 12
			};

			validationContext.When(model, m => m.Property).AddError("Error");

			Assert.True(validationContext.IsValid(model, m => m.List));
		}

		[Fact]
		public void ExpressionKey_EnsureIsValid_SameKey()
		{
			var model = new TestModel
			{
				Property = 12
			};

			validationContext.When(model, m => m.Property).AddError("Error");

			var exception = Assert.Throws<ValidationContextException>(
				() => validationContext.EnsureIsValid(model, m => m.Property));

			var validationDetail = Assert.Single(exception.ValidationDetails);

			Assert.Equal("Property", validationDetail.ValidationKey);
		}

		[Fact]
		public void ExpressionKey_EnsureIsValid_KeyNotPresent()
		{
			var model = new TestModel
			{
				Property = 12
			};

			validationContext.When(model, m => m.Property).AddError("Error");

			validationContext.EnsureIsValid(model, m => m.List);
		}

		[Fact]
		public void ExpressionKey_Property()
		{
			var model = new TestModel
			{
				Property = 12
			};

			var (key, message) = validationContext.When(model, m => m.Property)
				.Is(value =>
				{
					Assert.Equal(12, value);
					return true;
				})
				.AddError("Error");

			Assert.Equal("Property", key);
			Assert.Equal("Error", message);
		}

		[Fact]
		public void ExpressionKey_ArrayProperty_ConstantIndex()
		{
			var model = new TestModel
			{
				Array = new[] {12}
			};

			var (key, message) = validationContext.When(model, m => m.Array[0])
				.Is(value =>
				{
					Assert.Equal(12, value);
					return true;
				})
				.AddError("Error");

			Assert.Equal("Array[0]", key);
			Assert.Equal("Error", message);
		}

		[Fact]
		public void ExpressionKey_ArrayProperty_LocalIndex()
		{
			var model = new TestModel
			{
				Array = new[] {12}
			};

			// Should be a local variable for closure
			// ReSharper disable once ConvertToConstant.Local
			var index = 0;

			var (key, message) = validationContext.When(model, m => m.Array[index])
				.Is(value =>
				{
					Assert.Equal(12, value);
					return true;
				})
				.AddError("Error");

			Assert.Equal("Array[0]", key);
			Assert.Equal("Error", message);
		}

		[Fact]
		public void ExpressionKey_ListProperty_ConstantIndex()
		{
			var model = new TestModel
			{
				List = new List<int> {12}
			};

			var (key, message) = validationContext.When(model, m => m.List[0])
				.Is(value =>
				{
					Assert.Equal(12, value);
					return true;
				})
				.AddError("Error");

			Assert.Equal("List[0]", key);
			Assert.Equal("Error", message);
		}

		[Fact]
		public void ExpressionKey_ListProperty_LocalIndex()
		{
			var model = new TestModel
			{
				List = new List<int> {12}
			};

			var index = 0;

			var (key, message) = validationContext.When(model, m => m.List[index])
				.Is(value =>
				{
					Assert.Equal(12, value);
					return true;
				})
				.AddError("Error");

			Assert.Equal("List[0]", key);
			Assert.Equal("Error", message);
		}

		[Fact]
		public void CompositeIndexer()
		{
			var model = new TestModel
			{
				List = new List<int> {12}
			};

			var provider = new {ForModel = new {Index = 0}};

			var (key, message) = validationContext.When(model, m => m.List[provider.ForModel.Index]).AddError("Error");

			Assert.Equal("List[0]", key);
			Assert.Equal("Error", message);
		}

		[Fact]
		public void ExpressionKey_DoubleProperty_ConstantIndex()
		{
			var model = new TestModel
			{
				DoubleArray = new[,] {{12}}
			};

			var (key, message) = validationContext.When(model, m => m.DoubleArray[0, 0])
				.Is(value =>
				{
					Assert.Equal(12, value);
					return true;
				})
				.AddError("Error");

			Assert.Equal("DoubleArray[0, 0]", key);
			Assert.Equal("Error", message);
		}

		[Fact]
		public void ExpressionKey_DoubleProperty_LocalIndex()
		{
			var model = new TestModel
			{
				DoubleArray = new[,] {{12}}
			};

			var index = 0;

			var (key, message) = validationContext.When(model, m => m.DoubleArray[index, index])
				.Is(value =>
				{
					Assert.Equal(12, value);
					return true;
				})
				.AddError("Error");

			Assert.Equal("DoubleArray[0, 0]", key);
			Assert.Equal("Error", message);
		}

		[Fact]
		public void List_Expression_InnerPath()
		{
			var model = new TestModel
			{
				List = new List<int> {12}
			};

			var withPrefix = validationContext.CreateScope(model, m => m.List);

			var (key, _) = withPrefix.When(model.List, list => list.Count).AddError("Error");

			var validationDetail = Assert.Single(validationContext.ValidationDetails);

			Assert.Equal("List.Count", key);
			Assert.Equal("List.Count", validationDetail.ValidationKey);
		}

		[Fact]
		public void Expression_DoubleInnerCreate()
		{
			var model = new TestModel
			{
				List = new List<int> {12}
			};

			var withPrefix = validationContext.CreateScope(model, m => m.List);
			withPrefix = withPrefix.CreateScope(model, m => m.List);

			var (key, _) = withPrefix.When(model.List, c => c.Count).AddError("Error");

			Assert.Equal("List.List.Count", key);
		}

		[Fact]
		public void Expression_ChainExpressionPath()
		{
			var model = new TestModel
			{
				Model = new TestModel
				{
					Property = 12
				}
			};

			var validationDetail = validationContext.When(model, m => m.Model.Property)
				.AddError("Error");

			Assert.Equal("Model.Property", validationDetail.ValidationKey);
		}

		[Fact]
		public void Expression_ChainExpressionPath_Arrays()
		{
			var model = new TestModel
			{
				Model = new TestModel
				{
					List = new List<int> {12}
				}
			};

			var validationDetail = validationContext.When(model, m => m.Model.List[0])
				.AddError("Error");

			Assert.Equal("Model.List[0]", validationDetail.ValidationKey);
		}

		[Fact]
		public void Expression_CreateScope_ChainExpressionPath()
		{
			var model = new TestModel
			{
				Model = new TestModel
				{
					Property = 12
				}
			};

			var forList = validationContext.CreateScope(model, m => m.Model.List[0]);

			Assert.Equal("Model.List[0]", forList.ValidationPath);

			var validationDetail = forList.When("Key", "Value").AddError("Error");

			Assert.Equal("Model.List[0].Key", validationDetail.ValidationKey);
		}

		[Fact]
		public void Expression_ModelArray()
		{
			var model = new TestModel
			{
				Model = new TestModel
				{
					ModelArray = new[]
					{
						new TestModel
						{
							Property = 12
						}
					}
				}
			};

			var validationPath = validationContext.CreateScope(model, m => m.Model.ModelArray[0].Property).ValidationPath;

			Assert.Equal("Model.ModelArray[0].Property", validationPath);
		}

		[Fact]
		public void Expression_ModelList()
		{
			var model = new TestModel
			{
				Model = new TestModel
				{
					ModelList = new List<TestModel>
					{
						new TestModel
						{
							Property = 12
						}
					}
				}
			};

			var validationPath = validationContext.CreateScope(model, m => m.Model.ModelList[0].Property).ValidationPath;

			Assert.Equal("Model.ModelList[0].Property", validationPath);
		}

		private class TestModel
		{
			public int Property { get; set; }

			public int[] Array { get; set; }

			public List<int> List { get; set; }

			public int[,] DoubleArray { get; set; }

			public TestModel Model { get; set; }

			public TestModel[] ModelArray { get; set; }
			public List<TestModel> ModelList { get; set; }
		}
	}
}