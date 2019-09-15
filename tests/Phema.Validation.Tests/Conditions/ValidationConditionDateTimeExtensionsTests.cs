using System;
using Microsoft.Extensions.DependencyInjection;
using Phema.Validation.Conditions;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConditionDateTimeExtensionsTests
	{
		private readonly IValidationContext validationContext;

		public ValidationConditionDateTimeExtensionsTests()
		{
			validationContext = new ServiceCollection()
				.AddValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void IsDateTimeKind()
		{
			var validationDetail = validationContext
				.When("key", new DateTime(1, DateTimeKind.Local))
				.IsDateTimeKind(DateTimeKind.Local)
				.AddError("Error");

			Assert.NotNull(validationDetail);
		}

		[Fact]
		public void IsDateTimeKind_Valid()
		{
			var validationDetail = validationContext
				.When("key", new DateTime(1, DateTimeKind.Utc))
				.IsDateTimeKind(DateTimeKind.Local)
				.AddError("Error");

			Assert.Null(validationDetail);
		}

		[Fact]
		public void IsUtc()
		{
			var validationDetail = validationContext
				.When("key", new DateTime(1, DateTimeKind.Utc))
				.IsUtc()
				.AddError("Error");

			Assert.NotNull(validationDetail);
		}

		[Fact]
		public void IsUtc_Valid()
		{
			var validationDetail = validationContext
				.When("key", new DateTime(1, DateTimeKind.Local))
				.IsUtc()
				.AddError("Error");

			Assert.Null(validationDetail);
		}

		[Fact]
		public void IsNotUtc()
		{
			var validationDetail = validationContext
				.When("key", new DateTime(1, DateTimeKind.Local))
				.IsNotUtc()
				.AddError("Error");

			Assert.NotNull(validationDetail);
		}

		[Fact]
		public void IsNotUtc_Valid()
		{
			var validationDetail = validationContext
				.When("key", new DateTime(1, DateTimeKind.Utc))
				.IsNotUtc()
				.AddError("Error");

			Assert.Null(validationDetail);
		}

		[Fact]
		public void IsLocal()
		{
			var validationDetail = validationContext
				.When("key", new DateTime(1, DateTimeKind.Local))
				.IsLocal()
				.AddError("Error");

			Assert.NotNull(validationDetail);
		}

		[Fact]
		public void IsLocal_Valid()
		{
			var validationDetail = validationContext
				.When("key", new DateTime(1, DateTimeKind.Utc))
				.IsLocal()
				.AddError("Error");

			Assert.Null(validationDetail);
		}

		[Fact]
		public void IsNotLocal()
		{
			var validationDetail = validationContext
				.When("key", new DateTime(1, DateTimeKind.Utc))
				.IsNotLocal()
				.AddError("Error");

			Assert.NotNull(validationDetail);
		}

		[Fact]
		public void IsNotLocal_Valid()
		{
			var validationDetail = validationContext
				.When("key", new DateTime(1, DateTimeKind.Local))
				.IsNotLocal()
				.AddError("Error");

			Assert.Null(validationDetail);
		}

		[Fact]
		public void IsUnspecified()
		{
			var validationDetail = validationContext
				.When("key", new DateTime(1, DateTimeKind.Unspecified))
				.IsUnspecified()
				.AddError("Error");

			Assert.NotNull(validationDetail);
		}

		[Fact]
		public void IsUnspecified_Valid()
		{
			var validationDetail = validationContext
				.When("key", new DateTime(1, DateTimeKind.Utc))
				.IsUnspecified()
				.AddError("Error");

			Assert.Null(validationDetail);
		}

		[Fact]
		public void IsNotUnspecified()
		{
			var validationDetail = validationContext
				.When("key", new DateTime(1, DateTimeKind.Utc))
				.IsNotUnspecified()
				.AddError("Error");

			Assert.NotNull(validationDetail);
		}

		[Fact]
		public void IsNotUnspecified_Valid()
		{
			var validationDetail = validationContext
				.When("key", new DateTime(1, DateTimeKind.Unspecified))
				.IsNotUnspecified()
				.AddError("Error");

			Assert.Null(validationDetail);
		}
	}
}