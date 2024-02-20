using MailKit.Net.Smtp;
using MimeKit;

namespace TimetableManager
;

public static class ExceptionMailSender
{
	public static async Task SendEmailAsync(string message, string? email = null)
	{
		string pcInfo = $"Версия ос: {Environment.OSVersion.VersionString}\n";
		pcInfo += $"Ядер процессора: {Environment.ProcessorCount}\n";
		pcInfo += $"64 битная система: {Environment.Is64BitOperatingSystem}\n";

		var gcMemoryInfo = GC.GetGCMemoryInfo();
		long installedMemory = gcMemoryInfo.TotalAvailableMemoryBytes;
		// it will give the size of memory in MB
		var physicalMemory = (double) installedMemory / 1048576.0;
		pcInfo += $"Ram: {physicalMemory}MB\n";

		string sendTo = "techsupserp@mail.ru";

		string login = "suppersupporinstaller@mail.ru";
		string password = "NQ8ztXh2gqcMax0VFJeq";

		using var emailMessage = new MimeMessage();

		if (email != null)
		{
			message = email + "\n" + message;
		}

		emailMessage.From.Add(new MailboxAddress("BugReporter", login));
		emailMessage.To.Add(new MailboxAddress("Support", sendTo));
		emailMessage.Subject = "Bug";
		emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
		{
			Text = message + "\n" + pcInfo
		};

		using (var client = new SmtpClient())
		{
			await client.ConnectAsync("smtp.mail.ru", 465, true);
			await client.AuthenticateAsync(login, password);
			await client.SendAsync(emailMessage);

			await client.DisconnectAsync(true);
		}
	}
}