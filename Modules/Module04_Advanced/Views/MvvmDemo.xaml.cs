using System.Windows.Controls;
using LearnXaml.ViewModels;

namespace LearnXaml.Modules.Module04_Advanced.Views;

public partial class MvvmDemo : Page
{
    public MvvmDemo()
    {
        InitializeComponent();
        DataContext = new StudentViewModel();
    }
}