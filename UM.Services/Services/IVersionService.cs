using UM.Models.Files.Blank;
using UM.Models.Files.View;

namespace UM.Services.Services;

public interface IVersionService
{
	public Task<byte[]> GetUpdate(string build);

	public Task<byte[]> GetUpdate(Guid id);

	public Task<VersionView?> GetLastUpdate();

	public Task<IEnumerable<VersionView>> GetUpdates();

	public Task<Boolean> CreateVersion(VersionBlank versionBlank);
}