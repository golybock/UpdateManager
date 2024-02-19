using UM.Tools.Enums;

namespace UM.Tools.Extensions;

public static class PriorityExtension
{
	public static IEnumerable<string> GetAsStringEnumerable()
	{
		return Enum
			.GetValues(typeof(Priority))
			.Cast<Priority>()
			.ToList()
			.Select(c => c.ToString());
	}
}