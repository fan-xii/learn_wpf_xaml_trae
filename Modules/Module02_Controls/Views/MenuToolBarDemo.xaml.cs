using System.Windows;
using System.Windows.Controls;

namespace LearnXaml.Modules.Module02_Controls.Views;

public partial class MenuToolBarDemo : Page
{
    public MenuToolBarDemo()
    {
        InitializeComponent();
        UpdateStatusBar();
    }

    private void UpdateStatusBar()
    {
        TxtCharCount2.Text = $"字符: {TxtContent.Text.Length}";
        TxtDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    private void LogAction(string action)
    {
        TxtMenuLog.Text = $"执行操作: {action}";
        TxtStatus.Text = $"最后操作: {action}";
        UpdateStatusBar();
    }

    private void FileNew_Click(object sender, RoutedEventArgs e) => LogAction("新建文件");
    private void FileOpen_Click(object sender, RoutedEventArgs e) => LogAction("打开文件");
    private void FileSave_Click(object sender, RoutedEventArgs e) => LogAction("保存文件");
    private void FileSaveAs_Click(object sender, RoutedEventArgs e) => LogAction("另存为...");
    private void FilePrint_Click(object sender, RoutedEventArgs e) => LogAction("打印");
    private void FileExit_Click(object sender, RoutedEventArgs e) => LogAction("退出程序");

    private void EditUndo_Click(object sender, RoutedEventArgs e) => LogAction("撤销");
    private void EditRedo_Click(object sender, RoutedEventArgs e) => LogAction("重做");
    private void EditCut_Click(object sender, RoutedEventArgs e) => LogAction("剪切");
    private void EditCopy_Click(object sender, RoutedEventArgs e) => LogAction("复制");
    private void EditPaste_Click(object sender, RoutedEventArgs e) => LogAction("粘贴");
    private void EditFind_Click(object sender, RoutedEventArgs e) => LogAction("查找");
    private void EditReplace_Click(object sender, RoutedEventArgs e) => LogAction("替换");

    private void MnuToolbar_Click(object sender, RoutedEventArgs e)
    {
        ToolBarPanel.Visibility = MnuToolbar.IsChecked ? Visibility.Visible : Visibility.Collapsed;
        LogAction(MnuToolbar.IsChecked ? "显示工具栏" : "隐藏工具栏");
    }

    private void MnuStatusBar_Click(object sender, RoutedEventArgs e)
    {
        StatusBarPanel.Visibility = MnuStatusBar.IsChecked ? Visibility.Visible : Visibility.Collapsed;
        LogAction(MnuStatusBar.IsChecked ? "显示状态栏" : "隐藏状态栏");
    }

    private void FontSize_Click(object sender, RoutedEventArgs e)
    {
        if (sender is MenuItem mi)
        {
            var size = mi.Tag?.ToString() switch
            {
                "Small" => 12,
                "Medium" => 16,
                "Large" => 22,
                _ => 14
            };
            TxtContent.FontSize = size;
            LogAction($"字体大小: {mi.Tag}");
        }
    }

    private void Theme_Click(object sender, RoutedEventArgs e)
    {
        if (sender is MenuItem mi)
        {
            MnuLight.IsChecked = mi.Tag?.ToString() == "Light";
            MnuDark.IsChecked = mi.Tag?.ToString() == "Dark";
            LogAction($"切换主题: {mi.Header}");
        }
    }

    private void HelpDoc_Click(object sender, RoutedEventArgs e) => LogAction("打开帮助文档");
    private void HelpOnline_Click(object sender, RoutedEventArgs e) => LogAction("打开在线资源");
    private void HelpAbout_Click(object sender, RoutedEventArgs e) => LogAction("关于");

    private void BtnToggleToolbar_Click(object sender, RoutedEventArgs e)
    {
        MnuToolbar.IsChecked = !MnuToolbar.IsChecked;
        MnuToolbar_Click(sender, e);
    }

    private void BtnToggleStatusbar_Click(object sender, RoutedEventArgs e)
    {
        MnuStatusBar.IsChecked = !MnuStatusBar.IsChecked;
        MnuStatusBar_Click(sender, e);
    }

    private void BtnClearMenuLog_Click(object sender, RoutedEventArgs e)
    {
        TxtMenuLog.Text = "等待操作...";
        TxtStatus.Text = "就绪";
    }
}