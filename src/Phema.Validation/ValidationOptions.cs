namespace Phema.Validation
{
	/// <summary>
	/// Описывает настройки валидации
	/// </summary>
	public sealed class ValidationOptions
	{
		public ValidationOptions()
		{
			Severity = ValidationSeverity.Error;
		}
		
		/// <inheritdoc cref="ValidationSeverity"/>
		public ValidationSeverity Severity { get; set; }

	}
}