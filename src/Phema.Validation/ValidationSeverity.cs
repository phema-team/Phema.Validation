namespace Phema.Validation
{
	/// <summary>
	/// Validation severities. When add ValidationDetail.ValidationSeverity greater than ValidationContext.ValidationSeverity
	/// will throw <see cref="ValidationConditionException"/>.
	/// Example: Add fatal detail, when validation context severity is error
	/// </summary>
	public enum ValidationSeverity
	{
		Trace,

		Debug,

		Information,

		Warning,

		Error,

		Fatal
	}
}