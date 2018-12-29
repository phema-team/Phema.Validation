using System;

namespace Phema.Validation
{
	public sealed class ValidationMessage : IValidationMessage
	{
		public ValidationMessage(Func<object[], string> templateProvider)
		{
			if (templateProvider == null)
				throw new ArgumentNullException(nameof(templateProvider));
			
			TemplateProvider = templateProvider;
		}
		
		public ValidationMessage(Func<string> templateProvider)
		{
			if (templateProvider == null)
				throw new ArgumentNullException(nameof(templateProvider));
			
			TemplateProvider = arguments => templateProvider();
		}
		
		public Func<object[], string> TemplateProvider { get; }

		public string GetMessage(object[] arguments)
		{
			if (arguments == null)
				throw new ArgumentNullException();
			
			return TemplateProvider(arguments);
		}
	}
}