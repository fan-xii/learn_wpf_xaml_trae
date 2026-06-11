namespace LearnXaml.Models;

public class FileNode
{
    public string Icon { get; set; }
    public string Name { get; set; }
    public List<FileNode> Children { get; set; } = new();

    public FileNode(string icon, string name, List<FileNode>? children = null)
    {
        Icon = icon;
        Name = name;
        if (children != null) Children = children;
    }
}
