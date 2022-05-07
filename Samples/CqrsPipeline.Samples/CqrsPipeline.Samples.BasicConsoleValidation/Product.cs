namespace CqrsPipeline.Samples.BasicConsoleValidation;

public class Product
{
    public string? Code { get; }
    public string? Name { get; }
    public float? Price { get; }

    public Product(string? code, string? name, float? price)
    {
        Code = code;
        Name = name;
        Price = price;
    }
}