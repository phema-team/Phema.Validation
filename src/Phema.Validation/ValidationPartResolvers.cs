using System.Reflection;
using System.Runtime.Serialization;

namespace Phema.Validation
{
	public static class ValidationPartResolvers
	{
		/// <summary>
		/// Resolve validation parts using member names
		/// </summary>
		public static string Default(MemberInfo memberInfo)
		{
			return memberInfo.Name;
		}

		/// <summary>
		/// Resolve validation parts using <see cref="DataMemberAttribute"/> or fallback to default
		/// </summary>
		public static string DataMember(MemberInfo memberInfo)
		{
			return memberInfo.GetCustomAttribute<DataMemberAttribute>()?.Name ?? Default(memberInfo);
		}

		/// <summary>
		/// Resolve validation parts capitalizing first letter
		/// </summary>
		public static string PascalCase(MemberInfo memberInfo)
		{
			return char.IsUpper(memberInfo.Name[0])
				? memberInfo.Name
				: char.ToUpper(memberInfo.Name[0]) + memberInfo.Name.Substring(1);
		}

		/// <summary>
		/// Resolve validation parts decapitalizing first letter
		/// </summary>
		public static string CamelCase(MemberInfo memberInfo)
		{
			return char.IsLower(memberInfo.Name[0])
				? memberInfo.Name
				: char.ToLower(memberInfo.Name[0]) + memberInfo.Name.Substring(1);
		}
	}
}