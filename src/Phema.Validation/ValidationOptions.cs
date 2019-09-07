using System;
using System.Reflection;

namespace Phema.Validation
{
	public sealed class ValidationOptions
	{
		public ValidationOptions()
		{
			ValidationSeverity = ValidationDefaults.ValidationSeverity;
			ValidationPath = ValidationDefaults.ValidationPath;
			ValidationPartSeparator = ValidationDefaults.ValidationPartSeparator;
			ValidationPartResolver = ValidationPartResolvers.Default;
			GlobalValidationKey = ValidationDefaults.GlobalValidationKey;
		}

		public ValidationSeverity ValidationSeverity { get; set; }
		public string? ValidationPath { get; set; }
		public string ValidationPartSeparator { get; set; }
		public Func<MemberInfo, string> ValidationPartResolver { get; set; }
		public string GlobalValidationKey { get; set; }
	}
}