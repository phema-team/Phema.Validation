using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace Phema.Validation
{
	public interface IValidationContext
	{
		ICollection<IValidationMessage> ValidationMessages { get; }
		ValidationSeverity ValidationSeverity { get; set; }
		
		// TODO: Prefix? Options? CreateSubPrefixValidationContext?
		// string Prefix { get; }
	}

	public class ValidationContext : IValidationContext
	{
		public ValidationContext(IOptions<ValidationOptions> options)
		{
			ValidationMessages = new List<IValidationMessage>();
			ValidationSeverity = options.Value.DefaultValidationSeverity;

			// TODO: Prefix? Options? CreateSubPrefixValidationContext?
			//Prefix = options.Value.DefaultPrefix;
		}
		
		public ICollection<IValidationMessage> ValidationMessages { get; }
		public ValidationSeverity ValidationSeverity { get; set; }
		
		// TODO: Prefix? Options? CreateSubPrefixValidationContext?
		// public string Prefix { get; }
	}
}