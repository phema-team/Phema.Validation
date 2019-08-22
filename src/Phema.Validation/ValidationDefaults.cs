using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Phema.Validation
{
	public static class ValidationDefaults
	{
		public const string DefaultValidationPathSeparator = ".";
		public const string? DefaultValidationPath = null;
		public const ValidationSeverity DefaultValidationSeverity = ValidationSeverity.Error;

		public static Func<MemberInfo, string> DefaultValidationPartProvider { get; } = method => method.Name;
		public static Func<MemberInfo, string> DataMemberOrDefaultValidationPartProvider { get; } =
			method => method.GetCustomAttribute<DataMemberAttribute>()?.Name ?? DefaultValidationPartProvider(method);
	}
}