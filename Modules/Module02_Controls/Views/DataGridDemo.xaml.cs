using System.Windows;
using System.Windows.Controls;
using LearnXaml.Models;

namespace LearnXaml.Modules.Module02_Controls.Views;

public partial class DataGridDemo : Page
{
    public DataGridDemo()
    {
        InitializeComponent();
        LoadData();
    }

    private void LoadData()
    {
        var products = new List<Product>
        {
            new() { Id = 1, Name = "机械键盘", Category = "电脑外设", Price = 299.00m, Stock = 50 },
            new() { Id = 2, Name = "无线鼠标", Category = "电脑外设", Price = 89.00m, Stock = 150 },
            new() { Id = 3, Name = "4K显示器", Category = "显示设备", Price = 2999.00m, Stock = 20 },
            new() { Id = 4, Name = "机械硬盘", Category = "存储设备", Price = 399.00m, Stock = 80 },
            new() { Id = 5, Name = "固态硬盘 SSD", Category = "存储设备", Price = 599.00m, Stock = 35 },
            new() { Id = 6, Name = "电竞耳机", Category = "音频设备", Price = 459.00m, Stock = 0 },
            new() { Id = 7, Name = "USB集线器", Category = "电脑外设", Price = 39.00m, Stock = 300 },
        };

        DgAuto.ItemsSource = products;

        var detailedProducts = new List<ProductDetail>
        {
            new() { Id = 1, Name = "机械键盘", Category = "电脑外设", Price = 299.00m, Stock = 50, IsOnSale = true, Description = "Cherry MX 青轴" },
            new() { Id = 2, Name = "无线鼠标", Category = "电脑外设", Price = 89.00m, Stock = 150, IsOnSale = true, Description = "2.4G 双模连接" },
            new() { Id = 3, Name = "4K显示器", Category = "显示设备", Price = 2999.00m, Stock = 20, IsOnSale = true, Description = "27寸 IPS 面板" },
            new() { Id = 4, Name = "机械硬盘", Category = "存储设备", Price = 399.00m, Stock = 80, IsOnSale = false, Description = "4TB 7200RPM" },
            new() { Id = 5, Name = "固态硬盘 SSD", Category = "存储设备", Price = 599.00m, Stock = 35, IsOnSale = true, Description = "1TB NVMe M.2" },
            new() { Id = 6, Name = "电竞耳机", Category = "音频设备", Price = 459.00m, Stock = 0, IsOnSale = false, Description = "7.1 虚拟声道" },
            new() { Id = 7, Name = "USB集线器", Category = "电脑外设", Price = 39.00m, Stock = 300, IsOnSale = true, Description = "4口 USB 3.0" },
        };

        DgManual.ItemsSource = detailedProducts;
        DgDetails.ItemsSource = detailedProducts;
    }

    private void DgAuto_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (DgAuto.SelectedItem is Product p)
            TxtDgAutoSelect.Text = $"已选择: {p.Name} | 价格: ¥{p.Price:F2} | 库存: {p.Stock}";
    }

    private void DgManual_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (DgManual.SelectedItem is ProductDetail p)
            TxtDgManualSelect.Text = $"已选择: {p.Name} | {(p.IsOnSale ? "在售" : "停售")} | ￥{p.Price:F2}";
    }

    private void DgDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var count = DgDetails.SelectedItems.Count;
        TxtDgDetailsSelect.Text = count > 0
            ? $"已选择 {count} 行（支持Shift/Ctrl多选）"
            : "点击行左侧可展开详情";
    }

    private void BtnSelectAll_Click(object sender, RoutedEventArgs e)
    {
        DgDetails.SelectedItems.Clear();
        foreach (var item in DgDetails.Items)
        {
            if (item is ProductDetail p && p.Stock > 0)
                DgDetails.SelectedItems.Add(item);
        }
    }

    private void BtnClearSelection_Click(object sender, RoutedEventArgs e)
    {
        DgDetails.SelectedItems.Clear();
    }
}