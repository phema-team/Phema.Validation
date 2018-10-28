using System;

namespace Phema.Validation
{
	public class ValidationMessage
	{
		public ValidationMessage(Func<string> factory)
		{
			Factory = factory;
		}

		public Func<string> Factory { get; }

		protected internal virtual string GetMessage(params object[] arguments)
		{
			return string.Format(Factory(), arguments);
		}
	}
}