using CqrsPipeline.Commands;
using CqrsPipeline.Commands.Dispatchers;
using CqrsPipeline.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace CqrsPipeline.Samples.BasicConsole;

public class Program
{
    private static IServiceProvider? _serviceProvider;
    private static CommandDispatcher? _commandDispatcher;

    public static int Main(string[] args)
    {
        Console.WriteLine("Cqrs Pipeline - Basic sample");
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

        Console.ReadLine();

        return 0;
    }

    private static void AddServices()
    {
        var assembliesForSearch = AppDomain.CurrentDomain.GetAssemblies();

        _serviceProvider = new ServiceCollection()
            .AddCommandPipeline(assembliesForSearch)
            .AddQueryPipeline(assembliesForSearch)
            .AddSingleton<Products>()
            .BuildServiceProvider();
        _commandDispatcher = _serviceProvider?.GetService<CommandDispatcher>();
    }
}