using System;

namespace Phema.Validation
{
	public interface IValidationError
	{
		string Key { get; }

		string Message { get; }

		ValidationSeverity Severity { get; }
	}
}

namespace Phema.Validation.Internal
{
	internal sealed class ValidationError : IValidationError
	{
		public ValidationError(string key, string message, ValidationSeverity severity)
		{
			Key = key ?? throw new ArgumentNullException(nameof(key));
			Message = message ?? throw new ArgumentNullException(nameof(message));
			Severity = severity;
		}

		public string Key { get; }

		public string Message { get; }

		public ValidationSeverity Severity { get; }

		private bool Equals(IValidationError other)
		{
			return string.Equals(Key, other.Key);
		}

		public override bool Equals(object obj)
		{
			return ReferenceEquals(this, obj) || obj is ValidationError other && Equals(other);
		}

		public override int GetHashCode()
		{
			return Key != null ? Key.GetHashCode() : 0;
		}
	}
}