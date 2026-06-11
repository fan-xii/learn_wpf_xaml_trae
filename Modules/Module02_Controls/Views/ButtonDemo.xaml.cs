using System.Windows;
using System.Windows.Controls;

namespace LearnXaml.Modules.Module02_Controls.Views;

public partial class ButtonDemo : Page
{
    private int _repeatCount;

    public ButtonDemo()
    {
        InitializeComponent();
    }

    private void BtnNormal_Click(object sender, RoutedEventArgs e)
    {
        TxtEventLog.Text = "【普通按钮】被点击了！触发了 Click 事件。";
    }

    private void BtnColored_Click(object sender, RoutedEventArgs e)
    {
        TxtEventLog.Text = "【彩色按钮】被点击了！使用 AccentBrush 背景色。";
    }

    private void BtnDanger_Click(object sender, RoutedEventArgs e)
    {
        TxtEventLog.Text = "【危险按钮】被点击了！通常用于删除等危险操作。";
    }

    private void BtnSecondary_Click(object sender, RoutedEventArgs e)
    {
        TxtEventLog.Text = "【次要按钮】被点击了！使用 SecondaryBrush 背景色。";
    }

    private void ToggleBtn_Checked(object sender, RoutedEventArgs e)
    {
        TxtToggleStatus.Text = "已按下 (Checked)";
        TxtEventLog.Text = "【ToggleButton】状态切换为：已按下 (Checked)。适用于开关类操作。";
    }

    private void ToggleBtn_Unchecked(object sender, RoutedEventArgs e)
    {
        TxtToggleStatus.Text = "未按下 (Unchecked)";
        TxtEventLog.Text = "【ToggleButton】状态切换为：未按下 (Unchecked)。";
    }

    private void RepeatBtn_Click(object sender, RoutedEventArgs e)
    {
        _repeatCount++;
        TxtRepeatCount.Text = _repeatCount.ToString();
        TxtEventLog.Text = $"【RepeatButton】长按持续触发中... 第 {_repeatCount} 次。适用于滚动、增减等操作。";
    }

    private void BtnDefault_Click(object sender, RoutedEventArgs e)
    {
        TxtEventLog.Text = $"【确定按钮 (IsDefault)】被触发！文本框内容: \"{TxtDefaultCancelTest.Text}\"。按 Enter 键也能触发此按钮。";
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e)
    {
        TxtEventLog.Text = "【取消按钮 (IsCancel)】被触发！按 Escape 键也能触发此按钮。";
    }

    private void BtnIcon_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn)
            TxtEventLog.Text = $"【带图标按钮】「{btn.Tag}」被点击了！";
    }

    private void BtnClearLog_Click(object sender, RoutedEventArgs e)
    {
        TxtEventLog.Text = "等待按钮点击事件...";
        _repeatCount = 0;
        TxtRepeatCount.Text = "0";
        TxtToggleStatus.Text = "未按下";
    }
}