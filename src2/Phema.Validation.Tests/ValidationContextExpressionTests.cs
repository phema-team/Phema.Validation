using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationContextExpressionTests
	{
		private readonly IValidationContext validationContext;

		public ValidationContextExpressionTests()
		{
			validationContext = new ServiceCollection()
				.AddValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}
		
		[Fact]
		public void ValidationContext_ExpressionKey_Property()
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
		public void ValidationContext_ExpressionKey_ArrayProperty_ConstantIndex()
		{
			var model = new TestModel
			{
				Array = new[]{ 12 }
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
		public void ValidationContext_ExpressionKey_ArrayProperty_LocalIndex()
		{
			var model = new TestModel
			{
				Array = new[]{ 12 }
			};

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
		public void ValidationContext_ExpressionKey_ListProperty_ConstantIndex()
		{
			var model = new TestModel
			{
				List = new List<int>{ 12 }
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
		public void ValidationContext_ExpressionKey_ListProperty_LocalIndex()
		{
			var model = new TestModel
			{
				List = new List<int>{ 12 }
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
		public void ValidationContext_ExpressionKey_DoubleProperty_ConstantIndex()
		{
			var model = new TestModel
			{
				DoubleArray = new[,] { { 12 } }
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
		public void ValidationContext_ExpressionKey_DoubleProperty_LocalIndex()
		{
			var model = new TestModel
			{
				DoubleArray = new[,] { { 12 } }
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

		private class TestModel
		{
			public int Property { get; set; }

			public int[] Array { get; set; }
			
			public List<int> List { get; set; }

			public int[,] DoubleArray { get; set; }
		}
	}
}