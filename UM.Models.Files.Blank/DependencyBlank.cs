namespace UM.Models.Files.Blank;

// не принимать от клиента, генерировать автоматически после загрузки файла
public class DependencyBlank
{
	public String Name { get; set; } = null!;

	public String Version { get; set; } = null!;
}