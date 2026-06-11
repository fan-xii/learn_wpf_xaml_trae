# 模块一：XAML 布局基础

## 什么是 WPF 布局系统

WPF 的布局系统与传统的基于坐标的布局方式完全不同。在 WPF 中，控件的位置和大小不是由固定的像素坐标决定的，而是由其父容器（布局面板）根据一系列规则自动计算得出的。这种「流式布局」机制使得 WPF 应用程序能够优雅地适应不同的屏幕分辨率、DPI 设置和窗口大小。

布局过程分为两个阶段：

1. **测量（Measure）** - 父容器询问每个子元素「你需要多大空间？」。子元素根据自身内容计算所需的 `DesiredSize`。
2. **排列（Arrange）** - 父容器根据子元素的需求和自身可用空间，为每个子元素分配最终的 `RenderSize` 和位置。

每个布局面板都继承自 `Panel` 基类，并重写 `MeasureOverride` 和 `ArrangeOverride` 方法来定义自己的布局逻辑。这种设计使得 WPF 的布局系统高度可扩展。

## 布局面板详解

### 1. Grid（网格布局）

`Grid` 是 WPF 中最灵活、最常用的布局面板。它将空间划分为行和列的网格，子元素可以放置在指定的单元格中。**几乎所有的复杂布局都离不开 Grid。**

#### 关键属性

| 属性 | 说明 |
|------|------|
| `RowDefinitions` | 定义行的集合，每行可设置固定高度、比例高度或自动高度 |
| `ColumnDefinitions` | 定义列的集合，类似行定义 |
| `Grid.Row` | 附加属性，指定子元素所在的行（从 0 开始） |
| `Grid.Column` | 附加属性，指定子元素所在的列 |
| `Grid.RowSpan` | 跨行数 |
| `Grid.ColumnSpan` | 跨列数 |
| `ShowGridLines` | 是否显示网格线（仅用于调试） |

#### 行高/列宽的三种设置方式

- **固定值**：`<RowDefinition Height="100" />` - 固定 100 像素
- **自动**：`<RowDefinition Height="Auto" />` - 根据内容自动调整
- **比例**：`<RowDefinition Height="*" />` - 按比例分配剩余空间，`2*` 表示两倍份额

#### 代码示例

```xml
<Grid Margin="10">
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto" />    <!-- 标题行 -->
        <RowDefinition Height="*" />       <!-- 主内容区域 -->
        <RowDefinition Height="Auto" />    <!-- 底部状态栏 -->
    </Grid.RowDefinitions>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="200" />   <!-- 侧边栏固定宽度 -->
        <ColumnDefinition Width="*" />     <!-- 内容区自适应 -->
    </Grid.ColumnDefinitions>

    <!-- 标题栏，跨两列 -->
    <TextBlock Grid.Row="0" Grid.ColumnSpan="2"
               Text="应用程序标题" FontSize="20"
               Background="#2196F3" Foreground="White"
               Padding="15" />

    <!-- 侧边导航 -->
    <ListBox Grid.Row="1" Grid.Column="0"
             Background="#F5F5F5" />

    <!-- 主内容区域 -->
    <Border Grid.Row="1" Grid.Column="1"
            Background="White" />

    <!-- 底部状态栏 -->
    <StatusBar Grid.Row="2" Grid.ColumnSpan="2">
        <StatusBarItem Content="就绪" />
    </StatusBar>
</Grid>
```

#### 何时使用

- 需要创建类似表格的结构化布局
- 需要精确控制行和列的分配
- 复杂对话框、主窗口布局、表单等

---

### 2. StackPanel（堆叠面板）

`StackPanel` 将子元素按水平或垂直方向依次排列，是最简单的布局面板之一。

#### 关键属性

| 属性 | 说明 |
|------|------|
| `Orientation` | `Vertical`（默认）或 `Horizontal`，决定排列方向 |

#### 代码示例

```xml
<!-- 垂直堆叠 - 常用于表单 -->
<StackPanel Orientation="Vertical" Margin="10">
    <Label Content="用户名：" />
    <TextBox Width="200" Margin="0,0,0,10" />

    <Label Content="密码：" />
    <PasswordBox Width="200" Margin="0,0,0,15" />

    <Button Content="登录" Width="100"
            Background="#2196F3" Foreground="White" />
</StackPanel>
```

```xml
<!-- 水平堆叠 - 常用于工具栏 -->
<StackPanel Orientation="Horizontal">
    <Button Content="新建" Margin="3" />
    <Button Content="打开" Margin="3" />
    <Button Content="保存" Margin="3" />
    <Separator Width="10" />
    <Button Content="撤销" Margin="3" />
    <Button Content="重做" Margin="3" />
</StackPanel>
```

#### 何时使用

- 简单的表单布局
- 工具栏或按钮组
- 任何需要按顺序排列子元素的场景

#### 注意事项

`StackPanel` 在排列方向上不会限制子元素的大小（即提供无限空间），因此包含 `ScrollViewer` 时通常不会自动出现滚动条。需要配合设置子元素的高度或宽度限制。

---

### 3. DockPanel（停靠面板）

`DockPanel` 将子元素停靠在容器的边缘（上、下、左、右），最后一个子元素（未指定停靠方向）会填充剩余空间。

#### 关键属性

| 属性 | 说明 |
|------|------|
| `DockPanel.Dock` | 附加属性，指定停靠方向：`Left`、`Right`、`Top`、`Bottom` |
| `LastChildFill` | 最后一个子元素是否填充剩余空间（默认 `True`） |

#### 代码示例

```xml
<DockPanel>
    <!-- 顶部菜单栏 -->
    <Menu DockPanel.Dock="Top">
        <MenuItem Header="文件" />
        <MenuItem Header="编辑" />
        <MenuItem Header="帮助" />
    </Menu>

    <!-- 底部状态栏 -->
    <StatusBar DockPanel.Dock="Bottom">
        <StatusBarItem Content="就绪" />
    </StatusBar>

    <!-- 左侧工具栏 -->
    <ToolBarTray DockPanel.Dock="Left" Orientation="Vertical">
        <ToolBar>
            <Button Content="📁" />
            <Button Content="💾" />
        </ToolBar>
    </ToolBarTray>

    <!-- 主内容区（自动填充剩余空间） -->
    <Grid Background="White">
        <TextBlock Text="主内容区域" FontSize="24"
                   HorizontalAlignment="Center"
                   VerticalAlignment="Center" />
    </Grid>
</DockPanel>
```

#### 何时使用

- 经典的多区域布局（上-左-右-下结构）
- 带有菜单栏、工具栏和状态栏的应用主窗口
- IDE 风格的应用界面

#### 注意事项

子元素的停靠顺序很重要！先声明的子元素会先占据边缘空间，后声明的子元素只能使用剩余的边缘空间。

---

### 4. WrapPanel（换行/换列面板）

`WrapPanel` 类似于 `StackPanel`，但当一行（或一列）空间不足时，子元素会自动换行（或换列）。

#### 关键属性

| 属性 | 说明 |
|------|------|
| `Orientation` | `Horizontal`（默认，水平排列，超出换行）或 `Vertical`（垂直排列，超出换列） |
| `ItemWidth` | 每个子元素的统一宽度 |
| `ItemHeight` | 每个子元素的统一高度 |

#### 代码示例

```xml
<WrapPanel Orientation="Horizontal"
           ItemWidth="120" ItemHeight="100"
           Margin="10">
    <Button Content="按钮 1" Margin="5" />
    <Button Content="按钮 2" Margin="5" />
    <Button Content="按钮 3" Margin="5" />
    <Button Content="按钮 4" Margin="5" />
    <Button Content="按钮 5" Margin="5" />
    <Button Content="按钮 6" Margin="5" />
    <Button Content="按钮 7" Margin="5" />
    <Button Content="按钮 8" Margin="5" />
</WrapPanel>
```

#### 何时使用

- 流式标签/标签云
- 图片缩略图网格
- 动态数量的卡片列表（如本项目主界面的模块入口）

---

### 5. Canvas（画布）

`Canvas` 是唯一支持绝对定位的布局面板。子元素通过 `Canvas.Left`、`Canvas.Top`、`Canvas.Right`、`Canvas.Bottom` 附加属性指定位置。

#### 关键属性

| 属性 | 说明 |
|------|------|
| `Canvas.Left` | 元素左边缘距 Canvas 左边缘的距离 |
| `Canvas.Top` | 元素上边缘距 Canvas 上边缘的距离 |
| `Canvas.Right` | 元素右边缘距 Canvas 右边缘的距离 |
| `Canvas.Bottom` | 元素下边缘距 Canvas 下边缘的距离 |
| `Panel.ZIndex` | 控制重叠元素的渲染顺序（值越大越靠前） |

#### 代码示例

```xml
<Canvas Width="400" Height="300" Background="#FAFAFA">
    <Rectangle Canvas.Left="50" Canvas.Top="50"
               Width="100" Height="80"
               Fill="#2196F3" />

    <Ellipse Canvas.Left="120" Canvas.Top="90"
             Width="80" Height="80"
             Fill="#FF9800" />

    <TextBlock Canvas.Left="180" Canvas.Top="30"
               Text="绝对定位" FontSize="18"
               Panel.ZIndex="1" />

    <!-- 基于右边的定位 -->
    <Button Canvas.Right="20" Canvas.Bottom="20"
            Content="右下角按钮" Width="100" />
</Canvas>
```

#### 何时使用

- 绘图应用、图形编辑器
- 需要精确控制位置的场景
- 简单的 2D 游戏界面

#### 注意事项

`Canvas` 不会自动调整大小以适应子元素，也不会限制子元素溢出。**在大多数业务应用的常规布局中，应优先使用其他布局面板。**

---

### 6. UniformGrid（均匀网格）

`UniformGrid` 是 `Grid` 的简化版本，所有单元格大小完全相同。你只需指定行数和列数，子元素会按顺序自动填充。

#### 关键属性

| 属性 | 说明 |
|------|------|
| `Rows` | 行数 |
| `Columns` | 列数 |
| `FirstColumn` | 第一个子元素从第几列开始放置 |

#### 代码示例

```xml
<UniformGrid Rows="2" Columns="4" Margin="10">
    <Button Content="1" Margin="3" />
    <Button Content="2" Margin="3" />
    <Button Content="3" Margin="3" />
    <Button Content="4" Margin="3" />
    <Button Content="5" Margin="3" />
    <Button Content="6" Margin="3" />
    <Button Content="7" Margin="3" />
    <Button Content="8" Margin="3" />
</UniformGrid>
```

#### 何时使用

- 计算器数字按钮面板
- 棋盘类游戏界面
- 需要均匀分布的项目网格

---

## 布局面板对比表

| 面板 | 布局方式 | 自适应 | 适用场景 | 复杂度 |
|------|----------|--------|----------|--------|
| **Grid** | 行列网格 | ✅ 完美 | 复杂布局、表单、主窗口 | 中高 |
| **StackPanel** | 线性排列 | ✅ 单方向 | 简单列表、工具栏 | 低 |
| **DockPanel** | 边缘停靠 | ✅ 很好 | 多区域窗口 | 中 |
| **WrapPanel** | 流式排列 | ✅ 自动换行 | 卡片列表、标签 | 低 |
| **Canvas** | 绝对定位 | ❌ 不自动 | 绘图、游戏 | 低 |
| **UniformGrid** | 均匀网格 | ✅ 很好 | 计算器、棋盘 | 低 |

---

## 常用布局模式与最佳实践

### 模式一：经典三区域布局

```xml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto" />
        <RowDefinition Height="*" />
        <RowDefinition Height="Auto" />
    </Grid.RowDefinitions>

    <!-- 顶部 -->
    <Border Grid.Row="0" Background="#1976D2"
            Padding="15">
        <TextBlock Text="应用程序标题" Foreground="White"
                   FontSize="18" />
    </Border>

    <!-- 中部内容 -->
    <Grid Grid.Row="1" />

    <!-- 底部 -->
    <Border Grid.Row="2" Background="#E0E0E0"
            Padding="10">
        <TextBlock Text="状态信息" />
    </Border>
</Grid>
```

### 模式二：主从（Master-Detail）布局

```xml
<Grid>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="250" />
        <ColumnDefinition Width="Auto" MinWidth="5" />
        <ColumnDefinition Width="*" />
    </Grid.ColumnDefinitions>

    <!-- 列表（主） -->
    <ListBox Grid.Column="0" />

    <!-- 拖拽分隔条 -->
    <GridSplitter Grid.Column="1" Width="5"
                  HorizontalAlignment="Center"
                  VerticalAlignment="Stretch" />

    <!-- 详情（从） -->
    <Border Grid.Column="2" Background="White" />
</Grid>
```

### 模式三：居中表单布局

```xml
<Grid>
    <Border Width="400" MaxHeight="500"
            HorizontalAlignment="Center"
            VerticalAlignment="Center"
            BorderBrush="#E0E0E0" BorderThickness="1"
            CornerRadius="8" Padding="30"
            Background="White">
        <StackPanel>
            <TextBlock Text="用户登录" FontSize="24"
                       FontWeight="Bold" Margin="0,0,0,20" />
            <!-- 表单内容 -->
        </StackPanel>
    </Border>
</Grid>
```

### 最佳实践总结

1. **从外层 Grid 开始** - 大多数复杂布局都适合用 Grid 作为最外层容器。
2. **合理使用 Auto 和 \*** - 标题和状态栏用 `Auto`，内容区域用 `*`。
3. **嵌套布局** - 在 Grid 的某个单元格内嵌套 StackPanel 或另一个 Grid 来实现复杂布局。
4. **避免过深的嵌套** - 嵌套层级过多会影响性能，一般不超过 5 层。
5. **善用 Margin 和 Padding** - `Margin` 控制元素外间距，`Padding` 控制元素内边距。
6. **使用 GridSplitter** - 允许用户在运行时调整面板大小，提升用户体验。

---

## 练习与实践

### 练习一：登录窗口布局

使用 Grid 和 StackPanel 创建一个登录窗口，包含：
- 居中显示的表单区域
- 用户名输入框（带标签）
- 密码输入框（带标签）
- 「记住密码」复选框
- 「登录」和「取消」两个按钮水平排列

### 练习二：图片浏览器布局

使用 DockPanel 创建以下布局：
- 顶部：标题栏
- 底部：状态栏
- 左侧：缩略图列表（ListBox）
- 右侧：大图显示区域

### 练习三：卡片网格

使用 WrapPanel 创建类似本项目主界面的卡片布局：
- 每行 3 个卡片
- 卡片固定宽高
- 窗口缩小时自动调整列数
- 每个卡片包含图标和标题文字

### 练习四：计算器面板

使用 UniformGrid 创建计算器的数字按钮面板：
- 3 行 4 列，包含数字 1-9、0 以及运算符
- 按钮大小均匀分布

---

## 延伸阅读

- [WPF 布局系统概述](https://learn.microsoft.com/zh-cn/dotnet/desktop/wpf/advanced/layout/)
- [Grid 类文档](https://learn.microsoft.com/zh-cn/dotnet/api/system.windows.controls.grid)
- [对齐、边距和内边距概述](https://learn.microsoft.com/zh-cn/dotnet/desktop/wpf/advanced/alignment-margins-and-padding-overview)