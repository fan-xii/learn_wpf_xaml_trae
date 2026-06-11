using System.Windows;
using System.Windows.Controls;

namespace LearnXaml.Modules.Module02_Controls.Views;

public partial class TabControlDemo : Page
{
    private int _tabCounter = 2;

    public TabControlDemo()
    {
        InitializeComponent();
        UpdateTabInfo();
    }

    private void BtnAddTab_Click(object sender, RoutedEventArgs e)
    {
        var header = $"📄 动态标签 {_tabCounter}";
        var tabItem = new TabItem { Header = header };

        var panel = new StackPanel
        {
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        };
        panel.Children.Add(new TextBlock
        {
            Text = $"这是动态创建的标签页 #{_tabCounter}",
            FontSize = 14,
            Foreground = System.Windows.Media.Brushes.Gray
        });
        panel.Children.Add(new TextBlock
        {
            Text = $"创建时间: {DateTime.Now:HH:mm:ss}",
            FontSize = 12,
            Foreground = System.Windows.Media.Brushes.LightGray,
            Margin = new Thickness(0, 5, 0, 0)
        });

        tabItem.Content = panel;
        TcDynamic.Items.Add(tabItem);
        TcDynamic.SelectedItem = tabItem;
        _tabCounter++;

        UpdateTabInfo();
    }

    private void BtnRemoveTab_Click(object sender, RoutedEventArgs e)
    {
        if (TcDynamic.SelectedIndex <= 0)
        {
            MessageBox.Show("不能删除固定标签页！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        TcDynamic.Items.RemoveAt(TcDynamic.SelectedIndex);
        UpdateTabInfo();
    }

    private void BtnRemoveAll_Click(object sender, RoutedEventArgs e)
    {
        while (TcDynamic.Items.Count > 1)
        {
            TcDynamic.Items.RemoveAt(TcDynamic.Items.Count - 1);
        }
        _tabCounter = 2;
        UpdateTabInfo();
    }

    private void TcDynamic_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        UpdateTabInfo();
    }

    private void UpdateTabInfo()
    {
        TxtTabCount.Text = $"当前标签数: {TcDynamic.Items.Count}";
        if (TcDynamic.SelectedItem is TabItem tab)
            TxtTabName.Text = $"当前选中: {tab.Header}";
    }
}