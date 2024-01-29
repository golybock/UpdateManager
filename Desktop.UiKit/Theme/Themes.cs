namespace Desktop.UiKit.Theme;

public class Themes
{
    private static Theme DayTheme => new() {Id = 1, Name = "Light"};
    private static Theme NightTheme => new() {Id = 2, Name = "Dark"};
    private static Theme Default => new() {Id = 0, Name = "Default"};

    public static List<Theme> SystemThemes => new()
    {
        DayTheme, NightTheme, Default
    };
}