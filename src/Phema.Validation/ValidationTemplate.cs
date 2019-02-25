using System;

namespace Phema.Validation
{
	/// <inheritdoc cref="IValidationTemplate"/>
	public sealed class ValidationTemplate : IValidationTemplate
	{
		private readonly Func<string> templateProvider;
		
		public ValidationTemplate(Func<string> templateProvider)
		{
			this.templateProvider = templateProvider ?? throw new ArgumentNullException(nameof(templateProvider));
		}

		public string GetMessage(object[] arguments)
		{
			if (arguments == null)
				throw new ArgumentNullException();
			
			if (arguments.Length != 0)
				throw new ArgumentException(nameof(arguments));

			return templateProvider();
		}
	}
	
	/// <inheritdoc cref="IValidationTemplate"/>
	public sealed class ValidationTemplate<TArgument> : IValidationTemplate
	{
		private readonly Func<TArgument, string> templateProvider;
		
		public ValidationTemplate(Func<TArgument, string> templateProvider)
		{
			this.templateProvider = templateProvider ?? throw new ArgumentNullException(nameof(templateProvider));
		}

		public string GetMessage(object[] arguments)
		{
			if (arguments == null)
				throw new ArgumentNullException(nameof(arguments));

			if (arguments.Length != 1)
				throw new ArgumentException(nameof(arguments));

			var argument = (TArgument)arguments[0];

			return templateProvider(argument);
		}
	}

	/// <inheritdoc cref="IValidationTemplate"/>
	public sealed class ValidationTemplate<TArgument1, TArgument2> : IValidationTemplate
	{
		private readonly Func<TArgument1, TArgument2, string> templateProvider;
		
		public ValidationTemplate(Func<TArgument1, TArgument2, string> templateProvider)
		{
			this.templateProvider = templateProvider ?? throw new ArgumentNullException(nameof(templateProvider));
		}

		public string GetMessage(object[] arguments)
		{
			if (arguments == null)
				throw new ArgumentNullException(nameof(arguments));

			if (arguments.Length != 2)
				throw new ArgumentException(nameof(arguments));

			var argument1 = (TArgument1)arguments[0];
			var argument2 = (TArgument2)arguments[1];

			return templateProvider(argument1, argument2);
		}
	}

	/// <inheritdoc cref="IValidationTemplate"/>
	public sealed class ValidationTemplate<TArgument1, TArgument2, TArgument3> : IValidationTemplate
	{
		private readonly Func<TArgument1, TArgument2, TArgument3, string> templateProvider;
		
		public ValidationTemplate(Func<TArgument1, TArgument2, TArgument3, string> templateProvider)
		{
			this.templateProvider = templateProvider ?? throw new ArgumentNullException(nameof(templateProvider));
		}

		public string GetMessage(object[] arguments)
		{
			if (arguments == null)
				throw new ArgumentNullException(nameof(arguments));

			if (arguments.Length != 3)
				throw new ArgumentException(nameof(arguments));

			var argument1 = (TArgument1)arguments[0];
			var argument2 = (TArgument2)arguments[1];
			var argument3 = (TArgument3)arguments[2];

			return templateProvider(argument1, argument2, argument3);
		}
	}
}