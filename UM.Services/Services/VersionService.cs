using UM.Models.Files;
using UM.Models.Files.Blank;
using UM.Models.Files.Domain;
using UM.Models.Files.View;
using UM.Repositories.Repositories;

namespace UM.Services.Services;

public class VersionService : IVersionService
{
	private readonly IVersionRepository _versionRepository;

	public VersionService(IVersionRepository versionRepository)
	{
		_versionRepository = versionRepository;
	}

	public async Task<byte[]> GetUpdate(string build)
	{
		var version = await _versionRepository.GetVersionAsync(build);

		if (version == null)
			throw new Exception("version not found");

		var path = $"Updates/{version.Path}";

		if (!File.Exists(path))
			throw new Exception("file not found");

		var bytes = await File.ReadAllBytesAsync(path);

		return bytes;

		// return File(bytes, "application/zip");
	}

	public async Task<byte[]> GetUpdate(Guid id)
	{
		var version = await _versionRepository.GetVersionAsync(id);

		if (version == null)
			throw new Exception("version not found");

		var path = $"Updates/{version.Path}";

		if (!File.Exists(path))
			throw new Exception("file not found");

		var bytes = await File.ReadAllBytesAsync(path);

		return bytes;

		// return File(bytes, "application/zip");
	}

	public async Task<VersionView?> GetLastUpdate()
	{
		var versions = await _versionRepository.GetVersionsAsync();

		var lastVersion = versions.MaxBy(c => c.Timestamp);

		if (lastVersion == null)
			throw new Exception("version not found");

		var lastVersionDomain = new VersionDomain(lastVersion);

		var lastVersionView = new VersionView(lastVersionDomain);

		return lastVersionView;
	}

	public async Task<IEnumerable<VersionView>> GetUpdates()
	{
		var versions = await _versionRepository.GetVersionsAsync();

		var versionDomains = versions.Select(version => new VersionDomain(version));

		var versionViews = versionDomains.Select(version => new VersionView(version));

		return versionViews;
	}

	public async Task<bool> CreateVersion(VersionBlank versionBlank)
	{
		var versionDatabase = new VersionDatabase(versionBlank);

		var result = await _versionRepository.CreateVersionAsync(versionDatabase);

		return result;
	}
}