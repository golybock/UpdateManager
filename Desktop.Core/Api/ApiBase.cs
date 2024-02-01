using Desktop.Core.Models;

namespace Desktop.Core.Api;

public class ApiBase
{
	protected ApiBase(List<string> servers)
	{
		Servers = servers.Select(c => new Server() {Url = c, Available = null}).ToList();
	}

	public List<Server> Servers { get; }

	private async Task CheckAvailability()
	{
		foreach (var server in Servers)
		{
			try
			{
				var client = new HttpClient() {BaseAddress = new Uri(server.Url)};

				var res = await client.GetAsync("/");

				if (res.IsSuccessStatusCode)
				{
					server.Available = true;
				}
			}
			catch (Exception e)
			{
				server.Available = false;
			}
		}
	}

	protected async Task<HttpClient> InitHttpClient()
	{
		await CheckAvailability();

		var availableServer = Servers.FirstOrDefault(c => c.Available == true)?.Url ??
		                      throw new Exception("all servers not available");

		return new HttpClient() {BaseAddress = new Uri(availableServer)};
	}
}