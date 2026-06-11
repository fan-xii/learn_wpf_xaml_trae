using System.Windows;
using System.Windows.Controls;

namespace LearnXaml.Modules.Module02_Controls.Views;

public partial class TextBoxDemo : Page
{
    public TextBoxDemo()
    {
        InitializeComponent();
    }

    private void TxtSingle_TextChanged(object sender, TextChangedEventArgs e)
    {
        TxtCharCount.Text = $"{TxtSingle.Text.Length} / {TxtSingle.MaxLength} 字符";
    }

    private void PwdBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        TxtPwdLength.Text = $"密码长度: {PwdBox.Password.Length}";
    }

    private void TxtPlaceholder_TextChanged(object sender, TextChangedEventArgs e)
    {
        TxtWatermark.Visibility = string.IsNullOrEmpty(TxtPlaceholder.Text)
            ? Visibility.Visible
            : Visibility.Collapsed;
    }

    private void TxtPlaceholder2_TextChanged(object sender, TextChangedEventArgs e)
    {
        TxtWatermark2.Visibility = string.IsNullOrEmpty(TxtPlaceholder2.Text)
            ? Visibility.Visible
            : Visibility.Collapsed;
    }

    private void TxtEditor_TextChanged(object sender, TextChangedEventArgs e)
    {
        TxtEditorCount.Text = TxtEditor.Text.Length.ToString();
        TxtLineCount.Text = TxtEditor.LineCount.ToString();
    }
}