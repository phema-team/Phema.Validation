using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Phema.Validation
{
	internal sealed class PhemaValidationMetadataProvider : IValidationMetadataProvider
	{
		public void CreateValidationMetadata(ValidationMetadataProviderContext context)
		{
			if (context.Key.MetadataKind != ModelMetadataKind.Parameter)
				return;
			
			context.ValidationMetadata.IsRequired = true;
			context.ValidationMetadata.HasValidators = true;
			context.ValidationMetadata.ValidateChildren = false;
		}
	}
}