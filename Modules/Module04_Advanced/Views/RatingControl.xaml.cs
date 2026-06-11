using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LearnXaml.Modules.Module04_Advanced.Views;

public partial class RatingControl : UserControl
{
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(
            nameof(Value),
            typeof(int),
            typeof(RatingControl),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged));

    public static readonly DependencyProperty MaxRatingProperty =
        DependencyProperty.Register(
            nameof(MaxRating),
            typeof(int),
            typeof(RatingControl),
            new PropertyMetadata(5));

    public int Value
    {
        get => (int)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public int MaxRating
    {
        get => (int)GetValue(MaxRatingProperty);
        set => SetValue(MaxRatingProperty, value);
    }

    public RatingControl()
    {
        InitializeComponent();
        DataContext = this;
    }

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (RatingControl)d;
        control.UpdateStars();
    }

    private void UpdateStars()
    {
        var panel = StarsPanel;
        if (panel == null) return;

        for (int i = 0; i < panel.Children.Count; i++)
        {
            if (panel.Children[i] is TextBlock star)
            {
                star.Foreground = i < Value
                    ? new SolidColorBrush(Color.FromRgb(0xFF, 0xD7, 0x00))
                    : new SolidColorBrush(Color.FromRgb(0xE0, 0xE0, 0xE0));
            }
        }
    }

    private void OnStarMouseEnter(object sender, MouseEventArgs e)
    {
        if (sender is TextBlock star && star.Tag is int index)
        {
            var panel = StarsPanel;
            if (panel == null) return;

            for (int i = 0; i < panel.Children.Count; i++)
            {
                if (panel.Children[i] is TextBlock s)
                {
                    s.Foreground = i <= index
                        ? new SolidColorBrush(Color.FromRgb(0xFF, 0xCC, 0x00))
                        : new SolidColorBrush(Color.FromRgb(0xE0, 0xE0, 0xE0));
                }
            }
        }
    }

    private void OnStarMouseLeave(object sender, MouseEventArgs e)
    {
        UpdateStars();
    }

    private void OnStarClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is TextBlock star && star.Tag is int index)
        {
            Value = index + 1;
        }
    }
}