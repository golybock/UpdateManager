using MailKit.Net.Smtp;
using MimeKit;

namespace TaskWorkerApp;

public class EmailSender
{
	private const string Login = "techsupserp@mail.ru";
	private const string Password = "YRWQdj3vy98tevJx3w0u";

	public static async Task SendEmailAsync(string email, string text)
	{
		using var emailMessage = new MimeMessage();

		emailMessage.From.Add(new MailboxAddress("Support", Login));
		emailMessage.To.Add(new MailboxAddress("User", email));
		emailMessage.Subject = "Bug";
		emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
		{
			Text = text
		};

		using (var client = new SmtpClient())
		{
			await client.ConnectAsync("smtp.mail.ru", 465, true);
			await client.AuthenticateAsync(Login, Password);
			await client.SendAsync(emailMessage);

			await client.DisconnectAsync(true);
		}
	}
}