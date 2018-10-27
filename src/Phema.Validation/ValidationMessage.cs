namespace Phema.Validation
{
	public class ValidationMessage
	{
		public ValidationMessage(string template)
		{
			Template = template;
		}

		public string Template { get; }

		protected internal virtual string GetMessage(params object[] arguments)
		{
			return string.Format(Template, arguments);
		}
	}
}