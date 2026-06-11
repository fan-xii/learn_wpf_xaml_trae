using System.Collections.Generic;
using System.Windows.Controls;
using LearnXaml.Models;

namespace LearnXaml.Modules.Module04_Advanced.Views;

public partial class ConverterDemo : Page
{
    public List<Product> Products { get; } = new()
    {
        new() { Id = 1, Name = "笔记本电脑", Category = "电子产品", Price = 6999, Stock = 15 },
        new() { Id = 2, Name = "机械键盘", Category = "外设", Price = 399, Stock = 50 },
        new() { Id = 3, Name = "鼠标垫", Category = "外设", Price = 49, Stock = 200 },
        new() { Id = 4, Name = "显示器", Category = "电子产品", Price = 2499, Stock = 8 },
        new() { Id = 5, Name = "耳机", Category = "音频", Price = 899, Stock = 30 },
        new() { Id = 6, Name = "数据线", Category = "配件", Price = 29, Stock = 500 },
    };

    public ConverterDemo()
    {
        InitializeComponent();
        DataContext = this;
    }
}