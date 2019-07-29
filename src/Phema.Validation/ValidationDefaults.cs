using System;
using System.Collections.Generic;

namespace Phema.Validation
{
	public static class ValidationDefaults
	{
		public const string DefaultValidationPathSeparator = ".";
		public const string? DefaultValidationPath = null;
		public const ValidationSeverity DefaultValidationSeverity = ValidationSeverity.Error;

		public static Func<ICollection<ValidationDetail>> DefaultValidationDetailsProvider => () => new List<ValidationDetail>();
	}
}