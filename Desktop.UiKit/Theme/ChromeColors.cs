using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace Desktop.UiKit.Theme;

public static class ChromeColors
{
	public static SolidColorBrush SystemChromeLowColor => GetSolidColorBrushFromName(nameof(SystemChromeLowColor));

	public static SolidColorBrush SystemChromeMediumLowColor => GetSolidColorBrushFromName(nameof(SystemChromeMediumLowColor));

	public static SolidColorBrush SystemChromeMediumColor => GetSolidColorBrushFromName(nameof(SystemChromeMediumColor));

	public static SolidColorBrush SystemChromeHighColor => GetSolidColorBrushFromName(nameof(SystemChromeHighColor));

	public static SolidColorBrush SystemChromeAltLowColor => GetSolidColorBrushFromName(nameof(SystemChromeAltLowColor));

	public static SolidColorBrush SystemChromeDisabledLowColor => GetSolidColorBrushFromName(nameof(SystemChromeDisabledLowColor));

	public static SolidColorBrush SystemChromeDisabledHighColor => GetSolidColorBrushFromName(nameof(SystemChromeDisabledHighColor));

	public static SolidColorBrush SystemChromeBlackLowColor => GetSolidColorBrushFromName(nameof(SystemChromeBlackLowColor));

	public static SolidColorBrush SystemChromeBlackMediumLowColor => GetSolidColorBrushFromName(nameof(SystemChromeBlackMediumLowColor));


	private static SolidColorBrush GetSolidColorBrushFromName(string name)
	{
		var hex = Application.Current.Resources[name]?.ToString();

		if (hex == null)
			throw new ArgumentNullException(hex);

		return (SolidColorBrush) new BrushConverter().ConvertFrom(hex)!;
	}
}