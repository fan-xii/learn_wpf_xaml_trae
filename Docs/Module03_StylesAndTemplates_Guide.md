# 模块三：样式与模板

## WPF 样式化哲学与资源系统

WPF 的设计哲学之一是「外观与逻辑分离」。通过样式（Style）和模板（Template），你可以：
- 统一整个应用的视觉风格
- 在运行时切换主题
- 无痛修改 UI 外观而不改动业务逻辑
- 实现高度可复用的 UI 组件

WPF 的样式系统建立在**资源系统**之上。资源（Resource）是可以在 XAML 中定义和引用的可重用对象，包括画刷、颜色、样式、模板等。每个元素都有自己的 `Resources` 属性，资源按层级查找：从当前元素开始，逐级向上查找逻辑树，最终到 `Application.Resources`。

---

## 一、Style 基础

### Style 是什么

`Style` 是 WPF 中用于批量设置控件属性的机制。它相当于 CSS 中的类。一个 Style 可以设置多个属性的默认值，并可以被多个控件共享。

### 基本语法

```xml
<Style x:Key="MyButtonStyle" TargetType="Button">
    <Setter Property="Background" Value="#2196F3" />
    <Setter Property="Foreground" Value="White" />
    <Setter Property="FontSize" Value="14" />
    <Setter Property="Padding" Value="16,8" />
    <Setter Property="Cursor" Value="Hand" />
</Style>
```

使用：

```xml
<Button Style="{StaticResource MyButtonStyle}" Content="样式化按钮" />
```

### TargetType

`TargetType` 指定了该 Style 适用的控件类型。它有两个重要作用：
1. 限制 Style 只能应用于指定类型的控件
2. 允许在 Setter 中使用类型特定的属性（如 `Button.IsDefault`）

### 隐式样式 vs 显式样式（命名样式）

- **显式样式**：定义了 `x:Key`，控件需要通过 `Style="{StaticResource Key}"` 显式引用。
- **隐式样式**：不定义 `x:Key`，会自动应用于所有匹配 `TargetType` 的控件。

```xml
<!-- 隐式样式：所有 Button 自动应用 -->
<Style TargetType="Button">
    <Setter Property="Background" Value="#2196F3" />
    <Setter Property="Foreground" Value="White" />
</Style>

<!-- 显式样式：只有通过 Style 属性引用的控件才会应用 -->
<Style x:Key="DangerButton" TargetType="Button" BasedOn="{StaticResource {x:Type Button}}">
    <Setter Property="Background" Value="#F44336" />
</Style>
```

### BasedOn（样式继承）

通过 `BasedOn` 可以让一个样式继承另一个样式的所有设置，只追加或覆盖特定的属性：

```xml
<Style x:Key="BaseButton" TargetType="Button">
    <Setter Property="FontSize" Value="14" />
    <Setter Property="Padding" Value="16,8" />
    <Setter Property="Cursor" Value="Hand" />
</Style>

<Style x:Key="PrimaryButton" TargetType="Button"
       BasedOn="{StaticResource BaseButton}">
    <Setter Property="Background" Value="#2196F3" />
    <Setter Property="Foreground" Value="White" />
</Style>

<Style x:Key="DangerButton" TargetType="Button"
       BasedOn="{StaticResource BaseButton}">
    <Setter Property="Background" Value="#F44336" />
    <Setter Property="Foreground" Value="White" />
</Style>
```

### 项目中的实际样式示例

以本项目 `SharedStyles.xaml` 中的 `NavCardButton` 样式为例：

```xml
<Style x:Key="NavCardButton" TargetType="Button">
    <Setter Property="Width" Value="260" />
    <Setter Property="Height" Value="140" />
    <Setter Property="Margin" Value="10" />
    <Setter Property="Background" Value="White" />
    <Setter Property="Cursor" Value="Hand" />
    <!-- 通过 ControlTemplate 定义视觉外观 -->
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="Button">
                <Border Background="{TemplateBinding Background}"
                        CornerRadius="12"
                        BorderBrush="#E0E0E0"
                        BorderThickness="1">
                    <Border.Effect>
                        <DropShadowEffect ShadowDepth="2" Opacity="0.1" BlurRadius="8" />
                    </Border.Effect>
                    <StackPanel VerticalAlignment="Center"
                                HorizontalAlignment="Center">
                        <TextBlock Text="{TemplateBinding Tag}"
                                   FontSize="32"
                                   HorizontalAlignment="Center"
                                   Margin="0,0,0,8" />
                        <TextBlock Text="{TemplateBinding Content}"
                                   FontSize="15"
                                   FontWeight="SemiBold"
                                   HorizontalAlignment="Center"
                                   Foreground="#424242" />
                    </StackPanel>
                </Border>
                <ControlTemplate.Triggers>
                    <Trigger Property="IsMouseOver" Value="True">
                        <Setter Property="Background" Value="#E3F2FD" />
                    </Trigger>
                </ControlTemplate.Triggers>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

---

## 二、ControlTemplate（控件模板）

### 什么是 ControlTemplate

`ControlTemplate` 定义了控件的**视觉结构**。它决定了控件「长什么样」。每个 WPF 控件都有默认的 ControlTemplate，你可以用自定义的 ControlTemplate 完全替换它。

ControlTemplate 实现了 WPF 的「无外观控件」（Lookless Controls）理念：控件的**行为**（Button 可点击、CheckBox 可切换）由其类定义，而控件的**外观**完全由模板决定。

### ControlTemplate 的核心概念

#### TemplateBinding

`TemplateBinding` 是一种轻量级的单向绑定，用于将模板内部元素的属性连接到控件本身的属性上。它是 `{Binding RelativeSource={RelativeSource TemplatedParent}}` 的简写。

```xml
<ControlTemplate TargetType="Button">
    <Border Background="{TemplateBinding Background}"
            CornerRadius="{TemplateBinding BorderThickness}"
            Padding="{TemplateBinding Padding}">
        <ContentPresenter HorizontalAlignment="Center"
                          VerticalAlignment="Center" />
    </Border>
</ControlTemplate>
```

#### ContentPresenter

`ContentPresenter` 是模板中的占位符，用于显示控件的 `Content` 属性。对于内容控件（如 Button、Label），必须在模板中包含 `ContentPresenter`。对于列表控件（如 ListBox），需要使用 `ItemsPresenter`。

#### 自定义 Button 模板示例

```xml
<ControlTemplate x:Key="RoundedButtonTemplate" TargetType="Button">
    <Border x:Name="border"
            Background="{TemplateBinding Background}"
            CornerRadius="8"
            BorderBrush="{TemplateBinding BorderBrush}"
            BorderThickness="{TemplateBinding BorderThickness}"
            Padding="{TemplateBinding Padding}">
        <ContentPresenter HorizontalAlignment="Center"
                          VerticalAlignment="Center"
                          RecognizesAccessKey="True" />
    </Border>
    <ControlTemplate.Triggers>
        <!-- 鼠标悬停效果 -->
        <Trigger Property="IsMouseOver" Value="True">
            <Setter TargetName="border"
                    Property="Background" Value="#1976D2" />
        </Trigger>
        <!-- 按下效果 -->
        <Trigger Property="IsPressed" Value="True">
            <Setter TargetName="border"
                    Property="Background" Value="#0D47A1" />
        </Trigger>
        <!-- 禁用效果 -->
        <Trigger Property="IsEnabled" Value="False">
            <Setter TargetName="border"
                    Property="Opacity" Value="0.5" />
        </Trigger>
    </ControlTemplate.Triggers>
</ControlTemplate>
```

### VisualState

除了 Trigger，较新的模板还可以使用 `VisualStateManager`（VSM）来管理控件的视觉状态。VSM 将状态组织成「状态组」，比 Trigger 更加结构化和语义化。

```xml
<ControlTemplate TargetType="Button">
    <Border x:Name="RootBorder"
            Background="{TemplateBinding Background}"
            CornerRadius="6">
        <VisualStateManager.VisualStateGroups>
            <VisualStateGroup x:Name="CommonStates">
                <VisualState x:Name="Normal" />
                <VisualState x:Name="MouseOver">
                    <Storyboard>
                        <ColorAnimation
                            Storyboard.TargetName="RootBorder"
                            Storyboard.TargetProperty="(Border.Background).(SolidColorBrush.Color)"
                            To="#1976D2" Duration="0:0:0.2" />
                    </Storyboard>
                </VisualState>
                <VisualState x:Name="Pressed">
                    <Storyboard>
                        <ColorAnimation
                            Storyboard.TargetName="RootBorder"
                            Storyboard.TargetProperty="(Border.Background).(SolidColorBrush.Color)"
                            To="#0D47A1" Duration="0:0:0.1" />
                    </Storyboard>
                </VisualState>
                <VisualState x:Name="Disabled">
                    <Storyboard>
                        <DoubleAnimation
                            Storyboard.TargetName="RootBorder"
                            Storyboard.TargetProperty="Opacity"
                            To="0.5" Duration="0" />
                    </Storyboard>
                </VisualState>
            </VisualStateGroup>
        </VisualStateManager.VisualStateGroups>
        <ContentPresenter />
    </Border>
</ControlTemplate>
```

---

## 三、DataTemplate（数据模板）

### DataTemplate 的作用

`DataTemplate` 用于定义**数据对象**在 UI 中的呈现方式。它是「数据»视觉」的桥梁，让列表控件（ListBox、ComboBox、ListView）知道如何渲染数据项。

与 ControlTemplate 的区别：
- `ControlTemplate` 定义**控件**的外观
- `DataTemplate` 定义**数据**的呈现

### 自动类型匹配

WPF 可以根据数据类型自动选择合适的 DataTemplate，无需显式指定：

```xml
<Window.Resources>
    <!-- 当数据类型是 Student 时，自动使用此模板 -->
    <DataTemplate DataType="{x:Type local:Student}">
        <Border Padding="10" Margin="3" CornerRadius="6"
                Background="#F0F7FF">
            <StackPanel>
                <TextBlock Text="{Binding Name}"
                           FontWeight="Bold" FontSize="16" />
                <TextBlock Text="{Binding Grade}"
                           Foreground="Gray" />
            </StackPanel>
        </Border>
    </DataTemplate>
</Window.Resources>
```

由于使用了 `DataType` 属性，只需将 `Student` 对象放入任意 `ContentControl` 中，就会自动使用此模板渲染。

### 列表控件的 ItemTemplate

```xml
<ListBox ItemsSource="{Binding Students}">
    <ListBox.ItemTemplate>
        <DataTemplate>
            <Grid Margin="5" Width="280">
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="60" />
                    <ColumnDefinition Width="*" />
                </Grid.ColumnDefinitions>
                <Border Grid.Column="0" Width="45" Height="45"
                        CornerRadius="23"
                        Background="#2196F3">
                    <TextBlock Text="{Binding Name[0]}"
                               Foreground="White" FontSize="20"
                               HorizontalAlignment="Center"
                               VerticalAlignment="Center" />
                </Border>
                <StackPanel Grid.Column="1"
                            VerticalAlignment="Center">
                    <TextBlock Text="{Binding Name}"
                               FontWeight="SemiBold" />
                    <TextBlock Text="{Binding Email}"
                               Foreground="Gray" FontSize="12" />
                </StackPanel>
            </Grid>
        </DataTemplate>
    </ListBox.ItemTemplate>
</ListBox>
```

### DataTemplateSelector

当需要根据数据属性动态选择不同的模板时，使用 `DataTemplateSelector`：

```csharp
public class StudentTemplateSelector : DataTemplateSelector
{
    public DataTemplate EnrolledTemplate { get; set; }
    public DataTemplate NotEnrolledTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        if (item is Student student)
            return student.IsEnrolled ? EnrolledTemplate : NotEnrolledTemplate;
        return base.SelectTemplate(item, container);
    }
}
```

```xml
<local:StudentTemplateSelector x:Key="StudentSelector"
    EnrolledTemplate="{StaticResource EnrolledTemplate}"
    NotEnrolledTemplate="{StaticResource NotEnrolledTemplate}" />

<ListBox ItemsSource="{Binding Students}"
         ItemTemplateSelector="{StaticResource StudentSelector}" />
```

---

## 四、ResourceDictionary（资源字典）

### 什么是资源字典

`ResourceDictionary` 是 WPF 中存储和共享资源的容器。它可以让样式、模板、画刷等资源在多个 XAML 文件之间共享。

### StaticResource vs DynamicResource

| 特性 | StaticResource | DynamicResource |
|------|----------------|-----------------|
| 查找时机 | 加载时查找一次 | 每次需要时查找 |
| 性能 | 更快 | 较慢 |
| 运行时变更 | 不支持 | 支持（资源更新自动反映） |
| 适用场景 | 不变的资源（颜色、字体、样式） | 可能变化的资源（主题切换） |

```xml
<!-- StaticResource：加载时确定，性能好 -->
<Button Background="{StaticResource PrimaryBrush}" />

<!-- DynamicResource：运行时可以切换 -->
<Button Background="{DynamicResource ThemeBrush}" />
```

### MergedDictionaries（合并资源字典）

通过 `MergedDictionaries` 将多个资源字典合并到应用中。这是组织大型项目资源的最佳实践。

本项目 `App.xaml` 中的用法：

```xml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="Resources/Styles/SharedStyles.xaml" />
            <!-- 可以继续合并其他字典 -->
            <!-- <ResourceDictionary Source="Resources/Styles/ButtonStyles.xaml" /> -->
            <!-- <ResourceDictionary Source="Resources/Styles/TextBoxStyles.xaml" /> -->
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

### 在代码中访问资源

```csharp
// 从 Application 级别获取资源
var primaryBrush = (Brush)Application.Current.FindResource("PrimaryBrush");

// 从当前窗口获取资源
var style = (Style)this.FindResource("MyButtonStyle");
```

---

## 五、触发器（Triggers）

触发器让你能够根据条件动态改变控件的属性值，而无需编写 C# 代码。

### Property Trigger

最常用的触发器，当某个属性的值满足条件时触发：

```xml
<Style TargetType="Button">
    <Setter Property="Background" Value="#2196F3" />
    <Setter Property="Foreground" Value="White" />
    <Style.Triggers>
        <Trigger Property="IsMouseOver" Value="True">
            <Setter Property="Background" Value="#1976D2" />
            <Setter Property="Cursor" Value="Hand" />
        </Trigger>
        <Trigger Property="IsEnabled" Value="False">
            <Setter Property="Opacity" Value="0.5" />
        </Trigger>
    </Style.Triggers>
</Style>
```

### DataTrigger

基于数据绑定值的触发器：

```xml
<DataTemplate>
    <Border x:Name="Border" Padding="10" CornerRadius="4">
        <TextBlock Text="{Binding Name}" />
    </Border>
    <DataTemplate.Triggers>
        <!-- 根据数据属性改变外观 -->
        <DataTrigger Binding="{Binding IsEnrolled}" Value="False">
            <Setter TargetName="Border"
                    Property="Background" Value="#FFEBEE" />
        </DataTrigger>
        <DataTrigger Binding="{Binding Age}" Value="18">
            <Setter TargetName="Border"
                    Property="BorderBrush" Value="Green" />
            <Setter TargetName="Border"
                    Property="BorderThickness" Value="2" />
        </DataTrigger>
    </DataTemplate.Triggers>
</DataTemplate>
```

### MultiTrigger

当多个条件同时满足时触发：

```xml
<MultiTrigger>
    <MultiTrigger.Conditions>
        <Condition Property="IsMouseOver" Value="True" />
        <Condition Property="IsEnabled" Value="True" />
    </MultiTrigger.Conditions>
    <Setter Property="Background" Value="#1976D2" />
</MultiTrigger>
```

### MultiDataTrigger

基于多个数据绑定条件的触发器：

```xml
<MultiDataTrigger>
    <MultiDataTrigger.Conditions>
        <Condition Binding="{Binding IsEnrolled}" Value="True" />
        <Condition Binding="{Binding Age}" Value="20" />
    </MultiDataTrigger.Conditions>
    <Setter Property="Background" Value="#E8F5E9" />
</MultiDataTrigger>
```

### EventTrigger

响应路由事件的触发器，通常用于启动动画：

```xml
<Button Content="点击动画">
    <Button.Triggers>
        <EventTrigger RoutedEvent="Button.Click">
            <BeginStoryboard>
                <Storyboard>
                    <DoubleAnimation
                        Storyboard.TargetProperty="(Button.Opacity)"
                        From="1" To="0.3" Duration="0:0:0.2"
                        AutoReverse="True" />
                </Storyboard>
            </BeginStoryboard>
        </EventTrigger>
    </Button.Triggers>
</Button>
```

---

## 六、主题与皮肤策略

### 策略一：静态主题色

在本项目中，通过资源字典定义主题色：

```xml
<Color x:Key="PrimaryColor">#2196F3</Color>
<SolidColorBrush x:Key="PrimaryBrush"
                 Color="{StaticResource PrimaryColor}" />
```

改动一个颜色值，所有引用处自动更新。

### 策略二：动态主题切换

可以使用 `DynamicResource` + 替换资源字典实现运行时主题切换：

```csharp
// 切换主题
var newTheme = new ResourceDictionary
{
    Source = new Uri("Resources/Themes/DarkTheme.xaml", UriKind.Relative)
};

Application.Current.Resources.MergedDictionaries.Clear();
Application.Current.Resources.MergedDictionaries.Add(newTheme);
```

### 策略三：层叠样式

```
基础样式（字号、内边距）
    ↓ BasedOn
语义样式（Primary、Danger、Success）
    ↓ 引用
控件实例
```

---

## 练习与实践

### 练习一：设计一个按钮样式

创建一个包含以下状态的按钮样式：
- 正常状态：蓝色背景，白色文字，圆角边框
- 悬停状态：深蓝色背景
- 按下状态：更深蓝色，轻微缩小（ScaleTransform）
- 禁用状态：灰色半透明

### 练习二：学生卡片模板

创建一个 DataTemplate，将学生信息渲染成卡片形式，包含：
- 头像区域（显示姓名首字）
- 姓名（加粗）、年级、邮箱
- 在校状态用绿色/红色圆点表示

### 练习三：主题切换

创建两套主题（亮色/暗色），实现运行时一键切换：
- 定义 DarkTheme.xaml 和 LightTheme.xaml
- 在 App.xaml 中默认加载亮色主题
- 添加切换按钮，点击时切换资源字典

### 练习四：DataTemplateSelector

创建一个任务列表，根据任务状态（待办、进行中、已完成）使用不同的 ItemTemplate 渲染：
- 待办：白色背景
- 进行中：蓝色浅背景
- 已完成：绿色浅背景 + 删除线

---

## 延伸阅读

- [样式和模板](https://learn.microsoft.com/zh-cn/dotnet/desktop/wpf/controls/styles-templates-overview/)
- [ControlTemplate 创建](https://learn.microsoft.com/zh-cn/dotnet/desktop/wpf/controls/how-to-create-apply-template/)
- [DataTemplate 概述](https://learn.microsoft.com/zh-cn/dotnet/desktop/wpf/data/data-templating-overview/)