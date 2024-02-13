using UM.Models.Files;

namespace UM.Files.Repositories.Repositories;

// todo maybe create dependency repo
public interface IVersionRepository
{
	#region dependency

	public Task<IEnumerable<DependencyDatabase>> GetDependenciesAsync();

	public Task<IEnumerable<DependencyDatabase>> GetDependenciesAsync(Guid[] ids);

	public Task<DependencyDatabase?> GetDependencyAsync(Guid id);

	public Task<DependencyDatabase?> GetDependencyAsync(String name);

	public Task<IEnumerable<DependencyDatabase>> GetVersionDependenciesAsync(Guid versionId);

	public Task<Boolean> CreateDependencyAsync(DependencyDatabase dependencyDatabase);

	public Task<Boolean> CreateDependenciesAsync(IEnumerable<DependencyDatabase> dependenciesDatabase);

	public Task<Boolean> CreateVersionDependencyAsync(Guid versionId, Guid dependencyDatabaseId);

	public Task<Boolean> CreateVersionDependenciesAsync(Guid versionId, IEnumerable<Guid> dependenciesDatabaseIds);

	public Task<Boolean> DeleteDependencyAsync(Guid id);

	public Task<Boolean> DeleteDependenciesAsync(Guid[] ids);

	public Task<Boolean> DeleteVersionDependenciesAsync(Guid versionId);

	public Task<Boolean> DeleteDependencyAsync(String name);

	#endregion

	#region version

	public Task<IEnumerable<VersionDatabase>> GetVersionsAsync(Boolean archived = false);

	public Task<IEnumerable<VersionDatabase>> GetVersionsAsync(Guid[] ids, Boolean archived = false);

	public Task<IEnumerable<VersionDatabase>> GetVersionsAsync(Int32 type);

	public Task<VersionDatabase?> GetVersionAsync(Guid id);

	public Task<VersionDatabase?> GetVersionAsync(String build);

	public Task<Boolean> CreateVersionAsync(VersionDatabase versionDatabase);

	public Task<Boolean> CreateVersionAsync(VersionDatabase versionDatabase, IEnumerable<Guid> dependenciesDatabaseIds);

	public Task<Boolean> UpdateVersionAsync(Guid id, VersionDatabase versionDatabase);

	public Task<Boolean> UpdateVersionAsync(Guid id, VersionDatabase versionDatabase, IEnumerable<Guid> dependenciesDatabaseIds);

	public Task<Boolean> DeleteVersionAsync(Guid id);

	public Task<Boolean> DeleteVersionCascadeAsync(Guid id);

	public Task<Boolean> DeleteVersionAsync(String name);

	#endregion
}