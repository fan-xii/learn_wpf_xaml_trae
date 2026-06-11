using System.Windows;
using System.Windows.Controls;
using LearnXaml.Modules.Module01_Layout.Views;

namespace LearnXaml.Modules.Module01_Layout;

public partial class LayoutWindow : Window
{
    private readonly Dictionary<string, Page> _pages = new()
    {
        ["GridDemo"] = new GridDemo(),
        ["StackPanelDemo"] = new StackPanelDemo(),
        ["DockPanelDemo"] = new DockPanelDemo(),
        ["WrapPanelDemo"] = new WrapPanelDemo(),
        ["CanvasDemo"] = new CanvasDemo(),
        ["UniformGridDemo"] = new UniformGridDemo(),
    };

    public LayoutWindow()
    {
        InitializeComponent();
        MainListBox.SelectedIndex = 0;
        NavigateToPage("GridDemo");
    }

    private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (MainListBox.SelectedItem is ListBoxItem listBoxItem && listBoxItem.Tag is string pageName)
        {
            NavigateToPage(pageName);
        }
    }

    private void NavigateToPage(string pageName)
    {
        if (_pages.TryGetValue(pageName, out var page))
        {
            ContentFrame.Navigate(page);
        }
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}