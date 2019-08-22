using System.Collections;
using System.Collections.Generic;

namespace Phema.Validation
{
	internal sealed class ValidationDetailsCollection : ICollection<IValidationDetail>
	{
		private readonly ICollection<IValidationDetail> validationDetails;
		private readonly ICollection<IValidationDetail>? rootValidationDetails;

		public ValidationDetailsCollection(ICollection<IValidationDetail>? rootValidationDetails = null)
		{
			validationDetails = new List<IValidationDetail>();
			this.rootValidationDetails = rootValidationDetails;
		}

		public int Count => validationDetails.Count;
		public bool IsReadOnly => validationDetails.IsReadOnly;

		public void Add(IValidationDetail validationDetail)
		{
			validationDetails.Add(validationDetail);
			rootValidationDetails?.Add(validationDetail);
		}

		public bool Remove(IValidationDetail validationDetail)
		{
			var removed = validationDetails.Remove(validationDetail);

			if (removed)
			{
				rootValidationDetails?.Remove(validationDetail);
			}

			return removed;
		}

		public bool Contains(IValidationDetail validationDetail)
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

		public void CopyTo(IValidationDetail[] array, int arrayIndex)
		{
			validationDetails.CopyTo(array, arrayIndex);
		}

		public IEnumerator<IValidationDetail> GetEnumerator()
		{
			return validationDetails.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}