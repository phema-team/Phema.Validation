using System;

namespace Phema.Validation
{
	/// <inheritdoc cref="IValidationKey"/>
	internal sealed class ValidationKey : IValidationKey
	{
		public ValidationKey(string key)
		{
			Key = key ?? throw new ArgumentNullException(nameof(key));
		}

		/// <inheritdoc cref="IValidationKey.Key"/>
		public string Key { get; }

		#region Equality
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
		#endregion
	}
}