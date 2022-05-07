using CqrsPipeline.Commands;

namespace CqrsPipeline.Samples.BasicConsoleValidation;

internal class CreateProductCommandHandler:ICommandHandler<CreateProductCommand>
{
    private readonly Products _products;

    public CreateProductCommandHandler(Products products)
    {
        _products = products;
    }

    public async Task<CommandResult> ExecuteAsync(CreateProductCommand command)
    {
        return await Task.Run(() =>
        {
            if (command.Code.Length > 5)
                return new CommandResult(new CommandError("0", "Product code is to long", nameof(command.Code),
                    command.Code));
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