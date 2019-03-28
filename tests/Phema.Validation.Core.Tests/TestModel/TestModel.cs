using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Phema.Validation.Core.Tests
{
	[DataContract(Name = "test")]
	public class TestModel
	{
		[DataMember(Name = "string")]
		public string String { get; set; }
		
		[DataMember(Name = "list")]
		public List<TestModel> List { get; set; }

		[DataMember(Name = "array")]
		public TestModel[] Array { get; set; }
		
		[DataMember(Name = "nested")]
		public TestModel Nested { get; set; }
	}
}