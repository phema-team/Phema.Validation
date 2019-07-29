using System;
using System.Collections.Generic;

namespace Phema.Validation
{
	/// <summary>
	/// <see cref="IValidationContext"/> scope with validation path.
	/// Shares validation details, inherits validation severirty (do not override parent)
	/// </summary>
	public interface IValidationScope : IValidationContext, IDisposable
	{
	}

	internal sealed class ValidationScope : IValidationScope, IServiceProvider
	{
		private readonly IServiceProvider serviceProvider;

		public ValidationScope(IValidationContext validationContext, string validationPath)
		{
			ValidationPath = validationPath;
			ValidationDetails = validationContext.ValidationDetails;
			ValidationSeverity = validationContext.ValidationSeverity;
			serviceProvider = (IServiceProvider) validationContext;
		}

		public ICollection<ValidationDetail> ValidationDetails { get; }
		public ValidationSeverity ValidationSeverity { get; set; }
		public string ValidationPath { get; }

		public object GetService(Type serviceType)
		{
			return serviceProvider.GetService(serviceType);
		}

		public void Dispose()
		{
			// Used for 'using' syntax only for now
		}
	}
}