namespace Phema.Validation
{
	public class ValidationMessage
	{
		public ValidationMessage(Template template)
		{
			Template = template;
		}
		
		public Template Template { get; }
		
		protected internal virtual string GetMessage(object[] args)
		{
			return args == null
				? Template()
				: string.Format(Template(), args);
		}
	}
}