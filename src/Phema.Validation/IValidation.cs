using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Phema.Validation.Tests")]
[assembly: InternalsVisibleTo("Phema.Validation.Conditions.Tests")]
[assembly: InternalsVisibleTo("Phema.Validation.Expressions.Tests")]

namespace Phema.Validation
{
	public interface IValidation<in TModel>
	{
		void Validate(IValidationContext validationContext, TModel model);
	}
}