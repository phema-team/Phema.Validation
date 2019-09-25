using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Phema.Validation
{
	/// <summary>
	///   <see cref="IValidationContext" /> scope with validation path.
	///   Shares validation details, inherits validation severirty
	/// </summary>
	public interface IValidationScope : IValidationContext, IDisposable
	{
	}

	[DebuggerDisplay("Path={ValidationPath} Severity={ValidationSeverity} Details={ValidationDetails.Count}")]
	internal sealed class ValidationScope : IValidationScope, IServiceProvider
	{
		private readonly IServiceProvider serviceProvider;

		public ValidationScope(
			IValidationContext validationContext,
			string validationPath,
			ValidationSeverity? validationSeverity)
		{
			ValidationPath = validationPath;
			ValidationDetails = new ValidationDetailsCollection(validationContext.ValidationDetails);
			ValidationSeverity = validationSeverity ?? validationContext.ValidationSeverity;

			serviceProvider = (IServiceProvider) validationContext;
		}

		public ICollection<ValidationDetail> ValidationDetails { get; }
		public ValidationSeverity ValidationSeverity { get; }
		public string? ValidationPath { get; }

		public object GetService(Type serviceType)
		{
			return serviceProvider.GetService(serviceType);
		}

		public void Dispose()
		{
			// TODO: Already disposed checks?
			// Used for 'using' only
		}
	}
}