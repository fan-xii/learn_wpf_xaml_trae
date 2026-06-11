namespace LearnXaml.Models;

public class ProductDetail : Product
{
    public bool IsOnSale { get; set; }
    public string Description { get; set; } = string.Empty;
}
