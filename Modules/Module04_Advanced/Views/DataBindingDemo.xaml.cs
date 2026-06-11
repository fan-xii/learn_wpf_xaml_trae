using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using LearnXaml.ViewModels;

namespace LearnXaml.Modules.Module04_Advanced.Views;

public partial class DataBindingDemo : Page
{
    public DataBindingDemo()
    {
        InitializeComponent();
        DataContext = new DataBindingViewModel();
    }

    private void OnExplicitUpdateClick(object sender, RoutedEventArgs e)
    {
        var bindingExpression = ExplicitTextBox.GetBindingExpression(TextBox.TextProperty);
        bindingExpression?.UpdateSource();
    }
}

public class DataBindingViewModel : ObservableObject
{
    private string _customText = "这是来自代码后端的属性值";
    private double _sliderValue = 50;

    public string CustomText
    {
        get => _customText;
        set => SetProperty(ref _customText, value);
    }

    public double SliderValue
    {
        get => _sliderValue;
        set => SetProperty(ref _sliderValue, value);
    }
}