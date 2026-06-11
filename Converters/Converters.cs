using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace LearnXaml.Converters;

public class BoolToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            bool invert = parameter is string s && s.Equals("Invert", StringComparison.OrdinalIgnoreCase);
            return (boolValue ^ invert) ? Visibility.Visible : Visibility.Collapsed;
        }
        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility visibility)
            return visibility == Visibility.Visible;
        return false;
    }
}

public class AgeToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int age)
        {
            if (age < 18) return System.Windows.Media.Brushes.Green;
            if (age < 25) return System.Windows.Media.Brushes.Blue;
            return System.Windows.Media.Brushes.Orange;
        }
        return System.Windows.Media.Brushes.Gray;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class PriceToBackgroundConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is decimal price)
        {
            if (price > 500) return System.Windows.Media.Brushes.LightCoral;
            if (price > 100) return System.Windows.Media.Brushes.LightYellow;
            return System.Windows.Media.Brushes.LightGreen;
        }
        return System.Windows.Media.Brushes.White;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class StringNotEmptyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is string s && !string.IsNullOrWhiteSpace(s);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class RgbToColorConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length == 3
            && values[0] is double r
            && values[1] is double g
            && values[2] is double b)
        {
            byte red = (byte)Math.Clamp(r, 0, 255);
            byte green = (byte)Math.Clamp(g, 0, 255);
            byte blue = (byte)Math.Clamp(b, 0, 255);
            return Color.FromRgb(red, green, blue);
        }
        return Colors.Gray;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        if (value is Color color)
            return new object[] { (double)color.R, (double)color.G, (double)color.B };
        return new object[] { 0.0, 0.0, 0.0 };
    }
}

public class NotNullToBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool result = value != null;
        bool invert = parameter is string s && s.Equals("Invert", StringComparison.OrdinalIgnoreCase);
        return result ^ invert;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}