using Desktop.Core.Models;

namespace Desktop.Core.Api;

public class ApiBase(Settings settings)
{
	protected readonly Settings Settings = settings;

	protected HttpClient HttpClient => new HttpClient() {BaseAddress = new Uri(Settings.Servers.First())};
}