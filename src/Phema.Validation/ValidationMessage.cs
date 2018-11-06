using System;

namespace Phema.Validation
{
	public class ValidationMessage
	{
		public ValidationMessage(Func<string> template)
		{
			Template = template;
		}

		public Func<string> Template { get; }

		protected internal virtual string GetMessage(params object[] arguments)
		{
			return string.Format(Template(), arguments);
		}
	}
}