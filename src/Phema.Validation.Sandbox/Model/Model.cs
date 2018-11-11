using System.Runtime.Serialization;

namespace WebApplication1
{
	public class Model
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }
		
		[DataMember(Name = "age")]
		public int Age { get; set; }
	}
}