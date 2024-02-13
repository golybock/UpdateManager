using System.ComponentModel.DataAnnotations;

namespace UM.Tools.Enums;

public enum Priority : Int32
{
	[Display(Name = nameof(Minimum))]
	Minimum = 1,
	[Display(Name = nameof(Average))]
	Average = 2,
	[Display(Name = nameof(Maximum))]
	Maximum = 3
}