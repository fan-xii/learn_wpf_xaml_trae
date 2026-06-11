using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using LearnXaml.Models;

namespace LearnXaml.Modules.Module02_Controls.Views;

public partial class ListControlsDemo : Page
{
    public static readonly IValueConverter IsEnrolledConverter = new BoolToColorConverter();

    public ListControlsDemo()
    {
        InitializeComponent();
        LoadData();
    }

    private void LoadData()
    {
        var students = new List<Student>
        {
            new() { Id = 1, Name = "张三", Age = 20, Grade = "大三", Email = "zhangsan@example.com", IsEnrolled = true },
            new() { Id = 2, Name = "李四", Age = 19, Grade = "大二", Email = "lisi@example.com", IsEnrolled = true },
            new() { Id = 3, Name = "王五", Age = 21, Grade = "大四", Email = "wangwu@example.com", IsEnrolled = false },
            new() { Id = 4, Name = "赵六", Age = 18, Grade = "大一", Email = "zhaoliu@example.com", IsEnrolled = true },
            new() { Id = 5, Name = "孙七", Age = 22, Grade = "研一", Email = "sunqi@example.com", IsEnrolled = true },
            new() { Id = 6, Name = "周八", Age = 20, Grade = "大三", Email = "zhouba@example.com", IsEnrolled = false },
        };

        LstStudents.ItemsSource = students;
        LvStudents.ItemsSource = students;
        CboStudent.ItemsSource = students;

        var items = new List<GroupedItem>
        {
            new("苹果", "🍎 水果"), new("香蕉", "🍎 水果"), new("橙子", "🍎 水果"), new("葡萄", "🍎 水果"), new("草莓", "🍎 水果"),
            new("白菜", "🥬 蔬菜"), new("萝卜", "🥬 蔬菜"), new("番茄", "🥬 蔬菜"), new("黄瓜", "🥬 蔬菜"), new("茄子", "🥬 蔬菜"),
            new("猪肉", "🥩 肉类"), new("牛肉", "🥩 肉类"), new("鸡肉", "🥩 肉类"), new("羊肉", "🥩 肉类"),
        };

        var cvs = new CollectionViewSource { Source = items };
        cvs.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
        LstGrouped.ItemsSource = cvs.View;
    }

    private void LstSingle_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (LstSingle.SelectedItem is ListBoxItem item)
            TxtSingleSelect.Text = $"已选择: {item.Content}";
    }

    private void LstMultiple_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = LstMultiple.SelectedItems.Cast<ListBoxItem>().Select(i => i.Content.ToString());
        TxtMultiSelect.Text = selected.Any()
            ? $"已选 {LstMultiple.SelectedItems.Count} 项: {string.Join(", ", selected)}"
            : "请选择你的爱好（可多选）";
    }

    private void LstStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (LstStudents.SelectedItem is Student s)
            TxtStudentSelect.Text = $"已选择: {s.Name}, {s.Age}岁, {s.Grade}, {s.Email}";
    }

    private void LvStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (LvStudents.SelectedItem is Student s)
            TxtListViewSelect.Text = $"已选择: {s.Name} | {s.Age}岁 | {s.Grade} | {s.Email}";
    }

    private void CboStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (CboStudent.SelectedItem is Student s)
            TxtComboSelect.Text = $"已选择: {s.Name} ({s.Age}岁, {s.Grade})";
    }
}

public class GroupedItem
{
    public string Name { get; set; }
    public string Category { get; set; }

    public GroupedItem(string name, string category)
    {
        Name = name;
        Category = category;
    }
}

internal class BoolToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool b)
            return b ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;
        return System.Windows.Media.Brushes.Gray;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}