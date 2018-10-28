using System;

namespace Phema.Validation
{
	public class ValidationMessage<TArgument> : ValidationMessage
	{
		public ValidationMessage(Func<string> factory) : base(factory)
		{
		}

		protected override string GetMessage(params object[] arguments)
		{
			if (arguments.Length != 1)
			{
				throw new ArgumentException(nameof(arguments));
			}

			return string.Format(Factory(), arguments[0]);
		}
	}

	public class ValidationMessage<TArgument1, TArgument2> : ValidationMessage
	{
		public ValidationMessage(Func<string> factory) : base(factory)
		{
		}

		protected override string GetMessage(params object[] arguments)
		{
			if (arguments.Length != 2)
			{
				throw new ArgumentException(nameof(arguments));
			}

			return string.Format(Factory(), arguments[0], arguments[1]);
		}
	}
}