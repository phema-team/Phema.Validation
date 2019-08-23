using System.Collections;
using System.Collections.Generic;

namespace Phema.Validation
{
	internal sealed class ValidationDetailsCollection : ICollection<ValidationDetail>
	{
		private readonly ICollection<ValidationDetail> validationDetails;
		private readonly ICollection<ValidationDetail>? rootValidationDetails;

		public ValidationDetailsCollection(ICollection<ValidationDetail>? rootValidationDetails = null)
		{
			validationDetails = new List<ValidationDetail>();
			this.rootValidationDetails = rootValidationDetails;
		}

		public int Count => validationDetails.Count;
		public bool IsReadOnly => validationDetails.IsReadOnly;

		public void Add(ValidationDetail validationDetail)
		{
			validationDetails.Add(validationDetail);
			rootValidationDetails?.Add(validationDetail);
		}

		public bool Remove(ValidationDetail validationDetail)
		{
			var removed = validationDetails.Remove(validationDetail);

			if (removed)
			{
				rootValidationDetails?.Remove(validationDetail);
			}

			return removed;
		}

		public bool Contains(ValidationDetail validationDetail)
		{
			return validationDetails.Contains(validationDetail);
		}

		public void Clear()
		{
			if (rootValidationDetails != null)
			{
				foreach (var validationDetail in validationDetails)
				{
					rootValidationDetails.Remove(validationDetail);
				}
			}

			validationDetails.Clear();
		}

		public void CopyTo(ValidationDetail[] array, int arrayIndex)
		{
			validationDetails.CopyTo(array, arrayIndex);
		}

		public IEnumerator<ValidationDetail> GetEnumerator()
		{
			return validationDetails.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}