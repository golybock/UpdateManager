using Npgsql;
using Npgsql.Extension.Options;
using Npgsql.Extension.Repositories;
using UM.Models.Tasks.Database;

namespace UM.Tasks.Repositories.Repositories.Worker;

public class WorkerRepository : NpgsqlRepository, IWorkerRepository
{
	public WorkerRepository(IDatabaseOptions databaseOptions) : base(databaseOptions) { }

	public async Task<IEnumerable<WorkerDatabase>> GetWorkersAsync()
	{
		var query = "SELECT * FROM worker";

		return await GetListAsync<WorkerDatabase>(query);
	}

	public async Task<WorkerDatabase?> GetWorkerAsync(Guid id)
	{
		var query = "SELECT * from worker where id = $1";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter(){Value = id}
		};

		return await GetAsync<WorkerDatabase>(query, parameters);
	}

	public async Task<WorkerDatabase?> GetWorkerAsync(string login, byte[] password)
	{
		var query = "SELECT * from worker where login = $1 and password = encode($2, 'hex')";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter(){Value = login},
			new NpgsqlParameter(){ Value = password}
		};

		return await GetAsync<WorkerDatabase>(query, parameters);
	}

	public async Task<bool> CreateWorkerAsync(WorkerDatabase workerDatabase)
	{
		var query = "INSERT INTO worker (id, login, password, full_name, role_id) VALUES ($1, $2, $3, $4, $5)";

		var parameters = new NpgsqlParameter[]
		{
			new NpgsqlParameter(){Value = workerDatabase.Id},
			new NpgsqlParameter(){Value = workerDatabase.Login},
			new NpgsqlParameter(){Value = workerDatabase.Password},
			new NpgsqlParameter(){Value = workerDatabase.FullName},
			new NpgsqlParameter(){Value = workerDatabase.RoleId},
		};

		return await ExecuteAsync(query, parameters);
	}
}