namespace Phema.Validation
{
	/// <summary>
	/// Описывает настройки валидации
	/// </summary>
	public sealed class PhemaValidationOptions
	{
		public PhemaValidationOptions()
		{
			Severity = ValidationSeverity.Error;
		}
		
		/// <inheritdoc cref="ValidationSeverity"/>
		public ValidationSeverity Severity { get; set; }

	}
}