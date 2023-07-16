using CqrsPipeline.Commands;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CqrsPipeline.Dsl
{
    internal class SampleUsage
    {
        public void Usage(IServiceCollection service)
        {
            var pipeline = service.GetPipeline();

            pipeline.AddCommand<c>().ValidateWith<cv>().HandleWith<ch>();
            pipeline.AddCommand<>()
        }
    }

    public class c : ICommand
    {

    }

    public class cv : AbstractValidator<c>
    {
        public cv()
        {
            RuleFor(c => c).NotNull();
        }
    }

    public class ch : ICommandHandler<c>
    {
        public Task<CommandResult> ExecuteAsync(c command)
        {
            throw new NotImplementedException();
        }

        public Task<CommandResult> ExecuteAsync(c command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
