using System;
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
		ICollection<ValidationDetail> ValidationDetails { get; }

		/// <summary>
		/// Current validation context severity. Adding details greater than this value will throw <see cref="ValidationConditionException"/>.
		/// Example: Add fatal detail, when this severity is error
		/// </summary>
		ValidationSeverity ValidationSeverity { get; set; }

		/// <summary>
		/// Used as ValidationKey prefix. To create new context with specified validation path
		/// use <see cref="ValidationContextExtensions.CreateScope"/> extensions
		/// </summary>
		/// <example>
		/// ValidateDelivery(validationContext.CreateScope(order, o => o.Delivery))
		/// </example>
		string? ValidationPath { get; }
	}

	internal sealed class ValidationContext : IValidationContext, IServiceProvider
	{
		private readonly IServiceProvider serviceProvider;

		public ValidationContext(IServiceProvider serviceProvider, IOptions<ValidationOptions> validationOptions)
		{
			this.serviceProvider = serviceProvider;

			ValidationDetails = new ValidationDetailsCollection();
			ValidationSeverity = validationOptions.Value.ValidationSeverity;
			ValidationPath = validationOptions.Value.ValidationPath;
		}

		public ICollection<ValidationDetail> ValidationDetails { get; }
		public ValidationSeverity ValidationSeverity { get; set; }
		public string ValidationPath { get; }

		public object GetService(Type serviceType)
		{
			return serviceProvider.GetService(serviceType);
		}
	}
}