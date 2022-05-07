using CqrsPipeline.Commands.Dispatchers;
using CqrsPipeline.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace CqrsPipeline.Samples.BasicConsoleValidation;

public class Program
{
    private static IServiceProvider? _serviceProvider;
    private static ICommandDispatcher? _commandDispatcher;

    public static int Main(string[] args)
    {
        //Create product Successfully
        Console.WriteLine("Cqrs Pipeline - Basic sample");
        // AddLogger();
        AddServices();

        var command = new CreateProductCommand { Code = "A123", Name = "Sample product name", Price = 20000 };
        var commandResult = _commandDispatcher?.SendAsync(command).Result;

        switch (commandResult?.Success)
        {
            case true:
                {
                    Console.WriteLine(
                        $"Command result - Success: {commandResult.Success} Message: {commandResult.SuccessMessage}");
                    break;
                }

            case false:
                {
                    Console.WriteLine(
                        $"Command result - Success: {commandResult.Success} Errors:");
                    foreach (var commandError in commandResult.Errors)
                    {
                        Console.WriteLine($"Error - Code: {commandError.Code} Message: {commandError.Message}");
                    }

                    break;
                }
        }

        Console.WriteLine("--------------------------------------------------");

        //Create product with error in command handler
        command = new CreateProductCommand { Code = "A12368", Name = "Sample product name", Price = 20000 };
        commandResult = _commandDispatcher?.SendAsync(command).Result;

        switch (commandResult?.Success)
        {
            case true:
                {
                    Console.WriteLine(
                        $"Command result - Success: {commandResult.Success} Message: {commandResult.SuccessMessage}");
                    break;
                }

            case false:
                {
                    Console.WriteLine(
                        $"Command result - Success: {commandResult.Success} Errors:");
                    foreach (var commandError in commandResult.Errors)
                    {
                        Console.WriteLine($"Error - Code: {commandError.Code} Message: {commandError.Message}");
                    }

                    break;
                }
        }

        Console.WriteLine("--------------------------------------------------");

        //Create product with validation error
        command = new CreateProductCommand { Code = "A", Name = "Sample product name", Price = 20000 };
        commandResult = _commandDispatcher?.SendAsync(command).Result;

        switch (commandResult?.Success)
        {
            case true:
            {
                Console.WriteLine(
                    $"Command result - Success: {commandResult.Success} Message: {commandResult.SuccessMessage}");
                break;
            }

            case false:
            {
                Console.WriteLine(
                    $"Command result - Success: {commandResult.Success} Errors:");
                foreach (var commandError in commandResult.Errors)
                {
                    Console.WriteLine($"Error - Code: {commandError.Code} Message: {commandError.Message}");
                }

                break;
            }
        }

        Console.ReadLine();

        return 0;
    }

    private static void AddServices()
    {
        var assembliesForSearch = AppDomain.CurrentDomain.GetAssemblies();

        _serviceProvider = new ServiceCollection()
            .AddCommandPipeline(assembliesForSearch)
            .AddQueryPipeline(assembliesForSearch)
            .AddFluentValidators(assembliesForSearch)//Add validators
            .AddSingleton<Products>()
            .AddLogging()
            .BuildServiceProvider();
        _commandDispatcher = _serviceProvider.GetService<ICommandDispatcher>();
    }
}