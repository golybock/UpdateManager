using System.Windows;
using System.Windows.Controls;
using TaskWorkerApp.Data;
using UM.Tools.Crypto;

namespace TaskWorkerApp.Pages;

public partial class LoginPage : Page
{
	private readonly Frame _frame;
	private readonly UmFullContext _context = new UmFullContext();

	public LoginPage(Frame frame)
	{
		this._frame = frame;
		InitializeComponent();
	}

	private void EnterButton_OnClick(object sender, RoutedEventArgs e)
	{
		var hash = Sha256Hash.Hash(PasswordBox.Password);

		var user = _context.Workers.FirstOrDefault(c =>
			c.Login == LoginTextBox.Text && c.Password == hash);

		if (user == null)
		{
			MessageBox.Show("Пользователь не найден");
			return;
		}

		if (user.RoleId == 1)
		{
			_frame.Navigate(new AdminPage(_frame));
		}
		else
		{
			_frame.Navigate(new WorkerPage(user, _frame));
		}
	}
}