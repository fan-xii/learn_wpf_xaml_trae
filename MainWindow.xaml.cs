using System.Windows;
using LearnXaml.Modules.Module01_Layout;
using LearnXaml.Modules.Module02_Controls;
using LearnXaml.Modules.Module03_StylesAndTemplates;
using LearnXaml.Modules.Module04_Advanced;

namespace LearnXaml;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void BtnModule1_Click(object sender, RoutedEventArgs e)
    {
        new LayoutWindow { Owner = this }.ShowDialog();
    }

    private void BtnModule2_Click(object sender, RoutedEventArgs e)
    {
        new ControlsWindow { Owner = this }.ShowDialog();
    }

    private void BtnModule3_Click(object sender, RoutedEventArgs e)
    {
        new StylesWindow { Owner = this }.ShowDialog();
    }

    private void BtnModule4_Click(object sender, RoutedEventArgs e)
    {
        new AdvancedWindow { Owner = this }.ShowDialog();
    }
}