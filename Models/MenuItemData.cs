namespace LearnXaml.Models;

public class MenuItemData
{
    public string Header { get; set; } = string.Empty;
    public string ToolTip { get; set; } = string.Empty;
    public List<MenuItemData> Children { get; set; } = new();
}