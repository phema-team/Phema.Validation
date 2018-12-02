using System;

namespace Phema.Validation
{
	public interface IValidationMessage
	{
		string GetMessage(object[] arguments);
	}
	
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
}