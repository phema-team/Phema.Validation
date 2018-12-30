using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConditionCollectionExtensionsTests
	{
		private readonly IValidationContext validationContext;

		public ValidationConditionCollectionExtensionsTests()
		{
			validationContext = new ServiceCollection()
				.AddPhemaValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void IsEmpty()
		{
			var error = validationContext.When("key", Array.Empty<int>())
				.IsEmpty()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.NotNull(error);
			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}

		[Fact]
		public void PropertyIsEmpty()
		{
			var model = new TestModel
			{
				Statistics = new List<int>()
			};

			var error = validationContext.When(nameof(model.Statistics), model.Statistics)
				.IsEmpty()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("Statistics", error.Key);
			Assert.Equal("template", error.Message);
		}

		[Fact]
		public void IsNotEmpty()
		{
			var error = validationContext.When("key", new[]
				{
					1
				})
				.IsNotEmpty()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}

		[Fact]
		public void PropertyIsNotEmpty()
		{
			var model = new TestModel
			{
				Statistics = new List<int>
				{
					1
				}
			};

			var error = validationContext.When(nameof(model.Statistics), model.Statistics)
				.IsNotEmpty()
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("Statistics", error.Key);
			Assert.Equal("template", error.Message);
		}

		[Fact]
		public void HasCount()
		{
			var error = validationContext.When("key", new[]
				{
					1
				})
				.HasCount(1)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}

		[Fact]
		public void PropertyHasCount()
		{
			var model = new TestModel
			{
				Statistics = new List<int>
				{
					1
				}
			};

			var error = validationContext.When(nameof(model.Statistics), model.Statistics)
				.HasCount(1)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("Statistics", error.Key);
			Assert.Equal("template", error.Message);
		}

		[Fact]
		public void NotHasCount()
		{
			var error = validationContext.When("key", new[]
				{
					1
				})
				.NotHasCount(2)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}

		[Fact]
		public void PropertyNotHasCount()
		{
			var model = new TestModel
			{
				Statistics = new List<int>
				{
					1
				}
			};

			var error = validationContext.When(nameof(model.Statistics), model.Statistics)
				.NotHasCount(2)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("Statistics", error.Key);
			Assert.Equal("template", error.Message);
		}

		[Fact]
		public void IsContains()
		{
			var error = validationContext.When("key", new[]
				{
					1
				})
				.IsContains(1)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}

		[Fact]
		public void PropertyIsContains()
		{
			var model = new TestModel
			{
				Statistics = new List<int>
				{
					1
				}
			};

			var error = validationContext.When(nameof(model.Statistics), model.Statistics)
				.IsContains(1)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("Statistics", error.Key);
			Assert.Equal("template", error.Message);
		}

		[Fact]
		public void IsNotContains()
		{
			var error = validationContext.When("key", new[]
				{
					1
				})
				.IsNotContains(2)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("key", error.Key);
			Assert.Equal("template", error.Message);
		}

		[Fact]
		public void PropertyIsNotContains()
		{
			var model = new TestModel
			{
				Statistics = new List<int>
				{
					1
				}
			};

			var error = validationContext.When(nameof(model.Statistics), model.Statistics)
				.IsNotContains(2)
				.AddError(() => new ValidationMessage(() => "template"));

			Assert.Equal("Statistics", error.Key);
			Assert.Equal("template", error.Message);
		}
	}
}