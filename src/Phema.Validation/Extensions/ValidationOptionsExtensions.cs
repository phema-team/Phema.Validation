using System;
using System.Reflection;

namespace Phema.Validation
{
	public static class ValidationOptionsExtensions
	{
		public static ValidationOptions WithValidationPath(
			this ValidationOptions options,
			string validationPath)
		{
			options.ValidationPath = validationPath;
			return options;
		}

		public static ValidationOptions WithValidationSeverity(
			this ValidationOptions options,
			ValidationSeverity validationSeverity)
		{
			options.ValidationSeverity = validationSeverity;
			return options;
		}

		public static ValidationOptions WithGlobalValidationKey(
			this ValidationOptions options,
			string globalValidationKey)
		{
			options.GlobalValidationKey = globalValidationKey;
			return options;
		}

		public static ValidationOptions WithValidationPartResolver(
			this ValidationOptions options,
			Func<MemberInfo, string> validationPartResolver)
		{
			options.ValidationPartResolver = validationPartResolver;
			return options;
		}
		
		public static ValidationOptions WithValidationPartSeparator(
			this ValidationOptions options,
			string validationPartSeparator)
		{
			options.ValidationPartSeparator = validationPartSeparator;
			return options;
		}
	}
}