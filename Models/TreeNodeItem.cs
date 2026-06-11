namespace LearnXaml.Models;

public class TreeNodeItem
{
    public string Name { get; set; } = string.Empty;
    public List<TreeNodeItem> Children { get; set; } = new();
}