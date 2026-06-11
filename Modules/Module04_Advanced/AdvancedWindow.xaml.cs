using System.Windows;
using System.Windows.Controls;
using LearnXaml.Modules.Module04_Advanced.Views;

namespace LearnXaml.Modules.Module04_Advanced;

public partial class AdvancedWindow : Window
{
    private readonly Dictionary<string, Page> _pages = new()
    {
        ["DataBindingDemo"] = new DataBindingDemo(),
        ["MvvmDemo"] = new MvvmDemo(),
        ["AnimationDemo"] = new AnimationDemo(),
        ["CustomControlDemo"] = new CustomControlDemo(),
        ["ConverterDemo"] = new ConverterDemo(),
    };

    public AdvancedWindow()
    {
        InitializeComponent();
        NavigateFrame(DataBindingFrame, "DataBindingDemo");
    }

    private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0 && e.AddedItems[0] is TabItem selectedTab)
        {
            var tag = selectedTab.Tag as string;
            if (string.IsNullOrEmpty(tag)) return;

            Frame targetFrame = selectedTab.Header switch
            {
                "📊 数据绑定" => DataBindingFrame,
                "🏗 MVVM 模式" => MvvmFrame,
                "🎬 动画效果" => AnimationFrame,
                "🎛 自定义控件" => CustomControlFrame,
                "🔄 值转换器" => ConverterFrame,
                _ => DataBindingFrame
            };

            NavigateFrame(targetFrame, tag);
        }
    }

    private void NavigateFrame(Frame frame, string pageKey)
    {
        if (frame.Content != null) return;
        if (_pages.TryGetValue(pageKey, out var page))
        {
            frame.Navigate(page);
        }
    }
}