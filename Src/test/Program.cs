using CqrsPipeline;
using CqrsPipeline.Commands;
using CqrsPipeline.Dsl.Builder;
using CqrsPipeline.Pipeline.Command;
using CqrsPipeline.Pipeline.Query;
using CqrsPipeline = CqrsPipeline.Pipeline.CqrsPipeline;

namespace test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var builder = new CqrsPipelineBuilder();

            var resolver = new DependencyResolver();

            var pipeline =
                new global::CqrsPipeline.Pipeline.CqrsPipeline(new CommandPipeline(resolver),
                    new QueryPipeline(resolver));


            pipeline.AddCommand<c>();

        }
    }

    internal class c : ICommand
    {
    }
}