using System;

namespace Phema.Validation
{
	public interface IValidationKey
	{
		string Key { get; }
	}
}

namespace Phema.Validation.Internal
{
	internal sealed class ValidationKey : IValidationKey
	{
		public ValidationKey(string key)
		{
			Key = key ?? throw new ArgumentNullException(nameof(key));
		}

		public string Key { get; }

		#region Equality

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}

			return ReferenceEquals(this, obj) || string.Equals(Key, ((IValidationKey) obj).Key);
		}

		public override int GetHashCode()
		{
			return Key != null ? Key.GetHashCode() : 0;
		}

		#endregion
	}
}