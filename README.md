![CqrsPipeline logo](Icon.png)

# CqrsPipeline 

Easy to use by nuget package :


![Nuget logo](nuget.png)

[CqrsPipeline](https://www.nuget.org/packages/CqrsPipeline/)

[CqrsPipeline.DataAccess](https://www.nuget.org/packages/CqrsPipeline.DataAccess/)

[CqrsPipeline.DataAccess](https://www.nuget.org/packages/CqrsPipeline.DataAccess/)

[CqrsPipeline.DataAccess.EntityFramework](https://www.nuget.org/packages/CqrsPipeline.DataAccess.EntityFramework/)

[CqrsPipeline.DataAccess.EntityFramework.SqlServer](https://www.nuget.org/packages/CqrsPipeline.DataAccess.EntityFramework.SqlServer/)

[CqrsPipeline.DataAccess.DependencyInjection](https://www.nuget.org/packages/CqrsPipeline.DependencyInjection/)

CqrsPipeline is a light laibrary for implementing CQRS patern in your software.you can define your command and query objects and send to pipeline, CqrsPipeline send them to that`s handlers

## Commands
You can define your commands in three ways:

1 - Inherit from ICommand : For commands that have no output data and only the result of their correct execution or their errors are important

2 - Inherit from ICommand<TData> : For commands that have output data and in addition to the result of their correct execution or their errors, the output data must also be returned

3 - Inherit from ICommand<TResult,TData> : For commands that have output data and in addition to the result of their correct execution or their errors, the output data must also be returned and you want to change the format of the output result and add items to it

## Queries
You can define your commands in one ways yet but we are developing new ways for you in next versions:

1 - Inhrit from IQuery<TData>

## Validation
CqrsPipeline use FluentValidation library for validating your commands ,You just have to create the validator for commands or queries and CqrsPipeline find them and check on your commands
See Validators in FluentValidation

## Exception handling
CqrsPipeline has an exception model that if you create exceptions to this model, it will handle it for you and turn it into an error model.

## Data access layer
Data access layer developed for entity framework and microsoft sql server

## How to use
Install CqrsPipeline with nuget packages

[CqrsPipeline](https://www.nuget.org/packages/CqrsPipeline/) : The core layer of CqrsPipeline. Commands , queries , handlers and dispatchers definition put in it.

[CqrsPipeline.DataAccess](https://www.nuget.org/packages/CqrsPipeline.DataAccess/) : This is base layer for data access

[CqrsPipeline.DataAccess](https://www.nuget.org/packages/CqrsPipeline.DataAccess/) : This is base layer for data access

[CqrsPipeline.DataAccess.EntityFramework](https://www.nuget.org/packages/CqrsPipeline.DataAccess.EntityFramework/) : There is Some extenssion methods and tools for EF Core in this layer

[CqrsPipeline.DataAccess.EntityFramework.SqlServer](https://www.nuget.org/packages/CqrsPipeline.DataAccess.EntityFramework.SqlServer/) : Default implementations for DataAccess Layer for EF Core , Sql server

[CqrsPipeline.DataAccess.DependencyInjection](https://www.nuget.org/packages/CqrsPipeline.DependencyInjection/) : Some extension methods for register services


## Getting Started

1 - Create new project 

2 - Install packages :

[CqrsPipeline](https://www.nuget.org/packages/CqrsPipeline/)

[CqrsPipeline.DataAccess](https://www.nuget.org/packages/CqrsPipeline.DataAccess/)

3 - Define your command object and Inherit it from ICommand

```C#
internal class CreateProductCommand:ICommand
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public float? Price { get; set; }
}
```

4 - Define command validator class for this command. create a class that inherit from AbstractValidator<YOUR_COMMAND_CLASS>
    and write your validation rules. command validator is not mandatory

```C#
public class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Code).MinimumLength(3).WithMessage("Product code minimum length is 3");
    }
}
```

5 - Define command handler. create a class that implement ICommandHandler<YOUR_COMMAND_CLASS>

```C#
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
            // handling your command
            // Return Success or Error
        });
    }

    public async Task<CommandResult> ExecuteAsync(CreateProductCommand command, CancellationToken cancellationToken)
    {
        return await Task.Run(() => ExecuteAsync(command), cancellationToken);
    }
}
```

6 - Register services, Add CqrsPipeline services and validators and handlers in ServiceCollection. you most add Logging if your project is not a hosting project, for example your project is a console application

```C#
    services.AddCommandPipeline(assembliesForSearch)
            .AddQueryPipeline(assembliesForSearch)
            .AddFluentValidators(assembliesForSearch)
```

7 - Inject ICommandDispatcher in your main code or get it from service.

```C#
    _commandDispatcher = _serviceProvider.GetService<ICommandDispatcher>();

```

8 - Create new instance of command or you can get command from a web api. send command with command dispatcher and get command result.

```C#
    var command = new CreateProductCommand { Code = "A123", Name = "Sample product name", Price = 20000 };
    var commandResult = _commandDispatcher?.SendAsync(command).Result;
```


### How to return success result in handler

return new CommandResult. you can return a SuccessMessage 

```C#
    return new CommandResult("Message for success execution");

```

### How to return error in handler

return new CommandResult With Error, Property name and value is not mandatory

```C#
    return new CommandResult(new CommandError(CODE, MESSAGE, nameof(PROPERTY_NAME),PROPERTY_VALUE));

```