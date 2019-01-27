using System;

namespace Phema.Validation
{
	public sealed class ValidationKey : IValidationKey
	{
		private ValidationKey(string key)
		{
			Key = key ?? throw new ArgumentNullException(nameof(key));
		}

		public string Key { get; }

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}

			return ReferenceEquals(this, obj) || string.Equals(Key, ((IValidationKey)obj).Key);
		}

		public override int GetHashCode()
		{
			return Key != null ? Key.GetHashCode() : 0;
		}

		public static implicit operator ValidationKey(string key)
		{
			return new ValidationKey(key);
		}
	}
}