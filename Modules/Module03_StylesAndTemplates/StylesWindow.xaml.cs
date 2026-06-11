using System.Windows;
using System.Windows.Controls;
using LearnXaml.Modules.Module03_StylesAndTemplates.Views;

namespace LearnXaml.Modules.Module03_StylesAndTemplates;

public partial class StylesWindow : Window
{
    private readonly Dictionary<string, Page> _pages = new()
    {
        ["StyleBasicsDemo"] = new StyleBasicsDemo(),
        ["ControlTemplateDemo"] = new ControlTemplateDemo(),
        ["DataTemplateDemo"] = new DataTemplateDemo(),
        ["ResourceDictionaryDemo"] = new ResourceDictionaryDemo(),
        ["TriggerDemo"] = new TriggerDemo(),
    };

    private readonly Dictionary<string, Frame> _frames;

    public StylesWindow()
    {
        InitializeComponent();
        _frames = new Dictionary<string, Frame>
        {
            ["StyleBasicsDemo"] = FrameStyleBasics,
            ["ControlTemplateDemo"] = FrameControlTemplate,
            ["DataTemplateDemo"] = FrameDataTemplate,
            ["ResourceDictionaryDemo"] = FrameResourceDict,
            ["TriggerDemo"] = FrameTrigger,
        };

        MainTabControl.SelectedIndex = 0;
        NavigateFrame("StyleBasicsDemo");
    }

    private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (MainTabControl.SelectedItem is TabItem tabItem && tabItem.Tag is string pageKey)
        {
            NavigateFrame(pageKey);
        }
    }

    private void NavigateFrame(string pageKey)
    {
        if (_frames.TryGetValue(pageKey, out var frame) && _pages.TryGetValue(pageKey, out var page))
        {
            if (frame.Content == null)
            {
                frame.Navigate(page);
            }
        }
    }
}