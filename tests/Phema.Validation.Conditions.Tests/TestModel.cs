using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Phema.Validation.Tests
{
	public class TestModel
	{
		[DataMember(Name = "statistics")]
		public IList<int> Statistics { get; set; }
	}
}