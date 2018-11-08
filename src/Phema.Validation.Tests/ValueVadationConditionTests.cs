using System.Runtime.Serialization;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValueVadationConditionTests
	{
		private readonly IValidationContext validationContext;

		public ValueVadationConditionTests()
		{
			validationContext = new ValidationContext();
		}

		public class Stab
		{
			[DataMember(Name = "test")]
			public string Test { get; set; }
		}
		
		[Fact]
		public void IsNull()
		{
			var stab = new Stab();
			
			validationContext.When(stab, s => s.Test)
				.IsNull()
				.Add(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsNotNull()
		{
			var stab = new Stab
			{
				Test = "test"
			};
			
			validationContext.When(stab, s => s.Test)
				.IsNotNull()
				.Add(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsEmpty()
		{
			var stab = new Stab
			{
				Test = ""
			};
			
			validationContext.When(stab, s => s.Test)
				.IsEmpty()
				.Add(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsNotEmpty()
		{
			var stab = new Stab
			{
				Test = "done"
			};
			
			validationContext.When(stab, s => s.Test)
				.IsNotEmpty()
				.Add(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsNullOrWhitespace()
		{
			var stab = new Stab
			{
				Test = "  "
			};
			
			validationContext.When(stab, s => s.Test)
				.IsNullOrWhitespace()
				.Add(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsNotNullOrWhitespace()
		{
			var stab = new Stab
			{
				Test = "done"
			};
			
			validationContext.When(stab, s => s.Test)
				.IsNotNullOrWhitespace()
				.Add(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsEqual()
		{
			var stab = new Stab
			{
				Test = "done"
			};
			
			validationContext.When(stab, s => s.Test)
				.IsEqual("done")
				.Add(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsNotEqual()
		{
			var stab = new Stab
			{
				Test = ""
			};
			
			validationContext.When(stab, s => s.Test)
				.IsNotEqual("done")
				.Add(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsMatch()
		{
			var stab = new Stab
			{
				Test = "abc"
			};
			
			validationContext.When(stab, s => s.Test)
				.IsMatch("[a-c]+")
				.Add(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}

		[Fact]
		public void IsNotMatch()
		{
			var stab = new Stab
			{
				Test = "def"
			};
			
			validationContext.When(stab, s => s.Test)
				.IsNotMatch("[a-c]")
				.Add(() => new ValidationMessage(() => "works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("test", error.Key);
			Assert.Equal("works", error.Message);
		}
		
		public class TestClass
		{
			[DataMember(Name = "name")]
			public string Name { get; set; }

			[DataMember(Name = "age")]
			public int Age { get; set; }
		}

		[Fact]
		public void Test()
		{
			var test = new TestClass
			{
				Name = "test",
				Age = 13
			};

			validationContext.When(test, t => t.Name)
				.IsNullOrWhitespace()
				.Add(() => new ValidationMessage(() => "Name is null or whitespace"));

			validationContext.When(test, t => t.Age)
				.Is(age => age < 18)
				.Add<int>(() => new ValidationMessage<int>(() => "Age {0} is under 18"), test.Age);
			
			Assert.False(validationContext.IsValid());
			
			Assert.True(validationContext.IsValid<TestClass>(t => t.Name));
			Assert.False(validationContext.IsValid<TestClass>(t => t.Age));

			var error = Assert.Single(validationContext.Errors);
			
			Assert.Equal("age", error.Key);
			Assert.Equal("Age 13 is under 18", error.Message);
		}

		[Fact]
		public void ValueIsInRange()
		{
			var test = new TestClass
			{
				Age = 11
			};

			validationContext.When(test, t => t.Age)
				.IsInRange(10, 12)
				.Add(() => new ValidationMessage(() => "Works"));

			var error = Assert.Single(validationContext.Errors);

			Assert.Equal("age", error.Key);
			Assert.Equal("Works", error.Message);
		}
		
		[Fact]
		public void ValueLessThanRange()
		{
			var test = new TestClass
			{
				Age = 9
			};

			validationContext.When(test, t => t.Age)
				.IsInRange(10, 12)
				.Add(() => new ValidationMessage(() => "Works"));
			
			Assert.True(validationContext.IsValid());
		}
		
		[Fact]
		public void ValueGreaterThanRange()
		{
			var test = new TestClass
			{
				Age = 13
			};

			validationContext.When(test, t => t.Age)
				.IsInRange(10, 12)
				.Add(() => new ValidationMessage(() => "Works"));
			
			Assert.True(validationContext.IsValid());
		}
	}
}