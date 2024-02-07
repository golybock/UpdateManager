using System.ComponentModel.DataAnnotations;

namespace UM.Tools.Enums;

public enum UpdatesPeriod : int
{
	[Display(Name = nameof(Never))]
	Never = 1,
	[Display(Name = nameof(OnStartup))]
	OnStartup = 2,
	[Display(Name = nameof(EveryDay))]
	EveryDay = 3,
	[Display(Name = nameof(EveryWeek))]
	EveryWeek = 4
}