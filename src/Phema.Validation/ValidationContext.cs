using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace Phema.Validation
{
	/// <summary>
	/// Stores validation state
	/// </summary>
	public interface IValidationContext
	{
		/// <summary>
		/// Failed validation results
		/// </summary>
		ICollection<IValidationDetail> ValidationDetails { get; }

		/// <summary>
		/// Current validation context severity. Adding details greater than this value will throw <see cref="ValidationConditionException"/>.
		/// Example: Add fatal detail, when this severity is error
		/// </summary>
		ValidationSeverity ValidationSeverity { get; set; }
		
		/// <summary>
		/// Used as ValidationKey prefix. To create new context with specified validation path
		/// use <see cref="IValidationContext.CreateFor"/> extensions
		/// </summary>
		/// <example>
		/// ValidateDelivery(validationContext.CreateFor(order, o => o.Delivery))
		/// </example>
		string? ValidationPath { get; }
	}

	internal sealed class ValidationContext : IValidationContext
	{
		public ValidationContext(IOptions<ValidationOptions> options)
		{
			ValidationDetails = options.Value.ValidationDetailsProvider();
			ValidationSeverity = options.Value.ValidationSeverity;
			ValidationPath = options.Value.ValidationPath;
		}
		
		public ICollection<IValidationDetail> ValidationDetails { get; }
		public ValidationSeverity ValidationSeverity { get; set; }
		public string ValidationPath { get; }
	}
}