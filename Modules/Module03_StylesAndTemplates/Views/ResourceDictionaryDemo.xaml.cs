using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LearnXaml.Modules.Module03_StylesAndTemplates.Views;

public partial class ResourceDictionaryDemo : Page
{
    private static readonly Color BlueBg = Color.FromRgb(0xE3, 0xF2, 0xFD);
    private static readonly Color BlueFg = Color.FromRgb(0x15, 0x65, 0xC0);
    private static readonly Color BlueBorder = Color.FromRgb(0x90, 0xCA, 0xF9);

    private static readonly Color GreenBg = Color.FromRgb(0xE8, 0xF5, 0xE9);
    private static readonly Color GreenFg = Color.FromRgb(0x2E, 0x7D, 0x32);
    private static readonly Color GreenBorder = Color.FromRgb(0xA5, 0xD6, 0xA7);

    private static readonly Color PurpleBg = Color.FromRgb(0xF3, 0xE5, 0xF5);
    private static readonly Color PurpleFg = Color.FromRgb(0x7B, 0x1F, 0xA2);
    private static readonly Color PurpleBorder = Color.FromRgb(0xCE, 0x93, 0xD8);

    private static readonly Color OrangeBg = Color.FromRgb(0xFF, 0xF3, 0xE0);
    private static readonly Color OrangeFg = Color.FromRgb(0xE6, 0x51, 0x00);
    private static readonly Color OrangeBorder = Color.FromRgb(0xFF, 0xCC, 0x80);

    private int _themeIndex;

    public ResourceDictionaryDemo()
    {
        InitializeComponent();
    }

    private void SwitchTheme_Click(object sender, RoutedEventArgs e)
    {
        _themeIndex = (_themeIndex + 1) % 4;

        var (bg, fg, border, name) = _themeIndex switch
        {
            0 => (BlueBg, BlueFg, BlueBorder, "蓝色"),
            1 => (GreenBg, GreenFg, GreenBorder, "绿色"),
            2 => (PurpleBg, PurpleFg, PurpleBorder, "紫色"),
            3 => (OrangeBg, OrangeFg, OrangeBorder, "橙色"),
            _ => (BlueBg, BlueFg, BlueBorder, "蓝色"),
        };

        Resources["DynamicBgBrush"] = new SolidColorBrush(bg);
        Resources["DynamicFgBrush"] = new SolidColorBrush(fg);
        Resources["DynamicBorderBrush"] = new SolidColorBrush(border);

        ThemeStatusText.Text = $"当前主题: {name} (DynamicResource 已更新，StaticResource 保持原样)";
    }
}