using System.Runtime.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class SeverityAspNetCoreTests
	{
		private readonly IValidationContext validationContext;

		public SeverityAspNetCoreTests()
		{
			validationContext = new ServiceCollection()
				.AddValidation(validation =>
					validation.Add<Model, ModelValidation, ModelValidationComponent>())
				.AddScoped<IHttpContextAccessor>(
					sp => new HttpContextAccessor
					{
						HttpContext = new DefaultHttpContext()
					})
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		public class Model
		{
			[DataMember(Name = "Name")]
			public string Name { get; set; }
		}

		public class ModelValidation : IValidation<Model>
		{
			public void Validate(IValidationContext validationContext, Model model)
			{
				throw new System.NotImplementedException();
			}
		}

		public class ModelValidationComponent : IValidationComponent<Model, ModelValidation>
		{
			public ModelValidationComponent()
			{
				NoParameters = new ValidationMessage(() => "message");
				OneParameter = new ValidationMessage<int>(one => $"message: {one}");
				TwoParameters = new ValidationMessage<int, int>((one, two) => $"message: {one},{two}");
				ThreeParameters = new ValidationMessage<int, int, int>((one, two, three) => $"message: {one},{two},{three}");
			}

			public ValidationMessage NoParameters { get; }
			public ValidationMessage<int> OneParameter { get; }
			public ValidationMessage<int, int> TwoParameters { get; }
			public ValidationMessage<int, int, int> ThreeParameters { get; }
		}

		[Fact]
		public void AspNetErrorSeverity()
		{
			var model = new Model();

			var error = validationContext.When(nameof(model.Name), model.Name)
				.Is(value => value == null)
				.AddError<ModelValidationComponent>(c => c.NoParameters);

			Assert.NotNull(error);
			Assert.Equal("Name", error.Key);
			Assert.Equal("message", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}

		[Fact]
		public void AspNetErrorSeverity_OneParameter()
		{
			var model = new Model();

			var error = validationContext.When(nameof(model.Name), model.Name)
				.Is(value => value == null)
				.AddError<ModelValidationComponent, int>(c => c.OneParameter, 11);

			Assert.NotNull(error);
			Assert.Equal("Name", error.Key);
			Assert.Equal("message: 11", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}

		[Fact]
		public void AspNetErrorSeverity_TwoParameters()
		{
			var model = new Model();

			var error = validationContext.When(nameof(model.Name), model.Name)
				.Is(value => value == null)
				.AddError<ModelValidationComponent, int, int>(c => c.TwoParameters, 11, 22);

			Assert.NotNull(error);
			Assert.Equal("Name", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}

		[Fact]
		public void AspNetErrorSeverity_ThreeParameters()
		{
			var model = new Model();

			var error = validationContext.When(nameof(model.Name), model.Name)
				.Is(value => value == null)
				.AddError<ModelValidationComponent, int, int, int>(c => c.ThreeParameters, 11, 22, 33);

			Assert.NotNull(error);
			Assert.Equal("Name", error.Key);
			Assert.Equal("message: 11,22,33", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}

		[Fact]
		public void AspNetWarningSeverity()
		{
			var model = new Model();

			var error = validationContext.When(nameof(model.Name), model.Name)
				.Is(value => value == null)
				.AddWarning<ModelValidationComponent>(c => c.NoParameters);

			Assert.NotNull(error);
			Assert.Equal("Name", error.Key);
			Assert.Equal("message", error.Message);
			Assert.Equal(ValidationSeverity.Warning, error.Severity);
		}

		[Fact]
		public void AspNetWarningSeverity_OneParameter()
		{
			var model = new Model();

			var error = validationContext.When(nameof(model.Name), model.Name)
				.Is(value => value == null)
				.AddWarning<ModelValidationComponent, int>(c => c.OneParameter, 11);

			Assert.NotNull(error);
			Assert.Equal("Name", error.Key);
			Assert.Equal("message: 11", error.Message);
			Assert.Equal(ValidationSeverity.Warning, error.Severity);
		}

		[Fact]
		public void AspNetWarningSeverity_TwoParameters()
		{
			var model = new Model();

			var error = validationContext.When(nameof(model.Name), model.Name)
				.Is(value => value == null)
				.AddWarning<ModelValidationComponent, int, int>(c => c.TwoParameters, 11, 22);

			Assert.NotNull(error);
			Assert.Equal("Name", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Warning, error.Severity);
		}

		[Fact]
		public void AspNetWarningSeverity_ThreeParameters()
		{
			var model = new Model();

			var error = validationContext.When(nameof(model.Name), model.Name)
				.Is(value => value == null)
				.AddWarning<ModelValidationComponent, int, int, int>(c => c.ThreeParameters, 11, 22, 33);

			Assert.NotNull(error);
			Assert.Equal("Name", error.Key);
			Assert.Equal("message: 11,22,33", error.Message);
			Assert.Equal(ValidationSeverity.Warning, error.Severity);
		}

		[Fact]
		public void AspNetInformationSeverity()
		{
			var model = new Model();

			var error = validationContext.When(nameof(model.Name), model.Name)
				.Is(value => value == null)
				.AddInformation<ModelValidationComponent>(c => c.NoParameters);

			Assert.NotNull(error);
			Assert.Equal("Name", error.Key);
			Assert.Equal("message", error.Message);
			Assert.Equal(ValidationSeverity.Information, error.Severity);
		}

		[Fact]
		public void AspNetInformationSeverity_OneParameter()
		{
			var model = new Model();

			var error = validationContext.When(nameof(model.Name), model.Name)
				.Is(value => value == null)
				.AddInformation<ModelValidationComponent, int>(c => c.OneParameter, 11);

			Assert.NotNull(error);
			Assert.Equal("Name", error.Key);
			Assert.Equal("message: 11", error.Message);
			Assert.Equal(ValidationSeverity.Information, error.Severity);
		}

		[Fact]
		public void AspNetInformationSeverity_TwoParameters()
		{
			var model = new Model();

			var error = validationContext.When(nameof(model.Name), model.Name)
				.Is(value => value == null)
				.AddInformation<ModelValidationComponent, int, int>(c => c.TwoParameters, 11, 22);

			Assert.NotNull(error);
			Assert.Equal("Name", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Information, error.Severity);
		}

		[Fact]
		public void AspNetInformationSeverity_ThreeParameters()
		{
			var model = new Model();

			var error = validationContext.When(nameof(model.Name), model.Name)
				.Is(value => value == null)
				.AddInformation<ModelValidationComponent, int, int, int>(c => c.ThreeParameters, 11, 22, 33);

			Assert.NotNull(error);
			Assert.Equal("Name", error.Key);
			Assert.Equal("message: 11,22,33", error.Message);
			Assert.Equal(ValidationSeverity.Information, error.Severity);
		}

		[Fact]
		public void AspNetDebugSeverity()
		{
			var model = new Model();

			var error = validationContext.When(nameof(model.Name), model.Name)
				.Is(value => value == null)
				.AddDebug<ModelValidationComponent>(c => c.NoParameters);

			Assert.NotNull(error);
			Assert.Equal("Name", error.Key);
			Assert.Equal("message", error.Message);
			Assert.Equal(ValidationSeverity.Debug, error.Severity);
		}

		[Fact]
		public void AspNetDebugSeverity_OneParameter()
		{
			var model = new Model();

			var error = validationContext.When(nameof(model.Name), model.Name)
				.Is(value => value == null)
				.AddDebug<ModelValidationComponent, int>(c => c.OneParameter, 11);

			Assert.NotNull(error);
			Assert.Equal("Name", error.Key);
			Assert.Equal("message: 11", error.Message);
			Assert.Equal(ValidationSeverity.Debug, error.Severity);
		}

		[Fact]
		public void AspNetDebugSeverity_TwoParameters()
		{
			var model = new Model();

			var error = validationContext.When(nameof(model.Name), model.Name)
				.Is(value => value == null)
				.AddDebug<ModelValidationComponent, int, int>(c => c.TwoParameters, 11, 22);

			Assert.NotNull(error);
			Assert.Equal("Name", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Debug, error.Severity);
		}

		[Fact]
		public void AspNetDebugSeverity_ThreeParameters()
		{
			var model = new Model();

			var error = validationContext.When(nameof(model.Name), model.Name)
				.Is(value => value == null)
				.AddDebug<ModelValidationComponent, int, int, int>(c => c.ThreeParameters, 11, 22, 33);

			Assert.NotNull(error);
			Assert.Equal("Name", error.Key);
			Assert.Equal("message: 11,22,33", error.Message);
			Assert.Equal(ValidationSeverity.Debug, error.Severity);
		}

		[Fact]
		public void AspNetTraceSeverity()
		{
			var model = new Model();

			var error = validationContext.When(nameof(model.Name), model.Name)
				.Is(value => value == null)
				.AddTrace<ModelValidationComponent>(c => c.NoParameters);

			Assert.NotNull(error);
			Assert.Equal("Name", error.Key);
			Assert.Equal("message", error.Message);
			Assert.Equal(ValidationSeverity.Trace, error.Severity);
		}

		[Fact]
		public void AspNetTraceSeverity_OneParameter()
		{
			var model = new Model();

			var error = validationContext.When(nameof(model.Name), model.Name)
				.Is(value => value == null)
				.AddTrace<ModelValidationComponent, int>(c => c.OneParameter, 11);

			Assert.NotNull(error);
			Assert.Equal("Name", error.Key);
			Assert.Equal("message: 11", error.Message);
			Assert.Equal(ValidationSeverity.Trace, error.Severity);
		}

		[Fact]
		public void AspNetTraceSeverity_TwoParameters()
		{
			var model = new Model();

			var error = validationContext.When(nameof(model.Name), model.Name)
				.Is(value => value == null)
				.AddTrace<ModelValidationComponent, int, int>(c => c.TwoParameters, 11, 22);

			Assert.NotNull(error);
			Assert.Equal("Name", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Trace, error.Severity);
		}

		[Fact]
		public void AspNetTraceSeverity_ThreeParameters()
		{
			var model = new Model();

			var error = validationContext.When(nameof(model.Name), model.Name)
				.Is(value => value == null)
				.AddTrace<ModelValidationComponent, int, int, int>(c => c.ThreeParameters, 11, 22, 33);

			Assert.NotNull(error);
			Assert.Equal("Name", error.Key);
			Assert.Equal("message: 11,22,33", error.Message);
			Assert.Equal(ValidationSeverity.Trace, error.Severity);
		}
	}
}