# CqrsPipeline 

CqrsPipeline is a light laibrary for implementing CQRS patern in your software.you can define your command and query objects and send to pipeline, CqrsPipeline send them to that`s handlers

## Validation
CqrsPipeline use FluentValidation library for validating your commands ,You just have to create the validator for commands or queries and CqrsPipeline find them and check on your commands