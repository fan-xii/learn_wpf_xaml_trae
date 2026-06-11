using System.Windows;
using System.Windows.Controls;
using LearnXaml.Models;

namespace LearnXaml.Modules.Module02_Controls.Views;

public partial class TreeViewDemo : Page
{
    public TreeViewDemo()
    {
        InitializeComponent();
        LoadData();
    }

    private void LoadData()
    {
        var fileTree = new List<FileNode>
        {
            new("📁", "我的电脑", new List<FileNode>
            {
                new("📂", "C: 系统盘", new List<FileNode>
                {
                    new("📂", "Windows", new List<FileNode>
                    {
                        new("📄", "System32"),
                        new("📄", "explorer.exe"),
                    }),
                    new("📂", "Program Files", new List<FileNode>
                    {
                        new("📂", "Microsoft Office", new List<FileNode>
                        {
                            new("📄", "Word.exe"),
                            new("📄", "Excel.exe"),
                        }),
                        new("📄", "VS Code"),
                    }),
                    new("📄", "bootmgr"),
                }),
                new("📂", "D: 数据盘", new List<FileNode>
                {
                    new("📂", "Projects", new List<FileNode>
                    {
                        new("📂", "LearnXaml", new List<FileNode>
                        {
                            new("📄", "App.xaml"),
                            new("📄", "MainWindow.xaml"),
                            new("📂", "Models", new List<FileNode>
                            {
                                new("📄", "Student.cs"),
                                new("📄", "Product.cs"),
                            }),
                            new("📂", "Modules"),
                        }),
                        new("📂", "MyApp"),
                    }),
                    new("📁", "Documents", new List<FileNode>
                    {
                        new("📄", "简历.docx"),
                        new("📄", "学习笔记.txt"),
                    }),
                    new("📁", "Downloads", new List<FileNode>
                    {
                        new("📄", "setup.exe"),
                    }),
                }),
                new("📂", "E: 移动存储"),
            }),
        };

        TvFileSystem.ItemsSource = fileTree;

        var categories = new List<TreeNodeItem>
        {
            new()
            {
                Name = "电子产品",
                Children = new List<TreeNodeItem>
                {
                    new() { Name = "手机", Children = new List<TreeNodeItem> { new() { Name = "iPhone" }, new() { Name = "华为" }, new() { Name = "小米" } } },
                    new() { Name = "电脑", Children = new List<TreeNodeItem> { new() { Name = "笔记本" }, new() { Name = "台式机" }, new() { Name = "一体机" } } },
                    new() { Name = "平板", Children = new List<TreeNodeItem> { new() { Name = "iPad" }, new() { Name = "Surface" } } },
                }
            },
            new()
            {
                Name = "家用电器",
                Children = new List<TreeNodeItem>
                {
                    new() { Name = "冰箱" },
                    new() { Name = "洗衣机" },
                    new() { Name = "空调" },
                }
            },
            new()
            {
                Name = "图书",
                Children = new List<TreeNodeItem>
                {
                    new() { Name = "编程", Children = new List<TreeNodeItem> { new() { Name = "C#" }, new() { Name = "Python" }, new() { Name = "Java" } } },
                    new() { Name = "小说" },
                    new() { Name = "历史" },
                }
            },
        };

        TvCategory.ItemsSource = categories;
    }

    private void TvBasic_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (e.NewValue is TreeViewItem item)
            TxtTvBasicSelect.Text = $"已选择: {item.Header}";
    }

    private void TvFileSystem_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (e.NewValue is FileNode node)
            TxtTvFileSelect.Text = $"已选择: {node.Icon} {node.Name}";
    }

    private void TvCategory_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (e.NewValue is TreeNodeItem node)
        {
            var hasChildren = node.Children.Count > 0;
            TxtTvCategorySelect.Text = hasChildren
                ? $"已选择分类: {node.Name}（含 {node.Children.Count} 个子类）"
                : $"已选择: {node.Name}（叶子节点）";
        }
    }

    private void BtnExpandAll_Click(object sender, RoutedEventArgs e)
    {
        ExpandAll(TvFileSystem);
    }

    private void BtnCollapseAll_Click(object sender, RoutedEventArgs e)
    {
        CollapseAll(TvFileSystem);
    }

    private static void ExpandAll(ItemsControl parent)
    {
        foreach (var item in parent.Items)
        {
            if (parent.ItemContainerGenerator.ContainerFromItem(item) is TreeViewItem tvItem)
            {
                tvItem.IsExpanded = true;
                tvItem.UpdateLayout();
                ExpandAll(tvItem);
            }
        }
    }

    private static void CollapseAll(ItemsControl parent)
    {
        foreach (var item in parent.Items)
        {
            if (parent.ItemContainerGenerator.ContainerFromItem(item) is TreeViewItem tvItem)
            {
                CollapseAll(tvItem);
                tvItem.IsExpanded = false;
            }
        }
    }
}