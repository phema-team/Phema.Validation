using System;

namespace Phema.Validation
{
	/// <inheritdoc cref="IValidationTemplate"/>
	public sealed class ValidationTemplate : IValidationTemplate
	{
		public ValidationTemplate(Func<string> templateProvider)
		{
			if (templateProvider == null)
				throw new ArgumentNullException(nameof(templateProvider));

			TemplateProvider = templateProvider;
		}

		public Func<string> TemplateProvider { get; }

		public string GetMessage(object[] arguments)
		{
			if (arguments == null)
				throw new ArgumentNullException();
			
			if (arguments.Length != 0)
				throw new ArgumentException(nameof(arguments));

			return TemplateProvider();
		}
	}
	
	/// <inheritdoc cref="IValidationTemplate"/>
	public sealed class ValidationTemplate<TArgument> : IValidationTemplate
	{
		public ValidationTemplate(Func<TArgument, string> templateProvider)
		{
			TemplateProvider = templateProvider ?? throw new ArgumentNullException(nameof(templateProvider));
		}

		public Func<TArgument, string> TemplateProvider { get; }

		public string GetMessage(object[] arguments)
		{
			if (arguments == null)
				throw new ArgumentNullException(nameof(arguments));

			if (arguments.Length != 1)
				throw new ArgumentException(nameof(arguments));

			var argument = (TArgument)arguments[0];

			return TemplateProvider(argument);
		}
	}

	/// <inheritdoc cref="IValidationTemplate"/>
	public sealed class ValidationTemplate<TArgument1, TArgument2> : IValidationTemplate
	{
		public ValidationTemplate(Func<TArgument1, TArgument2, string> templateProvider)
		{
			TemplateProvider = templateProvider ?? throw new ArgumentNullException(nameof(templateProvider));
		}

		public Func<TArgument1, TArgument2, string> TemplateProvider { get; }

		public string GetMessage(object[] arguments)
		{
			if (arguments == null)
				throw new ArgumentNullException(nameof(arguments));

			if (arguments.Length != 2)
				throw new ArgumentException(nameof(arguments));

			var argument1 = (TArgument1)arguments[0];
			var argument2 = (TArgument2)arguments[1];

			return TemplateProvider(argument1, argument2);
		}
	}

	/// <inheritdoc cref="IValidationTemplate"/>
	public sealed class ValidationTemplate<TArgument1, TArgument2, TArgument3> : IValidationTemplate
	{
		public ValidationTemplate(Func<TArgument1, TArgument2, TArgument3, string> templateProvider)
		{
			TemplateProvider = templateProvider ?? throw new ArgumentNullException(nameof(templateProvider));
		}

		public Func<TArgument1, TArgument2, TArgument3, string> TemplateProvider { get; }

		public string GetMessage(object[] arguments)
		{
			if (arguments == null)
				throw new ArgumentNullException(nameof(arguments));

			if (arguments.Length != 3)
				throw new ArgumentException(nameof(arguments));

			var argument1 = (TArgument1)arguments[0];
			var argument2 = (TArgument2)arguments[1];
			var argument3 = (TArgument3)arguments[2];

			return TemplateProvider(argument1, argument2, argument3);
		}
	}
}