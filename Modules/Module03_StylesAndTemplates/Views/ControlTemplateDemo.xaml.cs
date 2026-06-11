using System.Windows;
using System.Windows.Controls;

namespace LearnXaml.Modules.Module03_StylesAndTemplates.Views;

public partial class ControlTemplateDemo : Page
{
    public ControlTemplateDemo()
    {
        InitializeComponent();
    }

    private void ProgressTo0_Click(object sender, RoutedEventArgs e) => CustomProgress.Value = 0;
    private void ProgressTo30_Click(object sender, RoutedEventArgs e) => CustomProgress.Value = 30;
    private void ProgressTo65_Click(object sender, RoutedEventArgs e) => CustomProgress.Value = 65;
    private void ProgressTo100_Click(object sender, RoutedEventArgs e) => CustomProgress.Value = 100;
}