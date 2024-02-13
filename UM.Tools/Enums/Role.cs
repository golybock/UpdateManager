using System.ComponentModel.DataAnnotations;

namespace UM.Tools.Enums;

public enum Role
{
	[Display(Name = nameof(Admin))]
	Admin,
	[Display(Name = nameof(Worker))]
	Worker
}