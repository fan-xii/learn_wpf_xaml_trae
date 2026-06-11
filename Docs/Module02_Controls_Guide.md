# 模块二：常用控件详解

## WPF 控件分类概览

WPF 提供了丰富的内置控件，可以分为以下几大类：

| 类别 | 代表控件 | 主要用途 |
|------|----------|----------|
| **按钮控件** | Button、ToggleButton、RepeatButton | 触发操作 |
| **文本控件** | TextBox、PasswordBox、TextBlock、RichTextBox | 文本输入与展示 |
| **选择控件** | CheckBox、RadioButton、ComboBox、Slider、DatePicker | 让用户从选项中选择 |
| **列表控件** | ListBox、ListView、DataGrid | 展示和管理数据集合 |
| **层级控件** | TreeView | 展示层级结构数据 |
| **容器控件** | TabControl、GroupBox、Expander | 组织和分组其他控件 |
| **菜单工具栏** | Menu、ToolBar、StatusBar | 应用程序命令系统 |
| **装饰控件** | Border、Image、Separator | 视觉效果与分隔 |

---

## 一、按钮控件

### Button

最基础的按钮控件，用于触发用户操作。

#### 关键属性

| 属性 | 说明 |
|------|------|
| `Content` | 按钮显示内容，可以是文本或任意 UI 元素 |
| `IsDefault` | 设为 `True` 时，按 Enter 键触发此按钮 |
| `IsCancel` | 设为 `True` 时，按 Esc 键触发此按钮 |
| `Command` | 绑定到 ICommand，是 MVVM 中按钮交互的首选方式 |
| `CommandParameter` | 传递给 Command 的参数 |

#### 关键事件

| 事件 | 说明 |
|------|------|
| `Click` | 按钮被点击时触发 |

#### 代码示例

```xml
<Button Content="普通按钮"
        Width="120" Height="40"
        Background="#2196F3" Foreground="White"
        Click="Button_Click" />

<Button Width="120" Height="40"
        IsDefault="True"
        Command="{Binding SaveCommand}">
    <StackPanel Orientation="Horizontal">
        <TextBlock Text="💾 " />
        <TextBlock Text="保存" />
    </StackPanel>
</Button>
```

```csharp
private void Button_Click(object sender, RoutedEventArgs e)
{
    MessageBox.Show("按钮被点击了！");
}
```

---

### ToggleButton

可以在「按下」和「弹起」两种状态之间切换的按钮。`CheckBox` 和 `RadioButton` 都继承自 `ToggleButton`。

#### 关键属性

| 属性 | 说明 |
|------|------|
| `IsChecked` | 可为 `true`、`false` 或 `null`（三态） |
| `IsThreeState` | 是否启用三态模式 |

#### 关键事件

| 事件 | 说明 |
|------|------|
| `Checked` | 切换到选中状态时触发 |
| `Unchecked` | 切换到未选中状态时触发 |
| `Indeterminate` | 切换到不确定状态时触发（三态模式） |

#### 代码示例

```xml
<ToggleButton Content="加粗"
              Width="60" Height="30"
              IsChecked="{Binding IsBold}"
              Checked="ToggleButton_Checked"
              Unchecked="ToggleButton_Unchecked" />
```

---

### RepeatButton

在按住时持续触发 `Click` 事件的按钮。常用于滚动条的箭头按钮或数值增减按钮。

#### 关键属性

| 属性 | 说明 |
|------|------|
| `Delay` | 首次触发前的等待时间（毫秒），默认 500 |
| `Interval` | 持续触发时的间隔时间（毫秒），默认 33 |

#### 代码示例

```xml
<RepeatButton Content="▲" Width="30" Height="30"
              Delay="300" Interval="50"
              Click="RepeatUp_Click" />
```

---

## 二、文本控件

### TextBox

用于接收用户输入的文本。

#### 关键属性

| 属性 | 说明 |
|------|------|
| `Text` | 获取或设置文本内容 |
| `MaxLength` | 最大字符数限制 |
| `TextWrapping` | 换行模式：`NoWrap`、`Wrap`、`WrapWithOverflow` |
| `AcceptsReturn` | 是否接受回车换行（多行模式） |
| `AcceptsTab` | 是否接受 Tab 键 |
| `IsReadOnly` | 设为只读 |
| `VerticalScrollBarVisibility` | 垂直滚动条可见性 |

#### 关键事件

| 事件 | 说明 |
|------|------|
| `TextChanged` | 文本内容变化时触发 |

#### 代码示例

```xml
<TextBox Width="300" Height="120"
         Text="{Binding Description, UpdateSourceTrigger=PropertyChanged}"
         TextWrapping="Wrap"
         AcceptsReturn="True"
         VerticalScrollBarVisibility="Auto"
         MaxLength="500" />
```

---

### PasswordBox

专门用于密码输入，不显示明文。

#### 关键属性

| 属性 | 说明 |
|------|------|
| `Password` | 获取密码明文（不是依赖属性，不支持绑定） |
| `PasswordChar` | 设置掩码字符，默认 `●` |
| `MaxLength` | 最大字符数 |

#### 代码示例

```xml
<PasswordBox x:Name="PwdBox"
             Width="200"
             PasswordChar="*"
             MaxLength="20" />
```

```csharp
string password = PwdBox.Password;
```

> **注意**：`Password` 不是依赖属性，因此无法直接进行数据绑定。如果需要绑定密码，可以创建一个附加属性来桥接。

---

### TextBlock

用于显示文本，不支持用户编辑。是 WPF 中最常用的只读文本显示控件。

#### 关键属性

| 属性 | 说明 |
|------|------|
| `Text` | 显示的文本内容 |
| `TextWrapping` | 换行模式 |
| `TextTrimming` | 文本溢出处理方式 |
| `FontSize`、`FontWeight`、`FontFamily` | 字体相关属性 |
| `Foreground`、`Background` | 颜色设置 |
| `TextDecorations` | 文本装饰（下划线、删除线等） |

#### 内联元素（Inline）

`TextBlock` 支持在内部使用 `Run`、`Bold`、`Italic`、`Underline`、`Hyperlink` 等内联元素来创建富文本：

```xml
<TextBlock FontSize="14">
    <Run Text="这是" />
    <Bold>粗体文本</Bold>
    <Run Text="和" />
    <Italic Foreground="Blue">斜体蓝色文本</Italic>
    <LineBreak />
    <Hyperlink NavigateUri="https://example.com"
               RequestNavigate="Hyperlink_RequestNavigate">
        点击访问网站
    </Hyperlink>
</TextBlock>
```

---

### RichTextBox

支持格式化文本编辑的丰富文本框，类似于一个简单的 Word 编辑器。

```xml
<RichTextBox Height="200" BorderBrush="#E0E0E0">
    <FlowDocument>
        <Paragraph>
            <Bold>格式化文本</Bold>编辑示例
        </Paragraph>
    </FlowDocument>
</RichTextBox>
```

---

## 三、选择控件

### CheckBox

多选框，让用户勾选一个或多个选项。

```xml
<CheckBox Content="我同意用户协议"
          IsChecked="{Binding IsAgreed}" />
```

### RadioButton

单选框，同一容器内的 RadioButton 自动互斥（确保同一组内只能选一个）。

```xml
<StackPanel>
    <RadioButton Content="男" IsChecked="True"
                 GroupName="Gender" />
    <RadioButton Content="女" GroupName="Gender" />
</StackPanel>
```

### ComboBox

下拉选择框，结合了选择器和下拉列表的功能。

#### 关键属性

| 属性 | 说明 |
|------|------|
| `ItemsSource` | 数据源集合 |
| `SelectedItem` | 当前选中项 |
| `SelectedIndex` | 当前选中索引 |
| `IsEditable` | 是否允许用户输入自定义文本 |
| `DisplayMemberPath` | 显示哪个属性的值 |

```xml
<ComboBox Width="200"
          ItemsSource="{Binding Cities}"
          SelectedItem="{Binding SelectedCity}"
          DisplayMemberPath="Name" />
```

### Slider

滑块控件，用于让用户在某个范围内选择数值。

```xml
<Slider Minimum="0" Maximum="100"
        Value="{Binding Volume}"
        TickFrequency="10"
        IsSnapToTickEnabled="True"
        Width="300" />
```

### DatePicker

日期选择器，弹出一个日历面板供用户选择日期。

```xml
<DatePicker SelectedDate="{Binding BirthDate}"
            DisplayDateStart="2000-01-01"
            DisplayDateEnd="2099-12-31" />
```

---

## 四、列表控件

列表控件是 WPF 中最强大的控件类别之一，用于展示和管理数据集合。

### ListBox

基础的列表控件，显示一组可选择的数据项。

```xml
<ListBox ItemsSource="{Binding Students}"
         SelectedItem="{Binding SelectedStudent}"
         DisplayMemberPath="Name" />
```

### ListView

`ListView` 是 `ListBox` 的增强版，原生支持多列视图和自定义布局。

#### 关键概念

| 概念 | 说明 |
|------|------|
| `ItemsSource` | 绑定数据源集合 |
| `ItemTemplate` | 定义每个数据项的显示模板 |
| `View` | 定义列表的视图模式，常用 `GridView` |

```xml
<ListView ItemsSource="{Binding Students}">
    <ListView.View>
        <GridView>
            <GridViewColumn Header="学号"
                            DisplayMemberBinding="{Binding Id}"
                            Width="80" />
            <GridViewColumn Header="姓名"
                            DisplayMemberBinding="{Binding Name}"
                            Width="120" />
            <GridViewColumn Header="年龄"
                            DisplayMemberBinding="{Binding Age}"
                            Width="80" />
            <GridViewColumn Header="年级"
                            DisplayMemberBinding="{Binding Grade}"
                            Width="100" />
        </GridView>
    </ListView.View>
</ListView>
```

### DataGrid

`DataGrid` 是功能最强大的列表控件，提供了编辑、排序、分组、筛选等完整的数据表格功能。

#### 关键属性

| 属性 | 说明 |
|------|------|
| `ItemsSource` | 数据源 |
| `AutoGenerateColumns` | 是否自动生成列，建议设为 `False` |
| `CanUserAddRows` | 是否允许用户添加行 |
| `CanUserDeleteRows` | 是否允许用户删除行 |
| `IsReadOnly` | 是否只读 |
| `SelectedItem` | 当前选中行对应的数据对象 |

```xml
<DataGrid ItemsSource="{Binding Products}"
          AutoGenerateColumns="False"
          CanUserAddRows="False"
          IsReadOnly="True">
    <DataGrid.Columns>
        <DataGridTextColumn Header="编号"
                            Binding="{Binding Id}" Width="60" />
        <DataGridTextColumn Header="名称"
                            Binding="{Binding Name}" Width="150" />
        <DataGridTextColumn Header="分类"
                            Binding="{Binding Category}" Width="100" />
        <DataGridTextColumn Header="价格"
                            Binding="{Binding Price, StringFormat=C}"
                            Width="100" />
        <DataGridTextColumn Header="库存"
                            Binding="{Binding Stock}" Width="80" />
    </DataGrid.Columns>
</DataGrid>
```

#### ItemTemplate 自定义

使用 `ItemTemplate` 可以完全自定义每个列表项的视觉效果：

```xml
<ListBox ItemsSource="{Binding Students}">
    <ListBox.ItemTemplate>
        <DataTemplate>
            <Border Padding="10" Margin="3"
                    CornerRadius="6"
                    Background="#F5F5F5">
                <StackPanel>
                    <TextBlock Text="{Binding Name}"
                               FontWeight="Bold" FontSize="16" />
                    <TextBlock Text="{Binding Grade}"
                               Foreground="Gray" />
                </StackPanel>
            </Border>
        </DataTemplate>
    </ListBox.ItemTemplate>
</ListBox>
```

---

## 五、TreeView 与层级数据

`TreeView` 用于展示具有层级关系的数据，如文件目录结构、组织架构等。

#### 关键概念

| 概念 | 说明 |
|------|------|
| `HierarchicalDataTemplate` | 专门用于 TreeView 的数据模板，支持递归展示层级数据 |
| `ItemsSource` | 指定子节点来源的属性路径 |
| `ItemTemplate` | 节点的显示模板 |

```xml
<TreeView>
    <TreeViewItem Header="中国">
        <TreeViewItem Header="北京">
            <TreeViewItem Header="海淀区" />
            <TreeViewItem Header="朝阳区" />
        </TreeViewItem>
        <TreeViewItem Header="上海">
            <TreeViewItem Header="浦东新区" />
            <TreeViewItem Header="黄浦区" />
        </TreeViewItem>
    </TreeViewItem>
</TreeView>
```

使用数据绑定的方式：

```xml
<TreeView ItemsSource="{Binding TreeNodes}">
    <TreeView.ItemTemplate>
        <HierarchicalDataTemplate
            ItemsSource="{Binding Children}">
            <TextBlock Text="{Binding Name}" />
        </HierarchicalDataTemplate>
    </TreeView.ItemTemplate>
</TreeView>
```

---

## 六、菜单、工具栏与状态栏

### Menu

应用程序主菜单，通常放在窗口顶部。

```xml
<Menu>
    <MenuItem Header="文件(_F)">
        <MenuItem Header="新建(_N)" InputGestureText="Ctrl+N" />
        <MenuItem Header="打开(_O)" InputGestureText="Ctrl+O" />
        <Separator />
        <MenuItem Header="退出(_X)" Click="Exit_Click" />
    </MenuItem>
    <MenuItem Header="编辑(_E)">
        <MenuItem Header="撤销(_U)" IsEnabled="False" />
        <MenuItem Header="重做(_R)" />
    </MenuItem>
    <MenuItem Header="帮助(_H)">
        <MenuItem Header="关于(_A)" />
    </MenuItem>
</Menu>
```

### ToolBar

工具栏，提供常用操作的快捷入口。

```xml
<ToolBar>
    <Button Command="ApplicationCommands.New">
        <StackPanel Orientation="Horizontal">
            <TextBlock Text="📄 " />
            <TextBlock Text="新建" />
        </StackPanel>
    </Button>
    <Button Command="ApplicationCommands.Open">
        <StackPanel Orientation="Horizontal">
            <TextBlock Text="📂 " />
            <TextBlock Text="打开" />
        </StackPanel>
    </Button>
    <Separator />
    <Button Command="ApplicationCommands.Save">
        <StackPanel Orientation="Horizontal">
            <TextBlock Text="💾 " />
            <TextBlock Text="保存" />
        </StackPanel>
    </Button>
</ToolBar>
```

### StatusBar

状态栏，位于窗口底部，显示应用程序状态信息。

```xml
<StatusBar>
    <StatusBarItem Content="就绪" />
    <Separator />
    <StatusBarItem Content="行: 1  列: 0" />
    <Separator />
    <StatusBarItem>
        <ProgressBar Width="100" Height="15"
                     Value="45" IsIndeterminate="False" />
    </StatusBarItem>
</StatusBar>
```

---

## 七、TabControl

`TabControl` 提供标签页式的多页面切换界面。

```xml
<TabControl>
    <TabItem Header="基本信息">
        <StackPanel Margin="10">
            <Label Content="姓名：" />
            <TextBox Width="200" />
            <Label Content="年龄：" />
            <TextBox Width="100" />
        </StackPanel>
    </TabItem>
    <TabItem Header="联系方式">
        <StackPanel Margin="10">
            <Label Content="电话：" />
            <TextBox Width="200" />
            <Label Content="邮箱：" />
            <TextBox Width="250" />
        </StackPanel>
    </TabItem>
    <TabItem Header="备注">
        <TextBox Margin="10" Width="400" Height="200"
                 TextWrapping="Wrap"
                 AcceptsReturn="True" />
    </TabItem>
</TabControl>
```

也可以使用 `ItemsSource` 动态生成标签页：

```xml
<TabControl ItemsSource="{Binding TabItems}"
            SelectedItem="{Binding SelectedTab}">
    <TabControl.ItemTemplate>
        <DataTemplate>
            <TextBlock Text="{Binding Header}" />
        </DataTemplate>
    </TabControl.ItemTemplate>
    <TabControl.ContentTemplate>
        <DataTemplate>
            <ContentControl Content="{Binding Content}" />
        </DataTemplate>
    </TabControl.ContentTemplate>
</TabControl>
```

---

## 练习与实践

### 练习一：个人信息录入

创建一个 TabControl，包含：
- **基本信息标签页**：姓名(TextBox)、年龄(NumericUpDown风格)、性别(RadioButton)
- **联系方式标签页**：电话(TextBox)、邮箱(TextBox)
- 底部「保存」按钮

### 练习二：学生管理列表

使用 DataGrid 展示学生列表，包含以下列：
- 学号、姓名、年龄、年级
- 使用 CheckBox 列表示「是否在校」
- 支持选中某行后在下方显示详细信息

### 练习三：文件树浏览器

使用 TreeView 展示以下层级目录结构：
```
桌面
├── 文档
│   ├── 工作
│   └── 个人
├── 图片
└── 下载
```

### 练习四：登录注册页面

结合 TextBox、PasswordBox、CheckBox、Button 等控件创建一个完整的登录/注册切换页面。

---

## 延伸阅读

- [WPF 控件库](https://learn.microsoft.com/zh-cn/dotnet/desktop/wpf/controls/)
- [ItemsControl 概述](https://learn.microsoft.com/zh-cn/dotnet/desktop/wpf/controls/itemscontrol-overview/)
- [DataGrid 类](https://learn.microsoft.com/zh-cn/dotnet/api/system.windows.controls.datagrid)