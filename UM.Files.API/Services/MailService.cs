namespace UM.Files.API.Services;

public class MailService : BackgroundService
{
	private readonly string _login;
	private readonly string _password;

	public MailService(string login, string password)
	{
		_login = login;
		_password = password;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{

	}
}