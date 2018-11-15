namespace Phema.Validation
{
	public class ValidationKey
	{
		protected ValidationKey(string key)
		{
			Key = key;
		}
		
		public string Key { get; }

		protected bool Equals(ValidationKey other)
		{
			return string.Equals(Key, other.Key);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;

			return obj.GetType() == GetType() && Equals((ValidationKey)obj);
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