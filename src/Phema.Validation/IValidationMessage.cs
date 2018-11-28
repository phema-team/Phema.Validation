using System;
using System.Globalization;

namespace Phema.Validation
{
	public interface IValidationMessage
	{
		string GetMessage(object[] arguments, CultureInfo cultureInfo);
	}
	
	public sealed class ValidationMessage : IValidationMessage
	{
		public ValidationMessage(Func<string> templateProvider)
		{
			TemplateProvider = templateProvider;
		}
		
		public Func<string> TemplateProvider { get; }

		public string GetMessage(object[] arguments, CultureInfo cultureInfo)
		{
			var template = TemplateProvider();
			
			return arguments == null
				? template
				: string.Format(cultureInfo, template, arguments);
		}
	}
}