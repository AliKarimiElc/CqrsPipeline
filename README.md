![CqrsPipeline logo](Icon.png)

# CqrsPipeline 

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
