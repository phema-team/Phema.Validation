using System;
using System.Reflection;
using System.Runtime.Serialization;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationPartResolversTests
	{
		private readonly Type testClassType = typeof(TestClass);
		
		[Fact]
		public void Default()
		{
			var memberInfo = testClassType.GetProperty(
				nameof(TestClass.Default),
				BindingFlags.Instance | BindingFlags.Public);

			var validationPart = ValidationPartResolvers.Default(memberInfo);
			
			Assert.Equal("Default", validationPart);
		}

		[Fact]
		public void DataMember()
		{
			var memberInfo = testClassType.GetProperty(
				nameof(TestClass.DataMember),
				BindingFlags.Instance | BindingFlags.Public);

			var validationPart = ValidationPartResolvers.DataMember(memberInfo);
			
			Assert.Equal("dataMember", validationPart);
		}

		[Fact]
		public void PascalCase()
		{
			var memberInfo = testClassType.GetProperty(
				nameof(TestClass.pascalCase),
				BindingFlags.Instance | BindingFlags.Public);

			var validationPart = ValidationPartResolvers.PascalCase(memberInfo);
			
			Assert.Equal("PascalCase", validationPart);
		}

		[Fact]
		public void CamelCase()
		{
			var memberInfo = testClassType.GetProperty(
				nameof(TestClass.CamelCase),
				BindingFlags.Instance | BindingFlags.Public);

			var validationPart = ValidationPartResolvers.CamelCase(memberInfo);
			
			Assert.Equal("camelCase", validationPart);
		}

		private class TestClass
		{
			public string Default { get; set; }

			[DataMember(Name = "dataMember")]
			public string DataMember { get; set; }

			// ReSharper disable once InconsistentNaming
			public string pascalCase { get; set; }
			public string CamelCase { get; set; }
		}
	}
}