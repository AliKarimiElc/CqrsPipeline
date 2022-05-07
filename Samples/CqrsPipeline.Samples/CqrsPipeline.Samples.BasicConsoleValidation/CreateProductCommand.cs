using CqrsPipeline.Commands;

namespace CqrsPipeline.Samples.BasicConsoleValidation;

public class CreateProductCommand:ICommand
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public float? Price { get; set; }
}