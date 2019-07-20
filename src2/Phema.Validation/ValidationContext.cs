using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace Phema.Validation
{
	public interface IValidationContext
	{
		ICollection<IValidationMessage> ValidationMessages { get; }
		ValidationSeverity ValidationSeverity { get; set; }
		string ValidationPath { get; }
	}

	public class ValidationContext : IValidationContext
	{
		public ValidationContext(IOptions<ValidationOptions> options)
		{
			ValidationMessages = options.Value.DefaultValidationMessageFactory();
			ValidationSeverity = options.Value.DefaultValidationSeverity;
			ValidationPath = options.Value.DefaultValidationPath;
		}
		
		public ICollection<IValidationMessage> ValidationMessages { get; }
		public ValidationSeverity ValidationSeverity { get; set; }
		public string ValidationPath { get; }
	}
}