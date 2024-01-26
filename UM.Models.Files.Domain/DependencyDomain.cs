namespace UM.Models.Files.Domain;

public class DependencyDomain
{
	public Guid Id { get; private set; }

	public String Name { get; private set; }

	public String Version { get; private set; }

	public DependencyDomain(DependencyDatabase dependencyDatabase)
	{
		Id = dependencyDatabase.Id;
		Name = dependencyDatabase.Name;
		Version = dependencyDatabase.Version;
	}

	public DependencyDomain(Guid id, string name, string version)
	{
		Id = id;
		Name = name;
		Version = version;
	}
}