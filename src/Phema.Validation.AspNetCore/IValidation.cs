using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Phema.Validation.AspNetCore.Tests")]

namespace Phema.Validation
{
	public interface IValidation<in TModel>
	{
		void Validate(IValidationContext validationContext, TModel model);
	}
}