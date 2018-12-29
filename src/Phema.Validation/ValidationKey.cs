using System;

namespace Phema.Validation
{
	public sealed class ValidationKey : IValidationKey
	{
		internal ValidationKey(string key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));
			
			Key = key;
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