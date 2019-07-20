using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace Phema.Validation
{
	public interface IValidationContext
	{
		ICollection<IValidationDetail> ValidationDetails { get; }
		ValidationSeverity ValidationSeverity { get; set; }
		string? ValidationPath { get; }
	}

	internal sealed class ValidationContext : IValidationContext
	{
		public ValidationContext(IOptions<ValidationOptions> options)
		{
			ValidationDetails = options.Value.DefaultValidationDetailsFactory();
			ValidationSeverity = options.Value.DefaultValidationSeverity;
			ValidationPath = options.Value.DefaultValidationPath;
		}
		
		public ICollection<IValidationDetail> ValidationDetails { get; }
		public ValidationSeverity ValidationSeverity { get; set; }
		public string ValidationPath { get; }
	}
}