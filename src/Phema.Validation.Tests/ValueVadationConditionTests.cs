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
	}
}