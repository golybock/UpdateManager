using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UM.Tools.Enums;

public enum Status : Int32
{
	[Display(Name = nameof(Created))]
	Created = 1,
	[Display(Name = nameof(InWork))]
	InWork = 2,
	[Display(Name = nameof(AttachedToWorker))]
	AttachedToWorker = 3,
	[Display(Name = nameof(Completed))]
	Completed = 4
}