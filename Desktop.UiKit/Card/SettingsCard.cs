using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Desktop.UiKit.Card;

public class SettingsCard : ContentCard
{
	private static Grid _contentGrid = null!;
	private static Image _arrayIcon = null!;

	public SettingsCard()
	{
		Height = 60;
		MinWidth = 355;

		_contentGrid = new Grid();

		InitGrid();

		InitArrayIcon();
	}

	#region properties

	public static readonly DependencyProperty IconPathProperty =
		DependencyProperty.Register(
			nameof(IconPath),
			typeof(String),
			typeof(SettingsCard),
			new PropertyMetadata(IconPathPropertyChangedCallback));

	public String IconPath
	{
		get => (String) GetValue(IconPathProperty);
		set => SetValue(IconPathProperty, value);
	}

	public static readonly DependencyProperty ShowArrayProperty =
		DependencyProperty.Register(
			nameof(ShowArray),
			typeof(bool),
			typeof(SettingsCard),
			new PropertyMetadata(ShowArrayPropertyChangedCallback));

	public bool ShowArray
	{
		get => (bool) GetValue(ShowArrayProperty);
		set => SetValue(ShowArrayProperty, value);
	}

	public static readonly DependencyProperty HeaderProperty =
		DependencyProperty.Register(
			nameof(Header),
			typeof(String),
			typeof(SettingsCard),
			new PropertyMetadata(HeaderPropertyChangedCallback));

	public String? Header
	{
		get => (String) GetValue(HeaderProperty);
		set => SetValue(HeaderProperty, value);
	}

	public static readonly DependencyProperty DescriptionProperty =
		DependencyProperty.Register(
			nameof(Description),
			typeof(String),
			typeof(SettingsCard),
			new PropertyMetadata(DescriptionPropertyChangedCallback));

	public String? Description
	{
		get => (String) GetValue(DescriptionProperty);
		set => SetValue(DescriptionProperty, value);
	}

	public new static readonly DependencyProperty ContentProperty =
		DependencyProperty.Register(
			nameof(Child),
			typeof(UIElement),
			typeof(SettingsCard),
			new PropertyMetadata(ContentPropertyChangedCallback));

	public new UIElement Child
	{
		get => (UIElement) GetValue(ContentProperty);
		set => SetValue(ContentProperty, value);
	}

	#endregion

	#region events

	private static void IconPathPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		InitIcon(e.NewValue.ToString());
	}

	private static void ShowArrayPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var show = e.NewValue as bool?;

		if (show == true)
		{
			_arrayIcon.Visibility = Visibility.Visible;
		}

		if (show == false)
		{
			_arrayIcon.Visibility = Visibility.Collapsed;
		}
	}

	private static void HeaderPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		InitHeader(e.NewValue.ToString());
	}

	private static void DescriptionPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		InitDescription(e.NewValue.ToString());
	}

	private static void ContentPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var content = e.NewValue as UIElement;

		InitContent(content);
	}

	#endregion

	private void InitGrid()
	{
		_contentGrid.RowDefinitions.Add(new RowDefinition() {Height = new GridLength(50, GridUnitType.Star)});
		_contentGrid.RowDefinitions.Add(new RowDefinition() {Height = new GridLength(50, GridUnitType.Star)});

		_contentGrid.ColumnDefinitions.Add(new ColumnDefinition() {Width = new GridLength(0, GridUnitType.Auto)});
		_contentGrid.ColumnDefinitions.Add(new ColumnDefinition() {Width = new GridLength(50, GridUnitType.Star)});
		_contentGrid.ColumnDefinitions.Add(new ColumnDefinition() {Width = new GridLength(0, GridUnitType.Auto)});
		_contentGrid.ColumnDefinitions.Add(new ColumnDefinition() {Width = new GridLength(0, GridUnitType.Auto)});

		base.Child = _contentGrid;
	}

	private static void InitIcon(string? path)
	{
		var icon = new Image
		{
			Source = path == null ? null : new BitmapImage(new Uri(path, UriKind.Relative)),
			Margin = new Thickness(1)
		};

		Grid.SetRow(icon, 0);
		Grid.SetRowSpan(icon, 2);

		Grid.SetColumn(icon, 0);

		_contentGrid.Children.Add(icon);
	}

	private static void InitArrayIcon()
	{
		_arrayIcon = new Image
		{
			Margin = new Thickness(20, 3, 3, 3),
			Source = new BitmapImage(new Uri("/Desktop.UiKit;component/Resources/array_forward.png", UriKind.Relative)),
			Visibility = Visibility.Collapsed
		};

		Grid.SetRow(_arrayIcon, 0);
		Grid.SetColumn(_arrayIcon, 4);

		Grid.SetRowSpan(_arrayIcon, 2);

		_contentGrid.Children.Add(_arrayIcon);
	}

	private static void InitHeader(string? text)
	{
		var header = new TextBlock
		{
			Text = text,
			Margin = new Thickness(10, 0, 0, 0),
			FontWeight = FontWeights.Bold,
			VerticalAlignment = VerticalAlignment.Bottom,
			FontSize = 14
		};

		Grid.SetRow(header, 0);
		Grid.SetColumn(header, 1);

		_contentGrid.Children.Add(header);
	}

	private static void InitDescription(string? text)
	{
		var description = new TextBlock
		{
			Text = text,
			Margin = new Thickness(10, 0, 0, 0),
			VerticalAlignment = VerticalAlignment.Top,
			FontSize = 10
		};

		Grid.SetRow(description, 1);
		Grid.SetColumn(description, 1);

		_contentGrid.Children.Add(description);
	}

	private static void InitContent(UIElement? content)
	{
		if (content == null)
		{
			// todo set null
			return;
		}

		Grid.SetRow(content, 0);
		Grid.SetRowSpan(content, 2);

		Grid.SetColumn(content, 2);

		_contentGrid.Children.Add(content);
	}
}