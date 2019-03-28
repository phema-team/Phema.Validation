using System;
using Xunit;

namespace Phema.Validation.Core.Tests
{
	public class ValidationTemplateTests
	{
		[Fact]
		public void ValidationTemplate_0_Renders()
		{
			var template = new ValidationTemplate(() => "template");
			
			Assert.Equal("template", template.GetMessage(Array.Empty<object>()));
		}
		
		[Fact]
		public void ValidationTemplate_0_Null()
		{
			var template = new ValidationTemplate(() => "template");

			Assert.Throws<ArgumentNullException>(() => template.GetMessage(null));
		}
		
		[Fact]
		public void ValidationTemplate_0_InvalidArgs()
		{
			var template = new ValidationTemplate(() => "template");

			Assert.Throws<ArgumentException>(() => template.GetMessage(new object[1]));
		}
		
		[Fact]
		public void ValidationTemplate_1_Renders()
		{
			var template = new ValidationTemplate<int>(a => $"template: {a}");
			
			Assert.Equal("template: 1", template.GetMessage(new object[] { 1 }));
		}
		
		[Fact]
		public void ValidationTemplate_1_Null()
		{
			var template = new ValidationTemplate<int>(a => $"template: {a}");

			Assert.Throws<ArgumentNullException>(() => template.GetMessage(null));
		}
		
		[Fact]
		public void ValidationTemplate_1_InvalidArgs()
		{
			var template = new ValidationTemplate<int>(a => $"template: {a}");

			Assert.Throws<ArgumentException>(() => template.GetMessage(new object[2]));
			Assert.Throws<ArgumentException>(() => template.GetMessage(new object[0]));
		}
		
		[Fact]
		public void ValidationTemplate_2_Renders()
		{
			var template = new ValidationTemplate<int, int>((a, b) => $"template: {a}, {b}");
			
			Assert.Equal("template: 1, 2", template.GetMessage(new object[] { 1, 2 }));
		}
		
		[Fact]
		public void ValidationTemplate_2_Null()
		{
			var template = new ValidationTemplate<int, int>((a, b) => $"template: {a}, {b}");

			Assert.Throws<ArgumentNullException>(() => template.GetMessage(null));
		}
		
		[Fact]
		public void ValidationTemplate_2_InvalidArgs()
		{
			var template = new ValidationTemplate<int, int>((a, b) => $"template: {a}, {b}");

			Assert.Throws<ArgumentException>(() => template.GetMessage(new object[0]));
			Assert.Throws<ArgumentException>(() => template.GetMessage(new object[1]));
			Assert.Throws<ArgumentException>(() => template.GetMessage(new object[3]));
		}
		
		[Fact]
		public void ValidationTemplate_3_Renders()
		{
			var template = new ValidationTemplate<int, int, int>((a, b, c) => $"template: {a}, {b}, {c}");
			
			Assert.Equal("template: 1, 2, 3", template.GetMessage(new object[] { 1, 2, 3 }));
		}
		
		[Fact]
		public void ValidationTemplate_3_Null()
		{
			var template = new ValidationTemplate<int, int, int>((a, b, c) => $"template: {a}, {b}, {c}");

			Assert.Throws<ArgumentNullException>(() => template.GetMessage(null));
		}
		
		[Fact]
		public void ValidationTemplate_3_InvalidArgs()
		{
			var template = new ValidationTemplate<int, int, int>((a, b, c) => $"template: {a}, {b}, {c}");

			Assert.Throws<ArgumentException>(() => template.GetMessage(new object[0]));
			Assert.Throws<ArgumentException>(() => template.GetMessage(new object[1]));
			Assert.Throws<ArgumentException>(() => template.GetMessage(new object[2]));
			Assert.Throws<ArgumentException>(() => template.GetMessage(new object[4]));
		}
	}
}