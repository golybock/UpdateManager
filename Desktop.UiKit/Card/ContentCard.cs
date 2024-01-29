using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Desktop.UiKit.Theme;

namespace Desktop.UiKit.Card;

public class ContentCard : Border
{
	public ContentCard()
	{
		BorderThickness = new Thickness(0);
		CornerRadius = new CornerRadius(10);
		BorderBrush = new SolidColorBrush(Colors.Transparent);
		Padding = new Thickness(12);

		SetBackground(ChromeColors.SystemChromeMediumLowColor);
	}

	#region properties

	// public static readonly DependencyProperty BackgroundColorProperty =
	// 	DependencyProperty.Register(
	// 		nameof(BackgroundColor),
	// 		typeof(String),
	// 		typeof(ContentCard),
	// 		new UIPropertyMetadata(null));
	//
	// public Brush BackgroundColor
	// {
	// 	get => (Brush) GetValue(BackgroundColorProperty);
	// 	set => SetValue(BackgroundColorProperty, value);
	// }
	//
	// public static readonly DependencyProperty BackgroundOnMouseEnterProperty =
	// 	DependencyProperty.Register(
	// 		nameof(BackgroundOnMouseEnter),
	// 		typeof(ChromeColors),
	// 		typeof(ContentCard),
	// 		new UIPropertyMetadata(null));
	//
	// public ChromeColors? BackgroundOnMouseEnter
	// {
	// 	get => (ChromeColors) GetValue(BackgroundOnMouseEnterProperty);
	// 	set => SetValue(BackgroundOnMouseEnterProperty, value);
	// }

	public static readonly DependencyProperty ChangeColorOnMouseEnterProperty =
		DependencyProperty.Register(
			nameof(ChangeColorOnMouseEnter),
			typeof(bool),
			typeof(ContentCard),
			new UIPropertyMetadata(null));

	public bool ChangeColorOnMouseEnter
	{
		get => (bool) GetValue(ChangeColorOnMouseEnterProperty);
		set => SetValue(ChangeColorOnMouseEnterProperty, value);
	}

	#endregion

	protected override void OnMouseEnter(MouseEventArgs e)
	{
		if (ChangeColorOnMouseEnter)
		{
			SetBackground(ChromeColors.SystemChromeMediumColor);
		}
	}

	protected override void OnMouseLeave(MouseEventArgs e)
	{
		if (ChangeColorOnMouseEnter)
		{
			SetBackground(ChromeColors.SystemChromeMediumLowColor);
		}
	}

	private void SetBackground(Brush color)
	{
		Background = color;
	}
}