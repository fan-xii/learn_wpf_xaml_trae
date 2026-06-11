using System.Windows.Controls;
using LearnXaml.ViewModels;

namespace LearnXaml.Modules.Module03_StylesAndTemplates.Views;

public partial class TriggerDemo : Page
{
    public TriggerDemo()
    {
        InitializeComponent();
        DataContext = new StudentViewModel();
    }
}