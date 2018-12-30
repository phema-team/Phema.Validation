using System;

namespace Phema.Validation
{
	internal sealed class ValidationError : IValidationError
	{
		public ValidationError(string key, string message, ValidationSeverity severity)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			if (message == null)
				throw new ArgumentNullException(nameof(message));

			Key = key;
			Message = message;
			Severity = severity;
		}

		public string Key { get; }
		public string Message { get; }
		public ValidationSeverity Severity { get; }

		private bool Equals(ValidationError other)
		{
			return string.Equals(Key, other.Key) && Severity == other.Severity;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}

			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			return obj is ValidationError other && Equals(other);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((Key != null ? Key.GetHashCode() : 0) * 397) ^ (int)Severity;
			}
		}
	}
}