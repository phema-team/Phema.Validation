using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Phema.Validation
{
	public class ValidationResult : ObjectResult
	{
		public ValidationResult(IEnumerable<IValidationError> errors)
			: base(new ValidationResponse(errors))
		{
			StatusCode = 400;
		}

		public ValidationResult(IValidationError error)
			: base(new ValidationResponse(error))
		{
			StatusCode = 400;
		}

		[DataContract]
		private class ValidationResponse
		{
			public ValidationResponse(IValidationError error)
			{
				Validation = new Dictionary<string, string>
				{
					[error.Key] = error.Message
				};
			}

			public ValidationResponse(IEnumerable<IValidationError> errors)
			{
				Validation = errors.ToDictionary(
					error => error.Key,
					error => error.Message);
			}
			
			[DataMember(Name = "validation")]
			public IDictionary<string, string> Validation { get; }
		}
	}
}