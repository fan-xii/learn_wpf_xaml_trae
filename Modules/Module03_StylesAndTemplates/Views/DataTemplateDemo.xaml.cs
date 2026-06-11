using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using LearnXaml.Models;

namespace LearnXaml.Modules.Module03_StylesAndTemplates.Views;

public partial class DataTemplateDemo : Page
{
    public DataTemplateDemo()
    {
        InitializeComponent();

        var students = new List<Student>
        {
            new() { Id = 1, Name = "张三", Age = 20, Grade = "大三", Email = "zhangsan@example.com", IsEnrolled = true, EnrollmentDate = new DateTime(2024, 9, 1) },
            new() { Id = 2, Name = "李四", Age = 21, Grade = "大四", Email = "lisi@example.com", IsEnrolled = true, EnrollmentDate = new DateTime(2023, 9, 1) },
            new() { Id = 3, Name = "王五", Age = 19, Grade = "大二", Email = "wangwu@example.com", IsEnrolled = false, EnrollmentDate = new DateTime(2024, 9, 1) },
            new() { Id = 4, Name = "赵六", Age = 22, Grade = "研一", Email = "zhaoliu@example.com", IsEnrolled = true, EnrollmentDate = new DateTime(2022, 9, 1) },
        };

        StudentCardList.ItemsSource = students;
        StudentCompactList.ItemsSource = students;
        StudentAutoList.ItemsSource = students;
        StudentComboBox.ItemsSource = students;
        StudentItemsControl.ItemsSource = students;
    }
}

public class EnrolledToBgConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool enrolled)
            return enrolled
                ? new SolidColorBrush(Color.FromRgb(0xE8, 0xF5, 0xE9))
                : new SolidColorBrush(Color.FromRgb(0xFF, 0xEB, 0xEE));
        return new SolidColorBrush(Colors.Transparent);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}

public class EnrolledToTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool enrolled)
            return enrolled ? "✅ 在读" : "❌ 休学";
        return "未知";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}