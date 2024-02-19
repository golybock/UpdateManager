using UM.Tools.Enums;

namespace UM.Tools.Extensions;

public static class StatusExtension
{
	public static IEnumerable<string> GetAsStringEnumerable()
	{
		return Enum
			.GetValues(typeof(Status))
			.Cast<Status>()
			.ToList()
			.Select(c => c.ToString());
	}
}