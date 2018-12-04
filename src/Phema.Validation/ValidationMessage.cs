using System;

namespace Phema.Validation
{
	public sealed class ValidationMessage : IValidationMessage
	{
		public ValidationMessage(Func<object[], string> templateProvider)
		{
			TemplateProvider = templateProvider;
		}
		
		public ValidationMessage(Func<string> templateProvider)
		{
			TemplateProvider = arguments => templateProvider();
		}
		
		public Func<object[], string> TemplateProvider { get; }

		public string GetMessage(object[] arguments)
		{
			return TemplateProvider(arguments);
		}
	}
	
	public sealed class ValidationMessage<TArgument> : IValidationMessage
	{
		public ValidationMessage(Func<TArgument, string> templateProvider)
		{
			TemplateProvider = templateProvider;
		}

		public Func<TArgument, string> TemplateProvider { get; }

		public string GetMessage(object[] arguments)
		{
			if (arguments?.Length != 1)
			{
				throw new ArgumentException(nameof(arguments));
			}

			var argument = (TArgument)arguments[0];

			return TemplateProvider(argument);
		}
	}

	public sealed class ValidationMessage<TArgument1, TArgument2> : IValidationMessage
	{
		public ValidationMessage(Func<TArgument1, TArgument2, string> templateProvider)
		{
			TemplateProvider = templateProvider;
		}

		public Func<TArgument1, TArgument2, string> TemplateProvider { get; }

		public string GetMessage(object[] arguments)
		{
			if (arguments?.Length != 2)
			{
				throw new ArgumentException(nameof(arguments));
			}

			var argument1 = (TArgument1)arguments[0];
			var argument2 = (TArgument2)arguments[1];
			
			return TemplateProvider(argument1, argument2);
		}
	}
	
	public sealed class ValidationMessage<TArgument1, TArgument2, TArgument3> : IValidationMessage
	{
		public ValidationMessage(Func<TArgument1, TArgument2, TArgument3, string> templateProvider)
		{
			TemplateProvider = templateProvider;
		}

		public Func<TArgument1, TArgument2, TArgument3, string> TemplateProvider { get; }

		public string GetMessage(object[] arguments)
		{
			if (arguments?.Length != 3)
			{
				throw new ArgumentException(nameof(arguments));
			}

			var argument1 = (TArgument1)arguments[0];
			var argument2 = (TArgument2)arguments[1];
			var argument3 = (TArgument3)arguments[2];
			
			return TemplateProvider(argument1, argument2, argument3);
		}
	}
}