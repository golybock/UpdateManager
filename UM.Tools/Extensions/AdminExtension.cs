using System.ComponentModel.DataAnnotations;
using System.Reflection;
using UM.Tools.Enums;

namespace UM.Tools.Extensions;

public static class AdminExtension
{
	public static IEnumerable<string> GetAsStringEnumerable()
	{
		return Enum
			.GetValues(typeof(Role))
			.Cast<Role>()
			.ToList()
			.Select(c => c.ToString());
	}
}