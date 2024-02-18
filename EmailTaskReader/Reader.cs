using System.ComponentModel.DataAnnotations;
using EmailTaskReader.Data;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MimeKit;
using Task = System.Threading.Tasks.Task;

namespace EmailTaskReader;

public class Reader
{
	private const string Login = "techsupserp@mail.ru";
	private const string Password = "YRWQdj3vy98tevJx3w0u";

	public static async Task ReadEmails()
	{
		using var client = new ImapClient();
		await client.ConnectAsync("imap.mail.ru", 993, true);
		await client.AuthenticateAsync(Login, Password);

		var inbox = client.Inbox;
		await inbox.OpenAsync(FolderAccess.ReadWrite);
		var unread = inbox.FirstUnread;

		if (unread == 0)
		{
			Console.WriteLine("Сообщения не найдены");
			return;
		}

		var message = await inbox.GetMessageAsync(unread);

		if (message.Subject == "Error")
		{
			string? email = null;

			var emailTry = message.TextBody.Split('\n')[0];
			if (new EmailAddressAttribute().IsValid(emailTry))
			{
				email = emailTry;
			}

			await using UmFullContext context = new UmFullContext();

			var task = new Data.Task() {Description = message.TextBody, ClientEmail = email, Status = 1, Id = Guid.NewGuid()};
			context.Tasks.Add(task);
			await context.SaveChangesAsync();

			await inbox.AddFlagsAsync(unread, MessageFlags.Seen, true);

			if (email != null)
			{
				await SendEmailAsync(email, "Задача создана");
			}

			Console.WriteLine(string.Concat(Enumerable.Repeat("-", 64)));
			Console.WriteLine("Получено сообщение");
			Console.WriteLine(message.Subject);
			Console.WriteLine(message.TextBody);
			Console.WriteLine(string.Concat(Enumerable.Repeat("-", 64)));
		}
		else
		{
			await inbox.AddFlagsAsync(unread, MessageFlags.Seen, true);
		}

		await client.DisconnectAsync(true);
	}

	static async Task SendEmailAsync(string email, string text)
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