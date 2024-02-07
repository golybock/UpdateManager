using UM.Tools.Enums;

namespace UM.Tools.Extensions;

public static class UpdatesPeriodExtension
{
	public static IEnumerable<string> GetAsStringEnumerable()
	{
		return Enum
			.GetValues(typeof(UpdatesPeriod))
			.Cast<UpdatesPeriod>()
			.ToList()
			.Select(c => c.ToString());
	}
}