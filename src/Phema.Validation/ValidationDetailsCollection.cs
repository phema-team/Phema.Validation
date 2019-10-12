using System;
using System.Collections;
using System.Collections.Generic;

namespace Phema.Validation
{
	public sealed class ValidationDetailsCollection : IList<ValidationDetail>
	{
		private readonly ValidationDetailsCollection? rootValidationDetails;
		private readonly List<ValidationDetail> validationDetails;

		internal ValidationDetailsCollection(ValidationDetailsCollection? rootValidationDetails = null)
		{
			validationDetails = new List<ValidationDetail>();
			this.rootValidationDetails = rootValidationDetails;
		}

		public bool IsReadOnly => false;
		public int Count => validationDetails.Count;

		public ValidationDetail this[int index]
		{
			get => validationDetails[index];
			set => validationDetails[index] = value;
		}

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

		public List<ValidationDetail>.Enumerator GetEnumerator()
		{
			return validationDetails.GetEnumerator();
		}

		IEnumerator<ValidationDetail> IEnumerable<ValidationDetail>.GetEnumerator()
		{
			return GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void CopyTo(ValidationDetail[] array, int arrayIndex)
		{
			throw new NotSupportedException();
		}

		public bool Contains(ValidationDetail validationDetail)
		{
			throw new NotSupportedException();
		}

		public int IndexOf(ValidationDetail item)
		{
			throw new NotSupportedException();
		}

		public void Insert(int index, ValidationDetail item)
		{
			throw new NotSupportedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}
	}
}