namespace CqrsPipeline.Samples.BasicConsole;

internal class Products
{
    public Products()
    {
        ProductList = new List<Product>();
    }
    public IList<Product> ProductList { get; private set; }
}