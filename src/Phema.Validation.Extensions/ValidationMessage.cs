using System;
using System.Globalization;

namespace Phema.Validation
{
	public sealed class ValidationMessage<TArgument> : IValidationMessage
	{
		public ValidationMessage(Func<string> templateProvider)
		{
			TemplateProvider = templateProvider;
		}

		public Func<string> TemplateProvider { get; }

		public string GetMessage(object[] arguments, CultureInfo cultureInfo)
		{
			if (arguments.Length != 1)
			{
				throw new ArgumentException(nameof(arguments));
			}

			var template = TemplateProvider();

			return string.Format(cultureInfo, template, arguments);
		}
	}

	public sealed class ValidationMessage<TArgument1, TArgument2> : IValidationMessage
	{
		public ValidationMessage(Func<string> templateProvider)
		{
			TemplateProvider = templateProvider;
		}

		public Func<string> TemplateProvider { get; }

		public string GetMessage(object[] arguments, CultureInfo cultureInfo)
		{
			if (arguments.Length != 2)
			{
				throw new ArgumentException(nameof(arguments));
			}

			var template = TemplateProvider();

			return string.Format(cultureInfo, template, arguments);
		}
	}
}