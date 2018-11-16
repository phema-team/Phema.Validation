using System.Runtime.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class SeverityAspNetCoreTests
	{
		private readonly IValidationContext validationContext;

		public SeverityAspNetCoreTests()
		{
			var services = new ServiceCollection();
			
			services.AddValidation(validation => 
				validation.AddValidation<Model, ModelValidation, ModelValidationComponent>());

			validationContext = services.BuildServiceProvider().GetRequiredService<IValidationContext>();
		}

		public class Model
		{
			[DataMember(Name = "name")]
			public string Name { get; set; }
		}
		
		public class ModelValidation : Validation<Model>
		{
			protected override void When(IValidationContext validationContext, Model model)
			{
				throw new System.NotImplementedException();
			}
		}
		
		public class ModelValidationComponent : ValidationComponent<Model, ModelValidation>
		{
			public ModelValidationComponent()
			{
				NoParameters = Register(() => "message");
				OneParameter = Register<int>(() => "message: {0}");
				TwoParameters = Register<int, int>(() => "message: {0},{1}");
			}
			
			public ValidationMessage NoParameters { get; }
			public ValidationMessage<int> OneParameter { get; }
			public ValidationMessage<int, int> TwoParameters { get; }
		}

		[Fact]
		public void AspNetErrorSeverity()
		{
			var model = new Model();

			var error = validationContext.When(model, m => m.Name)
				.IsNull()
				.AddError<ModelValidationComponent>(c => c.NoParameters);
			
			Assert.NotNull(error);
			Assert.Equal("name", error.Key);
			Assert.Equal("message", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}
		
		[Fact]
		public void AspNetErrorSeverity_OneParameter()
		{
			var model = new Model();

			var error = validationContext.When(model, m => m.Name)
				.IsNull()
				.AddError<ModelValidationComponent, int>(c => c.OneParameter, 11);
			
			Assert.NotNull(error);
			Assert.Equal("name", error.Key);
			Assert.Equal("message: 11", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}
		
		[Fact]
		public void AspNetErrorSeverity_TwoParameters()
		{
			var model = new Model();

			var error = validationContext.When(model, m => m.Name)
				.IsNull()
				.AddError<ModelValidationComponent, int, int>(c => c.TwoParameters, 11, 22);
			
			Assert.NotNull(error);
			Assert.Equal("name", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}
		
		[Fact]
		public void AspNetWarningSeverity()
		{
			var model = new Model();

			var error = validationContext.When(model, m => m.Name)
				.IsNull()
				.AddWarning<ModelValidationComponent>(c => c.NoParameters);
			
			Assert.NotNull(error);
			Assert.Equal("name", error.Key);
			Assert.Equal("message", error.Message);
			Assert.Equal(ValidationSeverity.Warning, error.Severity);
		}
		
		[Fact]
		public void AspNetWarningSeverity_OneParameter()
		{
			var model = new Model();

			var error = validationContext.When(model, m => m.Name)
				.IsNull()
				.AddWarning<ModelValidationComponent, int>(c => c.OneParameter, 11);
			
			Assert.NotNull(error);
			Assert.Equal("name", error.Key);
			Assert.Equal("message: 11", error.Message);
			Assert.Equal(ValidationSeverity.Warning, error.Severity);
		}
		
		[Fact]
		public void AspNetWarningSeverity_TwoParameters()
		{
			var model = new Model();

			var error = validationContext.When(model, m => m.Name)
				.IsNull()
				.AddWarning<ModelValidationComponent, int, int>(c => c.TwoParameters, 11, 22);
			
			Assert.NotNull(error);
			Assert.Equal("name", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Warning, error.Severity);
		}
		
		[Fact]
		public void AspNetInformationSeverity()
		{
			var model = new Model();

			var error = validationContext.When(model, m => m.Name)
				.IsNull()
				.AddInformation<ModelValidationComponent>(c => c.NoParameters);
			
			Assert.NotNull(error);
			Assert.Equal("name", error.Key);
			Assert.Equal("message", error.Message);
			Assert.Equal(ValidationSeverity.Information, error.Severity);
		}
		
		[Fact]
		public void AspNetInformationSeverity_OneParameter()
		{
			var model = new Model();

			var error = validationContext.When(model, m => m.Name)
				.IsNull()
				.AddInformation<ModelValidationComponent, int>(c => c.OneParameter, 11);
			
			Assert.NotNull(error);
			Assert.Equal("name", error.Key);
			Assert.Equal("message: 11", error.Message);
			Assert.Equal(ValidationSeverity.Information, error.Severity);
		}
		
		[Fact]
		public void AspNetInformationSeverity_TwoParameters()
		{
			var model = new Model();

			var error = validationContext.When(model, m => m.Name)
				.IsNull()
				.AddInformation<ModelValidationComponent, int, int>(c => c.TwoParameters, 11, 22);
			
			Assert.NotNull(error);
			Assert.Equal("name", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Information, error.Severity);
		}
		
		[Fact]
		public void AspNetDebugSeverity()
		{
			var model = new Model();

			var error = validationContext.When(model, m => m.Name)
				.IsNull()
				.AddDebug<ModelValidationComponent>(c => c.NoParameters);
			
			Assert.NotNull(error);
			Assert.Equal("name", error.Key);
			Assert.Equal("message", error.Message);
			Assert.Equal(ValidationSeverity.Debug, error.Severity);
		}
		
		[Fact]
		public void AspNetDebugSeverity_OneParameter()
		{
			var model = new Model();

			var error = validationContext.When(model, m => m.Name)
				.IsNull()
				.AddDebug<ModelValidationComponent, int>(c => c.OneParameter, 11);
			
			Assert.NotNull(error);
			Assert.Equal("name", error.Key);
			Assert.Equal("message: 11", error.Message);
			Assert.Equal(ValidationSeverity.Debug, error.Severity);
		}
		
		[Fact]
		public void AspNetDebugSeverity_TwoParameters()
		{
			var model = new Model();

			var error = validationContext.When(model, m => m.Name)
				.IsNull()
				.AddDebug<ModelValidationComponent, int, int>(c => c.TwoParameters, 11, 22);
			
			Assert.NotNull(error);
			Assert.Equal("name", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Debug, error.Severity);
		}
		
		[Fact]
		public void AspNetTraceSeverity()
		{
			var model = new Model();

			var error = validationContext.When(model, m => m.Name)
				.IsNull()
				.AddTrace<ModelValidationComponent>(c => c.NoParameters);
			
			Assert.NotNull(error);
			Assert.Equal("name", error.Key);
			Assert.Equal("message", error.Message);
			Assert.Equal(ValidationSeverity.Trace, error.Severity);
		}
		
		[Fact]
		public void AspNetTraceSeverity_OneParameter()
		{
			var model = new Model();

			var error = validationContext.When(model, m => m.Name)
				.IsNull()
				.AddTrace<ModelValidationComponent, int>(c => c.OneParameter, 11);
			
			Assert.NotNull(error);
			Assert.Equal("name", error.Key);
			Assert.Equal("message: 11", error.Message);
			Assert.Equal(ValidationSeverity.Trace, error.Severity);
		}
		
		[Fact]
		public void AspNetTraceSeverity_TwoParameters()
		{
			var model = new Model();

			var error = validationContext.When(model, m => m.Name)
				.IsNull()
				.AddTrace<ModelValidationComponent, int, int>(c => c.TwoParameters, 11, 22);
			
			Assert.NotNull(error);
			Assert.Equal("name", error.Key);
			Assert.Equal("message: 11,22", error.Message);
			Assert.Equal(ValidationSeverity.Trace, error.Severity);
		}
	}
}