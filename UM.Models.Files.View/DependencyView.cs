using UM.Models.Files.Domain;

namespace UM.Models.Files.View;

public class DependencyView
{
	public Guid Id { get; private set; }

	public String Name { get; private set; }

	public String Version { get; private set; }

	public DependencyView(DependencyDomain dependencyDomain)
	{
		Id = dependencyDomain.Id;
		Name = dependencyDomain.Name;
		Version = dependencyDomain.Version;
	}

	public DependencyView(Guid id, string name, string version)
	{
		Id = id;
		Name = name;
		Version = version;
	}
}