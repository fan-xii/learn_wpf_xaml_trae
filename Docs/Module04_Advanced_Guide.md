# 模块四：高级主题

## 一、数据绑定深度解析

数据绑定（Data Binding）是 WPF 最强大的特性之一，也是 MVVM 架构的核心基石。它让 UI 与数据自动同步，大幅减少样板代码。

### 绑定的四大要素

每个绑定都包含四个要素：

| 要素 | 说明 |
|------|------|
| **绑定目标** | 通常是 UI 控件的依赖属性（如 `TextBox.Text`） |
| **绑定源** | 提供数据的对象（如 ViewModel） |
| **绑定路径** | 源对象上的属性路径（如 `Student.Name`） |
| **绑定模式** | 数据流向的方式 |

### 绑定模式（Mode）

| 模式 | 说明 | 常见场景 |
|------|------|----------|
| `OneWay` | 源→目标，源变更时更新目标 | 只读显示标签 |
| `TwoWay` | 源↔目标，双向同步 | 可编辑表单 |
| `OneTime` | 源→目标，仅在初始化时更新一次 | 静态数据展示 |
| `OneWayToSource` | 目标→源，反向更新 | 不常见 |
| `Default` | 使用目标属性的默认模式 | 大多数情况 |

```xml
<TextBox Text="{Binding Name, Mode=TwoWay}" />
<TextBlock Text="{Binding Status, Mode=OneWay}" />
```

### 绑定源（Source）

有多种方式指定绑定源：

```xml
<!-- 1. DataContext（最常用，沿逻辑树继承） -->
<Window.DataContext>
    <local:StudentViewModel />
</Window.DataContext>
<TextBlock Text="{Binding Name}" />

<!-- 2. ElementName（绑定到另一个元素） -->
<Slider x:Name="FontSizeSlider" Minimum="10" Maximum="40" />
<TextBlock Text="缩放文本" FontSize="{Binding Value, ElementName=FontSizeSlider}" />

<!-- 3. RelativeSource（绑定到自身或祖先元素） -->
<TextBlock Text="{Binding Title, RelativeSource={RelativeSource AncestorType=Window}}" />
<TextBlock Text="{Binding Width, RelativeSource={RelativeSource Self}}" />

<!-- 4. Source 属性直接指定 -->
<TextBlock Text="{Binding Name, Source={StaticResource SomeObject}}" />
```

### UpdateSourceTrigger

控制 `TwoWay` 绑定何时将目标值写回源属性：

| 值 | 说明 | 适用场景 |
|------|------|----------|
| `PropertyChanged` | 每次属性变化时立即更新 | 实时搜索、即时验证 |
| `LostFocus` | 控件失去焦点时更新（Text 属性的默认值） | 表单输入 |
| `Explicit` | 需要手动调用 `UpdateSource()` | 批量提交表单 |

```xml
<TextBox Text="{Binding SearchText, UpdateSourceTrigger=PropertyChanged}" />
```

### StringFormat

在绑定中直接格式化数据：

```xml
<TextBlock Text="{Binding Price, StringFormat=C}" />               <!-- ¥123.45 -->
<TextBlock Text="{Binding BirthDate, StringFormat=yyyy年MM月dd日}" /> <!-- 2023年01月15日 -->
<TextBlock Text="{Binding Progress, StringFormat={}{0:P1}}" />      <!-- 75.0% -->
```

### INotifyPropertyChanged

要让绑定的数据源在属性变化时自动通知 UI，数据对象的类必须实现 `INotifyPropertyChanged` 接口：

```csharp
public class Student : INotifyPropertyChanged
{
    private string _name = string.Empty;

    public string Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}
```

本项目中的 `ObservableObject` 基类封装了这一逻辑，让 ViewModel 编写更简洁：

```csharp
public class StudentViewModel : ObservableObject
{
    private string _nameFilter = string.Empty;

    public string NameFilter
    {
        get => _nameFilter;
        set => SetProperty(ref _nameFilter, value);  // 自动比较并通知
    }
}
```

---

## 二、MVVM 模式

### MVVM 架构概述

MVVM（Model-View-ViewModel）是 WPF 开发的核心架构模式：

```
┌─────────────────────────────────────────────────┐
│                      View                        │
│               (XAML + Code-Behind)               │
│         只负责 UI 呈现，不包含业务逻辑              │
└──────────────────┬──────────────────────────────┘
                   │  Data Binding / Commands
┌──────────────────▼──────────────────────────────┐
│                   ViewModel                      │
│         (属性和命令，连接 View 和 Model)           │
│          实现 INotifyPropertyChanged             │
└──────────────────┬──────────────────────────────┘
                   │
┌──────────────────▼──────────────────────────────┐
│                    Model                         │
│           (数据模型 + 业务逻辑 + 数据访问)          │
└─────────────────────────────────────────────────┘
```

#### 核心原则

1. **View 不知道 Model**：View 只和 ViewModel 交互，不直接访问 Model
2. **ViewModel 不知道 View**：ViewModel 不引用任何 UI 类型，方便测试
3. **Model 独立存在**：纯数据类，不依赖 View 或 ViewModel

### ICommand 与 RelayCommand

在 MVVM 中，按钮点击等操作通过命令（Command）绑定实现，而不是使用 Click 事件：

```csharp
// RelayCommand 实现（本项目 ViewModels/RelayCommand.cs）
public class RelayCommand : ICommand
{
    private readonly Action<object?> _execute;
    private readonly Func<object?, bool>? _canExecute;

    public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;
    public void Execute(object? parameter) => _execute(parameter);
}
```

在 ViewModel 中使用：

```csharp
public class StudentViewModel : ObservableObject
{
    public RelayCommand AddStudentCommand { get; }
    public RelayCommand DeleteStudentCommand { get; }

    public StudentViewModel()
    {
        AddStudentCommand = new RelayCommand(AddStudent);
        DeleteStudentCommand = new RelayCommand(DeleteStudent, _ => SelectedStudent != null);
    }

    private void AddStudent(object? _)
    {
        Students.Add(new Student { Id = Students.Count + 1, Name = "新同学" });
    }

    private void DeleteStudent(object? _)
    {
        if (SelectedStudent != null)
            Students.Remove(SelectedStudent);
    }
}
```

在 XAML 中绑定：

```xml
<Button Content="添加" Command="{Binding AddStudentCommand}" />
<Button Content="删除" Command="{Binding DeleteStudentCommand}" />
```

当 `CanExecute` 返回 `false` 时，绑定了该命令的按钮会自动禁用，无需手动管理 `IsEnabled` 属性。

### DataContext 设置方式

```xml
<!-- 方式一：在 XAML 中直接设置 -->
<Window.DataContext>
    <local:StudentViewModel />
</Window.DataContext>

<!-- 方式二：在代码中设置 -->
```

```csharp
public MainWindow()
{
    InitializeComponent();
    DataContext = new StudentViewModel();
}
```

### ObservableCollection

`ObservableCollection<T>` 在集合内容变化时自动通知 UI 更新，是 MVVM 中展示列表数据的首选集合类型：

```csharp
public ObservableCollection<Student> Students { get; } = new()
{
    new() { Id = 1, Name = "张三", Age = 20 },
    new() { Id = 2, Name = "李四", Age = 21 },
};

// 增删操作会自动反映到 UI
Students.Add(new Student { Id = 3, Name = "王五" });
Students.RemoveAt(0);
```

---

## 三、值转换器（Value Converters）

值转换器在数据绑定的「管道」中充当中间件，将源值转换为目标需要的格式。

### IValueConverter

```csharp
public class BoolToVisibilityConverter : IValueConverter
{
    // 源 → 目标
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            bool invert = parameter is string s && s.Equals("Invert", StringComparison.OrdinalIgnoreCase);
            return (boolValue ^ invert) ? Visibility.Visible : Visibility.Collapsed;
        }
        return Visibility.Collapsed;
    }

    // 目标 → 源（TwoWay 绑定时使用）
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility visibility)
            return visibility == Visibility.Visible;
        return false;
    }
}
```

使用方式：

```xml
<!-- 先声明为资源 -->
<local:BoolToVisibilityConverter x:Key="BoolToVis" />

<!-- 使用 -->
<Button Visibility="{Binding IsLoggedIn, Converter={StaticResource BoolToVis}}" />
<!-- 反向逻辑 -->
<Button Visibility="{Binding IsLoggedIn, Converter={StaticResource BoolToVis}, ConverterParameter=Invert}" />
```

### 项目中的转换器示例

本项目实现了四个转换器（[Converters/Converters.cs](file:///d:/liqiang/develop/Trae_Pro/learn_xaml/Converters/Converters.cs#L1-L76)）：

| 转换器 | 功能 |
|--------|------|
| `BoolToVisibilityConverter` | `bool` → `Visibility`，支持反向参数 |
| `AgeToColorConverter` | `int`年龄 → 颜色画刷（<18绿色, <25蓝色, 其他橙色） |
| `PriceToBackgroundConverter` | `decimal`价格 → 背景色（>500红色, >100黄色, 其他绿色） |
| `StringNotEmptyConverter` | `string` → `bool`（非空字符串返回 true） |

### IMultiValueConverter

当需要组合多个绑定值时使用：

```csharp
public class FullNameConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        return $"{values[0]} {values[1]}";
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
```

```xml
<TextBlock>
    <TextBlock.Text>
        <MultiBinding StringFormat="{}{0} {1}">
            <Binding Path="FirstName" />
            <Binding Path="LastName" />
        </MultiBinding>
    </TextBlock.Text>
</TextBlock>
```

### 常见转换器模式

- **布尔取反**：`!boolValue` → `Visibility.Visible`
- **枚举转换**：`enum Status.Active` → `"活跃"`
- **空值处理**：`null` → `"暂无数据"`
- **格式转换**：时间戳 → 日期、字节数 → 文件大小

---

## 四、动画

WPF 的动画系统允许你为控件的属性添加动态变化效果，极大地提升用户体验。

### Storyboard

`Storyboard` 是动画的容器，可以包含多个动画并协调它们的播放：

```xml
<Storyboard x:Key="FadeInAnimation">
    <DoubleAnimation Storyboard.TargetProperty="Opacity"
                     From="0" To="1" Duration="0:0:0.5" />
    <DoubleAnimation Storyboard.TargetProperty="(UIElement.RenderTransform).(TranslateTransform.X)"
                     From="-50" To="0" Duration="0:0:0.5">
        <DoubleAnimation.EasingFunction>
            <CubicEase EasingMode="EaseOut" />
        </DoubleAnimation.EasingFunction>
    </DoubleAnimation>
</Storyboard>
```

### 关键动画类型

| 动画类型 | 用途 | 示例 |
|----------|------|------|
| `DoubleAnimation` | 动画化 `double` 属性 | `Opacity`、`Width`、`Height` |
| `ColorAnimation` | 动画化颜色 | `Background.Color` |
| `ThicknessAnimation` | 动画化边距 | `Margin`、`Padding` |
| `PointAnimation` | 动画化坐标点 | 路径动画 |

### 常用动画属性

```xml
<!-- 淡入效果 -->
<DoubleAnimation Storyboard.TargetProperty="Opacity"
                 From="0" To="1" Duration="0:0:0.3" />

<!-- 旋转效果 -->
<DoubleAnimation Storyboard.TargetProperty="(UIElement.RenderTransform).(RotateTransform.Angle)"
                 From="0" To="360" Duration="0:0:1"
                 RepeatBehavior="Forever" />

<!-- 缩放弹跳效果 -->
<DoubleAnimation Storyboard.TargetProperty="(UIElement.RenderTransform).(ScaleTransform.ScaleX)"
                 From="0.8" To="1" Duration="0:0:0.4"
                 AutoReverse="True" />
```

### 缓动函数（Easing Functions）

缓动函数让动画更加自然和生动：

| 缓动函数 | 效果 |
|----------|------|
| `CubicEase` | 三次方缓动 |
| `ElasticEase` | 弹性效果 |
| `BounceEase` | 弹跳效果 |
| `BackEase` | 回退效果（超出后再回到目标值） |
| `PowerEase` | 指数缓动，可通过 Power 属性控制强度 |
| `SineEase` | 正弦缓动 |

```xml
<DoubleAnimation.EasingFunction>
    <ElasticEase EasingMode="EaseOut" Oscillations="3" Springiness="5" />
</DoubleAnimation.EasingFunction>
```

### 变换（Transforms）

变换可以在不改变控件实际布局的前提下，对控件进行视觉变换：

```xml
<!-- 旋转 -->
<Button Content="旋转按钮">
    <Button.RenderTransform>
        <RotateTransform Angle="45" CenterX="50" CenterY="25" />
    </Button.RenderTransform>
</Button>

<!-- 缩放 -->
<Button Content="缩放按钮">
    <Button.RenderTransform>
        <ScaleTransform ScaleX="1.5" ScaleY="1.5" />
    </Button.RenderTransform>
</Button>

<!-- 倾斜 -->
<Button Content="倾斜按钮">
    <Button.RenderTransform>
        <SkewTransform AngleX="20" AngleY="10" />
    </Button.RenderTransform>
</Button>

<!-- 组合变换 -->
<Button Content="组合变换">
    <Button.RenderTransform>
        <TransformGroup>
            <ScaleTransform ScaleX="1.2" ScaleY="1.2" />
            <RotateTransform Angle="15" />
        </TransformGroup>
    </Button.RenderTransform>
</Button>
```

### 在代码中触发动画

```csharp
private void StartFadeIn()
{
    var storyboard = (Storyboard)FindResource("FadeInAnimation");
    storyboard.Begin(MyElement);
}
```

---

## 五、自定义控件

### UserControl vs Custom Control

| 特性 | UserControl | Custom Control |
|------|-------------|----------------|
| 创建方式 | XAML + Code-Behind | 纯 C# 代码 + Generic.xaml |
| 复用方式 | 组装现有控件 | 从零定义行为 |
| 模板支持 | 不直接支持 | 完全支持 ControlTemplate |
| 适用场景 | 特定页面片段 | 通用控件库 |
| 学习难度 | ⭐⭐ | ⭐⭐⭐⭐ |

### UserControl 示例

创建一个带标签的文本框：

```xml
<!-- LabeledTextBox.xaml -->
<UserControl x:Class="LearnXaml.Controls.LabeledTextBox">
    <StackPanel>
        <TextBlock Text="{Binding Label, RelativeSource={RelativeSource AncestorType=UserControl}}"
                   FontWeight="SemiBold" Margin="0,0,0,3" />
        <TextBox Text="{Binding Text, RelativeSource={RelativeSource AncestorType=UserControl}}"
                 Width="250" />
    </StackPanel>
</UserControl>
```

```csharp
// LabeledTextBox.xaml.cs
public partial class LabeledTextBox : UserControl
{
    public static readonly DependencyProperty LabelProperty =
        DependencyProperty.Register(nameof(Label), typeof(string), typeof(LabeledTextBox));

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(LabeledTextBox),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}
```

使用：

```xml
<controls:LabeledTextBox Label="用户名：" Text="{Binding UserName}" />
<controls:LabeledTextBox Label="邮箱：" Text="{Binding Email}" />
```

### 依赖属性（Dependency Property）

依赖属性是 WPF 的核心机制，支持以下功能：
- **数据绑定**：作为绑定的目标
- **样式**：通过 Style Setter 设置
- **动画**：作为动画的目标属性
- **属性值继承**：从父元素继承值
- **默认值和验证**：内置的默认值、值变更回调和强制值回调

```csharp
public static readonly DependencyProperty ValueProperty =
    DependencyProperty.Register(
        nameof(Value),                    // 属性名称
        typeof(int),                      // 属性类型
        typeof(MyControl),                // 所有者类型
        new PropertyMetadata(0, OnValueChanged, CoerceValue));

public int Value
{
    get => (int)GetValue(ValueProperty);
    set => SetValue(ValueProperty, value);
}

private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
{
    // 值变更时的回调
    var control = (MyControl)d;
    var oldValue = (int)e.OldValue;
    var newValue = (int)e.NewValue;
}

private static object CoerceValue(DependencyObject d, object baseValue)
{
    // 强制值回调：限制值范围
    var value = (int)baseValue;
    return Math.Max(0, Math.Min(100, value));
}
```

### 附加属性（Attached Property）

附加属性允许一个控件为另一个控件定义属性。典型例子是 `Grid.Row` 和 `DockPanel.Dock`：

```csharp
public static class WatermarkHelper
{
    public static readonly DependencyProperty WatermarkProperty =
        DependencyProperty.RegisterAttached(
            "Watermark",
            typeof(string),
            typeof(WatermarkHelper),
            new PropertyMetadata(string.Empty));

    public static string GetWatermark(DependencyObject obj)
        => (string)obj.GetValue(WatermarkProperty);

    public static void SetWatermark(DependencyObject obj, string value)
        => obj.SetValue(WatermarkProperty, value);
}
```

```xml
<TextBox local:WatermarkHelper.Watermark="请输入搜索内容" />
```

---

## 六、性能优化与调试

### 性能优化要点

1. **减少布局嵌套**：每层面板嵌套都会增加布局计算的复杂度，建议不超过 5 层。
2. **使用 VirtualizingStackPanel**：DataGrid、ListBox 等集合控件默认启用虚拟化，只渲染可见项。确保 `VirtualizingStackPanel.IsVirtualizing="True"`。
3. **延迟加载/按需加载**：使用 `DeferrableContent` 或在代码中按需加载数据。
4. **冻结 Freezable 对象**：对画刷、画笔等不可变资源调用 `Freeze()` 方法以提升性能。
5. **避免使用 `DockPanel` 作为深层嵌套**：`DockPanel` 每次布局都会重新计算所有子元素。

```xml
<!-- 确保虚拟化开启 -->
<ListBox ItemsSource="{Binding LargeCollection}"
         VirtualizingStackPanel.IsVirtualizing="True"
         VirtualizingStackPanel.VirtualizationMode="Recycling" />
```

### 调试技巧

#### WPF 调试输出

在 `App.xaml.cs` 或 `MainWindow` 构造函数中添加：

```csharp
// 启用数据绑定调试
PresentationTraceSources.DataBindingSource.Switch.Level = SourceLevels.Error;
```

也可以在 XAML 中的绑定上添加诊断信息：

```xml
<TextBlock Text="{Binding Name, diag:PresentationTraceSources.TraceLevel=High}" />
```

#### Snoop 工具

[Snoop](https://github.com/snoopwpf/snoopwpf) 是 WPF 开发的必备调试工具，可以：
- 查看和修改运行时的可视化树（Visual Tree）
- 实时查看和修改控件的所有依赖属性
- 追踪路由事件
- 高亮选中的元素

#### 常见调试场景

1. **绑定不生效**：检查输出窗口（Output Window）的绑定错误信息
2. **样式不生效**：检查 `TargetType` 是否匹配，检查资源字典是否正确合并
3. **命令不触发**：检查 `CanExecute` 是否返回 `true`
4. **DataContext 为空**：使用 Snoop 检查元素的 DataContext 是否正确继承

---

## 练习与实践

### 练习一：完整 MVVM 学生管理系统

基于项目中的 `StudentViewModel`，扩展实现：
1. 增删改查学生信息
2. 按姓名筛选（TextBox + 实时过滤）
3. 表单验证（年龄范围、邮箱格式）

### 练习二：值转换器应用

1. 创建一个 `IsEmptyToBoolConverter`：当字符串为空时返回 true
2. 创建一个 `StatusToColorConverter`：根据订单状态（待支付/已支付/已取消）显示不同颜色
3. 在 DataGrid 中使用转换器为不同价格区间设置行背景色

### 练习三：动画效果

1. 实现列表项的「淡入+滑动」入场动画
2. 实现一个加载指示器（旋转动画）
3. 实现按钮点击时的「涟漪」效果

### 练习四：自定义控件

1. 创建一个带水印和字符计数的 TextBox UserControl
2. 创建一个评分控件（StarRating），支持 1-5 星评分
3. 创建一个可切换图标的 ToggleSwitch 开关控件

### 练习五：综合项目

结合四个模块的知识，实现一个「个人财务管理」小应用：
- 使用 Grid 布局主界面
- 使用 DataGrid 展示收支记录
- 使用样式和模板美化 UI
- 使用 MVVM 架构组织代码
- 收入和支出用不同颜色显示
- 使用动画展示统计数据
- 按月份筛选和汇总

---

## 延伸阅读

- [数据绑定概述](https://learn.microsoft.com/zh-cn/dotnet/desktop/wpf/data/data-binding-overview/)
- [MVVM 模式](https://learn.microsoft.com/zh-cn/dotnet/architecture/maui/mvvm/)
- [依赖属性概述](https://learn.microsoft.com/zh-cn/dotnet/desktop/wpf/advanced/dependency-properties-overview/)
- [动画概述](https://learn.microsoft.com/zh-cn/dotnet/desktop/wpf/graphics-multimedia/animation-overview/)
- [WPF 性能优化建议](https://learn.microsoft.com/zh-cn/dotnet/desktop/wpf/advanced/optimizing-wpf-application-performance/)