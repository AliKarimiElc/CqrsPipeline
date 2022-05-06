using CqrsPipeline.Commands;

namespace CqrsPipeline.Samples.BasicConsole;

internal class CreateProductCommandHandler:ICommandHandler<CreateProductCommand>
{
    private Products _products;

    public CreateProductCommandHandler(Products products)
    {
        _products = products;
    }

    public async Task<CommandResult> ExecuteAsync(CreateProductCommand command)
    {
        return await Task.Run(() =>
        { 
            var product = new Product(command.Code, command.Name, command.Price);
            _products.ProductList.Add(product);
            return new CommandResult("Product created successfully");
        });
    }

    public async Task<CommandResult> ExecuteAsync(CreateProductCommand command, CancellationToken cancellationToken)
    {
        return await Task.Run(() => ExecuteAsync(command), cancellationToken);
    }
}