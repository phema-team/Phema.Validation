using System;
using System.Reflection;

namespace Phema.Validation
{
	public sealed class ValidationOptions
	{
		public ValidationOptions()
		{
			ValidationSeverity = ValidationDefaults.DefaultValidationSeverity;
			ValidationPath = ValidationDefaults.DefaultValidationPath;
			ValidationPathSeparator = ValidationDefaults.DefaultValidationPathSeparator;
			ValidationPartProvider = ValidationDefaults.DefaultValidationPartProvider;
		}

		public ValidationSeverity ValidationSeverity { get; set; }
		public string? ValidationPath { get; set; }
		public string ValidationPathSeparator { get; set; }
		public Func<MemberInfo, string> ValidationPartProvider { get; set; }
	}
}