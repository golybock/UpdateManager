using Npgsql;
using Npgsql.Extension.Options;
using Npgsql.Extension.Repositories;
using UM.Models.Files;

namespace UM.Files.Repositories.Repositories;

public class VersionRepository : NpgsqlRepository, IVersionRepository
{
	private readonly String _versionTable = "version";
	private readonly String _dependencyTable = "dependency";
	private readonly String _versionDependencyTable = "version_dependencies";

	public VersionRepository(IDatabaseOptions databaseOptions) : base(databaseOptions) { }

	#region dependency

		public async Task<IEnumerable<DependencyDatabase>> GetDependenciesAsync()
	{
		var query = "select * from dependency";

		return await GetListAsync<DependencyDatabase>(query);
	}

	public async Task<IEnumerable<DependencyDatabase>> GetDependenciesAsync(Guid[] ids)
	{
		var query = "select * from dependency";

		var parameters = new[]
		{
			new NpgsqlParameter {Value = ids}
		};

		return await GetListAsync<DependencyDatabase>(query, parameters);
	}

	public async Task<DependencyDatabase?> GetDependencyAsync(Guid id)
	{
		var query = "select * from dependency where id = $1";

		var parameters = new[]
		{
			new NpgsqlParameter {Value = id}
		};

		return await GetAsync<DependencyDatabase>(query, parameters);
	}

	public async Task<DependencyDatabase?> GetDependencyAsync(string name)
	{
		var query = "select * from dependency where name = $1";

		var parameters = new[]
		{
			new NpgsqlParameter {Value = name}
		};

		return await GetAsync<DependencyDatabase>(query, parameters);
	}

	public async Task<IEnumerable<DependencyDatabase>> GetVersionDependenciesAsync(Guid versionId)
	{
		var query = "select * from dependency d join version_dependencies vd on d.id = vd.dependency_id" +
		            " where vd.version_id = $1";

		var parameters = new[]
		{
			new NpgsqlParameter {Value = versionId}
		};

		return await GetListAsync<DependencyDatabase>(query, parameters);
	}

	public async Task<Boolean> CreateDependencyAsync(DependencyDatabase dependencyDatabase)
	{
		var query = "insert into dependency(id, name, version) values ($1, $2, $3)";

		var parameters = new[]
		{
			new NpgsqlParameter {Value = dependencyDatabase.Id},
			new NpgsqlParameter {Value = dependencyDatabase.Name},
			new NpgsqlParameter {Value = dependencyDatabase.Version},
		};

		return await ExecuteAsync(query, parameters);
	}

	// todo transaction?
	public async Task<Boolean> CreateDependenciesAsync(IEnumerable<DependencyDatabase> dependenciesDatabase)
	{
		var results = new List<Boolean>(dependenciesDatabase.Count());

		await Parallel.ForEachAsync(dependenciesDatabase, async (database, token) =>
		{
			var res = await CreateDependencyAsync(database);

			results.Add(res);
		});

		return results.All(c => c);
	}

	public async Task<bool> CreateVersionDependencyAsync(Guid versionId, Guid dependencyDatabaseId)
	{
		var query = "insert into version_dependencies(version_id, dependency_id) values ($1, $2)";

		var parameters = new[]
		{
			new NpgsqlParameter {Value = versionId},
			new NpgsqlParameter {Value = dependencyDatabaseId},
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<bool> CreateVersionDependenciesAsync(Guid versionId, IEnumerable<Guid> dependenciesDatabaseIds)
	{
		var results = new List<Boolean>(dependenciesDatabaseIds.Count());

		await Parallel.ForEachAsync(dependenciesDatabaseIds, async (dependencyId, token) =>
		{
			var res = await CreateVersionDependencyAsync(versionId, dependencyId);

			results.Add(res);
		});

		return results.All(c => c);
	}

	public Task<Boolean> DeleteDependencyAsync(Guid id)
	{
		return DeleteAsync(_dependencyTable, "id", id);
	}

	public async Task<Boolean> DeleteDependenciesAsync(Guid[] ids)
	{
		var results = new List<Boolean>(ids.Count());

		await Parallel.ForEachAsync(ids, async (id, token) =>
		{
			var res = await DeleteDependencyAsync(id);

			results.Add(res);
		});

		return results.All(c => c);
	}

	public Task<Boolean> DeleteVersionDependenciesAsync(Guid versionId)
	{
		return DeleteAsync(_versionDependencyTable, "version_id", versionId);
	}

	public Task<Boolean> DeleteDependencyAsync(string name)
	{
		return DeleteAsync(_dependencyTable, "name", name);
	}

	#endregion

	#region version

	// todo check
	public async Task<IEnumerable<VersionDatabase>> GetVersionsAsync(bool archived = false)
	{
		var query = "select * from version where available = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = !archived}
		};

		return await GetListAsync<VersionDatabase>(query, parameters);
	}

	public async Task<IEnumerable<VersionDatabase>> GetVersionsAsync(Guid[] ids, bool archived = false)
	{
		var query = "select * from version where id = any($1) and available = $2";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = ids},
			new NpgsqlParameter() {Value = !archived},
		};

		return await GetListAsync<VersionDatabase>(query, parameters);
	}

	public async Task<IEnumerable<VersionDatabase>> GetVersionsAsync(int type)
	{
		var query = "select * from version where type = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = type}
		};

		return await GetListAsync<VersionDatabase>(query, parameters);
	}

	public async Task<VersionDatabase?> GetVersionAsync(Guid id)
	{
		var query = "select * from version where id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = id}
		};

		return await GetAsync<VersionDatabase>(query, parameters);
	}

	public async Task<VersionDatabase?> GetVersionAsync(string build)
	{
		var query = "select * from version where build = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter() {Value = build}
		};

		return await GetAsync<VersionDatabase>(query, parameters);
	}

	public async Task<Boolean> CreateVersionAsync(VersionDatabase versionDatabase)
	{
		var query = "insert into version(id, build, notes, type, path, available) values ($1, $2, $3, $4, $5, $6)";

		var parameters = new[]
		{
			new NpgsqlParameter {Value = versionDatabase.Id},
			new NpgsqlParameter {Value = versionDatabase.Build},
			new NpgsqlParameter {Value = versionDatabase.Notes},
			new NpgsqlParameter {Value = versionDatabase.Type},
			new NpgsqlParameter {Value = versionDatabase.Path},
			new NpgsqlParameter {Value = versionDatabase.Available},
		};

		return await ExecuteAsync(query, parameters);
	}

	public async Task<Boolean> CreateVersionAsync(VersionDatabase versionDatabase, IEnumerable<Guid> dependenciesDatabaseIds)
	{
		var createVersionResult = await CreateVersionAsync(versionDatabase);

		// todo!!!! create dependencies

		return createVersionResult;
	}

	public async Task<Boolean> UpdateVersionAsync(Guid id, VersionDatabase versionDatabase)
	{
		var query = "update version set notes = $2, type = $3, available = $4, path = $5 where id = $1";

		var parameters = new[]
		{
			new NpgsqlParameter {Value = versionDatabase.Id},
			new NpgsqlParameter {Value = versionDatabase.Notes},
			new NpgsqlParameter {Value = versionDatabase.Type},
			new NpgsqlParameter {Value = versionDatabase.Available},
			new NpgsqlParameter {Value = versionDatabase.Path},
		};

		return await ExecuteAsync(query, parameters);
	}

	// todo transacrion
	public async Task<Boolean> UpdateVersionAsync(Guid id, VersionDatabase versionDatabase, IEnumerable<Guid> dependenciesDatabaseIds)
	{
		var updateRes = await UpdateVersionAsync(id, versionDatabase);

		// remove all dependencies
		var deleteVersionDependenciesRes = await DeleteVersionDependenciesAsync(id);

		var createVersionDependenciesRes = await CreateVersionDependenciesAsync(id, dependenciesDatabaseIds);

		return updateRes && deleteVersionDependenciesRes && createVersionDependenciesRes;
	}

	public Task<Boolean> DeleteVersionAsync(Guid id)
	{
		return DeleteAsync(_versionTable, "id", id);
	}

	public Task<Boolean> DeleteVersionCascadeAsync(Guid id)
	{
		return DeleteCascadeAsync(_versionTable, "id", id);
	}

	public Task<Boolean> DeleteVersionAsync(string name)
	{
		return DeleteAsync(_versionTable, "name", name);
	}

	#endregion
}