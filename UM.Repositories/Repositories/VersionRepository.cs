using Npgsql.Extension.Options;
using Npgsql.Extension.Repositories;
using UM.Models.Files;

namespace UM.Repositories.Repositories;

public class VersionRepository : NpgsqlRepository, IVersionRepository
{
	public readonly String VersionTable = "version";
	public readonly String DependencyTable = "dependency";
	public readonly String VersionDependencyName = "version_dependencies";

	public VersionRepository(IDatabaseOptions databaseOptions) : base(databaseOptions) { }

	public async Task<IEnumerable<DependencyDatabase>> GetDependenciesAsync()
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<DependencyDatabase>> GetDependenciesAsync(Guid[] ids)
	{
		throw new NotImplementedException();
	}

	public async Task<DependencyDatabase?> GetDependencyAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public async Task<DependencyDatabase?> GetDependencyAsync(string name)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<DependencyDatabase>> GetVersionDependenciesAsync(Guid versionId)
	{
		throw new NotImplementedException();
	}

	public async Task<Boolean> CreateDependencyAsync(DependencyDatabase dependencyDatabase)
	{
		throw new NotImplementedException();
	}

	public async Task<Boolean> CreateDependenciesAsync(IEnumerable<DependencyDatabase> dependenciesDatabase)
	{
		throw new NotImplementedException();
	}

	public async Task<Boolean> DeleteDependencyAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public async Task<Boolean> DeleteDependenciesAsync(Guid[] ids)
	{
		throw new NotImplementedException();
	}

	public async Task<Boolean> DeleteVersionDependenciesAsync(Guid versionId)
	{
		throw new NotImplementedException();
	}

	public async Task<Boolean> DeleteDependencyAsync(string name)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<VersionDatabase>> GetVersionsAsync(bool withNotAvailable = false)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<VersionDatabase>> GetVersionsAsync(Guid[] ids, bool withNotAvailable = false)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<VersionDatabase>> GetVersionsAsync(int type)
	{
		throw new NotImplementedException();
	}

	public async Task<VersionDatabase> GetVersionAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public async Task<VersionDatabase> GetVersionAsync(string build)
	{
		throw new NotImplementedException();
	}

	public async Task<Boolean> CreateVersionAsync(VersionDatabase versionDatabase)
	{
		throw new NotImplementedException();
	}

	public async Task<Boolean> CreateVersionAsync(VersionDatabase versionDatabase, IEnumerable<DependencyDatabase> dependenciesDatabase)
	{
		throw new NotImplementedException();
	}

	public async Task<Boolean> UpdateVersionAsync(Guid id, VersionDatabase versionDatabase)
	{
		throw new NotImplementedException();
	}

	public async Task<Boolean> UpdateVersionAsync(Guid id, VersionDatabase versionDatabase, IEnumerable<DependencyDatabase> dependenciesDatabase)
	{
		throw new NotImplementedException();
	}

	public async Task<Boolean> DeleteVersionAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public async Task<Boolean> DeleteVersionCascadeAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task<Boolean> DeleteVersionAsync(string name)
	{
		return DeleteAsync(VersionTable, "name", name);
	}
}